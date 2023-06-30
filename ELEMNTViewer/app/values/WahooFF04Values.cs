namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class WahooFF04Values
    {
        //Stoppunkte
        private double? _value0;
        private ushort? _value2;
        private byte? _value3;

        public void SetValue(byte fieldNum, int index, object value)
        {
            Type objType = value.GetType();
            switch (fieldNum)
            {
                case 0:
                    _value0 = Convert.ToDouble(value);
                    break;//Double
                case 2:
                    _value2 = Convert.ToUInt16(value);
                    break;//UInt16
                case 3:
                    _value3 = Convert.ToByte(value);
                    break;//Byte
                default:
                    break;
            }
        }

        public double? Value0 { get { return _value0; } }
        public UInt16? Value2
        {
            get
            {
                    return _value2;
            }
        }
        public byte? Value3 { get { return _value3; } }
    }
}
