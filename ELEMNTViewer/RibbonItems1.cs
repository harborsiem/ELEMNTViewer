using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using ELEMNTViewer;
using RibbonLib.Interop;

namespace RibbonLib.Controls
{
    partial class RibbonItems1
    {
        private StatisticsForm _form;

        public int SelectedYear { get; private set; }
        public int SelectedMonth { get; private set; }
        public ushort SelectedAntId { get; private set; }

        public void Init(StatisticsForm form)
        {
            _form = form;
            Ribbon.RibbonHeightChanged += Ribbon_RibbonHeightChanged;
            Ribbon.ViewCreated += Ribbon_ViewCreated;
            ComboMonth.RepresentativeString = "12XX";
            ComboYear.RepresentativeString = "2023XX";
            ComboAnt.RepresentativeString = "65535XX";
            ComboAntName.RepresentativeString = "Geschwindigkeit und TrittfrequenzXX";
            ComboAntName.Enabled = false;
            ComboYear.ItemsSourceReady += ComboYear_ItemsSourceReady;
            ComboMonth.ItemsSourceReady += ComboMonth_ItemsSourceReady;
            CheckMonthly.ExecuteEvent += CheckMonthly_ExecuteEvent;
            CheckAntDevice.ExecuteEvent += CheckAntDevice_ExecuteEvent;
            ComboYear.ExecuteEvent += ComboYear_ExecuteEvent;
            ComboMonth.ExecuteEvent += ComboMonth_ExecuteEvent;
            ComboAnt.ExecuteEvent += ComboAnt_ExecuteEvent;
        }

        private void ComboAnt_ExecuteEvent(object sender, Events.ExecuteEventArgs e)
        {
            if (ComboAnt.SelectedItem != Constants.UI_Collection_InvalidIndex)
            {
                ComboAntName.SelectedItem = ComboAnt.SelectedItem;
                if (CheckAntDevice.BooleanValue)
                    SelectedAntId = ushort.Parse(ComboAnt.StringValue);
                if (SelectedYear != 0)
                    _form.ShowStatisticValues(SelectedYear, SelectedMonth, SelectedAntId);
            }
            else
                ComboAntName.SelectedItem = Constants.UI_Collection_InvalidIndex;
        }

        private void ComboYear_ExecuteEvent(object sender, Events.ExecuteEventArgs e)
        {
            if (ComboYear.SelectedItem != Constants.UI_Collection_InvalidIndex)
            {
                SelectedYear = int.Parse(ComboYear.StringValue);
                InitComboAntDevices(SelectedYear);
                _form.ShowStatisticValues(SelectedYear, SelectedMonth, SelectedAntId);
            }
        }

        private void ComboMonth_ExecuteEvent(object sender, Events.ExecuteEventArgs e)
        {
            SelectedMonth = (int)ComboMonth.SelectedItem + 1;
            if (SelectedYear != 0)
                _form.ShowStatisticValues(SelectedYear, SelectedMonth, SelectedAntId);
        }

        private void CheckAntDevice_ExecuteEvent(object sender, Events.ExecuteEventArgs e)
        {
            if (CheckAntDevice.BooleanValue && ComboAnt.SelectedItem != Constants.UI_Collection_InvalidIndex)
                SelectedAntId = ushort.Parse(ComboAnt.StringValue);
            else
                SelectedAntId = 0;
            if (SelectedYear != 0)
                _form.ShowStatisticValues(SelectedYear, SelectedMonth, SelectedAntId);
        }

        private void CheckMonthly_ExecuteEvent(object sender, Events.ExecuteEventArgs e)
        {
            byte[] modes;
            if (CheckMonthly.BooleanValue)
            {
                modes = new byte[] { 0, 1 };
                SelectedMonth = (int)ComboMonth.SelectedItem + 1;
            }
            else
            {
                modes = new byte[] { 0, 2 };
                SelectedMonth = 0;
            }
            Ribbon.SetModes(modes);
            if (SelectedYear != 0)
                _form.ShowStatisticValues(SelectedYear, SelectedMonth, SelectedAntId);
        }

        private void ComboMonth_ItemsSourceReady(object sender, EventArgs e)
        {
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "1", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "2", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "3", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "4", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "5", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "6", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "7", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "8", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "9", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "10", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "11", CategoryID = Constants.UI_Collection_InvalidIndex });
            ComboMonth.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "12", CategoryID = Constants.UI_Collection_InvalidIndex });
        }

        private void ComboYear_ItemsSourceReady(object sender, EventArgs e)
        {
            Summaries summaries = DataManager.Instance.Summaries;
            foreach (int year in summaries.SummaryYears)
            {
                ComboYear.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = year.ToString(), CategoryID = Constants.UI_Collection_InvalidIndex });
            }
            if (summaries.SummaryYears.Count > 0)
            {
                SelectedYear = summaries.SummaryYears[0];
                ComboYear.SelectedItem = 0;
                InitComboAntDevices(SelectedYear);
                _form.ShowStatisticValues(SelectedYear, SelectedMonth, SelectedAntId);
            }
        }

        private void InitComboAntDevices(int year)
        {
            ComboAnt.GalleryItemItemsSource.Clear();
            ComboAntName.GalleryItemItemsSource.Clear();
            SelectedAntId = 0;
            Summaries summaries = DataManager.Instance.Summaries;
            List<AntDevice> antdevices = summaries.GetAntDevices(year);
            foreach (AntDevice device in antdevices)
            {
                ComboAnt.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = device.Id.ToString(), CategoryID = Constants.UI_Collection_InvalidIndex });
                ComboAntName.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = device.ProductName, CategoryID = Constants.UI_Collection_InvalidIndex });
            }
        }

        public void Load()
        {
            byte[] modes;
            modes = new byte[] { 0, 2 };
            Ribbon.SetModes(modes);
        }

        private void Ribbon_ViewCreated(object sender, EventArgs e)
        {
        }

        private void Ribbon_RibbonHeightChanged(object sender, EventArgs e)
        {
            Control control = _form.propertyGrid;
            int height = Ribbon.Height;
            Rectangle bounds = control.Bounds;
            bounds.Height -= (height + control.Margin.Top - bounds.Y);
            bounds.Y = height + control.Margin.Top;
            control.Bounds = bounds;
        }
    }
}
