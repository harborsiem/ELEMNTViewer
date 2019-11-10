using Dynastream.Fit;

namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class EventValues {
        private Event? event0;
        private EventType? eventType;
        private uint? data;
        private DateTime timestamp;

        public static void OnEventMesg(object sender, MesgEventArgs e) {
            EventValues values = new EventValues();
            EventMesg mesg = (EventMesg)e.mesg;
            try {
                values.event0 = mesg.GetEvent();
                values.eventType = mesg.GetEventType();
                values.data = mesg.GetData();

                ////Make sure properties with sub properties arent null before trying to create objects based on them
                if (mesg.GetTimestamp() != null) {
                    uint tc = (uint)mesg.GetTimestamp().GetTimeStamp();
                    values.timestamp = FitConvert.ToLocalDateTime(tc);
                }
            }
            catch (FitException exception) {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.EventValues.Add(values);
        }

        public Event? Event0 { get { return event0; } }
        public EventType? EventType { get { return eventType; } }
        public uint? Data { get { return data; } }
        public DateTime Timestamp { get { return timestamp; } }
    }
}
