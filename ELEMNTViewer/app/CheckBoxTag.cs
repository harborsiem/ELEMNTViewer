using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using System.ComponentModel;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;

namespace ELEMNTViewer {
    internal class CheckBoxTag {

        public static Chart Chart { get; set; }
        public static ChartControl ChartControl { get; set; }

        private static DateTime fromDateTime = new DateTime(2016, 1, 1); //DateTime.Now - TimeSpan.FromDays(400);
        private static DateTime toDateTime = DateTime.Now;
        private static int lap;
        private static int lapStartIndex;
        private static int lapEndIndex;

        private Series series;
        public RibbonCheckBox CheckBox { get; private set; }
        private RibbonComboBox _comboBox;
        private ToolTip toolTip;
        public string ConfigKey { get; private set; }
        private string propertyName;
        private string displayName;
        private Int32ArrayAttribute smoothAvgAttribute;
        private int index;
        private Func<RecordValues, double> propertyFunc;
        private Smoothing smoothing;

        public CheckBoxTag(RibbonCheckBox checkBox, int index, string configKey, string propertyName, RibbonComboBox comboBox) {
            series = new Series(propertyName);
            series.ChartType = SeriesChartType.FastLine;
            series.Color = ConfigDefaults.GetColor(index);
            CheckBox = checkBox;
            _comboBox = comboBox;
            checkBox.ExecuteEvent += CheckBoxCheckedChanged;
            this.index = index;
            this.ConfigKey = configKey;
            this.propertyName = propertyName;
            toolTip = new ToolTip();
            propertyFunc = PropertyNameToDelegate(propertyName);
            if (smoothAvgAttribute != null)
            {
                if (_comboBox != null)
                {
                    _comboBox.ExecuteEvent += ComboBox_SelectedItemChanged;
                }

                smoothing = new Smoothing(0);
            }
        }

        public static void LapChanged(object sender, ExecuteEventArgs e) {
            RibbonComboBox chartViewComboBox = sender as RibbonComboBox;
            if (chartViewComboBox == null) {
                return;
            }
            lap = (int)chartViewComboBox.SelectedItem;
            if (lap > 0) {
                lapStartIndex = DataManager.Instance.LapManager.GetStartIndex(lap);
                lapEndIndex = DataManager.Instance.LapManager.GetEndIndex(lap);
            } else {
                lapStartIndex = 0;
                lapEndIndex = DataManager.Instance.RecordList.Count - 1;
            }
            if (lap >= 0) {
                List<RecordValues> list = DataManager.Instance.RecordList;
                DateTime from = list[lapStartIndex].Timestamp;
                DateTime to = list[lapEndIndex].Timestamp;
                TimeSpan span = new TimeSpan(to.Ticks - from.Ticks);
                ChartControl.SetIntervals(span);
            }

            DataManager.Instance.ClearChart();
            DataManager.Instance.FillChart();
        }

        public static void SetDateTime(DateTime from, DateTime to) {
            fromDateTime = from;
            toDateTime = to;
            TimeSpan span = new TimeSpan(to.Ticks - from.Ticks);
            ChartControl.SetIntervals(span);
        }

        private void ComboBox_SelectedItemChanged(object sender, ExecuteEventArgs e)
        {
            RibbonComboBox comboBox = sender as RibbonComboBox;
            string str = comboBox.StringValue;
            int avgTime = int.Parse(str); // = (int)comboBox.SelectedItem;
            smoothing.AvgTime = avgTime;
            if (CheckBox.BooleanValue)
            {
                ClearAndRemoveSeries();
                CheckBoxCheckedChanged();
            }
        }

        private Func<RecordValues, double> PropertyNameToDelegate(string propertyName) {
            PropertyInfo property = typeof(RecordValues).GetProperty(propertyName);
            SetAttributeValues(property);
            MethodInfo method = property.GetGetMethod();
            Func<RecordValues, double> func = (Func<RecordValues, double>)Delegate.CreateDelegate(typeof(Func<RecordValues, double>), null, method);
            return func;
        }

        private void SetAttributeValues(PropertyInfo element) {
            Attribute attr = Attribute.GetCustomAttribute(element, typeof(DisplayNameAttribute), false);
            if (attr != null) {
                displayName = ((DisplayNameAttribute)attr).DisplayName;
            } else {
                displayName = element.Name;
            }
            attr = Attribute.GetCustomAttribute(element, typeof(Int32ArrayAttribute), false);
            if (attr != null) {
                smoothAvgAttribute = (Int32ArrayAttribute)attr;
            }
        }

        private void CheckBoxCheckedChanged(object sender, EventArgs e) {
            RibbonCheckBox checkBox = sender as RibbonCheckBox;
            if (checkBox != null && checkBox.Equals(CheckBox)) {
                CheckBoxCheckedChanged();
            }
        }

        public void CheckBoxCheckedChanged() {
            if (CheckBox.BooleanValue) {
                if (lap > 0) {
                    FillSeriesWithNewData(lapStartIndex, lapEndIndex);
                } else {
                    FillSeriesWithNewData(fromDateTime, toDateTime);
                }
            } else {
                ClearAndRemoveSeries();
            }
        }

        public void ClearAndRemoveSeries() {
            Chart chartMain = Chart;
            chartMain.BeginInit();
            DataPointCollection points = series.Points;
            points.ClearFast(); //MsChartExtension
            if (chartMain.Series.Contains(series)) {
                chartMain.Series.Remove(series);
            }
            chartMain.EndInit();
            if (smoothing != null) {
                smoothing.Clear();
            }
        }

        private void FillSeriesWithNewData(DateTime from, DateTime to) {
            int fromIndex = GetListIndex(from);
            int toIndex = GetListIndex(to);
            FillSeriesWithNewData(fromIndex, toIndex);
        }

        public void FillSeriesWithNewData(int fromIndex, int toIndex) {
            Chart chartMain = Chart;
            if (smoothing != null) {
                smoothing.StartIndex = fromIndex;
            }
            chartMain.BeginInit();
            DataPointCollection points = series.Points;
            points.ClearFast(); //MsChartExtension
            if (fromIndex < 0 || fromIndex >= toIndex) {
                return;
            }
            points.SuspendUpdates();
            for (int i = fromIndex; i <= toIndex; i++) {
                RecordValues values = DataManager.Instance.RecordList[i];
                if (smoothing == null) {
                    //points.AddXY(values.Distance, propertyFunc(values));
                    points.AddXY(values.Timestamp, propertyFunc(values));
                } else {
                    smoothing.SetSmoothValue(values, propertyFunc, i);
                    //points.AddXY(values.Distance, smoothing.SmoothValue);
                    points.AddXY(values.Timestamp, smoothing.SmoothValue);
                }
            }
            points.ResumeUpdates();
            if (!chartMain.Series.Contains(series)) {
                chartMain.Series.Add(series);
            }
            chartMain.EndInit();
        }

        private static int GetListIndex(DateTime value) {
            if (DataManager.Instance.RecordList.Count == 0) {
                return -1;
            }
            for (int i = 0; i < DataManager.Instance.RecordList.Count; i++) {
                if (DataManager.Instance.RecordList[i].Timestamp.CompareTo(value) >= 0) {
                    return i;
                }
            }
            return DataManager.Instance.RecordList.Count - 1;
        }
    }
}
