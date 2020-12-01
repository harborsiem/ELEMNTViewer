namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class WahooFF00Values
    {
        //Stoppunkte
        private DateTime _timestamp;
        private double? _positionLat;
        private double? _positionLong;
        private ushort? _value2;
        private byte? _value3;
        private uint? _value5;
        private ushort? _value6;
        private short? _value9;
        private sbyte? _value13;
        private byte? _value31;

        public void SetValue(byte fieldNum, int index, object value)
        {
            Type objType = value.GetType();
            switch (fieldNum)
            {
                case 0:
                    _positionLat = FitConvert.ToDegrees(Convert.ToInt32(value));
                    break;//Int32 positionLat
                case 1:
                    _positionLong = FitConvert.ToDegrees(Convert.ToInt32(value));
                    break;//Int32 positionLong
                case 2:
                    _value2 = Convert.ToUInt16(value);
                    break;//UInt16, Altitude
                case 3:
                    _value3 = Convert.ToByte(value);
                    break;//Byte, Heartrate?
                case 5:
                    _value5 = Convert.ToUInt32(value);
                    break;//UInt32, Distance?
                case 6:
                    _value6 = Convert.ToUInt16(value);
                    break;//UInt16, Speed?
                case 9:
                    _value9 = Convert.ToInt16(value);
                    break;//Int16, Grade?
                case 13:
                    _value13 = Convert.ToSByte(value);
                    break;//SByte, Temperature?
                case 31:
                    _value31 = Convert.ToByte(value);
                    break;//Byte, GpsAccuracy?
                case 253:
                    _timestamp = FitConvert.ToLocalDateTime((uint)value); //UInt32
                    break;//UInt32
                default:
                    break;
            }
        }

        public DateTime Timestamp { get { return _timestamp; } }
        public double? PositionLat { get { return _positionLat; } }
        public double? PositionLong { get { return _positionLong; } }
        public float? Altitude
        {
            get
            {
                if (_value2 != null)
                {
                    return ((float)_value2 / 5) - 500;
                }
                return null;
            }
        }
        public byte? HeartRate { get { return _value3; } }
        public double? Distance
        {
            get
            {
                if (_value5 != null)
                {
                    return FitConvert.ToKm((uint)_value5);
                }
                return null;
            }
        }
        public float? Speed
        {
            get
            {
                if (_value6 != null)
                {
                    return FitConvert.ToKmPerHour((ushort)_value6);
                }
                return null;
            }
        }
        public float? Grade
        {
            get
            {
                if (_value9 != null)
                {
                    return _value9 / 100f;
                }
                return null;
            }
        }
        public sbyte? Temperature { get { return _value13; } }
        public byte? GpsAccuracy { get { return _value31; } }
    }
}
