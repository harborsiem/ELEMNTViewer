using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class SportValues
    {
        private Sport? _sport;
        private SubSport? _subSport;

        public static void OnSportMesg(object sender, MesgEventArgs e)
        {
            SportValues values = new SportValues();
            SportMesg mesg = (SportMesg)e.mesg;
            try
            {
                int num = mesg.GetNumFields();
                values._sport = mesg.GetSport();
                values._subSport = mesg.GetSubSport();
            }
            catch (FitException exception)
            {
                Console.WriteLine("\tOnMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.SportValues.Add(values);
        }

        public Sport? Sport { get { return _sport; } }
        public SubSport? SubSport { get { return _subSport; } }
    }
}
