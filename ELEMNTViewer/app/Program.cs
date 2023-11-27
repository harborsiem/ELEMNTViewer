using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;

namespace ELEMNTViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Settings settings = Settings.Instance;
            settings.Read();
            bool localized = settings.Localized;
            if (!localized)
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture; //CultureInfo.CreateSpecificCulture("it");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
