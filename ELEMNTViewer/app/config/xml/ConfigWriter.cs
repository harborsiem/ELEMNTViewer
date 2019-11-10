using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Globalization;

namespace ELEMNTViewer {
    internal class ConfigWriter {
        private ConfigManager manager;
        private XmlDocument document;
        private XmlNode configNode;

        public ConfigWriter(ConfigManager manager) {
            if (manager == null) {
                throw new ArgumentNullException("manager");
            }
            this.manager = manager;
        }

        public bool Write(String fileName) {
            FileInfo file = new FileInfo(fileName);
            document = CreateDocument();
            configNode = document.CreateElement(ConfigXml.Configuration);
            if (manager.SensorConfigValues != null) {
                SetSensors();
            }
            SetSimpleNodeValue(ConfigXml.OpenDirTag, manager.OpenDir);
            document.AppendChild(configNode);
            SaveDocument(document, file);
            return true;
        }

        private void SetSimpleNodeValue(string tag, string value) {
            XmlNode node = document.CreateElement(tag);
            node.InnerText = value;
            configNode.AppendChild(node);
        }

        //private void SetConfigs(string tag, string value) {
        //    XmlNode node = document.CreateElement(tag);
        //    configNode.AppendChild(node);
        //    XmlAttribute attrValue = document.CreateAttribute(ConfigXml.AttributeValue);
        //    attrValue.Value = value;
        //    node.Attributes.SetNamedItem(attrValue);
        //}

        private void SetSensors() {
            XmlNode sensorsNode = document.CreateElement(ConfigXml.SensorsTag);
            SetElements(manager.SensorConfigValues, sensorsNode, ConfigXml.SensorTag);
            configNode.AppendChild(sensorsNode);
        }

        private void SetElements(IDictionary<String, ConfigData> list, XmlNode node, string tag) {
            IEnumerator<KeyValuePair<string, ConfigData>> it = list.GetEnumerator();
            while (it.MoveNext()) {
                KeyValuePair<string, ConfigData> config = (KeyValuePair<string, ConfigData>)it.Current;

                XmlNode node1 = document.CreateElement(tag);
                node.AppendChild(node1);

                XmlAttribute attrKey = document.CreateAttribute(ConfigXml.AttributeKey);
                XmlAttribute attrText = document.CreateAttribute(ConfigXml.AttributeText);
                XmlAttribute attrToolTipText = document.CreateAttribute(ConfigXml.AttributeToolTipText);
                XmlAttribute attrVisible = document.CreateAttribute(ConfigXml.AttributeVisible);
                XmlAttribute attrChecked = document.CreateAttribute(ConfigXml.AttributeChecked);
                XmlAttribute attrColor = document.CreateAttribute(ConfigXml.AttributeColor);
                XmlAttribute attrSmooth = document.CreateAttribute(ConfigXml.AttributeSmooth);

                attrKey.Value = config.Key;
                attrText.Value = config.Value.Text;
                attrToolTipText.Value = config.Value.ToolTipText;
                attrVisible.Value = XmlConvert.ToString(config.Value.Visible);
                attrChecked.Value = XmlConvert.ToString(config.Value.Checked);
                attrColor.Value = ConfigManager.GetColorString(config.Value.Color);
                attrSmooth.Value = XmlConvert.ToString(config.Value.Smooth);

                node1.Attributes.SetNamedItem(attrKey);
                node1.Attributes.SetNamedItem(attrText);
                node1.Attributes.SetNamedItem(attrToolTipText);
                node1.Attributes.SetNamedItem(attrVisible);
                node1.Attributes.SetNamedItem(attrChecked);
                node1.Attributes.SetNamedItem(attrColor);
                node1.Attributes.SetNamedItem(attrSmooth);
            }
        }

        public static XmlDocument CreateDocument() {
            XmlDocument document = new XmlDocument();
            //Create an XML declaration. 
            XmlDeclaration xmldecl;
            xmldecl = document.CreateXmlDeclaration("1.0", "UTF-8", String.Empty);
            xmldecl.Encoding = "UTF-8";
            document.AppendChild(xmldecl);

            return document;
        }

        public static void SaveDocument(XmlDocument document, FileInfo file) {
            try {
                document.Save(file.FullName);
            }
            catch (XmlException e) {
                AppExtension.PrintStackTrace(e);
            }
        }
    }
}
