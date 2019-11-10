namespace ELEMNTViewer {
    partial class AboutDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.aboutPicture = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.modifiedLabel = new System.Windows.Forms.Label();
            this.topLayout = new System.Windows.Forms.TableLayoutPanel();
            this.licenseLabel = new System.Windows.Forms.Label();
            this.licenseTextBox = new System.Windows.Forms.TextBox();
            this.bottomLayout = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.okButton = new System.Windows.Forms.Button();
            this.dialogLayout = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.aboutPicture)).BeginInit();
            this.topLayout.SuspendLayout();
            this.bottomLayout.SuspendLayout();
            this.dialogLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // aboutPicture
            // 
            this.aboutPicture.Image = global::ELEMNTViewer.Properties.Resources.ElemntBolt;
            this.aboutPicture.Location = new System.Drawing.Point(3, 3);
            this.aboutPicture.Name = "aboutPicture";
            this.topLayout.SetRowSpan(this.aboutPicture, 5);
            this.aboutPicture.Size = new System.Drawing.Size(96, 146);
            this.aboutPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.aboutPicture.TabIndex = 10;
            this.aboutPicture.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(105, 3);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(3);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(35, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "{Title}";
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(105, 22);
            this.versionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(50, 13);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "{Version}";
            // 
            // authorLabel
            // 
            this.authorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(105, 41);
            this.authorLabel.Margin = new System.Windows.Forms.Padding(3);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(46, 13);
            this.authorLabel.TabIndex = 2;
            this.authorLabel.Text = "{Author}";
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.copyrightLabel.AutoSize = true;
            this.copyrightLabel.Location = new System.Drawing.Point(105, 60);
            this.copyrightLabel.Margin = new System.Windows.Forms.Padding(3);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(59, 13);
            this.copyrightLabel.TabIndex = 3;
            this.copyrightLabel.Text = "{Copyright}";
            // 
            // modifiedLabel
            // 
            this.modifiedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.modifiedLabel.AutoSize = true;
            this.modifiedLabel.Location = new System.Drawing.Point(105, 79);
            this.modifiedLabel.Margin = new System.Windows.Forms.Padding(3);
            this.modifiedLabel.Name = "modifiedLabel";
            this.modifiedLabel.Size = new System.Drawing.Size(55, 70);
            this.modifiedLabel.TabIndex = 4;
            this.modifiedLabel.Text = "{Modified}";
            this.modifiedLabel.Visible = false;
            // 
            // topLayout
            // 
            this.topLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topLayout.AutoSize = true;
            this.topLayout.ColumnCount = 2;
            this.topLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topLayout.Controls.Add(this.aboutPicture, 0, 0);
            this.topLayout.Controls.Add(this.titleLabel, 1, 0);
            this.topLayout.Controls.Add(this.versionLabel, 1, 1);
            this.topLayout.Controls.Add(this.authorLabel, 1, 2);
            this.topLayout.Controls.Add(this.copyrightLabel, 1, 3);
            this.topLayout.Controls.Add(this.modifiedLabel, 1, 4);
            this.topLayout.Location = new System.Drawing.Point(12, 12);
            this.topLayout.Margin = new System.Windows.Forms.Padding(12);
            this.topLayout.Name = "topLayout";
            this.topLayout.RowCount = 5;
            this.topLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topLayout.Size = new System.Drawing.Size(460, 152);
            this.topLayout.TabIndex = 1;
            // 
            // licenseLabel
            // 
            this.licenseLabel.AutoSize = true;
            this.licenseLabel.ForeColor = System.Drawing.Color.Chocolate;
            this.licenseLabel.Location = new System.Drawing.Point(184, 179);
            this.licenseLabel.Margin = new System.Windows.Forms.Padding(184, 3, 0, 3);
            this.licenseLabel.Name = "licenseLabel";
            this.licenseLabel.Size = new System.Drawing.Size(116, 13);
            this.licenseLabel.TabIndex = 0;
            this.licenseLabel.Text = "The MIT License (MIT)";
            // 
            // licenseTextBox
            // 
            this.licenseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.licenseTextBox.Location = new System.Drawing.Point(12, 207);
            this.licenseTextBox.Margin = new System.Windows.Forms.Padding(12);
            this.licenseTextBox.Multiline = true;
            this.licenseTextBox.Name = "licenseTextBox";
            this.licenseTextBox.ReadOnly = true;
            this.licenseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.licenseTextBox.Size = new System.Drawing.Size(460, 135);
            this.licenseTextBox.TabIndex = 3;
            this.licenseTextBox.Text = resources.GetString("licenseTextBox.Text");
            // 
            // bottomLayout
            // 
            this.bottomLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomLayout.AutoSize = true;
            this.bottomLayout.BackColor = System.Drawing.Color.Gainsboro;
            this.bottomLayout.ColumnCount = 3;
            this.bottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.bottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.bottomLayout.Controls.Add(this.linkLabel, 0, 0);
            this.bottomLayout.Controls.Add(this.okButton, 2, 0);
            this.bottomLayout.Location = new System.Drawing.Point(0, 357);
            this.bottomLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.bottomLayout.Name = "bottomLayout";
            this.bottomLayout.RowCount = 1;
            this.bottomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.bottomLayout.Size = new System.Drawing.Size(484, 50);
            this.bottomLayout.TabIndex = 0;
            // 
            // linkLabel
            // 
            this.linkLabel.ActiveLinkColor = System.Drawing.SystemColors.Highlight;
            this.linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel.AutoSize = true;
            this.linkLabel.Image = global::ELEMNTViewer.Properties.Resources.Web_Image;
            this.linkLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel.LinkArea = new System.Windows.Forms.LinkArea(5, 9);
            this.linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabel.Location = new System.Drawing.Point(12, 21);
            this.linkLabel.Margin = new System.Windows.Forms.Padding(12, 12, 3, 12);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(78, 17);
            this.linkLabel.TabIndex = 1;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "     Home page";
            this.linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel.UseCompatibleTextRendering = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(397, 12);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 12, 12, 12);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 26);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // dialogLayout
            // 
            this.dialogLayout.AutoSize = true;
            this.dialogLayout.BackColor = System.Drawing.Color.White;
            this.dialogLayout.ColumnCount = 1;
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dialogLayout.Controls.Add(this.topLayout, 0, 0);
            this.dialogLayout.Controls.Add(this.licenseLabel, 0, 1);
            this.dialogLayout.Controls.Add(this.licenseTextBox, 0, 2);
            this.dialogLayout.Controls.Add(this.bottomLayout, 0, 3);
            this.dialogLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dialogLayout.Location = new System.Drawing.Point(0, 0);
            this.dialogLayout.Name = "dialogLayout";
            this.dialogLayout.RowCount = 4;
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.Size = new System.Drawing.Size(484, 407);
            this.dialogLayout.TabIndex = 0;
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 407);
            this.Controls.Add(this.dialogLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About {AppName}";
            ((System.ComponentModel.ISupportInitialize)(this.aboutPicture)).EndInit();
            this.topLayout.ResumeLayout(false);
            this.topLayout.PerformLayout();
            this.bottomLayout.ResumeLayout(false);
            this.bottomLayout.PerformLayout();
            this.dialogLayout.ResumeLayout(false);
            this.dialogLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel bottomLayout;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox licenseTextBox;
        private System.Windows.Forms.Label licenseLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label modifiedLabel;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.PictureBox aboutPicture;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.TableLayoutPanel topLayout;
        private System.Windows.Forms.TableLayoutPanel dialogLayout;
    }
}
