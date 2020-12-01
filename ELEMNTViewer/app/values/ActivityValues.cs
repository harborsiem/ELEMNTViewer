using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ActivityValues
    {
        private float? _totalTimerTime;
        private ushort? _numSessions;
        private Activity? _type;
        private Event? _actiEvent;
        private EventType? _eventType;
        private DateTime _localTimestamp;
        private DateTime _timestamp;

        public static void OnActivityMesg(object sender, MesgEventArgs e)
        {
            ActivityValues values = new ActivityValues();
            ActivityMesg mesg = (ActivityMesg)e.mesg;
            try
            {
                values._totalTimerTime = mesg.GetTotalTimerTime();
                values._numSessions = mesg.GetNumSessions();
                values._type = mesg.GetType();
                values._actiEvent = mesg.GetEvent();
                values._eventType = mesg.GetEventType();
                uint? localTimeStamp = mesg.GetLocalTimestamp();
                if (localTimeStamp != null)
                {
                    values._localTimestamp = FitConvert.ToDateTime(((uint)localTimeStamp));
                }

                //Make sure properties with sub properties arent null before trying to create objects based on them
                if (mesg.GetTimestamp() != null)
                {
                    //values.timestamp = new Dynastream.Fit.DateTime(mesg.GetTimestamp().GetTimeStamp());
                    uint tc = (uint)mesg.GetTimestamp().GetTimeStamp();
                    values._timestamp = FitConvert.ToLocalDateTime(mesg.GetTimestamp().GetTimeStamp());
                }
            }
            catch (FitException exception)
            {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.ActivityValues.Add(values);
        }

        public TimeSpan? TotalTimerTime
        {
            get
            {
                if (_totalTimerTime != null)
                {
                    return FitConvert.ToTimeSpan((float)_totalTimerTime);
                }
                return null;
            }
        }

        public ushort? NumSessions { get { return _numSessions; } }
        public Activity? Type { get { return _type; } }
        public Event? ActiEvent { get { return _actiEvent; } }
        public EventType? EventType { get { return _eventType; } }
        public DateTime LocalTimestamp { get { return _localTimestamp; } }
        public DateTime Timestamp { get { return _timestamp; } }
    }
}
