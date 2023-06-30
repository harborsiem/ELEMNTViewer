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
        [Category("Distance etc")]
        [DisplayName("Total Distance (km)")]
        public double TotalDistance { get; set; }
        [Category("Distance etc")]
        [DisplayName("Total Ascent (m)")]
        public double TotalAscent { get; set; }
    }
}
