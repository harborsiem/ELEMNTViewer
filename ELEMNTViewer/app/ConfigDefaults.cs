using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ELEMNTViewer {
    static class ConfigDefaults {
        private static Color[] _colorsBrightPastel = new Color[] { Color.FromArgb(0x41, 140, 240), Color.FromArgb(0xfc, 180, 0x41), Color.FromArgb(0xe0, 0x40, 10), Color.FromArgb(5, 100, 0x92), Color.FromArgb(0xbf, 0xbf, 0xbf), Color.FromArgb(0x1a, 0x3b, 0x69), Color.FromArgb(0xff, 0xe3, 130), Color.FromArgb(0x12, 0x9c, 0xdd), Color.FromArgb(0xca, 0x6b, 0x4b), Color.FromArgb(0, 0x5c, 0xdb), Color.FromArgb(0xf3, 210, 0x88), Color.FromArgb(80, 0x63, 0x81), Color.FromArgb(0xf1, 0xb9, 0xa8), Color.FromArgb(0xe0, 0x83, 10), Color.FromArgb(120, 0x93, 190) };
        private static Color[] _colorsFire = new Color[] { Color.Gold, Color.Red, Color.DeepPink, Color.Crimson, Color.DarkOrange, Color.Magenta, Color.Yellow, Color.OrangeRed, Color.MediumVioletRed, Color.FromArgb(0xdd, 0xe2, 0x21) };
        private static Color[] _colorsExcel = new Color[] { Color.FromArgb(0x99, 0x99, 0xff), Color.FromArgb(0x99, 0x33, 0x66), Color.FromArgb(0xff, 0xff, 0xcc), Color.FromArgb(0xcc, 0xff, 0xff), Color.FromArgb(0x66, 0, 0x66), Color.FromArgb(0xff, 0x80, 0x80), Color.FromArgb(0, 0x66, 0xcc), Color.FromArgb(0xcc, 0xcc, 0xff), Color.FromArgb(0, 0, 0x80), Color.FromArgb(0xff, 0, 0xff), Color.FromArgb(0xff, 0xff, 0), Color.FromArgb(0, 0xff, 0xff), Color.FromArgb(0x80, 0, 0x80), Color.FromArgb(0x80, 0, 0), Color.FromArgb(0, 0x80, 0x80), Color.FromArgb(0, 0, 0xff) };

        public static Color GetColor(int index) {
            int i;
            Color[] colorArray;
            if (index < _colorsBrightPastel.Length) {
                colorArray = _colorsBrightPastel;
                i = index;
            } else if (index < (_colorsBrightPastel.Length + _colorsFire.Length)) {
                colorArray = _colorsFire;
                i = index - _colorsBrightPastel.Length;
            } else if (index < (_colorsBrightPastel.Length + _colorsFire.Length + _colorsExcel.Length)) {
                colorArray = _colorsExcel;
                i = index - (_colorsBrightPastel.Length + _colorsFire.Length);
            } else {
                colorArray = _colorsBrightPastel;
                i = 0;
            }
            return colorArray[i];
        }
    }
}
