using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer
{
    struct Summary : IComparable<Summary>
    {
        public Summary(string fileName, double distance, double ascent)
        {
            Filename = fileName;
            Distance = distance;
            Ascent = ascent;
        }

        public string Filename { get; set; }
        public double Distance { get; set; }
        public double Ascent { get; set; }

        public int GetYear()
        {
            if (!string.IsNullOrEmpty(Filename))
                return int.Parse(Filename.Substring(0, 4));
            return -1;
        }

        public int GetMonth()
        {
            if (!string.IsNullOrEmpty(Filename))
                return int.Parse(Filename.Substring(5, 2));
            return -1;
        }

        public int GetDay()
        {
            if (!string.IsNullOrEmpty(Filename))
                return int.Parse(Filename.Substring(8, 2));
            return -1;
        }

        public int CompareTo(Summary x)
        {
            return (this.Filename.CompareTo(x.Filename));
        }
    }
}
