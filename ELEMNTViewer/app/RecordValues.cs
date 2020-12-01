using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;

    class RecordValues
    {

        private DateTime timestamp;
        private double positionLat;
        private double positionLong;
        private float altitude;
        private float grade;
        private double distance;
        private byte heartRate;
        private byte cadence;
        private float speed;
        private ushort power;
        private byte leftRightBalance;
        private float leftPedalSmoothness;
        private float rightPedalSmoothness;
        private float leftTorqueEffectiveness;
        private float rightTorqueEffectiveness;
        private sbyte temperature;

        public RecordValues()
        {
        }

        public void SetValue(byte fieldNum, object value)
        {
            //Type objType = value.GetType();
            switch (fieldNum)
            {
                case RecordMesg.FieldDefNum.PositionLat:
                    positionLat = FitConvert.ToDegrees((int)value); //Int32
                    break;
                case RecordMesg.FieldDefNum.PositionLong:
                    positionLong = FitConvert.ToDegrees((int)value); //Int32
                    break;
                case RecordMesg.FieldDefNum.Altitude:
                    altitude = Convert.ToSingle(value); //Single
                    break;
                case RecordMesg.FieldDefNum.HeartRate:
                    heartRate = Convert.ToByte(value); //Byte
                    break;
                case RecordMesg.FieldDefNum.Cadence:
                    cadence = Convert.ToByte(value); //Byte
                    break;
                case RecordMesg.FieldDefNum.Distance:
                    distance = FitConvert.ToKm((float)value); //Single
                    break;
                case RecordMesg.FieldDefNum.Speed:
                    speed = FitConvert.ToKmPerHour((float)value); //Single
                    break;
                case RecordMesg.FieldDefNum.Power:
                    power = Convert.ToUInt16(value); //UInt16
                    break;
                case RecordMesg.FieldDefNum.Grade:
                    grade = Convert.ToSingle(value); //Single
                    break;
                case RecordMesg.FieldDefNum.Temperature:
                    temperature = Convert.ToSByte(value);
                    break;
                case RecordMesg.FieldDefNum.LeftRightBalance:
                    leftRightBalance = Convert.ToByte(value); //Byte
                    break;
                case RecordMesg.FieldDefNum.LeftTorqueEffectiveness:
                    leftTorqueEffectiveness = Convert.ToSingle(value); //Single
                    break;
                case RecordMesg.FieldDefNum.RightTorqueEffectiveness:
                    rightTorqueEffectiveness = Convert.ToSingle(value); //Single
                    break;
                case RecordMesg.FieldDefNum.LeftPedalSmoothness:
                    leftPedalSmoothness = Convert.ToSingle(value); //Single
                    break;
                case RecordMesg.FieldDefNum.RightPedalSmoothness:
                    rightPedalSmoothness = Convert.ToSingle(value); //Single
                    break;
                case RecordMesg.FieldDefNum.Timestamp:
                    timestamp = FitConvert.ToLocalDateTime((uint)value); //UInt32
                    break;

                case RecordMesg.FieldDefNum.GpsAccuracy:
                case RecordMesg.FieldDefNum.EnhancedSpeed:
                case RecordMesg.FieldDefNum.EnhancedAltitude:
                case RecordMesg.FieldDefNum.BatterySoc:
                    break;
                default:
                    break;
            }
        }

        [Browsable(false)]
        public DateTime Timestamp { get { return timestamp; } }
        [Browsable(false)]
        public double PositionLat { get { return positionLat; } }
        [Browsable(false)]
        public double PositionLong { get { return positionLong; } }
        public double Altitude { get { return altitude; } }
        [Int32ArrayAttribute(0, 3, 10, 15, 30)]
        public double Grade { get { return grade; } }
        [Browsable(false)]
        public double Distance { get { return distance; } }
        public double HeartRate { get { return heartRate; } }
        public double Cadence { get { return cadence; } }
        public double Speed { get { return speed; } }
        [Int32ArrayAttribute(0, 3, 10, 15, 30)]
        public double Power { get { return power; } }
        [DisplayName("Left Right Balance")]
        [Int32ArrayAttribute(0, 3, 10, 15, 30)]
        public double LeftRightBalance { get { return leftRightBalance; } }
        [DisplayName("Left Pedal Smoothness")]
        [Int32ArrayAttribute(0, 3, 10, 15, 30)]
        public double LeftPedalSmoothness { get { return leftPedalSmoothness; } }
        [DisplayName("Right Pedal Smoothness")]
        [Int32ArrayAttribute(0, 3, 10, 15, 30)]
        public double RightPedalSmoothness { get { return rightPedalSmoothness; } }
        [DisplayName("Left Torque Effectiveness")]
        [Int32ArrayAttribute(0, 3, 10, 15, 30)]
        public double LeftTorqueEffectiveness { get { return leftTorqueEffectiveness; } }
        [DisplayName("Right Torque Effectiveness")]
        [Int32ArrayAttribute(0, 3, 10, 15, 30)]
        public double RightTorqueEffectiveness { get { return rightTorqueEffectiveness; } }
        public double Temperature { get { return temperature; } }

    }
}
