using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ELEMNTViewer
{
    class HeartRateZones
    {
        const string CatHeartRate = "HeartRate";
        //[DisplayName("Einfach")]
        [SRCategory(CatHeartRate)]
        [SRDisplayName(nameof(Easy))]
        [ReadOnly(true)]
        public string Easy { get; set; }
        //[DisplayName("Fettverbrennung")]
        [SRCategory(CatHeartRate)]
        [SRDisplayName(nameof(FatBurning))]
        [ReadOnly(true)]
        public string FatBurning { get; set; }
        [SRCategory(CatHeartRate)]
        [SRDisplayName(nameof(Cardio))]
        [ReadOnly(true)]
        public string Cardio { get; set; }
        //[DisplayName("Schwierig")]
        [SRCategory(CatHeartRate)]
        [SRDisplayName(nameof(Hard))]
        [ReadOnly(true)]
        public string Hard { get; set; }
        //[DisplayName("Spitze")]
        [SRCategory(CatHeartRate)]
        [SRDisplayName(nameof(Peak))]
        [ReadOnly(true)]
        public string Peak { get; set; }
    }
}
