using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Reflection;

namespace ELEMNTViewer {
    public static class FitConvert {

        //conversions for Latitude and Longitude positions
        public static double ToDegrees(int semicircles) {
            return semicircles * (180.0 / (double)0x7fffffff);
        }

        public static int ToSemicircles(double degrees) {
            return (int)(degrees * ((double)0x7fffffff / 180.0));
        }

        public static float ToKmPerHour(float meterPerSecond) {
            return (meterPerSecond * 3.6f);
        }

        public static float ToKmPerHour(ushort centimeterPerSecond) {
            return (centimeterPerSecond * 3.6f / 100f);
        }

        public static float ToMeterPerSecond(float kmPerHour) {
            return (kmPerHour / 3.6f);
        }

        public static float ToKm(float meters) {
            return (meters / 1000f);
        }

        public static float ToKm(uint centimeters) {
            return (centimeters / 100000f);
        }

        public static TimeSpan ToTimeSpan(float seconds) {
            return (new TimeSpan(0, 0, (int)(seconds)));
        }

        public static uint ToKJoule(uint joules) {
            return (joules / 1000);
        }

        public static DateTime ToDateTime(uint timeStamp) {
            Dynastream.Fit.DateTime dt = new Dynastream.Fit.DateTime(timeStamp);
            DateTime dt1 = dt.GetDateTime();
            return dt1;
        }

        public static DateTime ToLocalDateTime(uint timeStamp) {
            Dynastream.Fit.DateTime dt = new Dynastream.Fit.DateTime(timeStamp);
            DateTime dt1 = dt.GetDateTime();
            return dt1.ToLocalTime();
        }

        public static Dynastream.Fit.DateTime ToLocalDateTime(Dynastream.Fit.DateTime dt) {
            DateTime dateTime = dt.GetDateTime();
            DateTime lDateTime = dateTime.ToLocalTime();
            return new Dynastream.Fit.DateTime(lDateTime);
        }

        public static string GetConstName(Type constType, ushort constValue) {
            var props = constType.GetFields(BindingFlags.Public | BindingFlags.Static);
            var wanted = props.FirstOrDefault(prop => (ushort)prop.GetValue(null) == constValue);
            return wanted.Name;
        }

        public static string GetConstName(Type constType, byte constValue) {
            var props = constType.GetFields(BindingFlags.Public | BindingFlags.Static);
            var wanted = props.FirstOrDefault(prop => (byte)prop.GetValue(null) == constValue);
            return wanted.Name;
        }
    }
}
