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
    class SummaryReader
    {
        private XDocument _doc;

        public SummaryReader(string fileName)
        {
            if (!File.Exists(fileName))
                throw new ArgumentException("FileName does not exist", nameof(fileName));
            _doc = XDocument.Load(fileName);
        }

        public List<Summary> ReadSummeries()
        {
            List<Summary> result = new List<Summary>();
            foreach (XElement ele in _doc.Elements())
            {
                if (ele.Name.LocalName == "FitValues")
                {
                    foreach (XElement element in ele.Elements())
                    {
                        if (element.Name.LocalName == "Value")
                        {
                            string filename = element.Attribute("Filename").Value;
                            double distance = XmlConvert.ToDouble(element.Attribute("Distance").Value);
                            double ascent = XmlConvert.ToDouble(element.Attribute("Ascent").Value);
                            result.Add(new Summary(filename, distance, ascent));
                        }
                    }
                }
            }
            return result;
        }
    }
}
