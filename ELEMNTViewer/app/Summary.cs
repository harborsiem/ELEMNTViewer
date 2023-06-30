using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer
{
    class Summary : IComparable<Summary>
    {
        public Summary() { }

        public string Filename { get; set; }
        public double Distance { get; set; }
        public double Ascent { get; set; }
        public double AvgSpeed { get; set; }
        public TimeSpan TotalTimerTime { get; set; }
        public List<AntDevice> AntDevices { get; private set; } = new List<AntDevice>();

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
