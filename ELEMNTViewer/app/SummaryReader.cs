using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;
//using SummaryWriter;

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
                if (ele.Name.LocalName == SummaryWriter.FitValues)
                {
                    foreach (XElement element in ele.Elements())
                    {
                        if (element.Name.LocalName == SummaryWriter.Value)
                        {
                            string filename = element.Attribute(SummaryWriter.Filename).Value;
                            double distance = XmlConvert.ToDouble(element.Attribute(SummaryWriter.Distance).Value);
                            double ascent = XmlConvert.ToDouble(element.Attribute(SummaryWriter.Ascent).Value);
                            double avgSpeed = XmlConvert.ToDouble(element.Attribute(SummaryWriter.AvgSpeed).Value);
                            TimeSpan totalTimerTime = XmlConvert.ToTimeSpan(element.Attribute(SummaryWriter.TotalTimerTime).Value);
                            Summary summary = new Summary() { Filename = filename, Distance = distance, Ascent = ascent, AvgSpeed = avgSpeed, TotalTimerTime = totalTimerTime };
                            foreach (XElement antPlus in element.Elements())
                            {
                                if (antPlus.Name.LocalName == SummaryWriter.AntPlus)
                                {
                                    foreach (XElement device in antPlus.Elements())
                                    {
                                        AntDevice antDevice = new AntDevice();
                                        antDevice.Id = XmlConvert.ToUInt16(device.Attribute(SummaryWriter.Id).Value);
                                        antDevice.ProductName = (device.Attribute(SummaryWriter.Name).Value);
                                        summary.AntDevices.Add(antDevice);
                                    }
                                }
                            }
                            result.Add(summary);
                        }
                    }
                }
            }
            return result;
        }
    }
}
