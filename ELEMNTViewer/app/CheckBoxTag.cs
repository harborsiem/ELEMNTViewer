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

namespace ELEMNTViewer
{
    internal class CheckBoxTag
    {

        public static Chart Chart { get; set; }
        public static ChartHelp ChartHelp { get; set; }

        private static DateTime s_fromDateTime = new DateTime(2016, 1, 1); //DateTime.Now - TimeSpan.FromDays(400);
        private static DateTime s_toDateTime = DateTime.Now;
        private static int s_lap;
        private static int s_lapStartIndex;
        private static int s_lapEndIndex;

        private Series _series;
        public RibbonCheckBox ChartCheckBox { get; private set; }
        private RibbonComboBox _smoothChartComboBox;
        private string _propertyName;
        private string _displayName;
        private Int32ArrayAttribute _smoothAvgAttribute;
        private int _index;
        private Func<RecordValues, double> _propertyFunc;
        private Smoothing _smoothing;

        public CheckBoxTag(RibbonCheckBox chartCheckBox, int index, string propertyName, RibbonComboBox smoothChartComboBox)
        {
            _series = new Series(propertyName);
            _series.ChartType = SeriesChartType.FastLine;
            _series.Color = ConfigDefaults.GetColor(index);
            ChartCheckBox = chartCheckBox;
            _smoothChartComboBox = smoothChartComboBox;
            chartCheckBox.ExecuteEvent += ChartCheckBoxCheckedChanged;
            this._index = index;
            this._propertyName = propertyName;
            _propertyFunc = PropertyNameToDelegate(propertyName);
            if (_smoothAvgAttribute != null)
            {
                if (_smoothChartComboBox != null)
                {
                    _smoothChartComboBox.ExecuteEvent += SmoothChartComboBox_SelectedItemChanged;
                }

                _smoothing = new Smoothing(0);
            }
        }

        public static void SetToSession()
        {
            s_lap = 0;
        }

        public static void LapChanged(object sender, ExecuteEventArgs e)
        {
            RibbonComboBox selectChartViewComboBox = sender as RibbonComboBox;
            if (selectChartViewComboBox == null)
            {
                return;
            }
            s_lap = (int)selectChartViewComboBox.SelectedItem;
            if (s_lap > 0)
            {
                s_lapStartIndex = DataManager.Instance.LapManager.GetStartIndex(s_lap);
                s_lapEndIndex = DataManager.Instance.LapManager.GetEndIndex(s_lap);
            }
            else
            {
                s_lapStartIndex = 0;
                s_lapEndIndex = DataManager.Instance.RecordList.Count - 1;
            }
            if (s_lap >= 0)
            {
                List<RecordValues> list = DataManager.Instance.RecordList;
                DateTime from = list[s_lapStartIndex].Timestamp;
                DateTime to = list[s_lapEndIndex].Timestamp;
                TimeSpan span = new TimeSpan(to.Ticks - from.Ticks);
                ChartHelp.SetIntervals(span);
            }

            DataManager.Instance.ClearChart();
            DataManager.Instance.FillChart();
        }

        public static void SetDateTime(DateTime from, DateTime to)
        {
            s_fromDateTime = from;
            s_toDateTime = to;
            TimeSpan span = new TimeSpan(to.Ticks - from.Ticks);
            ChartHelp.SetIntervals(span);
        }

        private void SmoothChartComboBox_SelectedItemChanged(object sender, ExecuteEventArgs e)
        {
            RibbonComboBox comboBox = sender as RibbonComboBox;
            string str = comboBox.StringValue;
            int avgTime = int.Parse(str); // = (int)comboBox.SelectedItem;
            _smoothing.AvgTime = avgTime;
            if (ChartCheckBox.BooleanValue)
            {
                ClearAndRemoveSeries();
                ChartCheckBoxCheckedChanged();
            }
        }

        private Func<RecordValues, double> PropertyNameToDelegate(string propertyName)
        {
            PropertyInfo property = typeof(RecordValues).GetProperty(propertyName);
            SetAttributeValues(property);
            MethodInfo method = property.GetGetMethod();
            Func<RecordValues, double> func = (Func<RecordValues, double>)Delegate.CreateDelegate(typeof(Func<RecordValues, double>), null, method);
            return func;
        }

        private void SetAttributeValues(PropertyInfo element)
        {
            Attribute attr = Attribute.GetCustomAttribute(element, typeof(DisplayNameAttribute), false);
            if (attr != null)
            {
                _displayName = ((DisplayNameAttribute)attr).DisplayName;
            }
            else
            {
                _displayName = element.Name;
            }
            attr = Attribute.GetCustomAttribute(element, typeof(Int32ArrayAttribute), false);
            if (attr != null)
            {
                _smoothAvgAttribute = (Int32ArrayAttribute)attr;
            }
        }

        private void ChartCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            RibbonCheckBox checkBox = sender as RibbonCheckBox;
            if (checkBox != null && checkBox.Equals(ChartCheckBox))
            {
                ChartCheckBoxCheckedChanged();
            }
        }

        public void ChartCheckBoxCheckedChanged()
        {
            if (ChartCheckBox.BooleanValue)
            {
                if (s_lap > 0)
                {
                    FillSeriesWithNewData(s_lapStartIndex, s_lapEndIndex);
                }
                else
                {
                    FillSeriesWithNewData(s_fromDateTime, s_toDateTime);
                }
            }
            else
            {
                ClearAndRemoveSeries();
            }
        }

        public void ClearAndRemoveSeries()
        {
            Chart chartMain = Chart;
            chartMain.BeginInit();
            DataPointCollection points = _series.Points;
            points.ClearFast(); //MsChartExtension
            if (chartMain.Series.Contains(_series))
            {
                chartMain.Series.Remove(_series);
            }
            chartMain.EndInit();
            if (_smoothing != null)
            {
                _smoothing.Clear();
            }
        }

        private void FillSeriesWithNewData(DateTime from, DateTime to)
        {
            int fromIndex = GetRecordListIndex(from);
            int toIndex = GetRecordListIndex(to);
            FillSeriesWithNewData(fromIndex, toIndex);
        }

        public void FillSeriesWithNewData(int fromIndex, int toIndex)
        {
            Chart chartMain = Chart;
            if (_smoothing != null)
            {
                _smoothing.StartIndex = fromIndex;
            }
            chartMain.BeginInit();
            DataPointCollection points = _series.Points;
            points.ClearFast(); //MsChartExtension
            if (fromIndex < 0 || fromIndex >= toIndex)
            {
                return;
            }
            points.SuspendUpdates();
            for (int i = fromIndex; i <= toIndex; i++)
            {
                RecordValues values = DataManager.Instance.RecordList[i];
                if (_smoothing == null)
                {
                    //points.AddXY(values.Distance, propertyFunc(values));
                    points.AddXY(values.Timestamp, _propertyFunc(values));
                }
                else
                {
                    _smoothing.SetSmoothValue(values, _propertyFunc, i);
                    //points.AddXY(values.Distance, smoothing.SmoothValue);
                    points.AddXY(values.Timestamp, _smoothing.SmoothValue);
                }
            }
            if (_smoothing == null || _smoothing.AvgTime == 0)
                _series.Name = _propertyName;
            else
                _series.Name = _propertyName + " (" + _smoothing.AvgTime.ToString() + ")";

            points.ResumeUpdates();
            if (!chartMain.Series.Contains(_series))
            {
                chartMain.Series.Add(_series);
            }
            chartMain.EndInit();
        }

        private static int GetRecordListIndex(DateTime value)
        {
            if (DataManager.Instance.RecordList.Count == 0)
            {
                return -1;
            }
            for (int i = 0; i < DataManager.Instance.RecordList.Count; i++)
            {
                if (DataManager.Instance.RecordList[i].Timestamp.CompareTo(value) >= 0)
                {
                    return i;
                }
            }
            return DataManager.Instance.RecordList.Count - 1;
        }
    }
}
