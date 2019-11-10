using Dynastream.Fit;

namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class FileIdValues {
        private File? fileType;
        private ushort? manufacturer;
        private ushort? product;
        private uint? serialNumber;
        private DateTime timeCreated;

        public static void OnFileIDMesg(object sender, MesgEventArgs e) {
            FileIdValues values = new FileIdValues();
            FileIdMesg mesg = (FileIdMesg)e.mesg;
            try {
                values.fileType = mesg.GetType();
                values.manufacturer = mesg.GetManufacturer();
                values.product = mesg.GetProduct();
                values.serialNumber = mesg.GetSerialNumber();

                //Make sure properties with sub properties arent null before trying to create objects based on them
                if (mesg.GetTimeCreated() != null) {
                    uint tc = (uint)mesg.GetFieldValue("TimeCreated");
                    values.timeCreated = FitConvert.ToLocalDateTime(tc);
                }
            }
            catch (FitException exception) {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.FileIdValues.Add(values);
        }

        public File? FileType { get { return fileType; } }
        public ushort? ManufacturerRaw { get { return manufacturer; } }
        public string Manufacturer {
            get {
                if (manufacturer != null) {
                    return FitConvert.GetConstName(typeof(Manufacturer), (ushort)manufacturer);
                }
                return null;
            }
        }
        public ushort? Product { get { return product; } }
        public uint? SerialNumber { get { return serialNumber; } }
        public DateTime TimeCreated { get { return timeCreated; } }
    }
}
