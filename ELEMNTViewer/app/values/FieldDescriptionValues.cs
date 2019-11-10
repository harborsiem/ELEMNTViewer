using Dynastream.Fit;

namespace ELEMNTViewer {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class FieldDescriptionValues {
        private byte? developerDataIndex;
        private byte? fieldDefinitionNumber;
        private byte? fitBaseTypeId;
        private string[] fieldName;
        private string[] units;

        public static void OnFieldDescriptionMesg(object sender, MesgEventArgs e) {
            FieldDescriptionValues values = new FieldDescriptionValues();
            FieldDescriptionMesg mesg = (FieldDescriptionMesg)e.mesg;
            try {
                values.developerDataIndex = mesg.GetDeveloperDataIndex();
                values.fieldDefinitionNumber = mesg.GetFieldDefinitionNumber();
                values.fitBaseTypeId = mesg.GetFitBaseTypeId();
                int num3 = mesg.GetNumFieldValues(3);
                values.fieldName = new string[num3];
                values.units = new string[num3];
                for (int i = 0; i < num3; i++) {
                    values.fieldName[i] = (mesg.GetFieldNameAsString(i));
                    values.units[i] = (mesg.GetUnitsAsString(i));
                }
            }
            catch (FitException exception) {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.FieldDescriptionValues.Add(values);
        }

        public byte? DeveloperDataIndex { get { return developerDataIndex; } }
        public byte? FieldDefinitionNumber { get { return fieldDefinitionNumber; } }
        public byte? FitBaseTypeId { get { return fitBaseTypeId; } }
        public string FitBaseType {
            get {
                if (fitBaseTypeId != null) {
                    return FitConvert.GetConstName(typeof(FitBaseType), (byte)fitBaseTypeId);
                }
                return null;
            }
        }
        public string[] FieldName { get { return fieldName; } }
        public string[] Units {
            get {
                return units;
            }
        }
    }
}
