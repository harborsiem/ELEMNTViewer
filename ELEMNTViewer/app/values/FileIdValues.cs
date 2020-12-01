using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class FileIdValues
    {
        private File? _fileType;
        private ushort? _manufacturer;
        private ushort? _product;
        private uint? _serialNumber;
        private DateTime _timeCreated;

        public static void OnFileIDMesg(object sender, MesgEventArgs e)
        {
            FileIdValues values = new FileIdValues();
            FileIdMesg mesg = (FileIdMesg)e.mesg;
            try
            {
                values._fileType = mesg.GetType();
                values._manufacturer = mesg.GetManufacturer();
                values._product = mesg.GetProduct();
                values._serialNumber = mesg.GetSerialNumber();

                //Make sure properties with sub properties arent null before trying to create objects based on them
                if (mesg.GetTimeCreated() != null)
                {
                    uint tc = (uint)mesg.GetFieldValue("TimeCreated");
                    values._timeCreated = FitConvert.ToLocalDateTime(tc);
                }
            }
            catch (FitException exception)
            {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.FileIdValues.Add(values);
        }

        public File? FileType { get { return _fileType; } }
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
        public ushort? Product { get { return _product; } }
        public uint? SerialNumber { get { return _serialNumber; } }
        public DateTime TimeCreated { get { return _timeCreated; } }
    }
}
