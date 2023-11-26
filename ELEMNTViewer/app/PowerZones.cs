using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ELEMNTViewer
{
    class PowerZones
    {
        const string CatPower = "Power";
        //[DisplayName("FTP Wert")]
        [SRCategory(CatPower)]
        [SRDisplayName(nameof(FTPValue))]
        [ReadOnly(true)]
        public string FTPValue { get; set; }
        //[DisplayName("Aktive Erholung")]
        [SRCategory(CatPower)]
        [SRDisplayName(nameof(ActiveRecovery))]
        [ReadOnly(true)]
        public string ActiveRecovery { get; set; }
        //[DisplayName("Aerober Schwellenwert")]
        [SRCategory(CatPower)]
        [SRDisplayName(nameof(AerobeThreshold))]
        [ReadOnly(true)]
        public string AerobeThreshold { get; set; }
        [SRCategory(CatPower)]
        [SRDisplayName(nameof(Tempo))]
        [ReadOnly(true)]
        public string Tempo { get; set; }
        //[DisplayName("Laktat Schwellwert")]
        [SRCategory(CatPower)]
        [SRDisplayName(nameof(LaktatThreshold))]
        [ReadOnly(true)]
        public string LaktatThreshold { get; set; }
        //[DisplayName("Aerobe Kapazität")]
        [SRCategory(CatPower)]
        [SRDisplayName(nameof(AerobeCapacity))]
        [ReadOnly(true)]
        public string AerobeCapacity { get; set; }
        //[DisplayName("Anaerobe Kapazität")]
        [SRCategory(CatPower)]
        [SRDisplayName(nameof(AnaerobeCapacity))]
        [ReadOnly(true)]
        public string AnaerobeCapacity { get; set; }
    }
}
