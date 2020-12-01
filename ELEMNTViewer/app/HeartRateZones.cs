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
        //[DisplayName("Einfach")]
        [DisplayName("Easy")]
        [ReadOnly(true)]
        public string Easy { get; set; }
        //[DisplayName("Fettverbrennung")]
        [DisplayName("Fat Burning")]
        [ReadOnly(true)]
        public string FatBurning { get; set; }
        [DisplayName("Cardio")]
        [ReadOnly(true)]
        public string Cardio { get; set; }
        //[DisplayName("Schwierig")]
        [DisplayName("Hard")]
        [ReadOnly(true)]
        public string Hard { get; set; }
        //[DisplayName("Spitze")]
        [DisplayName("Peak")]
        [ReadOnly(true)]
        public string Peak { get; set; }
    }
}
