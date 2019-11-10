using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ELEMNTViewer {
    public partial class CheckControl : UserControl {

        public CheckControl() {
            InitializeComponent();
            this.Size = chartValuesControlLayout.Size;
            this.Load += CheckControl_Load;
            this.chartValuesControlLayout.Resize += new System.EventHandler(this.chartValuesControlLayout_Resize);
            this.v01Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v02Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v03Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v04Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v05Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v06Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v07Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v08Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v09Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v10Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v11Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v12Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v13Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v14Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v15Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            this.v16Check.VisibleChanged += new System.EventHandler(this.Check_VisibleChanged);
            MakeCheckControls();
        }

        private void MakeCheckControls() {
            List<CheckBox> checkBoxList = new List<CheckBox>();
            checkBoxList.Add(v01Check);
            checkBoxList.Add(v02Check);
            checkBoxList.Add(v03Check);
            checkBoxList.Add(v04Check);
            checkBoxList.Add(v05Check);
            checkBoxList.Add(v06Check);
            checkBoxList.Add(v07Check);
            checkBoxList.Add(v08Check);
            checkBoxList.Add(v09Check);
            checkBoxList.Add(v10Check);
            checkBoxList.Add(v11Check);
            checkBoxList.Add(v12Check);
            checkBoxList.Add(v13Check);
            checkBoxList.Add(v14Check);
            checkBoxList.Add(v15Check);
            checkBoxList.Add(v16Check);
            int i = 0;
            IList<string> list = GetRecordNames();
            foreach (string propertyName in list) {
                if (i < checkBoxList.Count) {
                    checkBoxList[i].Text = propertyName;
                    CheckBoxTag tag = new CheckBoxTag(checkBoxList[i], i, "", propertyName);
                    DataManager.Instance.CheckBoxTags.Add(tag);
                    checkBoxList[i].Tag = tag;
                    i++;
                }
            }
            for (; i < checkBoxList.Count; i++) {
                checkBoxList[i].Visible = false;
            }
        }

        private static IList<string> GetRecordNames() {
            List<string> result = new List<string>();
            Type record = typeof(RecordValues);
            PropertyInfo[] infoArray = record.GetProperties();
            foreach (PropertyInfo info in infoArray) {
                BrowsableAttribute browsableAttr = null;
                foreach (Attribute attr in info.GetCustomAttributes(false)) {
                    browsableAttr = attr as BrowsableAttribute;
                    if (browsableAttr != null) {
                        break;
                    }
                }
                //if (info.PropertyType != typeof(DateTime)) {
                if (browsableAttr == null || (browsableAttr != null && browsableAttr.Browsable)) {
                    result.Add(info.Name);
                }
            }
            return result;
        }

        private void CheckControl_Load(object sender, EventArgs e) {
            //this.Size = chartValuesControlLayout.Size;
        }

        private void Check_VisibleChanged(object sender, EventArgs e) {
            this.Size = chartValuesControlLayout.Size; //new Size(this.Width, chartValuesControlLayout.PreferredSize.Height);
        }

        private void chartValuesControlLayout_Resize(object sender, EventArgs e) {
            this.Size = chartValuesControlLayout.Size; //new Size(this.Width, chartValuesControlLayout.PreferredSize.Height);
        }
    }
}
