using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer {
    class PowerZonesManager {
        private List<ushort> _powerZones = new List<ushort>();

        //public List<ushort> PowerZones { get { return _powerZones; } }

        public void Add(ushort zone) {
            _powerZones.Add(zone);
        }

        public void Clear() {
            _powerZones.Clear();
        }

        public PowerZones GetPowerZones() {
            if (_powerZones.Count < 6) {
                return null;
            }
            PowerZones result = new PowerZones();
            result.FTPValue = DataManager.Instance.Session.ThresholdPower.ToString();
            result.ActiveRecovery = 0.ToString() + " - " + _powerZones[0].ToString();
            result.AerobeThreshold = _powerZones[0].ToString() + " - " + _powerZones[1].ToString();
            result.Tempo = _powerZones[1].ToString() + " - " + _powerZones[2].ToString();
            result.LaktatThreshold = _powerZones[2].ToString() + " - " + _powerZones[3].ToString();
            result.AerobeCapacity = _powerZones[3].ToString() + " - " + _powerZones[4].ToString();
            result.AnaerobeCapacity = _powerZones[4].ToString() + " +";
            return result;
        }
    }
}
