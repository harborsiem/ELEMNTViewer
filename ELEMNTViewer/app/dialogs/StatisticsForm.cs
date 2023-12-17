using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Resources = ELEMNTViewer.Properties.Resources;
using RibbonLib;
using RibbonLib.Controls;

namespace ELEMNTViewer
{
    public partial class StatisticsForm : Form
    {
        private RibbonItems1 _items1;
        private StatisticValues _statisticValues;

        public StatisticsForm()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.Font = SystemFonts.MessageBoxFont;
            }
            this.Text = Resources.RS_Statistics;
			_items1 = new RibbonItems1(statisticRibbon);
            _items1.Init(this);
            _statisticValues = new StatisticValues();
            propertyGrid.SelectedObject = _statisticValues;
            Load += StatisticsForm_Load;
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            _items1.Load();
        }

        public void ShowStatisticValues(int year, int month, ushort antId)
        {
            Summaries summaries = DataManager.Instance.Summaries;
            Summary summary = null;
            if (month == 0)
                if (antId == 0)
                    summary = summaries.GetYearSummaries(year, 0);
                else
                    summary = summaries.GetYearSummaries(year, antId);
            if (month != 0)
                if (antId == 0)
                    summary = summaries.GetMonthSummaries(year, month, 0);
                else
                    summary = summaries.GetMonthSummaries(year, month, antId);
            if (summary != null)
            {
                _statisticValues.TotalAscent = Math.Round(summary.Ascent, 0);
                _statisticValues.TotalDistance = Math.Round(summary.Distance, 2);
                propertyGrid.Refresh();
            }
        }

    }
}
