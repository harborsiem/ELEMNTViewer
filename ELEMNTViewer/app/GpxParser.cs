using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
//using MapControl;
using WpfMaps;

namespace ELEMNTViewer
{
    internal class GpxParser
    {
        private List<GpxValues> list = new List<GpxValues>();
        private int _mapWidth;
        private int _mapHeight;
        private List<PointItem> _pointItems = new List<PointItem>();

        public GpxParser(int mapWidth, int mapHeight)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
        }

        public void Parse(string path)
        {
            if (File.Exists(path))
            {
                FileStream stream = File.OpenRead(path);
                try
                {
                    XDocument doc = XDocument.Load(stream);
                    var nsMgr = new XmlNamespaceManager(new NameTable());
                    //Or: var nsMgr = new XmlNamespaceManager(doc.CreateReader().NameTable);
                    nsMgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");
                    var itemGroups = doc.XPathSelectElements(@"//x:trkpt", nsMgr).ToList();
                    if (itemGroups.Count == 0)
                    {
                        nsMgr.AddNamespace("x", "http://www.topografix.com/GPX/1/0");
                        itemGroups = doc.XPathSelectElements(@"//x:trkpt", nsMgr).ToList();
                    }
                    if (itemGroups.Count == 0)
                    {
                        MessageBox.Show(Path.GetFileName(path) + " file format not supported", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    foreach (XElement gpxEle in doc.Elements())
                    {
                        if (gpxEle.Name.LocalName == "gpx")
                            foreach (XElement wptEle in gpxEle.Elements())
                            {
                                if (wptEle.Name.LocalName == "wpt")
                                {
                                    double wptLon = 0, wptLat = 0;
                                    string wptName = string.Empty;
                                    foreach (XAttribute xAttribute in wptEle.Attributes())
                                    {
                                        if (xAttribute.Name == "lon")
                                            wptLon = XmlConvert.ToDouble(xAttribute.Value);
                                        if (xAttribute.Name == "lat")
                                            wptLat = XmlConvert.ToDouble(xAttribute.Value);
                                    }
                                    foreach (XElement wptSubEle in wptEle.Elements())
                                    {
                                        if (wptSubEle.Name.LocalName == "name")
                                        {
                                            wptName = wptSubEle.Value;
                                        }
                                    }
                                    PointItem point = new PointItem();
                                    point.Location = new MapControl.Location(wptLat, wptLon);
                                    point.Name = wptName;
                                    _pointItems.Add(point);
                                }
                            }
                    }
                    double lon = 0, lat = 0;
                    double ele = 0;
                    GpxValues lastGpxValues = null;
                    foreach (XElement xElement in itemGroups)
                    {
                        if (xElement.Name.LocalName == "trkpt")
                        {
                            foreach (XAttribute x in xElement.Attributes())
                            {
                                if (x.Name == "lon")
                                    lon = XmlConvert.ToDouble(x.Value);
                                if (x.Name == "lat")
                                    lat = XmlConvert.ToDouble(x.Value);
                            }
                            foreach (XElement x in xElement.Elements())
                            {
                                if (x.Name.LocalName == "ele")
                                {
                                    ele = XmlConvert.ToDouble(x.Value);
                                }
                            }
                            if (!(lat == 0.0 && lon == 0.0 && ele == 0.0))
                                list.Add(lastGpxValues = new GpxValues(lat, lon, ele, lastGpxValues));
                        }
                    }
                    GpxValues result = list[list.Count - 1];
                }
                finally
                {
                    stream.Close();
                }
                ShowMapDialog();
            }
        }

        private void ShowMapDialog()
        {
            try
            {
                MapControl.LocationCollection locations = new MapControl.LocationCollection();
                List<PointItem> pushpinItems = new List<PointItem>();
                MapControl.Location mapCenter = null;
                double distanceStart = 0;
                //List<GpxValues> list = this.list;
                for (int i = 0; i < list.Count; i++)
                {
                    double latitude = list[i].Latitude;
                    double longitude = list[i].Longitude;
                    double distance = list[i].Distance;
                    MapControl.Location location = new MapControl.Location(latitude, longitude);
                    if (mapCenter == null && latitude != 0)
                    {
                        mapCenter = location;
                        pushpinItems.Add(new PointItem() { Location = location, Name = distanceStart.ToString() + " km" });
                        distanceStart += 5;
                    }
                    if (mapCenter != null && latitude != 0)
                    {
                        locations.Add(location);
                        if (distance >= distanceStart)
                        {
                            pushpinItems.Add(new PointItem() { Location = location, Name = distanceStart.ToString() + " km" });
                            distanceStart += 5;
                        }
                    }
                }
                MapHandler handler = new MapHandler(_mapWidth, _mapHeight);
                handler.SetLocations(mapCenter, locations, pushpinItems, _pointItems);
                handler.ShowDialog();
            }
            catch
            {
                throw;
            }
        }

        class GpxValues
        {
            private const double RADIUS = 6371; //6378.16;
            //private static readonly double Radians1 = Math.PI / 180.0;

            public GpxValues(double latitude, double longitude, double ele, GpxValues lastRecord)
            {
                Latitude = latitude;
                Longitude = longitude;
                Elevation = ele;
                if (lastRecord == null)
                {
                    Distance = 0;
                }
                else
                {
                    Distance = DistanceBetweenPlaces(lastRecord.Longitude, lastRecord.Latitude, longitude, latitude) + lastRecord.Distance;
                    Ascent = lastRecord.Ascent;
                    Descent = lastRecord.Descent;
                    double delta = Elevation - lastRecord.Elevation;
                    if (delta > 0)
                        Ascent += delta;
                    if (delta < 0)
                        Descent -= delta;
                }
            }

            public double Latitude { get; private set; }
            public double Longitude { get; private set; }
            public double Distance { get; private set; }
            public double Elevation { get; private set; }
            public double Ascent { get; private set; }
            public double Descent { get; private set; }

            /// <summary>
            /// Convert degrees to Radians
            /// </summary>
            /// <param name="x">Degrees</param>
            /// <returns>The equivalent in radians</returns>
            public static double Radians(double x)
            {
                return x * Math.PI / 180;
            }

            /// <summary>
            /// Calculate the distance between two places.
            /// </summary>
            /// <param name="lon1"></param>
            /// <param name="lat1"></param>
            /// <param name="lon2"></param>
            /// <param name="lat2"></param>
            /// <returns></returns>
            public static double DistanceBetweenPlaces(
                double lon1,
                double lat1,
                double lon2,
                double lat2)
            {
                double dlon = Radians(lon2 - lon1);
                double dlat = Radians(lat2 - lat1);

                double sinDlat = Math.Sin(dlat / 2);
                double sinDlon = Math.Sin(dlon / 2);
                double a = (sinDlat * sinDlat) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * (sinDlon * sinDlon);

                //double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
                double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                return angle * RADIUS;
            }
        }
    }
}
