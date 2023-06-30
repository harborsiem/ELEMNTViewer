using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ELEMNTViewer
{
    class ChartHelp
    {
        private double resetZoomLabelInterval = 15D;
        private DateTimeIntervalType resetZoomLabelIntervalType;
        private double resetZoomAxisInterval = 15D;
        private DateTimeIntervalType resetZoomAxisIntervalType;
        private Chart chart;

        public ChartHelp(Chart chart)
        {
            this.chart = chart;
            chart.AxisViewChanged += new EventHandler<ViewEventArgs>(Chart_AxisViewChanged);
            CheckBoxTag.Chart = chart;
            CheckBoxTag.ChartHelp = this;
            ChartArea chartArea1 = chart.ChartAreas["ChartArea1"];
            resetZoomAxisInterval = chartArea1.AxisX.Interval;
            resetZoomAxisIntervalType = chartArea1.AxisX.IntervalType;
            resetZoomLabelInterval = chartArea1.AxisX.LabelStyle.Interval;
            resetZoomLabelIntervalType = chartArea1.AxisX.LabelStyle.IntervalType;
            chart.PrePaint += new EventHandler<ChartPaintEventArgs>(chartMain_PrePaint);
        }

        void chartMain_PrePaint(object sender, ChartPaintEventArgs e)
        {
            ChartArea area1 = chart.ChartAreas["ChartArea1"];
            if (double.IsNaN(area1.AxisY.Maximum))
            {
                return;
            }
            if (area1.AxisY.Maximum <= 100.0)
            {
                area1.AxisY.MajorGrid.Interval = 5;
            }
            else
            {
                if (area1.AxisY.Maximum <= 500.0)
                    area1.AxisY.MajorGrid.Interval = 20;
                else
                    area1.AxisY.MajorGrid.Interval = 50;
            }
        }

        private static void CalculateLabelInterval(Axis axisX1)
        {
            double diff = axisX1.ScaleView.ViewMaximum - axisX1.ScaleView.ViewMinimum;
            if (diff > 1.03)
            { // > 1 day
                axisX1.MinorGrid.Interval = 3D;
                axisX1.MinorGrid.IntervalType = DateTimeIntervalType.Hours;
                axisX1.Interval = 3;
                axisX1.IntervalType = DateTimeIntervalType.Hours;
                axisX1.LabelStyle.Interval = 3;
                axisX1.LabelStyle.IntervalType = DateTimeIntervalType.Hours;
            }
            else if (diff > 0.25)
            { // 24 hours > diff > 6 hours 
                axisX1.MinorGrid.Interval = 1D;
                axisX1.MinorGrid.IntervalType = DateTimeIntervalType.Hours;
                axisX1.Interval = 1;
                axisX1.IntervalType = DateTimeIntervalType.Hours;
                axisX1.LabelStyle.Interval = 1;
                axisX1.LabelStyle.IntervalType = DateTimeIntervalType.Hours;
            }
            else if (diff > 0.085)
            { // 6 hours > diff > 2 hours 
                axisX1.MinorGrid.Interval = 15D;
                axisX1.MinorGrid.IntervalType = DateTimeIntervalType.Minutes;
                axisX1.Interval = 15;
                axisX1.IntervalType = DateTimeIntervalType.Minutes;
                axisX1.LabelStyle.Interval = 15;
                axisX1.LabelStyle.IntervalType = DateTimeIntervalType.Minutes;
            }
            else if (diff > 0.0283)
            { // diff <= 2 hours 
                axisX1.MinorGrid.Interval = 5D;
                axisX1.MinorGrid.IntervalType = DateTimeIntervalType.Minutes;
                axisX1.Interval = 5;
                axisX1.IntervalType = DateTimeIntervalType.Minutes;
                axisX1.LabelStyle.Interval = 5;
                axisX1.LabelStyle.IntervalType = DateTimeIntervalType.Minutes;
            }
            else
            {
                axisX1.MinorGrid.Interval = 1D;
                axisX1.MinorGrid.IntervalType = DateTimeIntervalType.Minutes;
                axisX1.Interval = 1;
                axisX1.IntervalType = DateTimeIntervalType.Minutes;
                axisX1.LabelStyle.Interval = 1;
                axisX1.LabelStyle.IntervalType = DateTimeIntervalType.Minutes;
            }
        }

        public void ResetZoom()
        {
            chart.SuspendLayout();
            Axis axisX1 = chart.ChartAreas["ChartArea1"].AxisX;
            axisX1.LabelStyle.Angle = -90;
            if (axisX1.ScaleView.IsZoomed)
            {
                ResetZoom(axisX1);
                axisX1.ScaleView.ZoomReset(100);
            }
            chart.ResumeLayout();
        }

        private void ResetZoom(Axis axisX1)
        {
            axisX1.MinorGrid.Enabled = false;
            axisX1.LabelStyle.Angle = -90;
            axisX1.Interval = resetZoomAxisInterval;
            axisX1.IntervalType = resetZoomAxisIntervalType;
            axisX1.MajorGrid.Interval = resetZoomAxisInterval;
            axisX1.MajorGrid.IntervalType = resetZoomAxisIntervalType;
            axisX1.LabelStyle.Interval = resetZoomLabelInterval;
            axisX1.LabelStyle.IntervalType = resetZoomLabelIntervalType;
        }

        public void SetIntervals(TimeSpan delta)
        {
            chart.SuspendLayout();
            Axis axisX1 = chart.ChartAreas["ChartArea1"].AxisX;
            axisX1.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Minutes;
            axisX1.ScaleView.SmallScrollMinSize = 1.0;
            if (delta <= new TimeSpan(0, 5, 0, 0))
            {
                if (delta <= new TimeSpan(0, 0, 30, 0))
                {
                    axisX1.MajorGrid.Interval = 1D;
                    axisX1.MajorGrid.IntervalType = DateTimeIntervalType.Minutes;
                    axisX1.LabelStyle.Interval = 1D;
                    axisX1.LabelStyle.IntervalType = DateTimeIntervalType.Minutes;
                    axisX1.Interval = 1D;
                    axisX1.IntervalType = DateTimeIntervalType.Minutes;
                    resetZoomLabelInterval = 1D;
                    resetZoomLabelIntervalType = DateTimeIntervalType.Minutes;
                    resetZoomAxisInterval = 1D;
                    resetZoomAxisIntervalType = DateTimeIntervalType.Minutes;
                }
                else
                {
                    axisX1.MajorGrid.Interval = 15D;
                    axisX1.MajorGrid.IntervalType = DateTimeIntervalType.Minutes;
                    axisX1.LabelStyle.Interval = 15D;
                    axisX1.LabelStyle.IntervalType = DateTimeIntervalType.Minutes;
                    axisX1.Interval = 15D;
                    axisX1.IntervalType = DateTimeIntervalType.Minutes;
                    resetZoomLabelInterval = 15D;
                    resetZoomLabelIntervalType = DateTimeIntervalType.Minutes;
                    resetZoomAxisInterval = 15D;
                    resetZoomAxisIntervalType = DateTimeIntervalType.Minutes;
                }
            }
            else
            {
                axisX1.MajorGrid.Interval = 1D;
                axisX1.MajorGrid.IntervalType = DateTimeIntervalType.Hours;
                axisX1.LabelStyle.Interval = 1D;
                axisX1.LabelStyle.IntervalType = DateTimeIntervalType.Hours;
                axisX1.Interval = 1D;
                axisX1.IntervalType = DateTimeIntervalType.Hours;
                resetZoomLabelInterval = 1D;
                resetZoomLabelIntervalType = DateTimeIntervalType.Hours;
                resetZoomAxisInterval = 1D;
                resetZoomAxisIntervalType = DateTimeIntervalType.Hours;
            }
            chart.ResumeLayout();
        }

        private void Chart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (e.Axis.AxisName == AxisName.X)
            {
                chart.SuspendLayout();
                Axis axisX1 = chart.ChartAreas["ChartArea1"].AxisX;
                if (axisX1.ScrollBar.IsVisible)
                {
                    axisX1.MinorGrid.Enabled = true;
                    axisX1.LabelStyle.Angle = -90;
                    CalculateLabelInterval(axisX1);
                }
                else
                {
                    ResetZoom(axisX1);
                }
                chart.ResumeLayout();
            }
        }
    }
}
