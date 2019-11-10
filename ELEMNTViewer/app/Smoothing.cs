using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer {
    class Smoothing {

        private double sum;
        private int startIndex;
        private int firstIndex;
        private int avgTime; //seconds

        public Smoothing() {
        }

        public Smoothing(int avgTime) {
            this.avgTime = avgTime;
        }

        public int StartIndex {
            get { return startIndex; }
            set {
                startIndex = value;
                firstIndex = startIndex;
            }
        }

        public double SmoothValue { get; private set; }

        public int AvgTime { get { return avgTime; } set { avgTime = value; } }

        public void Clear() {
            SmoothValue = 0;
            sum = 0;
            firstIndex = 0;
            startIndex = 0;
        }

        //public void SetSmoothValue1(RecordValues values, Delegate propertyFunc, int recordIndex) {
        //    double result = (double)propertyFunc.DynamicInvoke(values);
        //    SmoothValue = result;
        //}

        public void SetSmoothValue(RecordValues values, Func<RecordValues, double> propertyFunc, int recordIndex) {
            if (avgTime == 0) {
                SmoothValue = propertyFunc(values);
            } else {
                sum += propertyFunc(values);
                if ((recordIndex - startIndex) >= avgTime) {
                    sum -= propertyFunc(DataManager.Instance.RecordList[firstIndex]);
                    firstIndex++;
                }
                SmoothValue = sum / (double)(recordIndex + 1 - firstIndex);
            }
        }
    }
}
