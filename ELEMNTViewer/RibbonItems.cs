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

using RibbonLib;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using ELEMNTViewer;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        const string ComboSize = "XXXX";
        private MainForm _form;
        private bool _toggleSmooth;
        private string _fileName;
        private DecodeFile _decodeFile;
        private bool _modifiedSettings;

        private UICollectionChangedEvent _uiCollectionChangedEvent;

        /// <summary>
        /// Must be called from MainForm Constructor
        /// </summary>
        /// <param name="form">The MainForm</param>
        public void Init(MainForm form)
        {
            this._form = form;
            ButtonSession.Enabled = false;
            ButtonLaps.Enabled = false;
            ToolStripMenuItems(false);
            ComboSelect.RepresentativeString = "Select" + ComboSize;
            ComboPower.RepresentativeString = ComboSize;
            ComboLRBalance.RepresentativeString = ComboSize;
            ComboLSmoothness.RepresentativeString = ComboSize;
            ComboRSmoothness.RepresentativeString = ComboSize;
            ComboLTorque.RepresentativeString = ComboSize;
            ComboRTorque.RepresentativeString = ComboSize;
            ButtonAbout.ExecuteEvent += ButtonAbout_ExecuteEvent;
            ButtonHelp.ExecuteEvent += ButtonAbout_ExecuteEvent;
            ToggleSmooth.ExecuteEvent += ToggleSmooth_ExecuteEvent;
            ButtonOpen.ExecuteEvent += ButtonOpen_ExecuteEvent;
            ButtonSaveGpx.ExecuteEvent += ButtonSaveGpx_ExecuteEvent;
            ButtonExit.ExecuteEvent += ButtonExit_ExecuteEvent;
            ButtonSession.ExecuteEvent += ButtonSession_ExecuteEvent;
            ButtonLaps.ExecuteEvent += ButtonLaps_ExecuteEvent;
            ButtonMyExtras.ExecuteEvent += ButtonMyExtras_ExecuteEvent;
            ButtonHeartRateZones.ExecuteEvent += ButtonHeartRateZones_ExecuteEvent;
            ButtonPowerZones.ExecuteEvent += ButtonPowerZones_ExecuteEvent;

            ButtonActivity.ExecuteEvent += ButtonActivity_ExecuteEvent;
            ButtonDeveloperDataId.ExecuteEvent += ButtonDeveloperDataId_ExecuteEvent;
            ButtonDeviceInfo.ExecuteEvent += ButtonDeviceInfo_ExecuteEvent;
            ButtonEvent.ExecuteEvent += ButtonEvent_ExecuteEvent;
            ButtonFieldDescription.ExecuteEvent += ButtonFieldDescription_ExecuteEvent;
            ButtonFileId.ExecuteEvent += ButtonFileId_ExecuteEvent;
            ButtonSport.ExecuteEvent += ButtonSport_ExecuteEvent;
            ButtonWahooFF00.ExecuteEvent += ButtonWahooFF00_ExecuteEvent;
            ButtonWahooFF01.ExecuteEvent += ButtonWahooFF01_ExecuteEvent;
            ButtonWorkout.ExecuteEvent += ButtonWorkout_ExecuteEvent;

            ComboPower.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLRBalance.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLSmoothness.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboRSmoothness.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLTorque.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboRTorque.ItemsSourceReady += Combo_ItemsSourceReady;
            _uiCollectionChangedEvent = new UICollectionChangedEvent();
            ComboSelect.ExecuteEvent += CheckBoxTag.LapChanged;
            ComboPower.ExecuteEvent += ComboSettings_ExecuteEvent;
            ComboLRBalance.ExecuteEvent += ComboSettings_ExecuteEvent;
            ComboLSmoothness.ExecuteEvent += ComboSettings_ExecuteEvent;
            ComboRSmoothness.ExecuteEvent += ComboSettings_ExecuteEvent;
            ComboLTorque.ExecuteEvent += ComboSettings_ExecuteEvent;
            ComboRTorque.ExecuteEvent += ComboSettings_ExecuteEvent;
            ButtonSetSettings.ExecuteEvent += ButtonSetSettings_ExecuteEvent;
            //ComboSelect.ItemsSourceReady += ComboSelect_ItemsSourceReady;
            MakeCheckControls();
            GetCheckedSettings();
        }

        private void GetCheckedSettings()
        {
            Settings settings = Settings.Instance;
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

        private void ButtonSetSettings_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Settings settings = Settings.Instance;
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

            Settings.Instance.Modified = true;
        }

        private void ComboSettings_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Settings settings = Settings.Instance;
            RibbonComboBox combo = sender as RibbonComboBox;
            switch (combo.CommandID)
            {
                case Cmd.cmdComboPower:
                    settings.PowerSmooth = int.Parse(combo.StringValue);
                    _modifiedSettings = true;
                    break;
                case Cmd.cmdComboLRBalance:
                    settings.LRBalanceSmooth = int.Parse(combo.StringValue);
                    _modifiedSettings = true;
                    break;
                case Cmd.cmdComboLTorque:
                    settings.TorqueLeftSmooth = int.Parse(combo.StringValue);
                    _modifiedSettings = true;
                    break;
                case Cmd.cmdComboRTorque:
                    settings.TorqueRightSmooth = int.Parse(combo.StringValue);
                    _modifiedSettings = true;
                    break;
                case Cmd.cmdComboLSmoothness:
                    settings.SmoothnessLeftSmooth = int.Parse(combo.StringValue);
                    _modifiedSettings = true;
                    break;
                case Cmd.cmdComboRSmoothness:
                    settings.SmoothnessRightSmooth = int.Parse(combo.StringValue);
                    _modifiedSettings = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// must be called from Main LoadEvent
        /// </summary>
        public void Load()
        {
            byte[] modes;
            if (Settings.Instance.Intern)
                modes = new byte[] { 0, 1, 3 };
            else
                modes = new byte[] { 0, 1 };
            Ribbon.SetModes(modes);
        }

        private void ButtonOpen_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            OpenFitFile();
        }

        private void OpenFitFile()
        {
            ButtonSession.Enabled = false;
            ButtonLaps.Enabled = false;
            ToolStripMenuItems(false);
            DataManager.Instance.Clear();
            MakeComboItems();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = "fit";
            dialog.Filter = "ELEMNT Fit-File" + " (*.fit)|*.fit";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {
                _form.Cursor = Cursors.WaitCursor;
                _form.Text = Path.GetFileName(dialog.FileName) + " - " + MainForm.MainFormText;
                _fileName = dialog.FileName;
                _decodeFile = new DecodeFile();
                _decodeFile.Decode(_fileName);
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
            ButtonWorkout.Enabled = enabled;
        }

        private void MakeComboItems()
        {
            IUICollection itemsSource = ComboSelect.ItemsSource;
            if (DataManager.Instance.Session == null)
            {
                ComboSelect.SelectedItem = Constants.UI_Collection_InvalidIndex;
                itemsSource.Clear();
                return;
            }
            itemsSource.Add(new GalleryItemPropertySet() { Label = "Session", CategoryID = Constants.UI_Collection_InvalidIndex });
            if (DataManager.Instance.LapManager.Count > 1)
            {
                for (int i = 0; i < DataManager.Instance.LapManager.Count; i++)
                {
                    itemsSource.Add(new GalleryItemPropertySet() { Label = "Lap " + (i + 1).ToString(), CategoryID = Constants.UI_Collection_InvalidIndex });
                }
            }
            ComboSelect.SelectedItem = 0;
        }

        private void ButtonSaveGpx_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.CheckPathExists = true;
            dialog.DefaultExt = "gpx";
            dialog.Filter = "GPS-File" + " (*.gpx)|*.gpx";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {
                DataManager.Instance.RecordManager.WriteGpx(new FileInfo(dialog.FileName), Path.GetFileNameWithoutExtension(_fileName));
            }
        }

        private void ButtonAbout_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.ShowDialog(_form);
        }

        private void ButtonExit_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void ComboSelect_ItemsSourceReady(object sender, EventArgs e)
        {
            IUICollection itemsSource1 = ComboSelect.ItemsSource;
            itemsSource1.Clear();
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
            IUICollection itemsSource = comboBox.ItemsSource;
            itemsSource.Add(new GalleryItemPropertySet() { Label = "0", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource.Add(new GalleryItemPropertySet() { Label = "3", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource.Add(new GalleryItemPropertySet() { Label = "10", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource.Add(new GalleryItemPropertySet() { Label = "15", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource.Add(new GalleryItemPropertySet() { Label = "30", CategoryID = Constants.UI_Collection_InvalidIndex });
            comboBox.SelectedItem = GetSelectedItem(comboBox);
        }

        private uint GetSelectedItem(RibbonComboBox combo)
        {
            Settings settings = Settings.Instance;
            switch (combo.CommandID)
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

        private uint GetSelectedItem(int value)
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

        private void ButtonSession_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObjects = null;
            dialog.SelectedObject = DataManager.Instance.Session;
            dialog.Header = "Session";
            dialog.PropertySort = PropertySort.Categorized;
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {
            }
        }

        private void ButtonLaps_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.LapManager.LapArray();
            dialog.Header = "Laps";
            dialog.PropertySort = PropertySort.Categorized;
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonHeartRateZones_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObjects = null;
            dialog.SelectedObject = DataManager.Instance.HRManager.GetHeartRateZones();
            dialog.Header = "HeartRate Zones";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonPowerZones_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObjects = null;
            dialog.SelectedObject = DataManager.Instance.PowerManager.GetPowerZones();
            dialog.Header = "Power Zones";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonMyExtras_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObjects = null;
            dialog.SelectedObject = DataManager.Instance.Others;
            dialog.Header = "Others";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ToggleSmooth_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            byte[] modes;
            if (_toggleSmooth)
            {
                if (Settings.Instance.Intern)
                    modes = new byte[] { 0, 1, 3 };
                else
                    modes = new byte[] { 0, 1 };
            }
            else
            {
                if (Settings.Instance.Intern)
                    modes = new byte[] { 0, 2, 3 };
                else
                    modes = new byte[] { 0, 2 };
            }
            _toggleSmooth = !_toggleSmooth;
            Ribbon.SetModes(modes);
        }


        private void MakeCheckControls()
        {
            List<RibbonCheckBox> checkBoxList = new List<RibbonCheckBox>();
            List<RibbonComboBox> comboBoxList = new List<RibbonComboBox>();
            checkBoxList.Add(ButtonAltitude);
            comboBoxList.Add(null);
            checkBoxList.Add(ButtonGrade);
            comboBoxList.Add(null);
            checkBoxList.Add(ButtonHeartRate);
            comboBoxList.Add(null);
            checkBoxList.Add(ButtonCadence);
            comboBoxList.Add(null);
            checkBoxList.Add(ButtonSpeed);
            comboBoxList.Add(null);
            checkBoxList.Add(ButtonPower);
            comboBoxList.Add(ComboPower);
            checkBoxList.Add(ButtonLRBalance);
            comboBoxList.Add(ComboLRBalance);
            checkBoxList.Add(ButtonLSmoothness);
            comboBoxList.Add(ComboLSmoothness);
            checkBoxList.Add(ButtonRSmoothness);
            comboBoxList.Add(ComboRSmoothness);
            checkBoxList.Add(ButtonLTorqueEff);
            comboBoxList.Add(ComboLTorque);
            checkBoxList.Add(ButtonRTorqueEff);
            comboBoxList.Add(ComboRTorque);
            checkBoxList.Add(ButtonTemperature);
            comboBoxList.Add(null);
            int i = 0;
            IList<string> list = GetRecordNames();
            foreach (string propertyName in list)
            {
                if (i < checkBoxList.Count)
                {
                    //checkBoxList[i].Label = propertyName;
                    CheckBoxTag tag = new CheckBoxTag(checkBoxList[i], i, "", propertyName, comboBoxList[i]);
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

        //following methods are only for intern use

        private void ButtonActivity_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.ActivityValues.ToArray();
            dialog.Header = "Activity";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonDeveloperDataId_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.DeveloperDataIdValues.ToArray();
            dialog.Header = "DeveloperDataId";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonDeviceInfo_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.DeviceInfoValues.ToArray();
            dialog.Header = "DeviceInfo";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonEvent_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.EventValues.ToArray();
            dialog.Header = "Event";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonFieldDescription_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.FieldDescriptionValues.ToArray();
            dialog.Header = "FieldDescription";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonFileId_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.FileIdValues.ToArray();
            dialog.Header = "FileId";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonSport_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.SportValues.ToArray();
            dialog.Header = "Sport";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonWahooFF00_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.WahooFF00Values.ToArray();
            dialog.Header = "WahooFF00";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonWahooFF01_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.WahooFF01Values.ToArray();
            dialog.Header = "WahooFF01";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }

        private void ButtonWorkout_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.WorkoutValues.ToArray();
            dialog.Header = "Workout";
            if (dialog.ShowDialog(_form) == DialogResult.OK)
            {

            }
        }
    }
}
