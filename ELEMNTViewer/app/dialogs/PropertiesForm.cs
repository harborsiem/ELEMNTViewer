using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ELEMNTViewer {
    public partial class PropertiesForm : Form {

        private object[] selectedObjects;
        private ToolTip numericToolTip;

        public PropertyGrid Grid { get { return propertyGrid; } }

        public PropertySort PropertySort { get { return propertyGrid.PropertySort; } set { propertyGrid.PropertySort = value; } }

        public object SelectedObject {
            get { return propertyGrid.SelectedObject; }
            set {
                propertyGrid.SelectedObject = value;
            }
        }
        public object[] SelectedObjects {
            get { return selectedObjects; }
            set {
                dialogLayout.SuspendLayout();
                selectedObjects = value;
                int min = 1;
                int max = 1;
                if (value != null) {
                    max = selectedObjects.Length;
                    if (min == max) {
                        numberLabel.Visible = false;
                        numberUpDown.Visible = false;
                    } else {
                        numberLabel.Visible = true;
                        numberUpDown.Visible = true;
                    }
                    numberUpDown.Enabled = true;
                    numberUpDown.Value = 1;
                    NumberUpDown_ValueChanged(numberUpDown, EventArgs.Empty);
                } else {
                    numberLabel.Visible = false;
                    numberUpDown.Visible = false;
                    numberUpDown.Enabled = false;
                }
                numberUpDown.Minimum = min;
                numberUpDown.Maximum = max;
                dialogLayout.ResumeLayout();
            }
        }

        public string Header { get { return this.Text; } set { this.Text = value; } }

        public PropertiesForm() {
            InitializeComponent();
            if (!DesignMode) {
                this.Font = SystemFonts.MessageBoxFont;
            }
            numericToolTip = new ToolTip();
            numericToolTip.SetToolTip(numberUpDown, "Number");
            numberUpDown.ValueChanged += NumberUpDown_ValueChanged;
            this.Shown += Dialog_Shown;
        }

        private void NumberUpDown_ValueChanged(object sender, EventArgs e) {
            if (selectedObjects != null && selectedObjects.Length > 0) {
                propertyGrid.SelectedObject = selectedObjects[(int)numberUpDown.Value - 1];
            }
        }

        private void Dialog_Shown(object sender, EventArgs e) //Tricky event to set ScrollBar to Top
        {
            Control pgv = FindControl(propertyGrid.Controls, "PropertyGridView");
            if (pgv != null)
            {
                ScrollBar sb = FindControl(pgv.Controls, typeof(VScrollBar)) as ScrollBar;
                if (sb != null)
                    sb.Value = 0;
                //// Reflection trickery to get a private/internal field
                //// and method, scrollBar in this case
                //Type type = pgv.GetType();
                //FieldInfo f = FindField(type, "scrollBar");
                //((ScrollBar)f.GetValue(pgv)).Value = 0;
            }
        }

        private static Control FindControl(
            Control.ControlCollection controls, Type name)
        {
            foreach (Control c in controls)
            {
                Type t = c.GetType();
                if (t == name)
                    return c;
            }
            return null;
        }

        private static Control FindControl(
            Control.ControlCollection controls, string name)
        {
            foreach (Control c in controls)
            {
                if (c.Text == name)
                    return c;
            }
            return null;
        }

        //private static FieldInfo FindField(Type type, string field)
        //{
        //    FieldInfo f = type.GetField(field,
        //       BindingFlags.Instance | BindingFlags.NonPublic);

        //    return f;
        //}

    }
}
