using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using ELEMNTViewer.Properties;

namespace ELEMNTViewer {
    internal class ConfigManager {
        public static string ConfigDir { get; private set; }
        public static string DocumentsDir { get; private set; }
        private static string userConfigFile;
        private static bool userConfigExists;
        public IDictionary<String, ConfigData> SensorConfigValues { get; private set; }
        public IDictionary<String, ConfigData> SensorConfigDefault { get; private set; }
        public string OpenDir { get; set; }
        public bool AppConfigChanged { get; set; }

        private static Color[] _colorsBrightPastel = new Color[] { Color.FromArgb(0x41, 140, 240), Color.FromArgb(0xfc, 180, 0x41), Color.FromArgb(0xe0, 0x40, 10), Color.FromArgb(5, 100, 0x92), Color.FromArgb(0xbf, 0xbf, 0xbf), Color.FromArgb(0x1a, 0x3b, 0x69), Color.FromArgb(0xff, 0xe3, 130), Color.FromArgb(0x12, 0x9c, 0xdd), Color.FromArgb(0xca, 0x6b, 0x4b), Color.FromArgb(0, 0x5c, 0xdb), Color.FromArgb(0xf3, 210, 0x88), Color.FromArgb(80, 0x63, 0x81), Color.FromArgb(0xf1, 0xb9, 0xa8), Color.FromArgb(0xe0, 0x83, 10), Color.FromArgb(120, 0x93, 190) };
        private static Color[] _colorsFire = new Color[] { Color.Gold, Color.Red, Color.DeepPink, Color.Crimson, Color.DarkOrange, Color.Magenta, Color.Yellow, Color.OrangeRed, Color.MediumVioletRed, Color.FromArgb(0xdd, 0xe2, 0x21) };
        private static Color[] _colorsSeaGreen = new Color[] { Color.SeaGreen, Color.MediumAquamarine, Color.SteelBlue, Color.DarkCyan, Color.CadetBlue, Color.MediumSeaGreen, Color.MediumTurquoise, Color.LightSteelBlue, Color.DarkSeaGreen, Color.SkyBlue };
        private static Color[] _colorsExcel = new Color[] { Color.FromArgb(0x99, 0x99, 0xff), Color.FromArgb(0x99, 0x33, 0x66), Color.FromArgb(0xff, 0xff, 0xcc), Color.FromArgb(0xcc, 0xff, 0xff), Color.FromArgb(0x66, 0, 0x66), Color.FromArgb(0xff, 0x80, 0x80), Color.FromArgb(0, 0x66, 0xcc), Color.FromArgb(0xcc, 0xcc, 0xff), Color.FromArgb(0, 0, 0x80), Color.FromArgb(0xff, 0, 0xff), Color.FromArgb(0xff, 0xff, 0), Color.FromArgb(0, 0xff, 0xff), Color.FromArgb(0x80, 0, 0x80), Color.FromArgb(0x80, 0, 0), Color.FromArgb(0, 0x80, 0x80), Color.FromArgb(0, 0, 0xff) };

        public ConfigManager() {
            AppConfigChanged = true; //@Todo: 
            OpenDir = AppManager.BaseDir;
            SensorConfigValues = new Dictionary<String, ConfigData>();
            SensorConfigDefault = new Dictionary<String, ConfigData>();
            ConfigDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppManager.ConfigPath);
            if (!Directory.Exists(ConfigDir)) {
                Directory.CreateDirectory(ConfigDir);
            }
            DocumentsDir = GetDocumentsDirectory();
            userConfigFile = ConfigDir + Path.DirectorySeparatorChar + "User.config";
            userConfigExists = File.Exists(userConfigFile);
        }

        public void Init() {
            SetDefaults();
            ReadConfig();
            ConfigureSensors();
            AppManager.ItemManager.UpdateItems();
        }

        private static void UpdateParameters(IDictionary<String, ConfigData> oldDict, IDictionary<String, ConfigData> newDict, bool options) {
            IEnumerator<KeyValuePair<String, ConfigData>> it = oldDict.GetEnumerator();
            while (it.MoveNext()) {
                KeyValuePair<String, ConfigData> pair = it.Current;
                ConfigData oldData = pair.Value;
                ConfigData newData = newDict[pair.Key];
                newData.Visible = oldData.Visible;
                newData.Checked = oldData.Checked;
                if (!options) {
                    newData.Text = oldData.Text;
                    newData.ToolTipText = oldData.Text;
                }
            }
        }

        public void UpdateMainForm() {
            ConfigureSensors();
            AppManager.MainForm.UpdateSeriesColors();
            AppManager.DataManager.UpdateSeries();
            AppManager.MainForm.UpdateToolTips();
            AppManager.ItemManager.UpdateItems();
        }

        private static Color GetColor(int index) {
            int i;
            Color[] colorTab;

            if (index < _colorsBrightPastel.Length) {
                colorTab = _colorsBrightPastel;
                i = index;
            } else if (index < _colorsBrightPastel.Length + _colorsFire.Length) {
                colorTab = _colorsFire;
                i = index - _colorsBrightPastel.Length;
            } else {
                colorTab = _colorsSeaGreen;
                i = index - _colorsBrightPastel.Length - _colorsFire.Length;
            }
            return colorTab[i];
        }

        private void SetDefaults() {
            IList<CheckBox> list = AppManager.MainForm.SensorsCheckBoxes;
            for (int i = 0; i < list.Count; i++) {
                string key = "S" + (i + 1).ToString("00", CultureInfo.InvariantCulture);
                CheckBox checkBox = list[i];
                ConfigData data = new ConfigData();
                data.Text = checkBox.Text;
                data.ToolTipText = checkBox.Text;
                data.Visible = checkBox.Visible;
                data.Checked = checkBox.Checked;
                data.Color = GetColor(i);
                //checkBox.ForeColor;
                SensorConfigDefault.Add(key, data);
            }
        }

        public void WriteConfig() {
            if (AppConfigChanged || userConfigExists == false) {
                ConfigWriter writer = new ConfigWriter(this);
                writer.Write(userConfigFile);
            }
        }

        private void ReadConfig() {
            if (userConfigExists) {
                ConfigReader reader = new ConfigReader(this);
                reader.Parse(userConfigFile);
            }
            if (SensorConfigValues.Count == 0) {
                SensorConfigValues = GetDefault(SensorConfigDefault);
            }
            if (SensorConfigValues.Count != SensorConfigDefault.Count) {
                foreach (KeyValuePair<String, ConfigData> pair in SensorConfigDefault) {
                    if (!SensorConfigValues.ContainsKey(pair.Key)) {
                        ConfigData value = pair.Value;
                        SensorConfigValues.Add(pair.Key, value.Clone());
                        AppConfigChanged = true;
                    }
                }
            }
        }

        private IDictionary<string, ConfigData> GetDefault(IDictionary<string, ConfigData> defaultDict) {
            IDictionary<String, ConfigData> configDatas = new Dictionary<String, ConfigData>();
            foreach (KeyValuePair<String, ConfigData> pair in defaultDict) {
                ConfigData value = pair.Value;
                configDatas.Add(pair.Key, value.Clone());
            }
            AppConfigChanged = true;
            return configDatas;
        }

        private void ConfigureSensors() {
            IDictionary<String, ConfigData> config = SensorConfigValues;
            Control parent = null;
            IList<CheckBox> list = AppManager.MainForm.SensorsCheckBoxes;
            if (list.Count > 0) {
                parent = list[0].Parent;
                parent.SuspendLayout();
            }
            for (int i = 0; i < list.Count; i++) {
                string key = "V" + (i + 1).ToString("00", CultureInfo.InvariantCulture);
                CheckBox checkBox = list[i];
                ConfigData data = null;
                if (config.ContainsKey(key)) {
                    data = config[key];
                }
                if (data != null) {
                    checkBox.Text = data.Text;
                    checkBox.Visible = data.Visible;
                    checkBox.Checked = data.Checked;
                    checkBox.ForeColor = data.Color;
                }
            }
            if (parent != null) {
                parent.ResumeLayout();
            }
        }

        public void SetConfigData(CheckBoxTag tag) {
            if (SensorConfigValues.ContainsKey(tag.ConfigKey)) {
                ConfigData data = SensorConfigValues[tag.ConfigKey];
                data.Checked = tag.CheckBox.Checked;
            }
        }

        public static Color GetColor(string colorRGB) {
            string[] values = colorRGB.Trim().Split(',');
            if (values != null && values.Length == 3) {
                try {
                    int red = (byte)Int32.Parse(values[0].Trim(), CultureInfo.InvariantCulture);
                    int green = (byte)Int32.Parse(values[1].Trim(), CultureInfo.InvariantCulture);
                    int blue = (byte)Int32.Parse(values[2].Trim(), CultureInfo.InvariantCulture);
                    return Color.FromArgb(red, green, blue);
                }
                catch (FormatException e) {
                    AppExtension.PrintStackTrace(e);
                }
            }
            return Color.DeepPink;
        }

        public static string GetColorString(Color color) {
            return color.R + "," + color.G + "," + color.B;
        }

        private static string GetDocumentsDirectory() {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), AppManager.ConfigPath);
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
