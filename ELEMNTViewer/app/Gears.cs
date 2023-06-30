using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Dynastream.Fit;

namespace ELEMNTViewer
{
    class Gears
    {
        [Browsable(false)]
        public bool AntGearChanger { get; private set; }
        [DisplayName("Front Gear Changes")]
        public int FrontGearChanges { get; private set; }
        [DisplayName("Rear Gear Changes")]
        public int RearGearChanges { get; private set; }
        [DisplayName("Total Gear Changes")]
        public int TotalGearChanges { get; private set; }

        //Data, Bit31..16 : FrontGear 36, 1 or 52, 2
        //Data, Bit15..0 : RearGear 34, 1 or 30, 2 or 11, 12
        public Gears()
        {
            List<EventValues> eventValues = DataManager.Instance.EventValues;
            for (int i = 0; i < eventValues.Count; i++)
            {
                EventValues values = eventValues[i];
                if (values.Event0 == Event.FrontGearChange)
                {
                    AntGearChanger = true;
                    FrontGearChanges++;
                    byte frontGear = (byte)(values.Data.Value >> 24);
                    byte frontGearNumber = (byte)(values.Data.Value >> 16);
                }
                if (values.Event0 == Event.RearGearChange)
                {
                    AntGearChanger = true;
                    RearGearChanges++;
                    byte rearGear = (byte)(values.Data.Value >> 8);
                    byte rearGearNumber = (byte)(values.Data.Value);
                }
            }
            TotalGearChanges = FrontGearChanges + RearGearChanges;
        }
    }
}
