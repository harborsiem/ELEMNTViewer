using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private MainForm form;
        private bool toggleSmooth;
        private string fileName;
        private DecodeFile decodeFile;

        private UICollectionChangedEvent _uiCollectionChangedEvent;

        /// <summary>
        /// Must be called from MainForm Constructor
        /// </summary>
        /// <param name="form">The MainForm</param>
        public void Init(MainForm form)
        {
            this.form = form;
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
            ComboPower.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLRBalance.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLSmoothness.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboRSmoothness.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboLTorque.ItemsSourceReady += Combo_ItemsSourceReady;
            ComboRTorque.ItemsSourceReady += Combo_ItemsSourceReady;
            _uiCollectionChangedEvent = new UICollectionChangedEvent();
            ComboSelect.ExecuteEvent += CheckBoxTag.LapChanged;
            //ComboSelect.ItemsSourceReady += ComboSelect_ItemsSourceReady;
            MakeCheckControls();
        }

        /// <summary>
        /// must be called from LoadEvent
        /// </summary>
        public void Load()
        {

        }

        private void ButtonOpen_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            OpenFitFile();
        }

        private void OpenFitFile()
        {
            ToolStripMenuItems(false);
            DataManager.Instance.Clear();
            MakeComboItems();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = "fit";
            dialog.Filter = "ELEMNT Fit-File" + " (*.fit)|*.fit";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {
                form.Cursor = Cursors.WaitCursor;
                form.Text = MainForm.MainFormText + "   " + Path.GetFileName(dialog.FileName);
                fileName = dialog.FileName;
                decodeFile = new DecodeFile();
                decodeFile.Decode(fileName);
                if (DataManager.Instance.Session != null)
                {
                    ButtonSession.Enabled = true;
                }
                if (DataManager.Instance.LapManager.Count > 0)
                {
                    ButtonLaps.Enabled = true;
                }
                ButtonHeartRateZones.Enabled = true;
                ButtonPowerZones.Enabled = true;
                ButtonMyExtras.Enabled = true;
                ButtonSaveGpx.Enabled = true;
                CheckBoxTag.SetDateTime(DataManager.Instance.RecordList[0].Timestamp, DataManager.Instance.RecordList[DataManager.Instance.RecordList.Count - 1].Timestamp);
                MakeComboItems();
                form.Cursor = Cursors.Default;
                DataManager.Instance.FillChart();
            }
            else
            {
                form.Text = MainForm.MainFormText;
            }
        }

        private void ToolStripMenuItems(bool enabled)
        {
            ButtonSaveGpx.Enabled = enabled;
            ButtonSession.Enabled = enabled;
            ButtonLaps.Enabled = enabled;
            ButtonHeartRateZones.Enabled = enabled;
            ButtonPowerZones.Enabled = enabled;
            ButtonMyExtras.Enabled = enabled;
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
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {
                DataManager.Instance.RecordManager.WriteGpx(new FileInfo(dialog.FileName), Path.GetFileNameWithoutExtension(fileName));
            }
        }

        private void ButtonAbout_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.ShowDialog(form);
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
            comboBox.SelectedItem = 0;
        }

        private void ButtonSession_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObjects = null;
            dialog.SelectedObject = DataManager.Instance.Session;
            dialog.Header = "Session";
            dialog.PropertySort = PropertySort.Categorized;
            if (dialog.ShowDialog(form) == DialogResult.OK)
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
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void ButtonHeartRateZones_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObjects = null;
            dialog.SelectedObject = DataManager.Instance.HRManager.GetHeartRateZones();
            dialog.Header = "HeartRate Zones";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void ButtonPowerZones_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObjects = null;
            dialog.SelectedObject = DataManager.Instance.PowerManager.GetPowerZones();
            dialog.Header = "Power Zones";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void ButtonMyExtras_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObjects = null;
            dialog.SelectedObject = DataManager.Instance.Others;
            dialog.Header = "Others";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void ToggleSmooth_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            byte mode;
            if (toggleSmooth)
            {
                mode = 0;
            }
            else
            {
                mode = 1;
            }
            toggleSmooth = !toggleSmooth;
            Ribbon.SetModes(mode);
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

        //following methods are unused

        private void ActivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.ActivityValues.ToArray();
            dialog.Header = "Activity";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void DeveloperDataIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.DeveloperDataIdValues.ToArray();
            dialog.Header = "DeveloperDataId";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void DeviceInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.DeviceInfoValues.ToArray();
            dialog.Header = "DeviceInfo";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void EventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.EventValues.ToArray();
            dialog.Header = "Event";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void FieldDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.FieldDescriptionValues.ToArray();
            dialog.Header = "FieldDescription";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void FileIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.FileIdValues.ToArray();
            dialog.Header = "FileId";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void SportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.SportValues.ToArray();
            dialog.Header = "Sport";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void WahooFF00ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.WahooFF00Values.ToArray();
            dialog.Header = "WahooFF00";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void WahooFF01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.WahooFF01Values.ToArray();
            dialog.Header = "WahooFF01";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }

        private void WorkoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesForm dialog = new PropertiesForm();
            dialog.SelectedObject = null;
            dialog.SelectedObjects = DataManager.Instance.WorkoutValues.ToArray();
            dialog.Header = "Workout";
            if (dialog.ShowDialog(form) == DialogResult.OK)
            {

            }
        }
    }
}
