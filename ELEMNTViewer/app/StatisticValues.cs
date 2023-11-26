using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ELEMNTViewer
{
    class StatisticValues
    {
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(TotalDistance))]
        public double TotalDistance { get; set; }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(TotalAscent))]
        public double TotalAscent { get; set; }
    }
}
