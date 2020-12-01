using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DeveloperDataIdValues
    {
        private byte? _developerDataIndex;

        public static void OnDeveloperDataIdMesg(object sender, MesgEventArgs e)
        {
            DeveloperDataIdValues values = new DeveloperDataIdValues();
            DeveloperDataIdMesg mesg = (DeveloperDataIdMesg)e.mesg;
            try
            {
                values._developerDataIndex = mesg.GetDeveloperDataIndex();
            }
            catch (FitException exception)
            {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.DeveloperDataIdValues.Add(values);
        }

        public byte? DeveloperDataIndex { get { return _developerDataIndex; } }
    }
}
