using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer {
    class Int32ArrayAttribute : Attribute {
        public int[] Values { get; private set; }
        public object[] ObjectValues {
            get {
                object[] result = new object[Values.Length];
                Array.Copy(Values, result, Values.Length);
                return result;
            }
        }

        public Int32ArrayAttribute(params int[] values) {
            this.Values = values;
        }
    }
}
