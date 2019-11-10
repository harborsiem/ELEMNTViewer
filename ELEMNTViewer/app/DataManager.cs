using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Globalization;
//using Dynastream.Fit;

namespace ELEMNTViewer {
    class DataManager {

        private SessionValues session;
        private OtherValues others;
        private PowerZonesManager powerManager = new PowerZonesManager();
        private HRZonesManager hrManager = new HRZonesManager();
        private LapManager lapManager = new LapManager();
        private RecordManager recordManager = new RecordManager();
        private List<CheckBoxTag> checkBoxTags = new List<CheckBoxTag>();

        public PowerZonesManager PowerManager { get { return powerManager; } }

        public HRZonesManager HRManager { get { return hrManager; } }

        public LapManager LapManager { get { return lapManager; } }

        public RecordManager RecordManager { get { return recordManager; } }

        public List<RecordValues> RecordList { get { return RecordManager.RecordValuesList; } }

        public SessionValues Session {
            get { return session; }
            set { session = value; } }

        public OtherValues Others {
            get { return others; }
            set { others = value; } }

        public List<CheckBoxTag> CheckBoxTags { get { return checkBoxTags; } }

        public static DataManager Instance = new DataManager();

        private DataManager() {
        }

        public void Clear() {
            recordManager.Clear();
            lapManager.Clear();
            hrManager.Clear();
            powerManager.Clear();
            session = null;
            ClearPlus();
            ClearChart();
        }

        public void FillChart() {
            for (int i = 0; i < checkBoxTags.Count; i++) {
                CheckBoxTag tag = checkBoxTags[i];
                if (tag.CheckBox.BooleanValue) {
                    tag.CheckBoxCheckedChanged();
                }
            }
        }

        public void ClearChart() {
            for (int i = 0; i < checkBoxTags.Count; i++) {
                CheckBoxTag tag = checkBoxTags[i];
                if (tag.CheckBox.BooleanValue) {
                    tag.ClearAndRemoveSeries();
                }
            }
        }

        private void ClearPlus() {
            WahooFF00Values.Clear();
            WahooFF01Values.Clear();
            ActivityValues.Clear();
            FileIdValues.Clear();
            DeviceInfoValues.Clear();
            WorkoutValues.Clear();
            FieldDescriptionValues.Clear();
            DeveloperDataIdValues.Clear();
            EventValues.Clear();
            SportValues.Clear();
        }

        public List<WahooFF00Values> WahooFF00Values = new List<WahooFF00Values>();
        public List<WahooFF01Values> WahooFF01Values = new List<WahooFF01Values>();
        public List<ActivityValues> ActivityValues = new List<ActivityValues>();
        public List<FileIdValues> FileIdValues = new List<FileIdValues>();
        public List<DeviceInfoValues> DeviceInfoValues = new List<DeviceInfoValues>();
        public List<WorkoutValues> WorkoutValues = new List<WorkoutValues>();
        public List<FieldDescriptionValues> FieldDescriptionValues = new List<FieldDescriptionValues>();
        public List<DeveloperDataIdValues> DeveloperDataIdValues = new List<DeveloperDataIdValues>();
        public List<EventValues> EventValues = new List<EventValues>();
        public List<SportValues> SportValues = new List<SportValues>();
    }
}
