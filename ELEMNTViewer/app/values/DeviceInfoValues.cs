using Dynastream.Fit;

namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DeviceInfoValues {
        private byte? deviceIndex;
        private byte? deviceType;
        private ushort? manufacturer;
        private uint? serialNumber;
        private ushort? product;
        private float? softwareVersion;
        private float? batteryVoltage;
        private byte? batteryStatus;
        private string descriptor;
        private ushort? antDeviceNumber;
        private SourceType? sourceType;
        private string productName;
        private DateTime timestamp;

        public static void OnDeviceInfoMesg(object sender, MesgEventArgs e) {
            DeviceInfoValues values = new DeviceInfoValues();
            DeviceInfoMesg mesg = (DeviceInfoMesg)e.mesg;
            try {
                values.deviceIndex = mesg.GetDeviceIndex();
                values.deviceType = mesg.GetDeviceType();
                values.manufacturer = mesg.GetManufacturer();
                values.serialNumber = mesg.GetSerialNumber();
                values.product = mesg.GetProduct();
                values.softwareVersion = mesg.GetSoftwareVersion();
                values.batteryVoltage = mesg.GetBatteryVoltage();
                values.batteryStatus = mesg.GetBatteryStatus();
                values.descriptor = mesg.GetDescriptorAsString();
                values.antDeviceNumber = mesg.GetAntDeviceNumber();
                values.sourceType = mesg.GetSourceType();
                values.productName = mesg.GetProductNameAsString();

                //Make sure properties with sub properties arent null before trying to create objects based on them
                if (mesg.GetTimestamp() != null) {
                    //values.timestamp = new Dynastream.Fit.DateTime(mesg.GetTimestamp().GetTimeStamp());
                    uint tc = (uint)mesg.GetTimestamp().GetTimeStamp();
                    values.timestamp = FitConvert.ToLocalDateTime(tc);
                }
            }
            catch (FitException exception) {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.DeviceInfoValues.Add(values);
        }

        public byte? DeviceIndex { get { return deviceIndex; } }
        public byte? DeviceTypeRaw { get { return deviceType; } }
        public string DeviceType {
            get {
                if (deviceType != null) {
                    return FitConvert.GetConstName(typeof(AntplusDeviceType), (byte)deviceType);
                }
                return null;
            }
        }
        public ushort? ManufacturerRaw { get { return manufacturer; } }
        public string Manufacturer {
            get {
                if (manufacturer != null) {
                    return FitConvert.GetConstName(typeof(Manufacturer), (ushort)manufacturer);
                }
                return null;
            }
        }
        public uint? SerialNumber { get { return serialNumber; } }
        public ushort? Product { get { return product; } }
        public float? SoftwareVersion { get { return softwareVersion; } }
        public float? BatteryVoltage { get { return batteryVoltage; } }
        public byte? BatteryStatusRaw { get { return batteryStatus; } }
        public string BatteryStatus {
            get {
                if (batteryStatus != null) {
                    return FitConvert.GetConstName(typeof(BatteryStatus), (byte)batteryStatus);
                }
                return null;
            }
        }
        public string Descriptor { get { return descriptor; } }
        public ushort? AntDeviceNumber { get { return antDeviceNumber; } }
        public SourceType? SourceType { get { return sourceType; } }
        public string ProductName { get { return productName; } }
        public DateTime Timestamp { get { return timestamp; } }
    }
}
