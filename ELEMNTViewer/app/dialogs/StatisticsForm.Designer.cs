namespace ELEMNTViewer
{
    partial class StatisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statisticRibbon = new RibbonLib.Ribbon();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // statisticRibbon
            // 
            this.statisticRibbon.Location = new System.Drawing.Point(0, 0);
            this.statisticRibbon.Name = "statisticRibbon";
            this.statisticRibbon.ResourceIdentifier = null;
            this.statisticRibbon.ResourceName = "ELEMNTViewer.RibbonMarkup1.ribbon";
            this.statisticRibbon.ShortcutTableResourceName = null;
            this.statisticRibbon.Size = new System.Drawing.Size(440, 58);
            this.statisticRibbon.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(0, 64);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(440, 213);
            this.propertyGrid.TabIndex = 1;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 302);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.statisticRibbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatisticsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private RibbonLib.Ribbon statisticRibbon;
        internal System.Windows.Forms.PropertyGrid propertyGrid;
    }
}