using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DeviceInfoValues
    {
        private byte? _deviceIndex;
        private byte? _deviceType;
        private ushort? _manufacturer;
        private uint? _serialNumber;
        private ushort? _product;
        private float? _softwareVersion;
        private float? _batteryVoltage;
        private byte? _batteryStatus;
        private string _descriptor;
        private ushort? _antDeviceNumber;
        private SourceType? _sourceType;
        private string _productName;
        private DateTime _timestamp;

        public static void OnDeviceInfoMesg(object sender, MesgEventArgs e)
        {
            DeviceInfoValues values = new DeviceInfoValues();
            DeviceInfoMesg mesg = (DeviceInfoMesg)e.mesg;
            try
            {
                values._deviceIndex = mesg.GetDeviceIndex();
                values._deviceType = mesg.GetDeviceType();
                values._manufacturer = mesg.GetManufacturer();
                values._serialNumber = mesg.GetSerialNumber();
                values._product = mesg.GetProduct();
                values._softwareVersion = mesg.GetSoftwareVersion();
                values._batteryVoltage = mesg.GetBatteryVoltage();
                values._batteryStatus = mesg.GetBatteryStatus();
                values._descriptor = mesg.GetDescriptorAsString();
                values._antDeviceNumber = mesg.GetAntDeviceNumber();
                values._sourceType = mesg.GetSourceType();
                values._productName = mesg.GetProductNameAsString();

                //Make sure properties with sub properties aren't null before trying to create objects based on them
                if (mesg.GetTimestamp() != null)
                {
                    //values.timestamp = new Dynastream.Fit.DateTime(mesg.GetTimestamp().GetTimeStamp());
                    uint tc = (uint)mesg.GetTimestamp().GetTimeStamp();
                    values._timestamp = FitConvert.ToLocalDateTime(tc);
                }
            }
            catch (FitException exception)
            {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.DeviceInfoValues.Add(values);
        }

        public byte? DeviceIndex { get { return _deviceIndex; } }

        public byte? DeviceTypeRaw { get { return _deviceType; } }

        public string DeviceType
        {
            get
            {
                if (_deviceType != null)
                {
                    return FitConvert.GetConstName(typeof(AntplusDeviceType), (byte)_deviceType);
                }
                return null;
            }
        }
        public ushort? ManufacturerRaw { get { return _manufacturer; } }

        public string Manufacturer
        {
            get
            {
                if (_manufacturer != null)
                {
                    return FitConvert.GetConstName(typeof(Manufacturer), (ushort)_manufacturer);
                }
                return null;
            }
        }

        public uint? SerialNumber { get { return _serialNumber; } }
        public ushort? Product { get { return _product; } }
        public float? SoftwareVersion { get { return _softwareVersion; } }
        public float? BatteryVoltage { get { return _batteryVoltage; } }
        public byte? BatteryStatusRaw { get { return _batteryStatus; } }
        public string BatteryStatus
        {
            get
            {
                if (_batteryStatus != null)
                {
                    return FitConvert.GetConstName(typeof(BatteryStatus), (byte)_batteryStatus);
                }
                return null;
            }
        }
        public string Descriptor { get { return _descriptor; } }
        public ushort? AntDeviceNumber { get { return _antDeviceNumber; } }
        public SourceType? SourceType { get { return _sourceType; } }
        public string ProductName { get { return _productName; } }
        public DateTime Timestamp { get { return _timestamp; } }
    }
}
