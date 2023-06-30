using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ELEMNTViewer
{
    class SummaryWriter
    {
        public const string FitValues = "FitValues";
        public const string Value = "Value";
        public const string Distance = "Distance";
        public const string Filename = "Filename";
        public const string Ascent = "Ascent";
        public const string AvgSpeed = "AvgSpeed";
        public const string TotalTimerTime = "TotalTimerTime";
        public const string AntPlus = "AntPlus";
        public const string Device = "Device";
        public const string Id = "Id";
        public const string Name = "Name";

        public SummaryWriter()
        {
        }

        public void WriteSummaries(int year, List<Summary> values)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(Path.Combine(Settings.ThisLocalAppData, "Summaries" + year.ToString() + ".xml"), settings);
            writer.WriteStartDocument();
            writer.WriteStartElement(FitValues);
            foreach (Summary value in values)
            {
                writer.WriteStartElement(Value);
                writer.WriteAttributeString(Filename, value.Filename);
                writer.WriteAttributeString(Distance, XmlConvert.ToString(value.Distance));
                writer.WriteAttributeString(Ascent, XmlConvert.ToString(value.Ascent));
                writer.WriteAttributeString(AvgSpeed, XmlConvert.ToString(value.AvgSpeed));
                writer.WriteAttributeString(TotalTimerTime, XmlConvert.ToString(value.TotalTimerTime));
                //if (value.AntDevices.Count > 0)
                {
                    writer.WriteStartElement(AntPlus);
                    for (int i = 0; i < value.AntDevices.Count; i++)
                    {
                        AntDevice device = value.AntDevices[i];
                        writer.WriteStartElement(Device + i.ToString());
                        writer.WriteAttributeString(Id, XmlConvert.ToString(device.Id));
                        writer.WriteAttributeString(Name, (device.ProductName));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
