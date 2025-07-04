using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;

using ELEMNTViewer;
using Resources = ELEMNTViewer.Properties.Resources;
using WpfMaps;

namespace WinForms.Ribbon
{
    partial class RibbonItems
    {
        public enum SmoothValues
        {
            None,
            Three = 3,
            Ten = 10,
            Fifteen = 15,
            Thirty = 30,
        }

        const string ComboSize = "XXXX";
        private static readonly string[] s_smooth = { "0", "3", "10", "15", "30" };
        private MainForm _form;
        private readonly Settings settings = Settings.Instance;
        private bool _toggleSmooth;
        private string _fileName;
        private DecodeFile _decodeFile;
        //private bool _modifiedSettings;
        private byte[] _loadedQatSettings;

        /// <summary>
        /// Must be called from MainForm Constructor
        /// </summary>
        /// <param name="form">The MainForm</param>
        public void Init(MainForm form)
        {
            this._form = form;
            Hidden1.Enabled = false;
            //Hidden2.Enabled = false;
            SetMapItems(false);
            InitChart();
            InitApplication();
            InitViewEvents();
            InitInternalsEvents();
            InitChartSmooth();

            InitSettings();

            SetQatEvents();
            MakeCheckControls();
            GetCheckedSettings();
            TabGroupSettings.ContextAvailable = ContextAvailability.Available;
        }

        /// <summary>
        /// must be called from Main LoadEvent
        /// </summary>
        public void Load()
        {
            //MakeCheckControls();
            Summaries summaries = new Summaries(StatisticsEnabled);
            summaries.Execute1();
            DataManager.Instance.Summaries = summaries;
            SetMapItems(true);
            InitModes();
            GetMapSettings();
        }

        private void StatisticsEnabled()
        {
            if (Ribbon.InvokeRequired)
                Ribbon.Invoke((Action)ButtonStatisticsEnabled);
            else
                ButtonStatisticsEnabled();
        }

        private void ButtonStatisticsEnabled()
        {
            ButtonStatistics.Enabled = true;
        }

        #region Chart Smooth

        private void InitChartSmooth()
        {
            ComboPower.RepresentativeString = ComboSize;
            ComboLRBalance.RepresentativeString = ComboSize;
            ComboLSmoothness.RepresentativeString = ComboSize;
            ComboRSmoothness.RepresentativeString = ComboSize;
            ComboLTorque.RepresentativeString = ComboSize;
            ComboRTorque.RepresentativeString = ComboSize;

            ToggleSmooth.ToggleChanged += ToggleSmooth_ExecuteEvent;
            ComboPower.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLRBalance.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLSmoothness.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboRSmoothness.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLTorque.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboRTorque.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboPower.SelectedIndexChanged += ComboSettings_ExecuteEvent;
            ComboLRBalance.SelectedIndexChanged += ComboSettings_ExecuteEvent;
            ComboLSmoothness.SelectedIndexChanged += ComboSettings_ExecuteEvent;
            ComboRSmoothness.SelectedIndexChanged += ComboSettings_ExecuteEvent;
            ComboLTorque.SelectedIndexChanged += ComboSettings_ExecuteEvent;
            ComboRTorque.SelectedIndexChanged += ComboSettings_ExecuteEvent;
        }

        private void ToggleSmooth_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                byte[] modes;
                if (_toggleSmooth)
                {
                    if (settings.Intern)
                        modes = new byte[] { 0, 1, 3 };
                    else
                        modes = new byte[] { 0, 1 };
                }
                else
                {
                    if (settings.Intern)
                        modes = new byte[] { 0, 2, 3 };
                    else
                        modes = new byte[] { 0, 2 };
                }
                _toggleSmooth = !_toggleSmooth;
                Ribbon.SetModes(modes);
            }
            catch
            {
                throw;
            }
        }

        private void Combo_ItemsSourceReady(object sender, EventArgs e)
        {
            RibbonComboBox combo = sender as RibbonComboBox;
            if (combo != null)
            {
                SetComboValues(combo);
            }
        }

        private void SetComboValues(RibbonComboBox comboBox)
        {
            UICollection<GalleryItemPropertySet> itemsSource = comboBox.GalleryItemItemsSource;
            itemsSource.Add(new GalleryItemPropertySet() { Label = "0", CategoryId = -1 });
            itemsSource.Add(new GalleryItemPropertySet() { Label = "3", CategoryId = -1 });
            itemsSource.Add(new GalleryItemPropertySet() { Label = "10", CategoryId = -1 });
            itemsSource.Add(new GalleryItemPropertySet() { Label = "15", CategoryId = -1 });
            itemsSource.Add(new GalleryItemPropertySet() { Label = "30", CategoryId = -1 });
            comboBox.SelectedItem = GetSelectedItem(comboBox);
        }

        private int GetSelectedItem(RibbonComboBox combo)
        {
            switch (combo.CommandId)
            {
                case Cmd.cmdComboPower:
                    return GetSelectedItem(settings.PowerSmooth);
                case Cmd.cmdComboLRBalance:
                    return GetSelectedItem(settings.LRBalanceSmooth);
                case Cmd.cmdComboLTorque:
                    return GetSelectedItem(settings.TorqueLeftSmooth);
                case Cmd.cmdComboRTorque:
                    return GetSelectedItem(settings.TorqueRightSmooth);
                case Cmd.cmdComboLSmoothness:
                    return GetSelectedItem(settings.SmoothnessLeftSmooth);
                case Cmd.cmdComboRSmoothness:
                    return GetSelectedItem(settings.SmoothnessRightSmooth);
                default:
                    return 0;
            }
        }

        private int GetSelectedItem(int value)
        {
            switch (value)
            {
                case 0:
                default:
                    return 0;
                case 3:
                    return 1;
                case 10:
                    return 2;
                case 15:
                    return 3;
                case 30:
                    return 4;
            }
        }

        #endregion

        #region Chart

        private void InitChart()
        {
            ButtonSession.Enabled = false;
            ButtonLaps.Enabled = false;
            ButtonGears.Enabled = false;
            ToolStripMenuItems(false);
            ComboSelect.RepresentativeString = "Select" + ComboSize;
            //ComboSelect.ItemsSourceReady += ComboSelect_ItemsSourceReady;
            ComboSelect.SelectedIndexChanged += CheckBoxTag.LapChanged;
        }

        private void ComboSelect_ItemsSourceReady(object sender, EventArgs e)
        {
            UICollection<GalleryItemPropertySet> itemsSource1 = ComboSelect.GalleryItemItemsSource;
            itemsSource1.Clear();
        }

        private void MakeComboItems()
        {
            UICollection<GalleryItemPropertySet> itemsSource = ComboSelect.GalleryItemItemsSource;
            if (DataManager.Instance.Session == null)
            {
                ComboSelect.SelectedItem = -1;
                itemsSource.Clear();
                return;
            }
            itemsSource.Add(new GalleryItemPropertySet() { Label = Resources.RS_Session, CategoryId = -1 });
            if (DataManager.Instance.LapManager.Count > 1)
            {
                for (int i = 0; i < DataManager.Instance.LapManager.Count; i++)
                {
                    itemsSource.Add(new GalleryItemPropertySet() { Label = Resources.RS_Lap + " " + (i + 1).ToString(), CategoryId = -1 });
                }
            }
            ComboSelect.SelectedItem = 0;
        }

        private void MakeCheckControls()
        {
            List<ConnectedItems> items = new List<ConnectedItems>();
            //items must be in the same sequence like RecordValues
            items.Add(new ConnectedItems(ButtonAltitude, null, 0, Resources.RS_Altitude));
            items.Add(new ConnectedItems(ButtonGrade, null, 0, Resources.RS_Grade));
            items.Add(new ConnectedItems(ButtonHeartRate, null, 0, Resources.RS_HeartRate));
            items.Add(new ConnectedItems(ButtonCadence, null, 0, Resources.RS_Cadence));
            items.Add(new ConnectedItems(ButtonSpeed, null, 0, Resources.RS_Speed));
            items.Add(new ConnectedItems(ButtonPower, ComboPower, settings.PowerSmooth, Resources.RS_Power));
            items.Add(new ConnectedItems(ButtonLRBalance, ComboLRBalance, settings.LRBalanceSmooth, Resources.RS_LRBalance));
            items.Add(new ConnectedItems(ButtonLSmoothness, ComboLSmoothness, settings.SmoothnessLeftSmooth, Resources.RS_LeftSmooth));
            items.Add(new ConnectedItems(ButtonRSmoothness, ComboRSmoothness, settings.SmoothnessRightSmooth, Resources.RS_RightSmooth));
            items.Add(new ConnectedItems(ButtonLTorqueEff, ComboLTorque, settings.TorqueLeftSmooth, Resources.RS_LeftTE));
            items.Add(new ConnectedItems(ButtonRTorqueEff, ComboRTorque, settings.TorqueRightSmooth, Resources.RS_RightTE));
            items.Add(new ConnectedItems(ButtonTemperature, null, 0, Resources.RS_Temperature));

            int i = 0;
            IList<string> list = GetRecordNames();
            foreach (string propertyName in list)
            {
                if (i < items.Count)
                {
                    CheckBoxTag tag = new CheckBoxTag(items[i], i, propertyName, _form.Font);
                    DataManager.Instance.CheckBoxTags.Add(tag);
                    i++;
                }
            }
        }

        private static IList<string> GetRecordNames()
        {
            List<string> result = new List<string>();
            Type record = typeof(RecordValues);
            PropertyInfo[] infoArray = record.GetProperties();
            foreach (PropertyInfo info in infoArray)
            {
                BrowsableAttribute browsableAttr = null;
                foreach (Attribute attr in info.GetCustomAttributes(false))
                {
                    browsableAttr = attr as BrowsableAttribute;
                    if (browsableAttr != null)
                    {
                        break;
                    }
                }
                //if (info.PropertyType != typeof(DateTime)) {
                if (browsableAttr == null || (browsableAttr != null && browsableAttr.Browsable))
                {
                    result.Add(info.Name);
                }
            }
            return result;
        }

        #endregion

        #region Map

        private void SetMapItems(bool isInLoad)
        {
            if (isInLoad)
            {
                SpinnerMapWidth.DecimalPlaces = 0;
                SpinnerMapWidth.MinValue = 1000;
                SpinnerMapWidth.MaxValue = Screen.PrimaryScreen.WorkingArea.Width;
                SpinnerMapHeight.DecimalPlaces = 0;
                SpinnerMapHeight.MinValue = 750;
                SpinnerMapHeight.MaxValue = Screen.PrimaryScreen.WorkingArea.Height;
            }
            else
            {
                SpinnerMapWidth.RepresentativeString = "xxxxxxx";
                SpinnerMapHeight.RepresentativeString = "xxxxxxx";
                ButtonMap.Click += ButtonMap_ExecuteEvent;
            }
        }

        private void ButtonMap_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                MapControl.LocationCollection locations = new MapControl.LocationCollection();
                List<PointItem> pushpinItems = new List<PointItem>();
                List<PointItem> pointItems = new List<PointItem>();
                MapControl.Location mapCenter = null;
                double distanceStart = 0;
                string km = Resources.RS_Km;
                List<RecordValues> list = DataManager.Instance.RecordList;
                for (int i = 0; i < list.Count; i++)
                {
                    double latitude = list[i].PositionLat;
                    double longitude = list[i].PositionLong;
                    double distance = list[i].Distance;
                    MapControl.Location location = new MapControl.Location(latitude, longitude);
                    if (mapCenter == null && latitude != 0)
                    {
                        mapCenter = location;
                        pushpinItems.Add(new PointItem() { Location = location, Name = distanceStart.ToString() + " " + km });
                        distanceStart += 5;
                    }
                    if (mapCenter != null && latitude != 0)
                    {
                        locations.Add(location);
                        if (distance >= distanceStart)
                        {
                            pushpinItems.Add(new PointItem() { Location = location, Name = distanceStart.ToString() + " " + km });
                            distanceStart += 5;
                        }
                    }
                }
                MapHandler handler = new MapHandler((int)SpinnerMapWidth.DecimalValue, (int)SpinnerMapHeight.DecimalValue);
                handler.SetLocations(mapCenter, locations, pushpinItems, pointItems);
                handler.ShowDialog();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region QAT Settings

        private void SetQatEvents()
        {
            Ribbon.ViewCreated += Ribbon_ViewCreated;
            Ribbon.ViewDestroy += Ribbon_ViewDestroy;
        }

        private string GetQatSettingsFileName()
        {
            string path = Settings.ThisLocalAppData;
            return Path.Combine(path, "Qat.xml");
        }

        private void Ribbon_ViewCreated(object sender, EventArgs e)
        {
            try
            {
                string fileName = GetQatSettingsFileName();
                if (File.Exists(fileName))
                {
                    Stream stream = File.OpenRead(fileName);
                    _loadedQatSettings = new byte[stream.Length];
                    stream.Read(_loadedQatSettings, 0, _loadedQatSettings.Length);
                    stream.Position = 0;
                    if (!settings.VersionChanged)
                        Ribbon.LoadSettingsFromStream(stream);
                    stream.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        private bool EqualSettings(MemoryStream stream)
        {
            if (_loadedQatSettings == null || stream.Length != _loadedQatSettings.Length)
                return false;
            byte[] buffer = stream.ToArray();
            for (int i = 0; i < buffer.Length; i++)
                if (_loadedQatSettings[i] != buffer[i])
                    return false;
            return true;
        }

        private void Ribbon_ViewDestroy(object sender, EventArgs e)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                Ribbon.SaveSettingsToStream(stream);
                stream.Position = 0;
                if (!EqualSettings(stream))
                {
                    string fileName = GetQatSettingsFileName();
                    FileStream fStream = File.Create(fileName);
                    stream.CopyTo(fStream);
                    fStream.Close();
                }
                stream.Close();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Settings

        private void InitSettings()
        {
            ButtonSaveSettings.Click += ButtonSaveSettings_ExecuteEvent;
            CheckLocalize.BooleanValue = settings.Localized;
        }

        private void GetCheckedSettings()
        {
            ButtonSpeed.BooleanValue = settings.SpeedChecked;
            ButtonCadence.BooleanValue = settings.CadenceChecked;
            ButtonPower.BooleanValue = settings.PowerChecked;
            ButtonLRBalance.BooleanValue = settings.LRBalanceChecked;
            ButtonHeartRate.BooleanValue = settings.HeartRateChecked;
            ButtonLTorqueEff.BooleanValue = settings.LTorqueChecked;
            ButtonRTorqueEff.BooleanValue = settings.RTorqueChecked;
            ButtonLSmoothness.BooleanValue = settings.LSmoothChecked;
            ButtonRSmoothness.BooleanValue = settings.RSmoothChecked;
            ButtonAltitude.BooleanValue = settings.AltitudeChecked;
            ButtonGrade.BooleanValue = settings.GradeChecked;
            ButtonTemperature.BooleanValue = settings.TemperatureChecked;
        }

        private void ButtonSaveSettings_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                settings.SpeedChecked = ButtonSpeed.BooleanValue;
                settings.CadenceChecked = ButtonCadence.BooleanValue;
                settings.PowerChecked = ButtonPower.BooleanValue;
                settings.LRBalanceChecked = ButtonLRBalance.BooleanValue;
                settings.HeartRateChecked = ButtonHeartRate.BooleanValue;
                settings.LTorqueChecked = ButtonLTorqueEff.BooleanValue;
                settings.RTorqueChecked = ButtonRTorqueEff.BooleanValue;
                settings.LSmoothChecked = ButtonLSmoothness.BooleanValue;
                settings.RSmoothChecked = ButtonRSmoothness.BooleanValue;
                settings.AltitudeChecked = ButtonAltitude.BooleanValue;
                settings.GradeChecked = ButtonGrade.BooleanValue;
                settings.TemperatureChecked = ButtonTemperature.BooleanValue;

                settings.MapWidth = (int)SpinnerMapWidth.DecimalValue;
                settings.MapHeight = (int)SpinnerMapHeight.DecimalValue;

                if (CheckCurrentAppSize.BooleanValue)
                {
                    settings.AppWidth = _form.Width;
                    settings.AppHeight = _form.Height;
                }
                settings.Localized = CheckLocalize.BooleanValue;

                settings.Modified = true;
            }
            catch
            {
                throw;
            }
        }

        private void ComboSettings_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                RibbonComboBox combo = sender as RibbonComboBox;
                switch (combo.CommandId)
                {
                    case Cmd.cmdComboPower:
                        settings.PowerSmooth = int.Parse(combo.StringValue);
                        //_modifiedSettings = true;
                        break;
                    case Cmd.cmdComboLRBalance:
                        settings.LRBalanceSmooth = int.Parse(combo.StringValue);
                        //_modifiedSettings = true;
                        break;
                    case Cmd.cmdComboLTorque:
                        settings.TorqueLeftSmooth = int.Parse(combo.StringValue);
                        //_modifiedSettings = true;
                        break;
                    case Cmd.cmdComboRTorque:
                        settings.TorqueRightSmooth = int.Parse(combo.StringValue);
                        //_modifiedSettings = true;
                        break;
                    case Cmd.cmdComboLSmoothness:
                        settings.SmoothnessLeftSmooth = int.Parse(combo.StringValue);
                        //_modifiedSettings = true;
                        break;
                    case Cmd.cmdComboRSmoothness:
                        settings.SmoothnessRightSmooth = int.Parse(combo.StringValue);
                        //_modifiedSettings = true;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        private void GetMapSettings()
        {
            SpinnerMapWidth.DecimalValue = settings.MapWidth;
            SpinnerMapHeight.DecimalValue = settings.MapHeight;
        }

        #endregion

        #region Application

        private void InitApplication()
        {
            ButtonStatistics.Enabled = false; //Button turn to true when background task for statistic value is executed
            ButtonOpenFit.Click += ButtonOpenFit_ExecuteEvent;
            ButtonOpenGpx.Click += ButtonOpenGpx_ExecuteEvent;
            ButtonSaveGpx.Click += ButtonSaveGpx_ExecuteEvent;
            ButtonHelp.Click += ButtonAbout_ExecuteEvent;
            ButtonAbout.Click += ButtonAbout_ExecuteEvent;
            ButtonExit.Click += ButtonExit_ExecuteEvent;
        }

        private void ButtonOpenGpx_ExecuteEvent(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = "gpx";
            dialog.Filter = Resources.RS_GpxFile + " (*.gpx)|*.gpx";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {
                _form.Text = Path.GetFileName(dialog.FileName); // + " - " + MainForm.MainFormText;
                try
                {
                    GpxParser parser = new GpxParser(settings.MapWidth, settings.MapHeight);
                    parser.Parse(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _form.Text = MainForm.MainFormText;
            }
        }

        private void InitModes()
        {
            byte[] modes;
            if (settings.Intern)
                modes = new byte[] { 0, 1, 3 };
            else
                modes = new byte[] { 0, 1 };
            Ribbon.SetModes(modes);
        }

        private void ButtonOpenFit_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                OpenFitFile();
            }
            catch
            {
                throw;
            }
        }

        private void OpenFitFile()
        {
            ButtonSession.Enabled = false;
            ButtonLaps.Enabled = false;
            ButtonGears.Enabled = false;
            ToolStripMenuItems(false);
            DataManager.Instance.Clear();
            MakeComboItems();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = "fit";
            dialog.Filter = Resources.RS_ELEMNT_FitFile + " (*.fit)|*.fit";
            if (!string.IsNullOrEmpty(settings.FitPath))
                dialog.InitialDirectory = settings.FitPath;
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {
                _form.Cursor = Cursors.WaitCursor;
                _form._chartHelp.ResetZoom();
                _form.Text = Path.GetFileName(dialog.FileName); // + " - " + MainForm.MainFormText;
                _fileName = dialog.FileName;
                string fitPath = Path.GetDirectoryName(_fileName);
                if (settings.FitPath != fitPath)
                {
                    settings.FitPath = fitPath;
                    settings.Modified = true;
                }
                //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                //watch.Restart();
                _decodeFile = new DecodeFile();
                _decodeFile.Decode(_fileName);
                Gears gears = new Gears();
                ButtonGears.Enabled = gears.AntGearChanger;
                DataManager.Instance.Gears = gears;
                //watch.Stop();
                //long elapsed = watch.ElapsedMilliseconds;
                if (DataManager.Instance.Session != null)
                {
                    ButtonSession.Enabled = true;
                }
                if (DataManager.Instance.LapManager.Count > 0)
                {
                    ButtonLaps.Enabled = true;
                }
                ToolStripMenuItems(true);
                CheckBoxTag.SetDateTime(DataManager.Instance.RecordList[0].Timestamp, DataManager.Instance.RecordList[DataManager.Instance.RecordList.Count - 1].Timestamp);
                MakeComboItems();
                CheckBoxTag.SetToSession(); //@ Todo: Refactoring
                _form.Cursor = Cursors.Default;
                DataManager.Instance.FillChart();
            }
            else
            {
                _form.Text = MainForm.MainFormText;
            }
        }

        private void ToolStripMenuItems(bool enabled)
        {
            ButtonSaveGpx.Enabled = enabled;
            ButtonHeartRateZones.Enabled = enabled;
            ButtonPowerZones.Enabled = enabled;
            ButtonMyExtras.Enabled = enabled;

            ButtonActivity.Enabled = enabled;
            ButtonDeveloperDataId.Enabled = enabled;
            ButtonDeviceInfo.Enabled = enabled;
            ButtonEvent.Enabled = enabled;
            ButtonFieldDescription.Enabled = enabled;
            ButtonFileId.Enabled = enabled;
            ButtonSport.Enabled = enabled;
            ButtonWahooFF00.Enabled = enabled;
            ButtonWahooFF01.Enabled = enabled;
            ButtonWahooFF04.Enabled = enabled;
            ButtonWorkout.Enabled = enabled;
            ButtonMap.Enabled = enabled;
        }

        private void ButtonSaveGpx_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.CheckPathExists = true;
                dialog.DefaultExt = "gpx";
                dialog.Filter = Resources.RS_GpsFile + " (*.gpx)|*.gpx";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                    DataManager.Instance.RecordManager.WriteGpx(new FileInfo(dialog.FileName), Path.GetFileNameWithoutExtension(_fileName));
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonAbout_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                AboutDialog dialog = new AboutDialog();
                dialog.ShowDialog(_form);
            }
            catch
            {
                throw;
            }
        }

        private void ButtonExit_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                _form.BeginInvoke(new MethodInvoker(System.Windows.Forms.Application.Exit));
                //System.Windows.Forms.Application.Exit();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region View

        private void InitViewEvents()
        {
            ButtonSession.Click += ButtonSession_ExecuteEvent;
            ButtonLaps.Click += ButtonLaps_ExecuteEvent;
            ButtonMyExtras.Click += ButtonMyExtras_ExecuteEvent;
            ButtonGears.Click += ButtonGears_ExecuteEvent;
            ButtonHeartRateZones.Click += ButtonHeartRateZones_ExecuteEvent;
            ButtonPowerZones.Click += ButtonPowerZones_ExecuteEvent;
            ButtonStatistics.Click += ButtonStatistics_ExecuteEvent;
        }

        private void ButtonGears_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObjects = null;
                dialog.SelectedObject = DataManager.Instance.Gears;
                dialog.Header = Resources.RS_Gears;
                dialog.Grid.PropertySort = PropertySort.Categorized;
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonSession_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObjects = null;
                dialog.SelectedObject = DataManager.Instance.Session;
                dialog.Header = Resources.RS_Session;
                dialog.Grid.PropertySort = PropertySort.Categorized;
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonLaps_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.LapManager.LapArray();
                dialog.Header = Resources.RS_Laps;
                dialog.Grid.PropertySort = PropertySort.Categorized;
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonStatistics_ExecuteEvent(object sender, EventArgs e)
        {
            StatisticsForm dialog = new StatisticsForm();
            dialog.ShowDialog(_form);
        }

        private void ButtonHeartRateZones_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObjects = null;
                dialog.SelectedObject = DataManager.Instance.HRManager.GetHeartRateZones();
                dialog.Header = Resources.RS_HeartRateZones;
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonPowerZones_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObjects = null;
                dialog.SelectedObject = DataManager.Instance.PowerManager.GetPowerZones();
                dialog.Header = Resources.RS_PowerZones;
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonMyExtras_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObjects = null;
                dialog.SelectedObject = DataManager.Instance.SessionExtras;
                dialog.Grid.PropertySort = PropertySort.Categorized;
                dialog.Header = Resources.RS_SessionExtras;
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Internals
        //following methods are only for internal use

        private void InitInternalsEvents()
        {
            ButtonActivity.Click += ButtonActivity_ExecuteEvent;
            ButtonDeveloperDataId.Click += ButtonDeveloperDataId_ExecuteEvent;
            ButtonDeviceInfo.Click += ButtonDeviceInfo_ExecuteEvent;
            ButtonEvent.Click += ButtonEvent_ExecuteEvent;
            ButtonFieldDescription.Click += ButtonFieldDescription_ExecuteEvent;
            ButtonFileId.Click += ButtonFileId_ExecuteEvent;
            ButtonSport.Click += ButtonSport_ExecuteEvent;
            ButtonWahooFF00.Click += ButtonWahooFF00_ExecuteEvent;
            ButtonWahooFF01.Click += ButtonWahooFF01_ExecuteEvent;
            ButtonWahooFF04.Click += ButtonWahooFF04_ExecuteEvent;
            ButtonWorkout.Click += ButtonWorkout_ExecuteEvent;
        }

        private void ButtonActivity_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.ActivityValues.ToArray();
                dialog.Header = "Activity";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonDeveloperDataId_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.DeveloperDataIdValues.ToArray();
                dialog.Header = "DeveloperDataId";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonDeviceInfo_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.DeviceInfoValues.ToArray();
                dialog.Header = "DeviceInfo";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonEvent_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.EventValues.ToArray();
                dialog.Header = "Event";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonFieldDescription_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.FieldDescriptionValues.ToArray();
                dialog.Header = "FieldDescription";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonFileId_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.FileIdValues.ToArray();
                dialog.Header = "FileId";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonSport_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.SportValues.ToArray();
                dialog.Header = "Sport";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonWahooFF00_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.WahooFF00Values.ToArray();
                dialog.Header = "WahooFF00";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonWahooFF01_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.WahooFF01Values.ToArray();
                dialog.Header = "WahooFF01";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonWahooFF04_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.WahooFF04Values.ToArray();
                dialog.Header = "WahooFF04";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonWorkout_ExecuteEvent(object sender, EventArgs e)
        {
            try
            {
                PropertiesForm dialog = new PropertiesForm();
                dialog.SelectedObject = null;
                dialog.SelectedObjects = DataManager.Instance.WorkoutValues.ToArray();
                dialog.Header = "Workout";
                if (dialog.ShowDialog(_form) == DialogResult.OK)
                {
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
