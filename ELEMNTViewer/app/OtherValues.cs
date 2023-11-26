using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Globalization;

    class OtherValues
    {
        private double _maxNegGrade;
        private double _avgGrade;
        private double _maxPosGrade;
        private double _leftRightBalance;
        private double _leftPS;
        private double _leftTE;
        private double _rightPS;
        private double _rightTE;
        private double _vam;
        private double _vi;

        public OtherValues()
        {
            CalculateSessionMaxValues();
            CalculatePowerExtras();
            AvgLeftRight();
        }

        void CalculatePowerExtras()
        {
            if (DataManager.Instance.Session.TotalTimerTime.TotalHours != 0)
                _vam = DataManager.Instance.Session.TotalAscent / DataManager.Instance.Session.TotalTimerTime.TotalHours;
            else
                _vam = 0.0d;
            if (DataManager.Instance.Session.AvgPower != 0)
                _vi = (double)DataManager.Instance.Session.NormalizedPower / (double)DataManager.Instance.Session.AvgPower;
            else
                _vi = 0.0d;
        }

        void AvgLeftRight()
        {
            double balance = 0;
            double leftPowerSmooth = 0;
            double leftThreshholdEff = 0;
            double rightPowerSmooth = 0;
            double rightThreshholdEff = 0;
            int validCount = 0;

            List<RecordValues> list = DataManager.Instance.RecordList;
            DateTime lastTime;
            if (list.Count > 0)
            {
                lastTime = list[0].Timestamp;
            }
            else
            {
                _leftRightBalance = 0;
                return;
            }
            bool hasValidPowerFlag = DataManager.Instance.RecordManager.HasValidPowerFlag;
            for (int i = 0; i < list.Count; i++)
            {
                RecordValues values = list[i];
                if (!hasValidPowerFlag || hasValidPowerFlag && values.HasValidLeftRightBalance)
                {
                    validCount++;
                    balance += values.LeftRightBalance;
                    leftPowerSmooth += values.LeftPedalSmoothness;
                    leftThreshholdEff += values.LeftTorqueEffectiveness;
                    rightPowerSmooth += values.RightPedalSmoothness;
                    rightThreshholdEff += values.RightTorqueEffectiveness;
                }

                DateTime actTime = values.Timestamp;
                TimeSpan delta = actTime - lastTime;
                long deltaSeconds = delta.Ticks / TimeSpan.TicksPerSecond;
                lastTime = actTime;
                if (deltaSeconds > 1)
                {

                }
            }
            _leftRightBalance = balance / validCount;
            _leftPS = leftPowerSmooth / validCount;
            _leftTE = leftThreshholdEff / validCount;
            _rightPS = rightPowerSmooth / validCount;
            _rightTE = rightThreshholdEff / validCount;
        }

        void CalculateSessionMaxValues()
        {
            double maxPosGrade = 0;
            double maxNegGrade = 0;
            double avgGrade = 0;
            //DateTime firstDateTime;
            //DateTime lastDateTime = DateTime.Now;
            //TimeSpan span = new TimeSpan();
            //bool greater = false;
            List<RecordValues> list = DataManager.Instance.RecordList;
            for (int i = 0; i < list.Count; i++)
            {
                RecordValues values = list[i];
                //if (i == 0)
                //{
                //    lastDateTime = values.Timestamp;
                //    firstDateTime = lastDateTime;
                //}
                //else
                //{
                //    span = values.Timestamp - lastDateTime;
                //    lastDateTime = values.Timestamp;
                //}
                //if (span.Milliseconds > 1000)
                //    greater = true;
                avgGrade += values.Grade;
                if (values.Grade > maxPosGrade)
                {
                    maxPosGrade = values.Grade;
                }
                if (values.Grade < maxNegGrade)
                {
                    maxNegGrade = values.Grade;
                }
            }
            if (list.Count > 0)
            {
                this._avgGrade = avgGrade / list.Count;
            }
            else
            {
                this._avgGrade = 0;
            }
            this._maxNegGrade = maxNegGrade;
            this._maxPosGrade = maxPosGrade;
        }

        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(AvgGrade))]
        public float AvgGrade { get { return (float)Math.Round(_avgGrade, 3); } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(MaxPosGrade))]
        public float MaxPosGrade { get { return (float)_maxPosGrade; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(MaxNegGrade))]
        public float MaxNegGrade { get { return (float)_maxNegGrade; } }
        //[Category("Distance etc")]
        //[DisplayName("Vam")]
        //public float Vam { get { return (float)Math.Round(_vam); } }
        //[Category("Power")]
        //[DisplayName("VI")]
        //public float VI { get { return (float)Math.Round(_vi, 2); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(LRBalance))]
        //public float LRBalance { get { return (float)Math.Round(_leftRightBalance); } }
        public string LRBalance
        {
            get
            {
                int right = (int)Math.Round(_leftRightBalance);
                int left = 100 - right;
                return left.ToString() + " / " + right.ToString();
            }
        }
        [SRCategory("Power")]
        [SRDisplayName(nameof(LeftSmooth))]
        public float LeftSmooth { get { return (float)Math.Round(_leftPS); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(RightSmooth))]
        public float RightSmooth { get { return (float)Math.Round(_rightPS); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(LeftTorque))]
        public float LeftTorque { get { return (float)Math.Round(_leftTE); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(RightTorque))]
        public float RightTorque { get { return (float)Math.Round(_rightTE); } }
    }
}
