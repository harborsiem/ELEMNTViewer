using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer
{
    class Smoothing
    {
        private double _sum;
        private int _startIndex;
        private int _firstIndex;
        private int _avgTime; //seconds

        public Smoothing()
        {
        }

        public Smoothing(int avgTime)
        {
            this._avgTime = avgTime;
        }

        public int StartIndex
        {
            get { return _startIndex; }
            set
            {
                _startIndex = value;
                _firstIndex = _startIndex;
            }
        }

        public double SmoothValue { get; private set; }

        public int AvgTime { get { return _avgTime; } set { _avgTime = value; } }

        public void Clear()
        {
            SmoothValue = 0;
            _sum = 0;
            _firstIndex = 0;
            _startIndex = 0;
        }

        //public void SetSmoothValue1(RecordValues values, Delegate propertyFunc, int recordIndex) {
        //    double result = (double)propertyFunc.DynamicInvoke(values);
        //    SmoothValue = result;
        //}

        public void SetSmoothValue(RecordValues values, Func<RecordValues, double> propertyFunc, int recordIndex)
        {
            if (_avgTime == 0)
            {
                SmoothValue = propertyFunc(values);
            }
            else
            {
                _sum += propertyFunc(values);
                if ((recordIndex - _startIndex) >= _avgTime)
                {
                    _sum -= propertyFunc(DataManager.Instance.RecordList[_firstIndex]);
                    _firstIndex++;
                }
                SmoothValue = _sum / (double)(recordIndex + 1 - _firstIndex);
            }
        }
    }
}
