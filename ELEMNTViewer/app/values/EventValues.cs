using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class EventValues
    {
        private Event? _event0;
        private EventType? _eventType;
        private uint? _data;
        private DateTime _timestamp;

        public static void OnEventMesg(object sender, MesgEventArgs e)
        {
            EventValues values = new EventValues();
            EventMesg mesg = (EventMesg)e.mesg;
            try
            {
                values._event0 = mesg.GetEvent();
                values._eventType = mesg.GetEventType();
                values._data = mesg.GetData();

                ////Make sure properties with sub properties arent null before trying to create objects based on them
                if (mesg.GetTimestamp() != null)
                {
                    uint tc = (uint)mesg.GetTimestamp().GetTimeStamp();
                    values._timestamp = FitConvert.ToLocalDateTime(tc);
                }
            }
            catch (FitException exception)
            {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.EventValues.Add(values);
        }

        public Event? Event0 { get { return _event0; } }
        public EventType? EventType { get { return _eventType; } }
        public uint? Data { get { return _data; } }
        public DateTime Timestamp { get { return _timestamp; } }
    }
}
