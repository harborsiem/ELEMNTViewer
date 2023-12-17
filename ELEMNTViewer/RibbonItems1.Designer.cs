//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using RibbonLib;
using RibbonLib.Controls;

namespace RibbonLib.Controls
{
    partial class RibbonItems1
    {
        private static class Cmd
        {
            public const uint cmdDialogApplication = 15;
            public const uint cmdTabStart = 2;
            public const uint cmdGroupSelectFunction = 6;
            public const uint cmdCheckMonthly = 12;
            public const uint cmdCheckAntDevice = 9;
            public const uint cmdGroupSelect2 = 11;
            public const uint cmdComboYear = 3;
            public const uint cmdGroupSelect = 5;
            public const uint cmdComboMonth = 4;
            public const uint cmdGroupAnt = 10;
            public const uint cmdComboAnt = 13;
            public const uint cmdComboAntName = 14;
        }

        // ContextPopup CommandName

        public Ribbon Ribbon { get; private set; }
        public RibbonApplicationMenu DialogApplication { get; private set; }
        public RibbonTab TabStart { get; private set; }
        public RibbonGroup GroupSelectFunction { get; private set; }
        public RibbonCheckBox CheckMonthly { get; private set; }
        public RibbonCheckBox CheckAntDevice { get; private set; }
        public RibbonGroup GroupSelect2 { get; private set; }
        public RibbonComboBox ComboYear { get; private set; }
        public RibbonGroup GroupSelect { get; private set; }
        public RibbonComboBox ComboMonth { get; private set; }
        public RibbonGroup GroupAnt { get; private set; }
        public RibbonComboBox ComboAnt { get; private set; }
        public RibbonComboBox ComboAntName { get; private set; }

        public RibbonItems1(Ribbon ribbon)
        {
            if (ribbon == null)
                throw new ArgumentNullException(nameof(ribbon), "Parameter is null");
            this.Ribbon = ribbon;
            DialogApplication = new RibbonApplicationMenu(ribbon, Cmd.cmdDialogApplication);
            TabStart = new RibbonTab(ribbon, Cmd.cmdTabStart);
            GroupSelectFunction = new RibbonGroup(ribbon, Cmd.cmdGroupSelectFunction);
            CheckMonthly = new RibbonCheckBox(ribbon, Cmd.cmdCheckMonthly);
            CheckAntDevice = new RibbonCheckBox(ribbon, Cmd.cmdCheckAntDevice);
            GroupSelect2 = new RibbonGroup(ribbon, Cmd.cmdGroupSelect2);
            ComboYear = new RibbonComboBox(ribbon, Cmd.cmdComboYear);
            GroupSelect = new RibbonGroup(ribbon, Cmd.cmdGroupSelect);
            ComboMonth = new RibbonComboBox(ribbon, Cmd.cmdComboMonth);
            GroupAnt = new RibbonGroup(ribbon, Cmd.cmdGroupAnt);
            ComboAnt = new RibbonComboBox(ribbon, Cmd.cmdComboAnt);
            ComboAntName = new RibbonComboBox(ribbon, Cmd.cmdComboAntName);
        }

    }
}