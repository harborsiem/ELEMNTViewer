using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEMNTViewer
{
    class LapManager
    {
        private List<LapValues> _lapList = new List<LapValues>();
        private List<int> _endLapRecordIndex = new List<int>();

        //public List<LapValues> LapList { get { return _lapList; } }

        public void Add(LapValues item, int endIndex)
        {
            _lapList.Add(item);
            _endLapRecordIndex.Add(endIndex);
        }

        public void Clear()
        {
            _lapList.Clear();
            _endLapRecordIndex.Clear();
        }

        public int Count
        {
            get { return _lapList.Count; }
        }

        public LapValues[] LapArray()
        {
            return _lapList.ToArray();
        }

        public int GetStartIndex(int lap)
        {
            if (lap <= 0 || lap > _lapList.Count)
            {
                throw new ArgumentException("Lap is not possible", nameof(lap));
            }
            int result = 0;
            if (lap - 2 >= 0)
            {
                result = _endLapRecordIndex[lap - 2];
            }
            return result;
        }

        public int GetEndIndex(int lap)
        {
            if (lap <= 0 || lap > _lapList.Count)
            {
                throw new ArgumentException("Lap is not possible", nameof(lap));
            }
            if (lap == 0)
            {
                return _endLapRecordIndex[_endLapRecordIndex.Count - 1];
            }
            return _endLapRecordIndex[lap - 1];
        }
    }
}
