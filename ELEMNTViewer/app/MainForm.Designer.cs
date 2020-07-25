namespace ELEMNTViewer {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.chartControl = new ELEMNTViewer.ChartControl();
            this.ribbon = new RibbonLib.Ribbon();
            this.SuspendLayout();
            // 
            // chartControl
            // 
            this.chartControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl.Location = new System.Drawing.Point(3, 80);
            this.chartControl.Name = "chartControl";
            this.chartControl.Size = new System.Drawing.Size(1002, 679);
            this.chartControl.TabIndex = 1;
            this.chartControl.TabStop = false;
            // 
            // ribbon
            // 
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Minimized = false;
            this.ribbon.Name = "ribbon";
            this.ribbon.ResourceName = "ELEMNTViewer.RibbonMarkup.ribbon";
            this.ribbon.ShortcutTableResourceName = null;
            this.ribbon.Size = new System.Drawing.Size(1008, 23);
            this.ribbon.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1008, 762);
            this.Controls.Add(this.ribbon);
            this.Controls.Add(this.chartControl);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MinimumSize = new System.Drawing.Size(1024, 800);
            this.Name = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        internal ChartControl chartControl;
        private RibbonLib.Ribbon ribbon;
    }
}

