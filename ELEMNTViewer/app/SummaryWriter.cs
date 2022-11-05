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
        public SummaryWriter()
        {
        }

        public void WriteSummaries(int year, List<Summary> values)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(Path.Combine(Settings.ThisLocalAppData, "Summaries" + year.ToString() + ".xml"), settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("FitValues");
            foreach (Summary value in values)
            {
                writer.WriteStartElement("Value");
                writer.WriteAttributeString("Filename", value.Filename);
                writer.WriteAttributeString("Distance", XmlConvert.ToString(value.Distance));
                writer.WriteAttributeString("Ascent", XmlConvert.ToString(value.Ascent));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
