using Dynastream.Fit;

namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ActivityValues {
        private float? totalTimerTime;
        private ushort? numSessions;
        private Activity? type;
        private Event? actiEvent;
        private EventType? eventType;
        private DateTime localTimestamp;
        private DateTime timestamp;

        public static void OnActivityMesg(object sender, MesgEventArgs e) {
            ActivityValues values = new ActivityValues();
            ActivityMesg mesg = (ActivityMesg)e.mesg;
            try {
                values.totalTimerTime = mesg.GetTotalTimerTime();
                values.numSessions = mesg.GetNumSessions();
                values.type = mesg.GetType();
                values.actiEvent = mesg.GetEvent();
                values.eventType = mesg.GetEventType();
                uint? lts = mesg.GetLocalTimestamp();
                if (lts != null) {
                    values.localTimestamp = FitConvert.ToDateTime(((uint)lts));
                }

                //Make sure properties with sub properties arent null before trying to create objects based on them
                if (mesg.GetTimestamp() != null) {
                    //values.timestamp = new Dynastream.Fit.DateTime(mesg.GetTimestamp().GetTimeStamp());
                    uint tc = (uint)mesg.GetTimestamp().GetTimeStamp();
                    values.timestamp = FitConvert.ToLocalDateTime(mesg.GetTimestamp().GetTimeStamp());
                }
            }
            catch (FitException exception) {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.ActivityValues.Add(values);
        }

        public TimeSpan? TotalTimerTime {
            get {
                if (totalTimerTime != null) {
                    return FitConvert.ToTimeSpan((float)totalTimerTime);
                }
                return null;
            }
        }
        public ushort? NumSessions { get { return numSessions; } }
        public Activity? Type { get { return type; } }
        public Event? ActiEvent { get { return actiEvent; } }
        public EventType? EventType { get { return eventType; } }
        public DateTime LocalTimestamp { get { return localTimestamp; } }
        public DateTime Timestamp { get { return timestamp; } }

    }
}
