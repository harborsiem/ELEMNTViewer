using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer {
    class LapManager {
        private List<LapValues> lapList = new List<LapValues>();
        private List<int> endLapRecordIndex = new List<int>();

        //public List<LapValues> LapList { get { return lapList; } }

        public void Add(LapValues item, int endIndex) {
            lapList.Add(item);
            endLapRecordIndex.Add(endIndex);
        }

        public void Clear() {
            lapList.Clear();
            endLapRecordIndex.Clear();
        }

        public int Count {
            get { return lapList.Count; }
        }

        public LapValues[] LapArray() {
            return lapList.ToArray();
        }

        public int GetStartIndex(int lap) {
            if (lap <= 0 || lap > lapList.Count) {
                throw new ArgumentException("Lap is not possible", nameof(lap));
            }
            int result = 0;
            if (lap - 2 >= 0) {
                result = endLapRecordIndex[lap - 2];
            }
            return result;
        }

        public int GetEndIndex(int lap) {
            if (lap <= 0 || lap > lapList.Count) {
                throw new ArgumentException("Lap is not possible", nameof(lap));
            }
            if (lap == 0) {
                return endLapRecordIndex[endLapRecordIndex.Count - 1];
            }
            return endLapRecordIndex[lap - 1];
        }
    }
}
