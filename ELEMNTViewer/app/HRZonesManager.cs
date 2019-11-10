using Dynastream.Fit;

namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class HRZonesManager {
        private List<byte> hrZones = new List<byte>();

        //internal List<byte> HrZones { get { return hrZones; } }

        //public static void OnHrZoneMesg(object sender, MesgEventArgs e) {
        //    HeartRateZones values = new HeartRateZones();
        //    HrZoneMesg mesg = (HrZoneMesg)e.mesg;
        //    try {
        //        values.Easy = mesg.GetHighBpm();

        //    }
        //    catch (FitException exception) {
        //        Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
        //        Console.WriteLine("\t{0}", exception.InnerException);
        //    }
        //    DataManager.Instance.DeviceInfoValues.Add(values);
        //}

        public void Add(byte zone) {
            hrZones.Add(zone);
        }

        public void Clear() {
            hrZones.Clear();
        }

        public HeartRateZones GetHeartRateZones() {
            if (hrZones.Count < 5) {
                return null;
            }
            HeartRateZones result = new HeartRateZones();
            result.Easy = 0.ToString() + " - " + hrZones[0].ToString();
            result.FatBurning = hrZones[0].ToString() + " - " + hrZones[1].ToString();
            result.Cardio = hrZones[1].ToString() + " - " + hrZones[2].ToString();
            result.Hard = hrZones[2].ToString() + " - " + hrZones[3].ToString();
            result.Peak = hrZones[3].ToString() + " +";
            return result;
        }
    }
}
