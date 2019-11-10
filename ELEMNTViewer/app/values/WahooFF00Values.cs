namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class WahooFF00Values {
        //Stoppunkte
        private DateTime timestamp;
        private double? positionLat;
        private double? positionLong;
        private ushort? value2;
        private byte? value3;
        private uint? value5;
        private ushort? value6;
        private short? value9;
        private sbyte? value13;
        private byte? value31;

        public void SetValue(byte fieldNum, int index, object value) {
            Type objType = value.GetType();
            switch (fieldNum) {
                case 0:
                    positionLat = FitConvert.ToDegrees(Convert.ToInt32(value));
                    break;//Int32 positionLat
                case 1:
                    positionLong = FitConvert.ToDegrees(Convert.ToInt32(value));
                    break;//Int32 positionLong
                case 2:
                    value2 = Convert.ToUInt16(value);
                    break;//UInt16, Altitude? welche Umrechnung?
                case 3:
                    value3 = Convert.ToByte(value);
                    break;//Byte, Heartrate?
                case 5:
                    value5 = Convert.ToUInt32(value);
                    break;//UInt32, Distance?
                case 6:
                    value6 = Convert.ToUInt16(value);
                    break;//UInt16, Speed?
                case 9:
                    value9 = Convert.ToInt16(value);
                    break;//Int16, Grade?
                case 13:
                    value13 = Convert.ToSByte(value);
                    break;//SByte, Temperature?
                case 31:
                    value31 = Convert.ToByte(value);
                    break;//Byte, GpsAccuracy?
                case 253:
                    timestamp = FitConvert.ToLocalDateTime((uint)value); //UInt32
                    break;//UInt32
                default:
                    break;
            }
        }

        public DateTime Timestamp { get { return timestamp; } }
        public double? PositionLat { get { return positionLat; } }
        public double? PositionLong { get { return positionLong; } }
        public ushort? Value2 {
            get {
                if (value2 != null) {
                    return value2;
                }
                return null;
            }
        }
        public byte? HeartRate { get { return value3; } }
        public double? Distance {
            get {
                if (value5 != null) {
                    return FitConvert.ToKm((uint)value5);
                }
                return null;
            }
        }
        public float? Speed {
            get {
                if (value6 != null) {
                    return FitConvert.ToKmPerHour((ushort)value6);
                }
                return null;
            }
        }
        public float? Value9 {
            get {
                if (value9 != null) {
                    return value9 / 100f;
                }
                return null;
            }
        }
        public sbyte? Temperature { get { return value13; } }
        public byte? GpsAccuracy { get { return value31; } }
    }
}
