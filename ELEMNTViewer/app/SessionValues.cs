
using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;

    [DefaultProperty("TotalDistance")]
    class SessionValues
    {
        private DateTime timestamp;
        private DateTime startTime;
        private float totalElapsedTime;
        private float totalTimerTime;
        private float avgSpeed;
        private float maxSpeed;
        private float totalDistance;
        private byte avgCadence;
        private byte maxCadence;
        private byte minHeartRate;
        private byte avgHeartRate;
        private byte maxHeartRate;
        private float timeInHrZone0;
        private float timeInHrZone1;
        private float timeInHrZone2;
        private float timeInHrZone3;
        private float timeInHrZone4;
        private ushort avgPower;
        private ushort maxPower;
        private float timeInPowerZone0;
        private float timeInPowerZone1;
        private float timeInPowerZone2;
        private float timeInPowerZone3;
        private float timeInPowerZone4;
        private float timeInPowerZone5;
        private uint totalWork;
        private float minAltitude;
        private float avgAltitude;
        private float maxAltitude;
        private float maxNegGrade;
        private float avgGrade;
        private float maxPosGrade;
        private ushort totalCalories;
        private ushort normalizedPower;
        private sbyte avgTemperature;
        private sbyte maxTemperature;
        private ushort totalAscent;
        private ushort totalDescent;
        private ushort numLaps;
        private ushort thresholdPower;
        private float trainingStressScore;
        private float intensityFactor;

        private Sport sport;

        public void SetValue(byte fieldNum, int index, object value)
        {
            Type objType = value.GetType();
            switch (fieldNum)
            {
                case SessionMesg.FieldDefNum.Event:
                    break;
                case SessionMesg.FieldDefNum.EventType:
                    break;
                case SessionMesg.FieldDefNum.StartTime:
                    startTime = FitConvert.ToLocalDateTime((uint)value); //UInt32
                    break;
                case SessionMesg.FieldDefNum.StartPositionLat:
                    break;
                case SessionMesg.FieldDefNum.StartPositionLong:
                    break;
                case SessionMesg.FieldDefNum.Sport:
                    sport = (Sport)value;
                    break;
                case SessionMesg.FieldDefNum.SubSport:
                    break;
                case SessionMesg.FieldDefNum.TotalElapsedTime:
                    totalElapsedTime = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.TotalTimerTime:
                    totalTimerTime = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.TotalDistance:
                    totalDistance = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.TotalCalories:
                    totalCalories = Convert.ToUInt16(value); //UInt16
                    break;
                case SessionMesg.FieldDefNum.AvgSpeed:
                    avgSpeed = FitConvert.ToKmPerHour(Convert.ToSingle(value)); //Single
                    break;
                case SessionMesg.FieldDefNum.MaxSpeed:
                    maxSpeed = FitConvert.ToKmPerHour(Convert.ToSingle(value)); //Single
                    break;
                case SessionMesg.FieldDefNum.AvgHeartRate:
                    avgHeartRate = Convert.ToByte(value); //Byte
                    break;
                case SessionMesg.FieldDefNum.MaxHeartRate:
                    maxHeartRate = Convert.ToByte(value); //Byte
                    break;
                case SessionMesg.FieldDefNum.AvgCadence:
                    avgCadence = Convert.ToByte(value); //Byte
                    break;
                case SessionMesg.FieldDefNum.MaxCadence:
                    maxCadence = Convert.ToByte(value); //Byte
                    break;
                case SessionMesg.FieldDefNum.AvgPower:
                    avgPower = Convert.ToUInt16(value); //UInt16
                    break;
                case SessionMesg.FieldDefNum.MaxPower:
                    maxPower = Convert.ToUInt16(value); //UInt16
                    break;
                case SessionMesg.FieldDefNum.TotalAscent:
                    totalAscent = Convert.ToUInt16(value); //UInt16
                    break;
                case SessionMesg.FieldDefNum.TotalDescent:
                    totalDescent = Convert.ToUInt16(value); //UInt16
                    break;
                case SessionMesg.FieldDefNum.NumLaps:
                    numLaps = Convert.ToUInt16(value); //UInt16
                    break;
                case SessionMesg.FieldDefNum.NormalizedPower:
                    normalizedPower = Convert.ToUInt16(value); //UInt16
                    break;
                case SessionMesg.FieldDefNum.TrainingStressScore:
                    trainingStressScore = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.IntensityFactor:
                    intensityFactor = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.LeftRightBalance:
                    break;
                case SessionMesg.FieldDefNum.ThresholdPower:
                    thresholdPower = Convert.ToUInt16(value); //UInt16
                    break;
                case SessionMesg.FieldDefNum.TotalWork:
                    totalWork = Convert.ToUInt32(value); //UInt32
                    break;
                case SessionMesg.FieldDefNum.AvgAltitude:
                    avgAltitude = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.MaxAltitude:
                    maxAltitude = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.AvgGrade:
                    avgGrade = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.MaxPosGrade:
                    maxPosGrade = Convert.ToSingle(value); // (float)(Math.Tan(Convert.ToSingle(value) * Math.PI / 180.0) * 100.0); //Single
                    break;
                case SessionMesg.FieldDefNum.MaxNegGrade:
                    maxNegGrade = Convert.ToSingle(value); // (float)(Math.Tan(Convert.ToSingle(value) * Math.PI / 180.0) * 100.0); //Single
                    break;
                case SessionMesg.FieldDefNum.AvgTemperature:
                    avgTemperature = Convert.ToSByte(value); //SByte
                    break;
                case SessionMesg.FieldDefNum.MaxTemperature:
                    maxTemperature = Convert.ToSByte(value); //SByte
                    break;
                case SessionMesg.FieldDefNum.MinHeartRate:
                    minHeartRate = Convert.ToByte(value); //Byte
                    break;
                case SessionMesg.FieldDefNum.TimeInHrZone:
                    float val = Convert.ToSingle(value); //Single
                    switch (index)
                    {
                        case 0:
                            timeInHrZone0 = val;
                            break;
                        case 1:
                            timeInHrZone1 = val;
                            break;
                        case 2:
                            timeInHrZone2 = val;
                            break;
                        case 3:
                            timeInHrZone3 = val;
                            break;
                        case 4:
                            timeInHrZone4 = val;
                            break;
                        default:
                            break;
                    }
                    break;
                case SessionMesg.FieldDefNum.TimeInPowerZone:
                    val = Convert.ToSingle(value); //Single
                    switch (index)
                    {
                        case 0:
                            timeInPowerZone0 = val;
                            break;
                        case 1:
                            timeInPowerZone1 = val;
                            break;
                        case 2:
                            timeInPowerZone2 = val;
                            break;
                        case 3:
                            timeInPowerZone3 = val;
                            break;
                        case 4:
                            timeInPowerZone4 = val;
                            break;
                        case 5:
                            timeInPowerZone5 = val;
                            break;
                        default:
                            break;
                    }
                    break;
                case SessionMesg.FieldDefNum.MinAltitude:
                    minAltitude = Convert.ToSingle(value); //Single
                    break;
                case SessionMesg.FieldDefNum.Timestamp:
                    timestamp = FitConvert.ToLocalDateTime((uint)value); //UInt32
                    break;

                case SessionMesg.FieldDefNum.EnhancedAvgSpeed:
                case SessionMesg.FieldDefNum.EnhancedMaxSpeed:
                case SessionMesg.FieldDefNum.EnhancedAvgAltitude:
                case SessionMesg.FieldDefNum.EnhancedMinAltitude:
                case SessionMesg.FieldDefNum.EnhancedMaxAltitude:
                    break;
                default:
                    break;
            }
        }

        [Category("Time")]
        public DateTime Timestamp { get { return timestamp; } }
        [Category("Time")]
        public DateTime StartTime { get { return startTime; } }
        [Category("Time")]
        [DisplayName("Total Elapsed Time")]
        public TimeSpan TotalElapsedTime { get { return FitConvert.ToTimeSpan(totalElapsedTime); } }
        [Category("Time")]
        [DisplayName("Total Timer Time")]
        public TimeSpan TotalTimerTime { get { return FitConvert.ToTimeSpan(totalTimerTime); } }
        [Category("Speed")]
        [DisplayName("Average Speed")]
        public float AvgSpeed { get { return avgSpeed; } }
        [Category("Speed")]
        [DisplayName("Maximum Speed")]
        public float MaxSpeed { get { return maxSpeed; } }
        [Category("Distance etc")]
        [DisplayName("Total Distance")]
        public float TotalDistance { get { return FitConvert.ToKm(totalDistance); } }
        [Category("Speed")]
        [DisplayName("Average Cadence")]
        public byte AvgCadence { get { return avgCadence; } }
        [Category("Speed")]
        [DisplayName("Maximum Cadence")]
        public byte MaxCadence { get { return maxCadence; } }
        [Category("HeartRate")]
        [DisplayName("Minimum HeartRate")]
        public byte MinHeartRate { get { return minHeartRate; } }
        [Category("HeartRate")]
        [DisplayName("Average HeartRate")]
        public byte AvgHeartRate { get { return avgHeartRate; } }
        [Category("HeartRate")]
        [DisplayName("Maximum HeartRate")]
        public byte MaxHeartRate { get { return maxHeartRate; } }
        [Category("HeartRate")]
        public TimeSpan TimeInHrZone0 { get { return FitConvert.ToTimeSpan(timeInHrZone0); } }
        [Category("HeartRate")]
        public TimeSpan TimeInHrZone1 { get { return FitConvert.ToTimeSpan(timeInHrZone1); } }
        [Category("HeartRate")]
        public TimeSpan TimeInHrZone2 { get { return FitConvert.ToTimeSpan(timeInHrZone2); } }
        [Category("HeartRate")]
        public TimeSpan TimeInHrZone3 { get { return FitConvert.ToTimeSpan(timeInHrZone3); } }
        [Category("HeartRate")]
        public TimeSpan TimeInHrZone4 { get { return FitConvert.ToTimeSpan(timeInHrZone4); } }
        [Category("Power")]
        [DisplayName("Average Power")]
        public ushort AvgPower { get { return avgPower; } }
        [Category("Power")]
        [DisplayName("Maximum Power")]
        public ushort MaxPower { get { return maxPower; } }
        [Category("Power")]
        public TimeSpan TimeInPowerZone0 { get { return FitConvert.ToTimeSpan(timeInPowerZone0); } }
        [Category("Power")]
        public TimeSpan TimeInPowerZone1 { get { return FitConvert.ToTimeSpan(timeInPowerZone1); } }
        [Category("Power")]
        public TimeSpan TimeInPowerZone2 { get { return FitConvert.ToTimeSpan(timeInPowerZone2); } }
        [Category("Power")]
        public TimeSpan TimeInPowerZone3 { get { return FitConvert.ToTimeSpan(timeInPowerZone3); } }
        [Category("Power")]
        public TimeSpan TimeInPowerZone4 { get { return FitConvert.ToTimeSpan(timeInPowerZone4); } }
        [Category("Power")]
        public TimeSpan TimeInPowerZone5 { get { return FitConvert.ToTimeSpan(timeInPowerZone5); } }
        [Category("Power")]
        public uint TotalWork { get { return FitConvert.ToKJoule(totalWork); } }

        [Category("Distance etc")]
        [DisplayName("Minimum Altitude")]
        public float MinAltitude { get { return minAltitude; } }
        [Category("Distance etc")]
        [DisplayName("Average Altitude")]
        public float AvgAltitude { get { return avgAltitude; } }
        [Category("Distance etc")]
        [DisplayName("Maximum Altitude")]
        public float MaxAltitude { get { return maxAltitude; } }
        [Category("Distance etc")]
        [DisplayName("Maximum Negative Grade")]
        public float MaxNegGrade { get { return maxNegGrade; } }
        [Category("Distance etc")]
        [DisplayName("Average Grade")]
        public float AvgGrade { get { return avgGrade; } }
        [Category("Distance etc")]
        [DisplayName("Maximum Positive Grade")]
        public float MaxPosGrade { get { return maxPosGrade; } }

        [Category("Misc")]
        [DisplayName("Total Calories")]
        public ushort TotalCalories { get { return totalCalories; } }
        [Category("Power")]
        [DisplayName("Normalized Power")]
        public ushort NormalizedPower { get { return normalizedPower; } }
        [Category("Temperature")]
        [DisplayName("Average Temperature")]
        public sbyte AvgTemperature { get { return avgTemperature; } }
        [Category("Temperature")]
        [DisplayName("Maximum Temperature")]
        public sbyte MaxTemperature { get { return maxTemperature; } }
        [Category("Distance etc")]
        [DisplayName("Total Ascent")]
        public ushort TotalAscent { get { return totalAscent; } }
        [Category("Distance etc")]
        [DisplayName("Total Descent")]
        public ushort TotalDescent { get { return totalDescent; } }
        [Category("Distance etc")]
        [DisplayName("Vam")]
        public float Vam
        {
            get
            {
                if (TotalTimerTime.TotalHours != 0)
                    return (float)Math.Round(TotalAscent / TotalTimerTime.TotalHours);
                else
                    return 0.0f;
            }
        }
        //public byte Sport { get { return sport; } }
        [Category("Misc")]
        public ushort NumLaps { get { return numLaps; } }
        [Category("Power")]
        public ushort ThresholdPower { get { return thresholdPower; } }
        [Category("Power")]
        public float TrainingStressScore { get { return trainingStressScore; } }
        [Category("Power")]
        public float IntensityFactor { get { return intensityFactor; } }
        [Category("Power")]
        [DisplayName("VI")]
        public float VI
        {
            get
            {
                if (AvgPower != 0)
                    return (float)Math.Round((double)NormalizedPower / (double)AvgPower, 2);
                else
                    return 0.0f;
            }
        }
    }
}
