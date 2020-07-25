using Dynastream.Fit;

namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;

    class OtherValues {

        private double _maxNegGrade;
        private double _avgGrade;
        private double _maxPosGrade;
        private double _leftRightBalance;

        public OtherValues() {
            CalculateSessionMaxValues();
            AvgLeftRightBalance();
        }

        void AvgLeftRightBalance() {
            double value = 0;
            List<RecordValues> list = DataManager.Instance.RecordList;
            DateTime lastTime;
            if (list.Count > 0) {
                lastTime = list[0].Timestamp;
            } else {
                _leftRightBalance = 0;
                return;
            }
            for (int i = 0; i < list.Count; i++) {
                RecordValues values = list[i];
                value += values.LeftRightBalance;
                DateTime actTime = values.Timestamp;
                TimeSpan delta = actTime - lastTime;
                long deltaSeconds = delta.Ticks / TimeSpan.TicksPerSecond;
                lastTime = actTime;
                if (deltaSeconds > 1) {

                }
            }
            _leftRightBalance = value / list.Count;
        }

        void CalculateSessionMaxValues() {
            double maxPosGrade = 0;
            double maxNegGrade = 0;
            double avgGrade = 0;
            List<RecordValues> list = DataManager.Instance.RecordList;
            for (int i = 0; i < list.Count; i++) {
                RecordValues values = list[i];
                avgGrade += values.Grade;
                if (values.Grade > maxPosGrade) {
                    maxPosGrade = values.Grade;
                }
                if (values.Grade < maxNegGrade) {
                    maxNegGrade = values.Grade;
                }
            }
            if (list.Count > 0) {
                this._avgGrade = avgGrade / list.Count;
            } else {
                this._avgGrade = 0;
            }
            this._maxNegGrade = maxNegGrade;
            this._maxPosGrade = maxPosGrade;
        }

        [Category("Distance etc")]
        [DisplayName("Maximum Negative Grade")]
        public float MaxNegGrade { get { return (float)_maxNegGrade; } }
        [Category("Distance etc")]
        [DisplayName("Average Grade")]
        public float AvgGrade { get { return (float)_avgGrade; } }
        [Category("Distance etc")]
        [DisplayName("Maximum Positive Grade")]
        public float MaxPosGrade { get { return (float)_maxPosGrade; } }
        [Category("Power")]
        [DisplayName("Left Right Balance")]
        public float LeftRightBalance { get { return (float)_leftRightBalance; } }
    }
}
