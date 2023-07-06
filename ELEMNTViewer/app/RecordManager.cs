namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Xml;
    using System.Globalization;

    class RecordManager
    {
        const string CrLf = "";
        private List<RecordValues> _recordValuesList = new List<RecordValues>();

        public List<RecordValues> RecordValuesList { get { return _recordValuesList; } }

        public void Clear()
        {
            _recordValuesList.Clear();
            HasValidPowerFlag = false;
        }

        public bool HasValidPowerFlag { get; set; }

        public int Count
        {
            get { return _recordValuesList.Count; }
        }

        private void WithXmlDocument(string fileName, string trackName, int recordIncrement)
        {
            XmlDocument document = new XmlDocument();
            document.PreserveWhitespace = false;
            //Create an XML declaration. 
            XmlDeclaration xmldecl;
            xmldecl = document.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldecl.Encoding = "utf-8";
            document.AppendChild(xmldecl);
            XmlElement gpxNode = document.CreateElement("gpx");
            gpxNode.SetAttribute("xmlns", "http://www.topografix.com/GPX/1/1");
            gpxNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            gpxNode.SetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd");

            XmlAttribute creatorAttr = document.CreateAttribute("creator");
            XmlAttribute versionAttr = document.CreateAttribute("version");
            creatorAttr.Value = "ELEMNTViewer";
            versionAttr.Value = "1.1";
            gpxNode.Attributes.SetNamedItem(creatorAttr);
            gpxNode.Attributes.SetNamedItem(versionAttr);

            XmlNode metadataNode = document.CreateElement("metadata");
            XmlNode nameNode = document.CreateElement("name");
            XmlCDataSection cData = document.CreateCDataSection(trackName);
            metadataNode.AppendChild(nameNode);
            nameNode.AppendChild(cData);
            gpxNode.AppendChild(metadataNode);

            XmlNode trkNode = document.CreateElement("trk");
            XmlNode nameNodeTrk = document.CreateElement("name");
            nameNodeTrk.InnerText = trackName;
            trkNode.AppendChild(nameNodeTrk);
            XmlNode trksegNode = document.CreateElement("trkseg");
            gpxNode.AppendChild(trkNode);
            trkNode.AppendChild(trksegNode);

            double lon;
            double lat;
            double ele;
            DateTime time;
            for (int i = 0; i < _recordValuesList.Count; i += recordIncrement)
            {
                RecordValues values = _recordValuesList[i];
                lon = values.PositionLong;
                lat = values.PositionLat;
                ele = values.Altitude;
                time = values.Timestamp;
                if (!(lat == 0.0 && lon == 0.0 && ele == 0.0))
                {
                    XmlNode trkptNode = document.CreateElement("trkpt");
                    XmlAttribute lonAttr = document.CreateAttribute("lon");
                    lonAttr.Value = lon.ToString(CultureInfo.InvariantCulture);
                    XmlAttribute latAttr = document.CreateAttribute("lat");
                    latAttr.Value = lat.ToString(CultureInfo.InvariantCulture);
                    trkptNode.Attributes.SetNamedItem(lonAttr);
                    trkptNode.Attributes.SetNamedItem(latAttr);
                    XmlNode eleNode = document.CreateElement("ele");
                    eleNode.InnerText = ele.ToString(CultureInfo.InvariantCulture);
                    trkptNode.AppendChild(eleNode);
                    long utcLong = time.ToFileTimeUtc();
                    DateTime utcTime = DateTime.FromFileTimeUtc(utcLong);
                    XmlNode timeNode = document.CreateElement("time");
                    timeNode.InnerText = utcTime.ToString("s") + "Z";
                    trkptNode.AppendChild(timeNode);
                    trksegNode.AppendChild(trkptNode);
                }
            }

            document.AppendChild(gpxNode);
            document.Save(fileName);

        }

        private void WithXmlWriter(string fileName, string trackName, int recordIncrement)
        {
            Stream stream = File.Create(fileName);
            //StringBuilder builder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = string.Empty;
            settings.Encoding = Encoding.UTF8;
            settings.NewLineOnAttributes = false;
            settings.CloseOutput = true;
            settings.WriteEndDocumentOnClose = true;

            XmlWriter writer = XmlWriter.Create(stream, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("gpx", "http://www.topografix.com/GPX/1/1");
            writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd");
            writer.WriteAttributeString("creator", "ELEMNTViewer");
            writer.WriteAttributeString("version", "1.1");

            writer.WriteStartElement("metadata");
            writer.WriteStartElement("name");
            writer.WriteCData(trackName);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("trk");
            writer.WriteElementString("name", trackName);
            writer.WriteStartElement("trkseg");

            double lon;
            double lat;
            double ele;
            for (int i = 0; i < _recordValuesList.Count; i += recordIncrement)
            {
                RecordValues values = _recordValuesList[i];
                lon = values.PositionLong;
                lat = values.PositionLat;
                ele = values.Altitude;
                writer.WriteStartElement("trkpt");
                writer.WriteAttributeString("lon", lon.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("lat", lat.ToString(CultureInfo.InvariantCulture));
                writer.WriteElementString("ele", ele.ToString(CultureInfo.InvariantCulture));
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteEndElement();
            //writer.WriteEndDocument();
            writer.Close();

        }

        public void WriteGpx(FileInfo file, string trackName)
        {
            string fileName;
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            if (string.IsNullOrWhiteSpace(trackName))
            {
                trackName = "unknown";
            }
            fileName = file.FullName;
            //WithXmlWriter(fileName, trackName, 10);
            WithXmlDocument(fileName, trackName, 1);
        }
    }
}
