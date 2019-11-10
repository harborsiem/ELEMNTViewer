using Dynastream.Fit;

namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class WorkoutValues {
        private string wktName;

        public static void OnWorkoutMesg(object sender, MesgEventArgs e) {
            WorkoutValues values = new WorkoutValues();
            WorkoutMesg mesg = (WorkoutMesg)e.mesg;
            try {
                values.wktName = mesg.GetWktNameAsString();
            }
            catch (FitException exception) {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.WorkoutValues.Add(values);
        }

        public string WktName { get { return wktName; } }
    }
}
