using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Dynastream.Fit;

namespace ELEMNTViewer
{
    class DecodeFile
    {
        private static List<Task<RecordValues>> s_Records = new List<Task<RecordValues>>();
        private static int s_RecordCount;
        private static Task<SessionValues> s_Session;
        private static List<Task<LapValues>> s_Laps = new List<Task<LapValues>>();
        private static List<int> s_LapEnd = new List<int>();
        private Stream _fitSource;

        public void Decode(string fileName)
        {
            s_Records.Clear();
            s_RecordCount = 0;
            s_Laps.Clear();
            s_LapEnd.Clear();
            try
            {
                // Attempt to open .FIT file
                _fitSource = new FileStream(fileName, FileMode.Open);

                Decode decodeDemo = new Decode();
                MesgBroadcaster mesgBroadcaster = new MesgBroadcaster();

                // Connect the Broadcaster to our event (message) source (in this case the Decoder)
                decodeDemo.MesgEvent += mesgBroadcaster.OnMesg;
                // Subscribe to message events of interest by connecting to the Broadcaster
                mesgBroadcaster.MesgEvent += OnMesg;

                mesgBroadcaster.ActivityMesgEvent += ActivityValues.OnActivityMesg;
                mesgBroadcaster.DeveloperDataIdMesgEvent += DeveloperDataIdValues.OnDeveloperDataIdMesg;
                mesgBroadcaster.DeviceInfoMesgEvent += DeviceInfoValues.OnDeviceInfoMesg;
                mesgBroadcaster.FieldDescriptionMesgEvent += FieldDescriptionValues.OnFieldDescriptionMesg;
                mesgBroadcaster.FileIdMesgEvent += FileIdValues.OnFileIDMesg;
                mesgBroadcaster.EventMesgEvent += EventValues.OnEventMesg;
                mesgBroadcaster.SportMesgEvent += SportValues.OnSportMesg;
                mesgBroadcaster.WorkoutMesgEvent += WorkoutValues.OnWorkoutMesg;

                bool status = decodeDemo.IsFIT(_fitSource);
                status &= decodeDemo.CheckIntegrity(_fitSource);

                // Process the file
                if (status)
                {
                    //Console.WriteLine("Decoding...");
                    decodeDemo.Read(_fitSource);
                    //Console.WriteLine("Decoded FIT file {0}", args[0]);
                }
                else
                {
                    try
                    {
                        //Console.WriteLine("Integrity Check Failed {0}", args[0]);
                        if (decodeDemo.InvalidDataSize)
                        {
                            //Console.WriteLine("Invalid Size Detected, Attempting to decode...");
                            decodeDemo.Read(_fitSource);
                        }
                        else
                        {
                            //Console.WriteLine("Attempting to decode by skipping the header...");
                            decodeDemo.Read(_fitSource, DecodeMode.InvalidHeader);
                        }
                    }
                    catch (FitException ex)
                    {
                        MessageBox.Show("Decode caught FitException: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Console.WriteLine("DecodeDemo caught FitException: " + ex.Message);
                    }
                }
                _fitSource.Close();

            }
            catch (FitException ex)
            {
                MessageBox.Show("A FitException occurred when trying to decode the FIT file. Message: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("A FitException occurred when trying to decode the FIT file. Message: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred when trying to decode the FIT file. Message: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("Exception occurred when trying to decode the FIT file. Message: " + ex.Message);
            }
            finally
            {

            }
            Task task = EndAsync();
        }

        static async Task EndAsync()
        {
            RecordValues[] records = await Task.WhenAll(s_Records);
            for (int i = 0; i < records.Length; i++)
            {
                DataManager.Instance.RecordList.Add(records[i]);
            }
            SessionValues[] sessions = await Task.WhenAll(s_Session);
            DataManager.Instance.Session = sessions[0];
            HandleSessionExtras();
            LapValues[] laps = await Task.WhenAll(s_Laps);
            for (int i = 0; i < laps.Length; i++)
            {
                DataManager.Instance.LapManager.Add(laps[i], s_LapEnd[i]);
            }
        }

        static void OnMesg(object sender, MesgEventArgs e)
        {
            //Console.WriteLine("OnMesg: Received Mesg with global ID#{0}, its name is {1}", e.mesg.Num, e.mesg.Name);
            switch (e.mesg.Num)
            {
                case MesgNum.HrZone:
                    HandleHrZones(e);
                    break;
                case MesgNum.PowerZone:
                    HandlePowerZones(e);
                    break;
                case MesgNum.Session:
                    s_Session = HandleSessionAsync(e);
                    //HandleSession(e);
                    //HandleSessionExtras();
                    break;
                case MesgNum.Lap:
                    s_Laps.Add(HandleLapAsync(e));
                    s_LapEnd.Add(s_RecordCount - 1);
                    //HandleLap(e);
                    break;
                case MesgNum.Record:
                    s_RecordCount++;
                    s_Records.Add(HandleRecordAsync(e));
                    //HandleRecord(e);
                    break;
                case 65280: //0xff00
                    HandleWahooFF00(e);
                    break;
                case 65281: //0xff01
                    HandleWahooFF01(e);
                    break;
                case 0xff04:
                    HandleWahooFF04(e);
                    break;
                default:
                    //if (e.mesg.Name == "Activity" || e.mesg.Name == "DeveloperDataId" || e.mesg.Name == "DeviceInfo" || e.mesg.Name == "FieldDescription" || e.mesg.Name == "FileId"
                    //     || e.mesg.Name == "Event" || e.mesg.Name == "Sport" || e.mesg.Name == "Workout")
                    //    break;
                    break;
            }
        }

        static void HandleWahooFF04(MesgEventArgs e)
        {
            WahooFF04Values values = new WahooFF04Values();
            object value;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }
            }
            DataManager.Instance.WahooFF04Values.Add(values);
        }

        static void HandleWahooFF00(MesgEventArgs e)
        {
            WahooFF00Values values = new WahooFF00Values();
            object value;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }
            }
            DataManager.Instance.WahooFF00Values.Add(values);
        }

        static void HandleWahooFF01(MesgEventArgs e)
        {
            WahooFF01Values values = new WahooFF01Values();
            object value;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }
            }
            DataManager.Instance.WahooFF01Values.Add(values);
        }

        static void HandleSessionExtras()
        {
            OtherValues others = new OtherValues();
            DataManager.Instance.SessionExtras = others;
        }

        static void HandleHrZones(MesgEventArgs e)
        {
            object value = 0;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                }
            }
            DataManager.Instance.HRManager.Add((byte)value);
        }

        static void HandlePowerZones(MesgEventArgs e)
        {
            object value = 0;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                }
            }
            DataManager.Instance.PowerManager.Add((ushort)value);
        }

        static Task<SessionValues> HandleSessionAsync(MesgEventArgs e)
        {
            return Task<SessionValues>.Run(() =>
            {
                SessionValues values = new SessionValues();
                object value;

                foreach (Field field in e.mesg.Fields)
                {
                    for (int j = 0; j < field.GetNumValues(); j++)
                    {
                        value = field.GetValue(j);
                        values.SetValue(field.Num, j, value);
                    }
                }
                return values;
            });
        }

        static void HandleSession(MesgEventArgs e)
        {
            SessionValues values = new SessionValues();
            object value;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }
            }
            DataManager.Instance.Session = (values);
        }

        static Task<LapValues> HandleLapAsync(MesgEventArgs e)
        {
            return Task<LapValues>.Run(() =>
            {
                LapValues values = new LapValues();
                object value;

                foreach (Field field in e.mesg.Fields)
                {
                    for (int j = 0; j < field.GetNumValues(); j++)
                    {
                        value = field.GetValue(j);
                        values.SetValue(field.Num, j, value);
                    }
                }
                return values;
            });
        }

        static void HandleLap(MesgEventArgs e)
        {
            LapValues values = new LapValues();
            object value;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }
            }
            DataManager.Instance.LapManager.Add(values, DataManager.Instance.RecordList.Count - 1);
        }

        static Task<RecordValues> HandleRecordAsync(MesgEventArgs e)
        {
            return Task<RecordValues>.Run(() =>
            {
                RecordValues values = new RecordValues();
                object value = 0;

                foreach (Field field in e.mesg.Fields)
                {
                    for (int j = 0; j < field.GetNumValues(); j++)
                    {
                        value = field.GetValue(j);
                        values.SetValue(field.Num, value);
                    }
                }
                return values;
            });
        }

        static void HandleRecord(MesgEventArgs e)
        {
            RecordValues values = new RecordValues();
            object value;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, value);
                }
            }
            DataManager.Instance.RecordList.Add(values);
        }
    }
}
