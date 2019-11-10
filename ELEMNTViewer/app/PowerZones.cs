using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ELEMNTViewer {
    class PowerZones {
        //[DisplayName("FTP Wert")]
        [DisplayName("FTP value")]
        [ReadOnly(true)]
        public string FTPValue { get; set; }
        //[DisplayName("Aktive Erholung")]
        [DisplayName("Active Recovery")]
        [ReadOnly(true)]
        public string ActiveRecovery { get; set; }
        //[DisplayName("Aerober Schwellenwert")]
        [DisplayName("Aerobe Threshold")]
        [ReadOnly(true)]
        public string AerobeThreshold { get; set; }
        [DisplayName("Tempo")]
        [ReadOnly(true)]
        public string Tempo { get; set; }
        //[DisplayName("Laktat Schwellwert")]
        [DisplayName("Laktat Threshold")]
        [ReadOnly(true)]
        public string LaktatThreshold { get; set; }
        //[DisplayName("Aerobe Kapazität")]
        [DisplayName("Aerobe Capacity")]
        [ReadOnly(true)]
        public string AerobeCapacity { get; set; }
        //[DisplayName("Anaerobe Kapazität")]
        [DisplayName("Anaerobe Capacity")]
        [ReadOnly(true)]
        public string AnaerobeCapacity { get; set; }
    }
}
