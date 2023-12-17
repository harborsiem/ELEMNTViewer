using System;
using System.Collections.Generic;
using System.Text;
using RibbonLib.Controls;

namespace ELEMNTViewer
{
    class ConnectedItems
    {
        private RibbonCheckBox _checkBox;
        private RibbonComboBox _comboBox;
        private string _seriesName;
        private int _smoothValue;

        public ConnectedItems(RibbonCheckBox checkBox, RibbonComboBox comboBox, int smoothValue, string seriesName)
        {
            _checkBox = checkBox;
            _comboBox = comboBox;
            _smoothValue = smoothValue;
            _seriesName = seriesName;
        }

        public RibbonCheckBox CheckBox => _checkBox;
        public RibbonComboBox ComboBox => _comboBox;
        public string SeriesName => _seriesName;
        public int InitialSmooth => _smoothValue;
    }
}
