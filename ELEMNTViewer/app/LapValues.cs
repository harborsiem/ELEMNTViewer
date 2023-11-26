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
    class LapValues
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

        public void SetValue(byte fieldNum, int index, object value)
        {
            //Type objType = value.GetType();
            switch (fieldNum)
            {
                case LapMesg.FieldDefNum.Event:
                    break;
                case LapMesg.FieldDefNum.EventType:
                    break;
                case LapMesg.FieldDefNum.StartTime:
                    startTime = FitConvert.ToLocalDateTime((uint)value); //UInt32
                    break;
                case LapMesg.FieldDefNum.StartPositionLat:
                    break;
                case LapMesg.FieldDefNum.StartPositionLong:
                    break;
                case LapMesg.FieldDefNum.EndPositionLat:
                    break;
                case LapMesg.FieldDefNum.EndPositionLong:
                    break;
                case LapMesg.FieldDefNum.TotalElapsedTime:
                    totalElapsedTime = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.TotalTimerTime:
                    totalTimerTime = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.TotalDistance:
                    totalDistance = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.TotalCalories:
                    totalCalories = Convert.ToUInt16(value); //UInt16
                    break;
                case LapMesg.FieldDefNum.AvgSpeed:
                    avgSpeed = FitConvert.ToKmPerHour(Convert.ToSingle(value)); //Single
                    break;
                case LapMesg.FieldDefNum.MaxSpeed:
                    maxSpeed = FitConvert.ToKmPerHour(Convert.ToSingle(value)); //Single
                    break;
                case LapMesg.FieldDefNum.AvgHeartRate:
                    avgHeartRate = Convert.ToByte(value); //Byte
                    break;
                case LapMesg.FieldDefNum.MaxHeartRate:
                    maxHeartRate = Convert.ToByte(value); //Byte
                    break;
                case LapMesg.FieldDefNum.AvgCadence:
                    avgCadence = Convert.ToByte(value); //Byte
                    break;
                case LapMesg.FieldDefNum.MaxCadence:
                    maxCadence = Convert.ToByte(value); //Byte
                    break;
                case LapMesg.FieldDefNum.AvgPower:
                    avgPower = Convert.ToUInt16(value); //UInt16
                    break;
                case LapMesg.FieldDefNum.MaxPower:
                    maxPower = Convert.ToUInt16(value); //UInt16
                    break;
                case LapMesg.FieldDefNum.TotalAscent:
                    totalAscent = Convert.ToUInt16(value); //UInt16
                    break;
                case LapMesg.FieldDefNum.TotalDescent:
                    totalDescent = Convert.ToUInt16(value); //UInt16
                    break;
                case LapMesg.FieldDefNum.NormalizedPower:
                    normalizedPower = Convert.ToUInt16(value); //UInt16
                    break;
                case LapMesg.FieldDefNum.LeftRightBalance:
                    break;
                case LapMesg.FieldDefNum.TotalWork:
                    totalWork = Convert.ToUInt32(value); //UInt32
                    break;
                case LapMesg.FieldDefNum.AvgAltitude:
                    avgAltitude = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.MaxAltitude:
                    maxAltitude = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.AvgGrade:
                    //avgGrade = (float)(Math.Tan(Convert.ToSingle(value) * Math.PI / 180.0) * 100.0); //Single
                    avgGrade = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.MaxPosGrade:
                    //maxPosGrade = (float)(Math.Tan(Convert.ToSingle(value) * Math.PI / 180.0) * 100.0); //Single
                    maxPosGrade = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.MaxNegGrade:
                    //maxNegGrade = (float)(Math.Tan(Convert.ToSingle(value) * Math.PI / 180.0) * 100.0); //Single
                    maxNegGrade = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.AvgTemperature:
                    avgTemperature = Convert.ToSByte(value); //SByte
                    break;
                case LapMesg.FieldDefNum.MaxTemperature:
                    maxTemperature = Convert.ToSByte(value); //SByte
                    break;
                case LapMesg.FieldDefNum.MinHeartRate:
                    minHeartRate = Convert.ToByte(value); //Byte
                    break;
                case LapMesg.FieldDefNum.TimeInHrZone:
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
                case LapMesg.FieldDefNum.TimeInPowerZone:
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
                case LapMesg.FieldDefNum.MinAltitude:
                    minAltitude = Convert.ToSingle(value); //Single
                    break;
                case LapMesg.FieldDefNum.Timestamp:
                    timestamp = FitConvert.ToLocalDateTime((uint)value); //UInt32
                    break;

                case LapMesg.FieldDefNum.EnhancedAvgSpeed:
                case LapMesg.FieldDefNum.EnhancedMaxSpeed:
                case LapMesg.FieldDefNum.EnhancedAvgAltitude:
                case LapMesg.FieldDefNum.EnhancedMinAltitude:
                case LapMesg.FieldDefNum.EnhancedMaxAltitude:
                    break;
                default:
                    break;
            }
        }

        [SRCategory("Time")]
        [SRDisplayName(nameof(Timestamp))]
        public DateTime Timestamp { get { return timestamp; } }
        [SRCategory("Time")]
        [SRDisplayName(nameof(StartTime))]
        public DateTime StartTime { get { return startTime; } }
        [SRCategory("Time")]
        [SRDisplayName(nameof(TotalElapsedTime))]
        public TimeSpan TotalElapsedTime { get { return FitConvert.ToTimeSpan(totalElapsedTime); } }
        [SRCategory("Time")]
        [SRDisplayName(nameof(TotalTimerTime))]
        public TimeSpan TotalTimerTime { get { return FitConvert.ToTimeSpan(totalTimerTime); } }
        [SRCategory("Speed")]
        [SRDisplayName(nameof(AvgSpeed))]
        public float AvgSpeed { get { return (float)Math.Round(avgSpeed, 2); } }
        [SRCategory("Speed")]
        [SRDisplayName(nameof(MaxSpeed))]
        public float MaxSpeed { get { return (float)Math.Round(maxSpeed, 2); } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(TotalDistance))]
        public float TotalDistance { get { return (float)Math.Round(FitConvert.ToKm(totalDistance), 2); } }
        [SRCategory("Speed")]
        [SRDisplayName(nameof(AvgCadence))]
        public byte AvgCadence { get { return avgCadence; } }
        [SRCategory("Speed")]
        [SRDisplayName(nameof(MaxCadence))]
        public byte MaxCadence { get { return maxCadence; } }
        [SRCategory("HeartRate")]
        [SRDisplayName(nameof(MinHeartRate))]
        public byte MinHeartRate { get { return minHeartRate; } }
        [SRCategory("HeartRate")]
        [SRDisplayName(nameof(AvgHeartRate))]
        public byte AvgHeartRate { get { return avgHeartRate; } }
        [SRCategory("HeartRate")]
        [SRDisplayName(nameof(MaxHeartRate))]
        public byte MaxHeartRate { get { return maxHeartRate; } }
        [SRCategory("HeartRate")]
        [SRDisplayName(nameof(TimeInHrZone0))]
        public TimeSpan TimeInHrZone0 { get { return FitConvert.ToTimeSpan(timeInHrZone0); } }
        [SRCategory("HeartRate")]
        [SRDisplayName(nameof(TimeInHrZone1))]
        public TimeSpan TimeInHrZone1 { get { return FitConvert.ToTimeSpan(timeInHrZone1); } }
        [SRCategory("HeartRate")]
        [SRDisplayName(nameof(TimeInHrZone2))]
        public TimeSpan TimeInHrZone2 { get { return FitConvert.ToTimeSpan(timeInHrZone2); } }
        [SRCategory("HeartRate")]
        [SRDisplayName(nameof(TimeInHrZone3))]
        public TimeSpan TimeInHrZone3 { get { return FitConvert.ToTimeSpan(timeInHrZone3); } }
        [SRCategory("HeartRate")]
        [SRDisplayName(nameof(TimeInHrZone4))]
        public TimeSpan TimeInHrZone4 { get { return FitConvert.ToTimeSpan(timeInHrZone4); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(AvgPower))]
        public ushort AvgPower { get { return avgPower; } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(MaxPower))]
        public ushort MaxPower { get { return maxPower; } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(TimeInPowerZone0))]
        public TimeSpan TimeInPowerZone0 { get { return FitConvert.ToTimeSpan(timeInPowerZone0); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(TimeInPowerZone1))]
        public TimeSpan TimeInPowerZone1 { get { return FitConvert.ToTimeSpan(timeInPowerZone1); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(TimeInPowerZone2))]
        public TimeSpan TimeInPowerZone2 { get { return FitConvert.ToTimeSpan(timeInPowerZone2); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(TimeInPowerZone3))]
        public TimeSpan TimeInPowerZone3 { get { return FitConvert.ToTimeSpan(timeInPowerZone3); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(TimeInPowerZone4))]
        public TimeSpan TimeInPowerZone4 { get { return FitConvert.ToTimeSpan(timeInPowerZone4); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(TimeInPowerZone5))]
        public TimeSpan TimeInPowerZone5 { get { return FitConvert.ToTimeSpan(timeInPowerZone5); } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(TotalWork))]
        public uint TotalWork { get { return FitConvert.ToKJoule(totalWork); } }

        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(MinAltitude))]
        public float MinAltitude { get { return minAltitude; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(AvgAltitude))]
        public float AvgAltitude { get { return avgAltitude; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(MaxAltitude))]
        public float MaxAltitude { get { return maxAltitude; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(MaxNegGrade))]
        public float MaxNegGrade { get { return maxNegGrade; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(AvgGrade))]
        public float AvgGrade { get { return avgGrade; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(MaxPosGrade))]
        public float MaxPosGrade { get { return maxPosGrade; } }

        [SRCategory("Misc")]
        [SRDisplayName(nameof(TotalCalories))]
        public ushort TotalCalories { get { return totalCalories; } }
        [SRCategory("Power")]
        [SRDisplayName(nameof(NormalizedPower))]
        public ushort NormalizedPower { get { return normalizedPower; } }
        [SRCategory("Temperature")]
        [SRDisplayName(nameof(AvgTemperature))]
        public sbyte AvgTemperature { get { return avgTemperature; } }
        [SRCategory("Temperature")]
        [SRDisplayName(nameof(MaxTemperature))]
        public sbyte MaxTemperature { get { return maxTemperature; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(TotalAscent))]
        public ushort TotalAscent { get { return totalAscent; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(TotalDescent))]
        public ushort TotalDescent { get { return totalDescent; } }
        [SRCategory("DistanceEtc")]
        [SRDisplayName(nameof(Vam))]
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
        [SRCategory("Power")]
        [SRDisplayName(nameof(VI))]
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
