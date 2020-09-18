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
using RibbonLib;
using RibbonLib.Controls;

namespace ELEMNTViewer
{
    public partial class MainForm : Form
    {

        public const string MainFormText = "ELEMNTViewer";
        private RibbonItems _ribbonItems;

        public MainForm()
        {
            InitializeComponent();
            if (!DesignMode)
                this.Font = SystemFonts.MessageBoxFont;
            Settings.Instance.Read();
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

            //new Test();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Instance.Write();
            Application.Exit();
        }

        private void Ribbon_RibbonHeightChanged(object sender, EventArgs e)
        {
            int height = ribbon.Height;
            Rectangle bounds = chartControl.Bounds;
            bounds.Height -= (height + chartControl.Margin.Top - bounds.Y);
            bounds.Y = height + chartControl.Margin.Top;
            chartControl.Bounds = bounds;
        }

        private void Ribbon_RibbonEventException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (MessageBox.Show("Unhandled Exception " + e.Exception.StackTrace, "ELEMNTViewer shall Close", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                Environment.Exit(1);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //int height = ribbon.Height;
            //Rectangle b = chartControl.Bounds;
            //b.Height -= (height + 3 - b.Y);
            //b.Y = height + 3;
            //chartControl.Bounds = b;
            _ribbonItems.Load();
        }
    }
}
