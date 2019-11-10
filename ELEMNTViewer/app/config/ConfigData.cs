using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ELEMNTViewer {
    internal class ConfigData {
        //public string Key { get; set; }
        public string Text { get; set; }
        public string ToolTipText { get; set; }
        public bool Visible { get; set; }
        public bool Checked { get; set; }
        public Color Color { get; set; }
        public int Smooth { get; set; }

        public void Copy(ConfigData data) {
            data.Text = this.Text;
            data.ToolTipText = this.ToolTipText;
            data.Visible = this.Visible;
            data.Checked = this.Checked;
            data.Color = this.Color;
            data.Smooth = this.Smooth;
        }

        public ConfigData Clone() {
            ConfigData data = new ConfigData();
            data.Text = this.Text;
            data.ToolTipText = this.ToolTipText;
            data.Visible = this.Visible;
            data.Checked = this.Checked;
            data.Color = this.Color;
            data.Smooth = this.Smooth;
            return data;
        }
    }
}
