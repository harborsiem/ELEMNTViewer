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
        private Stream _fitSource;

        public void Decode(string fileName)
        {
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
                //mesgBroadcaster.HrZoneMesgEvent += HRZonesManager.OnMesg;

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
                    HandleSession(e);
                    HandleSessionExtras();
                    break;
                case MesgNum.Lap:
                    HandleLap(e);
                    break;
                case MesgNum.Record:
                    HandleRecord(e);
                    break;
                case 65280: //0xff00
                    HandleWahooFF00(e);
                    break;
                case 65281: //0xff01
                    HandleWahooFF01(e);
                    break;
                default:
                    break;
            }
        }

        static void HandleWahooFF00(MesgEventArgs e)
        {
            WahooFF00Values values = new WahooFF00Values();
            object value;

            int i = 0;
            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }

                i++;
            }
            DataManager.Instance.WahooFF00Values.Add(values);
        }

        static void HandleWahooFF01(MesgEventArgs e)
        {
            WahooFF01Values values = new WahooFF01Values();
            object value;

            int i = 0;
            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }

                i++;
            }
            DataManager.Instance.WahooFF01Values.Add(values);
        }

        static async void HandleSessionExtras()
        {
            OtherValues others = new OtherValues();
            DataManager.Instance.SessionExtras = others;
            await Task.CompletedTask;
        }

        static async void HandleHrZones(MesgEventArgs e)
        {
            object value = 0;

            int i = 0;
            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                }

                i++;
            }
            DataManager.Instance.HRManager.Add((byte)value);
            await Task.CompletedTask;
        }

        static async void HandlePowerZones(MesgEventArgs e)
        {
            object value = 0;

            int i = 0;
            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                }

                i++;
            }
            DataManager.Instance.PowerManager.Add((ushort)value);
            await Task.CompletedTask;
        }

        static async void HandleSession(MesgEventArgs e)
        {
            SessionValues values = new SessionValues();
            object value;

            int i = 0;
            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }

                i++;
            }
            DataManager.Instance.Session = (values);
            await Task.CompletedTask;
        }

        static async void HandleLap(MesgEventArgs e)
        {
            LapValues values = new LapValues();
            object value;

            int i = 0;
            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, j, value);
                }

                i++;
            }
            DataManager.Instance.LapManager.Add(values, DataManager.Instance.RecordList.Count - 1);
            await Task.CompletedTask;
        }

        static async void HandleRecord(MesgEventArgs e)
        {
            RecordValues values = new RecordValues();
            object value;

            int i = 0;
            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    values.SetValue(field.Num, value);
                }

                i++;
            }
            DataManager.Instance.RecordList.Add(values);
            await Task.CompletedTask;
        }
    }
}
