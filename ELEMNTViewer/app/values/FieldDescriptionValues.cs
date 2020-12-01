using Dynastream.Fit;

namespace ELEMNTViewer
{
    using System;
    using System.Collections.Generic;
    //using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class FieldDescriptionValues
    {
        private byte? _developerDataIndex;
        private byte? _fieldDefinitionNumber;
        private byte? _fitBaseTypeId;
        private string[] _fieldName;
        private string[] _units;

        public static void OnFieldDescriptionMesg(object sender, MesgEventArgs e)
        {
            FieldDescriptionValues values = new FieldDescriptionValues();
            FieldDescriptionMesg mesg = (FieldDescriptionMesg)e.mesg;
            try
            {
                values._developerDataIndex = mesg.GetDeveloperDataIndex();
                values._fieldDefinitionNumber = mesg.GetFieldDefinitionNumber();
                values._fitBaseTypeId = mesg.GetFitBaseTypeId();
                int num3 = mesg.GetNumFieldValues(3);
                values._fieldName = new string[num3];
                values._units = new string[num3];
                for (int i = 0; i < num3; i++)
                {
                    values._fieldName[i] = (mesg.GetFieldNameAsString(i));
                    values._units[i] = (mesg.GetUnitsAsString(i));
                }
            }
            catch (FitException exception)
            {
                Console.WriteLine("\tOnFileIDMesg Error {0}", exception.Message);
                Console.WriteLine("\t{0}", exception.InnerException);
            }
            DataManager.Instance.FieldDescriptionValues.Add(values);
        }

        public byte? DeveloperDataIndex { get { return _developerDataIndex; } }
        public byte? FieldDefinitionNumber { get { return _fieldDefinitionNumber; } }
        public byte? FitBaseTypeId { get { return _fitBaseTypeId; } }
        public string FitBaseType
        {
            get
            {
                if (_fitBaseTypeId != null)
                {
                    return FitConvert.GetConstName(typeof(FitBaseType), (byte)_fitBaseTypeId);
                }
                return null;
            }
        }
        public string[] FieldName { get { return _fieldName; } }
        public string[] Units { get { return _units; } }
    }
}
