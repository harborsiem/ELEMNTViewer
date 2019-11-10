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

namespace ELEMNTViewer {
    public partial class MainForm : Form {

        public const string MainFormText = "ELEMNT-Viewer";
        private RibbonItems ribbonItems;

        public MainForm() {
            if (!DesignMode) {
                this.Font = SystemFonts.MessageBoxFont;
            }
            InitializeComponent();
            ribbonItems = new RibbonItems(ribbon);
            ribbonItems.Init(this);

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ELEMNTViewer.Resources.AppIcon.ico");
            this.Icon = new Icon(stream);
            this.Text = MainFormText;
            Load += MainForm_Load;

            //new Test();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int height = ribbon.Height;
            Rectangle b = chartControl.Bounds;
            b.Height -= (height + 3 - b.Y);
            b.Y = height + 3;
            chartControl.Bounds = b;
        }
    }
}
