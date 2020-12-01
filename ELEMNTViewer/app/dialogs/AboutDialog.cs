using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ELEMNTViewer {
    partial class AboutDialog : Form {

        private AboutModelView _modelView;

        public AboutDialog() {
            InitializeComponent();
            if (!DesignMode) {
                this.Font = SystemFonts.MessageBoxFont;
            }
            _modelView = new AboutModelView();
            this.dialogLayout.Paint += DialogLayout_Paint;
            this.titleLabel.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.SizeInPoints + 2);
            this.bottomLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.BottomLayout_Paint);
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            InitControls();
        }

        private void DialogLayout_Paint(object sender, PaintEventArgs e) {
            int y = ((this.licenseLabel.Height) / 2) + this.licenseLabel.Location.Y + 1;
            e.Graphics.DrawLine(new Pen(Color.LightGray, 1), new Point(0, y), new Point(this.licenseLabel.Left, y));
            e.Graphics.DrawLine(new Pen(Color.LightGray, 1), new Point(this.licenseLabel.Right, y), new Point(this.dialogLayout.Right, y));
        }

        private void BottomLayout_Paint(object sender, PaintEventArgs e) {
            e.Graphics.DrawLine(new Pen(Color.Silver, 2), new Point(0, 1), new Point(bottomLayout.Width, 1));
        }

        private void OkButton_Click(object sender, EventArgs e) {
            _modelView.Close(CloseForm);
        }

        private void CloseForm() {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ProjectPage() {
            MessageBox.Show(this, "No project page available", "Project page", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            _modelView.ShowProjectPage(ProjectPage);
        }

        private void InitControls() {
            this.Text = _modelView.DisplayAssemblyTitle;
            this.titleLabel.Text = _modelView.AssemblyProduct;
            this.versionLabel.Text = _modelView.DisplayAssemblyFileVersion;
            this.authorLabel.Text = _modelView.DisplayAuthor;
            this.modifiedLabel.Text = _modelView.DisplayModified;
            this.copyrightLabel.Text = _modelView.AssemblyCopyright;
            //this.licenseTextBox.Text = modelView.AssemblyDescription;
        }
    }
}
