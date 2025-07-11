using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using WinForms.Ribbon;

namespace ELEMNTViewer
{
    public partial class MainForm : Form
    {
        public const string MainFormText = "ELEMNTViewer";
        private RibbonItems _ribbonItems;
        internal ChartHelp _chartHelp;

        public MainForm()
        {
            InitializeComponent();
            if (!DesignMode)
                this.Font = SystemFonts.MessageBoxFont;
            Settings settings = Settings.Instance;
            if (settings.AppWidth > 0 && settings.AppHeight > 0)
            {
                this.Size = new Size(settings.AppWidth, settings.AppHeight);
            }
            ribbon.RibbonEventException += Ribbon_RibbonEventException;
            _ribbonItems = new RibbonItems(ribbon);
            _ribbonItems.Init(this);
            //_ribbonItems.GetSettings();

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ELEMNTViewer.Resources.AppIcon.ico");
            this.Icon = new Icon(stream);
            this.Text = MainFormText;
            Load += MainForm_Load;
            FormClosed += MainForm_FormClosed;
            ribbon.RibbonHeightChanged += Ribbon_RibbonHeightChanged;

            chart.AxisViewChanging += Chart_AxisViewChanging;
            ChartArea area = chart.ChartAreas["ChartArea1"];
            area.AxisX.TitleFont = this.Font;
            area.AxisY.TitleFont = this.Font;
            chart.Legends[0].Font = this.Font;
            area.AxisX.LabelStyle.Font = this.Font;
            area.AxisY.LabelStyle.Font = this.Font;

            //new Test();
        }

        private void Chart_AxisViewChanging(object sender, ViewEventArgs e)
        {
            if (e.NewSizeType == DateTimeIntervalType.Number && e.NewSize != double.NaN)
            {
                AxisScaleView view = e.ChartArea.AxisX.ScaleView;
                view.SmallScrollSizeType = DateTimeIntervalType.Number;
                view.SmallScrollSize = e.NewSize;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Instance.Write();
            Application.Exit();
        }

        private void Ribbon_RibbonHeightChanged(object sender, EventArgs e)
        {
            Control control = chart;
            int height = ribbon.Height;
            Rectangle bounds = control.Bounds;
            bounds.Height -= (height + control.Margin.Top - bounds.Y);
            bounds.Y = height + control.Margin.Top;
            control.Bounds = bounds;
        }

        private void Ribbon_RibbonEventException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (MessageBox.Show("Unhandled Exception " + e.Exception.StackTrace, "ELEMNTViewer shall Close", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                Environment.Exit(1);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _chartHelp = new ChartHelp(chart);
            _ribbonItems.Load();
        }
    }
}
