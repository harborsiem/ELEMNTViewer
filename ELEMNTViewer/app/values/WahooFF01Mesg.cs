using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class WahooFF01Mesg : Mesg
    {
        #region Fields
        #endregion

        /// <summary>
        /// Field Numbers for <see cref="SportMesg"/>
        /// </summary>
        public sealed class FieldDefNum
        {
            public const byte DeviceName = 0;
            public const byte Tour = 1;
            public const byte SubTour = 2;
            public const byte Invalid = Fit.FieldNumInvalid;
        }

        #region Constructors
        public WahooFF01Mesg() : base(Profile.GetMesg(MesgNum.Sport))
        {
        }

        public WahooFF01Mesg(Mesg mesg) : base(mesg)
        {
        }
        #endregion // Constructors

        #region Methods

        ///<summary>
        /// Retrieves the Name field</summary>
        /// <returns>Returns byte[] representing the Name field</returns>
        public byte[] GetDeviceName()
        {
            byte[] data = (byte[])GetFieldValue(0, 0, Fit.SubfieldIndexMainField);
            return data.Take(data.Length - 1).ToArray();
        }

        ///<summary>
        /// Retrieves the Name field</summary>
        /// <returns>Returns String representing the Name field</returns>
        public String GetDeviceNameAsString()
        {
            byte[] data = (byte[])GetFieldValue(0, 0, Fit.SubfieldIndexMainField);
            return data != null ? Encoding.UTF8.GetString(data, 0, data.Length - 1) : null;
        }

        ///<summary>
        /// Set Name field</summary>
        /// <param name="deviceName"> field value to be set</param>
        public void SetDeviceName(String deviceName)
        {
            byte[] data = Encoding.UTF8.GetBytes(deviceName);
            byte[] zdata = new byte[data.Length + 1];
            data.CopyTo(zdata, 0);
            SetFieldValue(0, 0, zdata, Fit.SubfieldIndexMainField);
        }


        /// <summary>
        /// Set Name field</summary>
        /// <param name="deviceName">field value to be set</param>
        public void SetDeviceName(byte[] deviceName)
        {
            SetFieldValue(0, 0, deviceName, Fit.SubfieldIndexMainField);
        }

        public uint? GetTour()
        {
            Object val = GetFieldValue(1, 0, Fit.SubfieldIndexMainField);
            if (val == null)
            {
                return null;
            }

            return (Convert.ToUInt32(val));
        }

        /// <summary>
        /// Set Sport field</summary>
        /// <param name="tour">Nullable field value to be set</param>
        public void SetTour(uint? tour)
        {
            SetFieldValue(1, 0, tour, Fit.SubfieldIndexMainField);
        }

        public ushort? GetSubTour()
        {
            Object val = GetFieldValue(2, 0, Fit.SubfieldIndexMainField);
            if (val == null)
            {
                return null;
            }

            return (Convert.ToUInt16(val));
        }

        /// <summary>
        /// Set Sport field</summary>
        /// <param name="tour">Nullable field value to be set</param>
        public void SetSubTour(ushort? subtour)
        {
            SetFieldValue(2, 0, subtour, Fit.SubfieldIndexMainField);
        }

        #endregion // Methods
    }
}
