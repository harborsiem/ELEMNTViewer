using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Globalization;

namespace ELEMNTViewer {
    internal class ConfigReader {
        private ConfigManager manager;
        private XmlDocument document;

        public ConfigReader(ConfigManager manager) {
            if (manager == null) {
                throw new ArgumentNullException("manager");
            }
            this.manager = manager;
        }

        public bool Parse(String fileName) {
            try {
                FileInfo file = new FileInfo(fileName);
                if (file.Exists) {
                    document = GetDocument(file);
                    XmlNodeList xList = document.SelectNodes("configuration");
                    XmlNode node0 = xList.Item(0);
                    foreach (XmlNode node1 in node0.ChildNodes) {
                        switch (node1.Name) {
                            case ConfigXml.SensorsTag:
                                ParseSensors(node1);
                                break;
                            case ConfigXml.OpenDirTag:
                                manager.OpenDir = node1.InnerText;
                                break;
                            default:
                                break;
                        }
                    }
                    return true;
                }
            }
            catch (Exception e) {
                AppExtension.PrintStackTrace(e);
            }
            return false;
        }

        //private static string ParseConfigs(XmlNode child) {
        //    XmlNamedNodeMap param = child.Attributes;

        //    XmlNode nodeValue = param.GetNamedItem(ConfigXml.AttributeValue);
        //    return nodeValue.Value;
        //}

        private void ParseSensors(XmlNode node) {
            if (manager.SensorConfigValues != null && node != null) {
                ParseElements(manager.SensorConfigValues, node, ConfigXml.SensorTag);
            }
        }

        private static void ParseElements(IDictionary<String, ConfigData> config, XmlNode node, string tag) {
            XmlNodeList nodeList = node.ChildNodes;
            for (int i = 0; i < nodeList.Count; i++) {
                XmlNode child = nodeList.Item(i);
                String nodeName = child.Name;

                if (nodeName.Equals(tag)) {
                    XmlNamedNodeMap param = child.Attributes;

                    XmlNode nodeKey = param.GetNamedItem(ConfigXml.AttributeKey);
                    XmlNode nodeText = param.GetNamedItem(ConfigXml.AttributeText);
                    XmlNode nodeToolTipText = param.GetNamedItem(ConfigXml.AttributeToolTipText);
                    XmlNode nodeVisible = param.GetNamedItem(ConfigXml.AttributeVisible);
                    XmlNode nodeChecked = param.GetNamedItem(ConfigXml.AttributeChecked);
                    XmlNode nodeColor = param.GetNamedItem(ConfigXml.AttributeColor);
                    XmlNode nodeSmooth = param.GetNamedItem(ConfigXml.AttributeSmooth);
                    if (nodeKey != null && nodeText != null && nodeVisible != null && nodeChecked != null && nodeColor != null && nodeSmooth != null) {
                        string key = nodeKey.Value;
                        string text = nodeText.Value;
                        string visible = nodeVisible.Value;
                        string checked0 = nodeChecked.Value;
                        string color = nodeColor.Value;
                        string smooth = nodeSmooth.Value;
                        string toolTipText;
                        if (nodeToolTipText == null) {
                            toolTipText = string.Copy(text);
                        } else {
                            toolTipText = nodeToolTipText.Value;
                        }
                        if (key != null && text != null && visible != null && checked0 != null && color != null) {
                            ConfigData data = new ConfigData();
                            data.Text = text;
                            data.ToolTipText = toolTipText;
                            data.Visible = XmlConvert.ToBoolean(visible.ToLowerInvariant());
                            data.Checked = XmlConvert.ToBoolean(checked0.ToLowerInvariant());
                            data.Color = ConfigManager.GetColor(color);
                            data.Smooth = XmlConvert.ToInt32(smooth);

                            config.Add(key, data);
                        }
                    }
                }
            }
        }

        private static XmlDocument GetDocument(FileInfo file) {
            XmlDocument document = new XmlDocument();
            try {
                document.Load(file.FullName);
            }
            catch (IOException ioe) {
                AppExtension.PrintStackTrace(ioe);
            }
            return document;
        }
    }
}
