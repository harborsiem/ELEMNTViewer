using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer {
    class PowerZonesManager {
        private List<ushort> powerZones = new List<ushort>();

        //public List<ushort> PowerZones { get { return powerZones; } }

        public void Add(ushort zone) {
            powerZones.Add(zone);
        }

        public void Clear() {
            powerZones.Clear();
        }

        public PowerZones GetPowerZones() {
            if (powerZones.Count < 6) {
                return null;
            }
            PowerZones result = new PowerZones();
            result.FTPValue = DataManager.Instance.Session.ThresholdPower.ToString();
            result.ActiveRecovery = 0.ToString() + " - " + powerZones[0].ToString();
            result.AerobeThreshold = powerZones[0].ToString() + " - " + powerZones[1].ToString();
            result.Tempo = powerZones[1].ToString() + " - " + powerZones[2].ToString();
            result.LaktatThreshold = powerZones[2].ToString() + " - " + powerZones[3].ToString();
            result.AerobeCapacity = powerZones[3].ToString() + " - " + powerZones[4].ToString();
            result.AnaerobeCapacity = powerZones[4].ToString() + " +";
            return result;
        }
    }
}
