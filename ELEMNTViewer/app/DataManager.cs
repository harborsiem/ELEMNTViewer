using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Globalization;
//using Dynastream.Fit;

namespace ELEMNTViewer
{
    class DataManager
    {
        private SessionValues _session;
        private OtherValues _sessionExtras;
        private PowerZonesManager _powerManager = new PowerZonesManager();
        private HRZonesManager _hrManager = new HRZonesManager();
        private LapManager _lapManager = new LapManager();
        private RecordManager _recordManager = new RecordManager();
        private List<CheckBoxTag> _checkBoxTags = new List<CheckBoxTag>();

        public Summaries Summaries { get; set; }

        public PowerZonesManager PowerManager { get { return _powerManager; } }

        public HRZonesManager HRManager { get { return _hrManager; } }

        public LapManager LapManager { get { return _lapManager; } }

        public RecordManager RecordManager { get { return _recordManager; } }

        public List<RecordValues> RecordList { get { return RecordManager.RecordValuesList; } }

        public Gears Gears { get; set; }

        public SessionValues Session
        {
            get { return _session; }
            set { _session = value; }
        }

        public OtherValues SessionExtras
        {
            get { return _sessionExtras; }
            set { _sessionExtras = value; }
        }

        public List<CheckBoxTag> CheckBoxTags { get { return _checkBoxTags; } }

        public static DataManager Instance = new DataManager();

        private DataManager()
        {
        }

        public void Clear()
        {
            _recordManager.Clear();
            _lapManager.Clear();
            _hrManager.Clear();
            _powerManager.Clear();
            _session = null;
            Gears = null;
            ClearAdditionalValues();
            ClearChart();
        }

        public void FillChart()
        {
            for (int i = 0; i < _checkBoxTags.Count; i++)
            {
                CheckBoxTag tag = _checkBoxTags[i];
                if (tag.ChartCheckBox.BooleanValue)
                {
                    tag.ChartCheckBoxCheckedChanged();
                }
            }
        }

        public void ClearChart()
        {
            for (int i = 0; i < _checkBoxTags.Count; i++)
            {
                CheckBoxTag tag = _checkBoxTags[i];
                if (tag.ChartCheckBox.BooleanValue)
                {
                    tag.ClearAndRemoveSeries();
                }
            }
        }

        private void ClearAdditionalValues()
        {
            WahooFF00Values.Clear();
            WahooFF01Values.Clear();
            WahooFF04Values.Clear();
            ActivityValues.Clear();
            FileIdValues.Clear();
            DeviceInfoValues.Clear();
            WorkoutValues.Clear();
            FieldDescriptionValues.Clear();
            DeveloperDataIdValues.Clear();
            EventValues.Clear();
            SportValues.Clear();
        }

        public List<WahooFF00Values> WahooFF00Values { get; private set; } = new List<WahooFF00Values>();
        public List<WahooFF01Values> WahooFF01Values { get; private set; } = new List<WahooFF01Values>();
        public List<WahooFF04Values> WahooFF04Values { get; private set; } = new List<WahooFF04Values>();
        public List<ActivityValues> ActivityValues { get; private set; } = new List<ActivityValues>();
        public List<FileIdValues> FileIdValues { get; private set; } = new List<FileIdValues>();
        public List<DeviceInfoValues> DeviceInfoValues { get; private set; } = new List<DeviceInfoValues>();
        public List<WorkoutValues> WorkoutValues { get; private set; } = new List<WorkoutValues>();
        public List<FieldDescriptionValues> FieldDescriptionValues { get; private set; } = new List<FieldDescriptionValues>();
        public List<DeveloperDataIdValues> DeveloperDataIdValues { get; private set; } = new List<DeveloperDataIdValues>();
        public List<EventValues> EventValues { get; private set; } = new List<EventValues>();
        public List<SportValues> SportValues { get; private set; } = new List<SportValues>();
    }
}
