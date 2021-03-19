using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;

namespace ELEMNTViewer
{
    class Settings
    {
        private const string SettingsFile = "Settings.xml";

        public static Settings Instance = new Settings();

        private string _settingsPath;
        private bool _modified;

        private Settings()
        {
            _settingsPath = ThisLocalAppData;
            _appVersion = Application.ProductVersion;
        }

        public static string ThisLocalAppData
        {
            get
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string path = Path.Combine(localAppData, "ELEMNTViewer");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        public int PowerSmooth { get; set; } = 0;
        public int LRBalanceSmooth { get; set; } = 0;
        public int TorqueLeftSmooth { get; set; } = 0;
        public int TorqueRightSmooth { get; set; } = 0;
        public int SmoothnessLeftSmooth { get; set; } = 0;
        public int SmoothnessRightSmooth { get; set; } = 0;
        public bool Intern { get; set; } = true;

        public bool SpeedChecked { get; set; }
        public bool CadenceChecked { get; set; }
        public bool PowerChecked { get; set; }
        public bool LRBalanceChecked { get; set; }
        public bool HeartRateChecked { get; set; }
        public bool LTorqueChecked { get; set; }
        public bool RTorqueChecked { get; set; }
        public bool LSmoothChecked { get; set; }
        public bool RSmoothChecked { get; set; }
        public bool AltitudeChecked { get; set; }
        public bool GradeChecked { get; set; }
        public bool TemperatureChecked { get; set; }
        public bool VersionChanged { get; private set; }
        public int AppWidth { get; set; } = 0;
        public int AppHeight { get; set; } = 0;
        public int MapWidth { get; set; } = 0;
        public int MapHeight { get; set; } = 0;
        public bool AppSizeWrite { get; set; } = false;

        public bool Modified { get => _modified; set => _modified = value; }

        private string _appVersion;
        private string _settingsVersion;

        private bool HasVersionChanged()
        {
            bool result = true;
            if (_settingsVersion != null)
                result = _appVersion != _settingsVersion;
            return result;
        }

        public void Read()
        {
            bool modified = true;
            string settingsFile = Path.Combine(_settingsPath, SettingsFile);
            if (File.Exists(settingsFile))
            {
                XDocument xdoc = XDocument.Load(settingsFile);
                XElement root = xdoc.Root;
                if (root.Name.LocalName == Attributes.Settings)
                {
                    foreach (XElement ele in root.Elements())
                    {
                        switch (ele.Name.LocalName)
                        {
                            case Attributes.Version:
                                _settingsVersion = (ele.Value);
                                break;
                            case Attributes.PowerSmooth:
                                PowerSmooth = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.LRBalanceSmooth:
                                LRBalanceSmooth = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.TorqueLeftSmooth:
                                TorqueLeftSmooth = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.TorqueRightSmooth:
                                TorqueRightSmooth = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.SmoothnessLeftSmooth:
                                SmoothnessLeftSmooth = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.SmoothnessRightSmooth:
                                SmoothnessRightSmooth = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.Intern:
                                Intern = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.AppWidth:
                                AppWidth = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.AppHeight:
                                AppHeight = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.MapWidth:
                                MapWidth = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.MapHeight:
                                MapHeight = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.SpeedChecked:
                                SpeedChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.CadenceChecked:
                                CadenceChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.PowerChecked:
                                PowerChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.LRBalanceChecked:
                                LRBalanceChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.HeartRateChecked:
                                HeartRateChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.LTorqueChecked:
                                LTorqueChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.RTorqueChecked:
                                RTorqueChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.LSmoothChecked:
                                LSmoothChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.RSmoothChecked:
                                RSmoothChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.AltitudeChecked:
                                AltitudeChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.GradeChecked:
                                GradeChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.TemperatureChecked:
                                TemperatureChecked = XmlConvert.ToBoolean(ele.Value);
                                break;
                        }
                    }
                    modified = false;
                }
            }
            VersionChanged = HasVersionChanged();
            if (VersionChanged)
                modified = true;

            _modified = modified;
            //MinimumSize(minimumSize);
        }

        public void Write()
        {
            if (_modified)
            {
                string settingsFile = Path.Combine(_settingsPath, SettingsFile);
                XmlWriterSettings writerSettings = new XmlWriterSettings();
                writerSettings.Indent = true;
                XmlWriter writer = XmlWriter.Create(settingsFile, writerSettings);
                try
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement(Attributes.Settings);

                    writer.WriteStartElement(Attributes.PowerSmooth);
                    writer.WriteString(XmlConvert.ToString(PowerSmooth));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.LRBalanceSmooth);
                    writer.WriteString(XmlConvert.ToString(LRBalanceSmooth));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.TorqueLeftSmooth);
                    writer.WriteString(XmlConvert.ToString(TorqueLeftSmooth));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.TorqueRightSmooth);
                    writer.WriteString(XmlConvert.ToString(TorqueRightSmooth));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.SmoothnessLeftSmooth);
                    writer.WriteString(XmlConvert.ToString(SmoothnessLeftSmooth));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.SmoothnessRightSmooth);
                    writer.WriteString(XmlConvert.ToString(SmoothnessRightSmooth));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.SpeedChecked);
                    writer.WriteString(XmlConvert.ToString(SpeedChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.CadenceChecked);
                    writer.WriteString(XmlConvert.ToString(CadenceChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.PowerChecked);
                    writer.WriteString(XmlConvert.ToString(PowerChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.LRBalanceChecked);
                    writer.WriteString(XmlConvert.ToString(LRBalanceChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.HeartRateChecked);
                    writer.WriteString(XmlConvert.ToString(HeartRateChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.LTorqueChecked);
                    writer.WriteString(XmlConvert.ToString(LTorqueChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.RTorqueChecked);
                    writer.WriteString(XmlConvert.ToString(RTorqueChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.LSmoothChecked);
                    writer.WriteString(XmlConvert.ToString(LSmoothChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.RSmoothChecked);
                    writer.WriteString(XmlConvert.ToString(RSmoothChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.AltitudeChecked);
                    writer.WriteString(XmlConvert.ToString(AltitudeChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.GradeChecked);
                    writer.WriteString(XmlConvert.ToString(GradeChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.TemperatureChecked);
                    writer.WriteString(XmlConvert.ToString(TemperatureChecked));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.Version);
                    writer.WriteString(_appVersion);
                    writer.WriteEndElement();

                    if (AppSizeWrite) {
                        writer.WriteStartElement(Attributes.AppWidth);
                        writer.WriteString(XmlConvert.ToString(AppWidth));
                        writer.WriteEndElement();

                        writer.WriteStartElement(Attributes.AppHeight);
                        writer.WriteString(XmlConvert.ToString(AppHeight));
                        writer.WriteEndElement();

                        AppSizeWrite = false;
                    }

                    writer.WriteStartElement(Attributes.MapWidth);
                    writer.WriteString(XmlConvert.ToString(MapWidth));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.MapHeight);
                    writer.WriteString(XmlConvert.ToString(MapHeight));
                    writer.WriteEndElement();

                    if (Intern)
                    {
                        writer.WriteStartElement(Attributes.Intern);
                        writer.WriteString(XmlConvert.ToString(Intern));
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                finally
                {
                    writer.Close();
                }
            }
        }

        private static class Attributes
        {
            public const string Settings = nameof(Settings);
            public const string PowerSmooth = nameof(PowerSmooth);
            public const string LRBalanceSmooth = nameof(LRBalanceSmooth);
            public const string TorqueLeftSmooth = nameof(TorqueLeftSmooth);
            public const string TorqueRightSmooth = nameof(TorqueRightSmooth);
            public const string SmoothnessLeftSmooth = nameof(SmoothnessLeftSmooth);
            public const string SmoothnessRightSmooth = nameof(SmoothnessRightSmooth);

            public const string SpeedChecked = nameof(SpeedChecked);
            public const string CadenceChecked = nameof(CadenceChecked);
            public const string PowerChecked = nameof(PowerChecked);
            public const string LRBalanceChecked = nameof(LRBalanceChecked);
            public const string HeartRateChecked = nameof(HeartRateChecked);
            public const string LTorqueChecked = nameof(LTorqueChecked);
            public const string RTorqueChecked = nameof(RTorqueChecked);
            public const string LSmoothChecked = nameof(LSmoothChecked);
            public const string RSmoothChecked = nameof(RSmoothChecked);
            public const string AltitudeChecked = nameof(AltitudeChecked);
            public const string GradeChecked = nameof(GradeChecked);
            public const string TemperatureChecked = nameof(TemperatureChecked);
            public const string Version = nameof(Version);

            public const string Intern = nameof(Intern);
            public const string AppWidth = nameof(AppWidth);
            public const string AppHeight = nameof(AppHeight);
            public const string MapWidth = nameof(MapWidth);
            public const string MapHeight = nameof(MapHeight);
        }
    }
}
