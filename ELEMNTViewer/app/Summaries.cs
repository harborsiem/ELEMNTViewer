using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Dynastream.Fit;

namespace ELEMNTViewer
{
    class Summaries
    {
        private List<string> _remainedFiles;
        private List<string> _fitFiles;
        private List<int> _updateYears;
        private string[] _summariesFiles;
        private Dictionary<int, List<Summary>> _processedSummaries;
        private Summary _current;

        public Summaries()
        {
            _processedSummaries = new Dictionary<int, List<Summary>>();
            _updateYears = new List<int>();
        }

        public void Execute1()
        {
            ThreadPool.QueueUserWorkItem(ExecuteAsync, null);
        }

        public void Execute()
        {
            Task task = Task.Run(() => ExecuteAsync(null));
        }

        public Summary GetYearSummaries(int year)
        {
            double sumDistance = 0;
            double sumAscent = 0;
            if (_processedSummaries.ContainsKey(year))
            {
                List<Summary> summaries = _processedSummaries[year];
                foreach (Summary summary in summaries)
                {
                    sumDistance += summary.Distance;
                    sumAscent += summary.Ascent;
                }
            }
            Summary result = new Summary()
            {
                Distance = sumDistance,
                Ascent = sumAscent
            };
            return result;
        }

        public Summary GetMonthSummaries(int year, int month)
        {
            if (!(month > 0 && month <= 12))
                throw new ArgumentException("Month wrong", nameof(month));
            double sumDistance = 0;
            double sumAscent = 0;
            if (_processedSummaries.ContainsKey(year))
            {
                List<Summary> summaries = _processedSummaries[year];
                foreach (Summary summary in summaries)
                {
                    if (summary.GetMonth() == month)
                    {
                        sumDistance += summary.Distance;
                        sumAscent += summary.Ascent;
                    }
                }
            }
            Summary result = new Summary()
            {
                Distance = sumDistance,
                Ascent = sumAscent
            };
            return result;
        }

        public void ExecuteAsync(object state)
        {
            string fitPath = Settings.Instance.FitPath;
            if (Directory.Exists(fitPath))
            {
                _fitFiles = GetFitFileNames(fitPath);
                ReadProcessedSummaries(Settings.ThisLocalAppData);
                BuildRemaindedFiles();
                ProcessFitFiles();
                WriteSummaries();
            }
        }

        private List<string> GetFitFileNames(string path)
        {
            List<string> fitFiles = new List<string>();
            if (Directory.Exists(path))
            {
                //fitFiles = Directory.GetFiles(path, "*.fit", SearchOption.TopDirectoryOnly);
                DirectoryInfo dInfo = new DirectoryInfo(path);
                FileInfo[] fInfos = dInfo.GetFiles("*.fit", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fInfo in fInfos)
                {
                    fitFiles.Add(fInfo.Name);
                }
            }
            return fitFiles;
        }

        private void ReadProcessedSummaries(string path)
        {
            if (Directory.Exists(path))
            {
                _summariesFiles = Directory.GetFiles(path, "Summaries????.xml", SearchOption.TopDirectoryOnly);
                foreach (string file in _summariesFiles)
                {
                    string withoutExt = Path.GetFileNameWithoutExtension(file);
                    string sYear = withoutExt.Substring("Summaries".Length);
                    int year = int.Parse(sYear);
                    SummaryReader reader = new SummaryReader(file);
                    _processedSummaries.Add(year, reader.ReadSummeries());
                }
            }
        }

        private void BuildRemaindedFiles()
        {
            _remainedFiles = new List<string>(_fitFiles);
            foreach (KeyValuePair<int, List<Summary>> kvp in _processedSummaries)
            {
                foreach (Summary values in kvp.Value)
                {
                    if (_remainedFiles.Contains(values.Filename))
                    {
                        _remainedFiles.Remove(values.Filename);
                    }
                }
            }
        }

        private void ProcessFitFiles()
        {
            foreach (string fitFile in _remainedFiles)
            {
                Summary value = GetSummary(fitFile);
                int year = value.GetYear();
                bool yearFound = false;
                foreach (int key in _processedSummaries.Keys)
                {
                    if (key == year)
                    {
                        yearFound = true;
                        var list = _processedSummaries[key];
                        list.Add(value);
                        if (!_updateYears.Contains(year))
                            _updateYears.Add(year);
                        break;
                    }
                }
                if (!yearFound)
                {
                    List<Summary> newList = new List<Summary>();
                    newList.Add(value);
                    if (!_updateYears.Contains(year))
                        _updateYears.Add(year);
                    _processedSummaries.Add(year, newList);
                }
            }
        }

        private void WriteSummaries()
        {
            foreach (int key in _processedSummaries.Keys)
            {
                List<Summary> list = _processedSummaries[key];
                int year = list[0].GetYear();
                if (_updateYears.Contains(year))
                {
                    list.Sort();
                    new SummaryWriter().WriteSummaries(year, list);
                }
            }
        }

        /// <summary>
        /// Dummy for Test
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Summary GetSummary(string fileName)
        {
            Summary result = new Summary();
            result.Filename = fileName;
            _current = result;
            FastDecodeFit(Path.Combine(Settings.Instance.FitPath, fileName));
            result = _current;
            return result;
        }

        private void FastDecodeFit(string fileName)
        {
            string exMessage = String.Empty;
            Stream fitSource = null;
            byte[] definition = new byte[6];
            byte[] field = new byte[3];
            int[] fields;
            int datalength;
            bool devData;
            bool session = false;
            try
            {
                fitSource = new FileStream(fileName, FileMode.Open);
                fitSource.Position = 14;
                while (fitSource.Position < fitSource.Length)
                {
                    devData = false;
                    fitSource.Read(definition, 0, definition.Length);
                    if (definition[0] < 0x40)
                        throw new FitException("Filename: " + Path.GetFileName(fileName) + ", Invalid Definition at Position " + (fitSource.Position - 6).ToString());
                    if ((definition[0] & Fit.DevDataMask) == Fit.DevDataMask)
                        devData = true;
                    if (definition[4] == MesgNum.Session)
                    {
                        session = true;
                    }
                    fields = new int[definition[5]];
                    datalength = 0;
                    for (int i = 0; i < definition[5]; i++)
                    {
                        fitSource.Read(field, 0, 3); //3 Bytes per field
                        fields[i] = (field[0] << 16) + (field[1] << 8) + field[2];
                        datalength += field[1];
                    }
                    if (devData)
                    {
                        int devDataNum = fitSource.ReadByte();
                        for (int i = 0; i < devDataNum; i++)
                        {
                            fitSource.Read(field, 0, 3); //3 Bytes per field
                            datalength += field[1];
                        }
                    }
                    if (session)
                    {
                        int dataStart = 0;
                        fitSource.ReadByte();
                        for (int i = 0; i < definition[5]; i++)
                        {
                            int fi = fields[i];
                            if ((fi & 0xff0000) >> 16 == SessionMesg.FieldDefNum.TotalDistance)
                            {
                                fitSource.Seek(dataStart, SeekOrigin.Current);
                                byte[] buffer = new byte[4];
                                fitSource.Read(buffer, 0, 4);
                                if (definition[2] == Fit.BigEndian)
                                    Array.Reverse(buffer);
                                double distance = Convert.ToSingle(BitConverter.ToUInt32(buffer, 0)) / 100000.0d;
                                _current.Distance = distance;
                                dataStart = 0;
                                continue;
                            }
                            if ((fi & 0xff0000) >> 16 == SessionMesg.FieldDefNum.TotalAscent)
                            {
                                fitSource.Seek(dataStart, SeekOrigin.Current);
                                byte[] buffer = new byte[2];
                                fitSource.Read(buffer, 0, 2);
                                if (definition[2] == Fit.BigEndian)
                                    Array.Reverse(buffer);
                                UInt16 ascent = BitConverter.ToUInt16(buffer, 0);
                                _current.Ascent = ascent;
                                dataStart = 0;
                                continue;
                            }
                            dataStart += (fi & 0xff00) >> 8;
                        }
                        break;
                    }
                    while (fitSource.ReadByte() == 0)
                    {
                        fitSource.Seek(datalength, SeekOrigin.Current);
                    }
                    fitSource.Position -= 1;
                }
            }
            catch (Exception ex)
            {
                exMessage = ex.Message;
            }
            finally
            {
                if (fitSource != null)
                    fitSource.Close();
            }
        }

        private void DecodeFit(string fileName)
        {
            Stream fitSource;
            try
            {
                // Attempt to open .FIT file
                fitSource = new FileStream(fileName, FileMode.Open);

                Decode decodeDemo = new Decode();
                decodeDemo.MesgEvent += OnMesg;
                //MesgBroadcaster mesgBroadcaster = new MesgBroadcaster();
                bool status = decodeDemo.IsFIT(fitSource);
                status &= decodeDemo.CheckIntegrity(fitSource);

                // Process the file
                if (status)
                {
                    //Console.WriteLine("Decoding...");
                    decodeDemo.Read(fitSource);
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
                            decodeDemo.Read(fitSource);
                        }
                        else
                        {
                            //Console.WriteLine("Attempting to decode by skipping the header...");
                            decodeDemo.Read(fitSource, DecodeMode.InvalidHeader);
                        }
                    }
                    catch (FitException ex)
                    {
                        //MessageBox.Show("Decode caught FitException: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Console.WriteLine("DecodeDemo caught FitException: " + ex.Message);
                    }
                }
                fitSource.Close();
            }
            catch (FitException ex)
            {
                //MessageBox.Show("A FitException occurred when trying to decode the FIT file. Message: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("A FitException occurred when trying to decode the FIT file. Message: " + ex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Exception occurred when trying to decode the FIT file. Message: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("Exception occurred when trying to decode the FIT file. Message: " + ex.Message);
            }
            finally
            {

            }
        }

        private void OnMesg(object sender, MesgEventArgs e)
        {
            //Console.WriteLine("OnMesg: Received Mesg with global ID#{0}, its name is {1}", e.mesg.Num, e.mesg.Name);
            switch (e.mesg.Num)
            {
                case MesgNum.Session:
                    HandleSession(e);
                    break;
                default:
                    break;
            }
        }

        private void HandleSession(MesgEventArgs e)
        {
            object value;

            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    value = field.GetValue(j);
                    SetValue(field.Num, j, value);
                }
            }
        }

        private void SetValue(byte fieldNum, int index, object value)
        {
            Type objType = value.GetType();
            switch (fieldNum)
            {
                case SessionMesg.FieldDefNum.TotalDistance:
                    _current.Distance = Math.Round(Convert.ToSingle(value) / 1000.0, 2); //Single
                    break;
                case SessionMesg.FieldDefNum.TotalAscent:
                    _current.Ascent = Convert.ToUInt16(value); //UInt16
                    break;
                default:
                    break;
            }
        }
    }
}
