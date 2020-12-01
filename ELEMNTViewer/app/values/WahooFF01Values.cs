namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;

    class WahooFF01Values
    {
        private string _deviceName;
        private uint _tour;
        private ushort _subTour;

        public void SetValue(byte fieldNum, int index, object value)
        {
            Type objType = value.GetType();
            switch (fieldNum)
            {
                case 0:
                    byte[] chArray = (byte[])value;
                    _deviceName = Encoding.UTF8.GetString(chArray, 0, chArray.Length - 1);
                    break; //Byte[]
                case 1:
                    _tour = Convert.ToUInt32(value);
                    break; //UInt32
                case 2:
                    _subTour = Convert.ToUInt16(value);
                    break; //UInt16
                default:
                    break;
            }
        }

        [Category("Tour")]
        public String DeviceName { get { return _deviceName; } }
        [Category("Tour")]
        public uint Tour { get { return _tour; } }
        [Category("Tour")]
        //[DisplayName("Total Elapsed Time")]
        public ushort SubTour { get { return _subTour; } }
    }
}
