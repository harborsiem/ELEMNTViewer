using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;

namespace ELEMNTViewer {
    class AboutModelView {
        public string DisplayAssemblyTitle {
            get {
                return String.Format(CultureInfo.CurrentCulture, "About {0}", AssemblyTitle);
            }
        }

        private string AssemblyTitle {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!string.IsNullOrEmpty(titleAttribute.Title)) {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string DisplayAssemblyVersion {
            get {
                return String.Format(CultureInfo.CurrentCulture, "Version: {0}", AssemblyVersion);
            }
        }

        private string AssemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string DisplayAssemblyFileVersion {
            get {
                return String.Format(CultureInfo.CurrentCulture, "Version: {0}", AssemblyFileVersion);
            }
        }

        private string AssemblyFileVersion {
            get {
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                return info.FileVersion;
            }
        }

        public string DisplayAuthor {
            get {
                return "Author: Hartmut Borkenhagen";
            }
        }

        public string DisplayModified {
            get {
                return "Modified: ";
            }
        }

        public string AssemblyDescription {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return string.Empty;
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return string.Empty;
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return string.Empty;
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return string.Empty;
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public void ShowProjectPage(Action viewAction) {
            Process.Start("https://github.com/harborsiem/ELEMNTViewer");
        }

        public void Close(Action callback) {
            callback.Invoke();
        }

    }
}
