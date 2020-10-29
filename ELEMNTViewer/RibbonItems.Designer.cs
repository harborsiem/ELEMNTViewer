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
using RibbonLib.Interop;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private static class Cmd
        {
            public const uint cmdApplication = 3;
            public const uint cmdButtonOpen = 15;
            public const uint cmdButtonSaveGpx = 16;
            public const uint cmdButtonAbout = 48;
            public const uint cmdButtonExit = 17;
            public const uint cmdButtonHelp = 18;
            public const uint cmdQAT = 2;
            public const uint cmdButtonSession = 43;
            public const uint cmdButtonLaps = 44;
            public const uint CmdTabChart = 4;
            public const uint cmdGroupChartSelect = 28;
            public const uint cmdComboSelect = 29;
            public const uint cmdToggleSmooth = 47;
            public const uint cmdButtonSetSettings = 64;
            public const uint cmdGroupChartSpeed = 7;
            public const uint cmdButtonSpeed = 22;
            public const uint cmdButtonCadence = 23;
            public const uint cmdGroupChartPower = 40;
            public const uint cmdButtonPower = 9;
            public const uint cmdButtonLRBalance = 10;
            public const uint cmdButtonHeartRate = 19;
            public const uint cmdGroupChartPower1 = 6;
            public const uint cmdComboPower = 30;
            public const uint cmdComboLRBalance = 31;
            public const uint cmdGroupChartEffectiveness = 41;
            public const uint cmdButtonLTorqueEff = 13;
            public const uint cmdButtonRTorqueEff = 14;
            public const uint cmdGroupChartEffectiveness1 = 25;
            public const uint cmdComboLTorque = 34;
            public const uint cmdComboRTorque = 35;
            public const uint cmdGroupChartSmoothness = 42;
            public const uint cmdButtonLSmoothness = 11;
            public const uint cmdButtonRSmoothness = 12;
            public const uint cmdGroupChartSmoothness1 = 26;
            public const uint cmdComboLSmoothness = 32;
            public const uint cmdComboRSmoothness = 33;
            public const uint cmdGroupChartClimbing = 8;
            public const uint cmdButtonAltitude = 20;
            public const uint cmdButtonGrade = 21;
            public const uint cmdGroupChartExtras = 27;
            public const uint cmdButtonTemperature = 24;
            public const uint cmdTabView = 5;
            public const uint cmdGroupTotals = 37;
            public const uint cmdGroupExtras = 39;
            public const uint cmdButtonMyExtras = 49;
            public const uint cmdGroupMaps = 63;
            public const uint cmdButtonMap = 62;
            public const uint cmdGroupZones = 38;
            public const uint cmdButtonHeartRateZones = 45;
            public const uint cmdButtonPowerZones = 46;
            public const uint cmdGroupIntern = 50;
            public const uint cmdButtonActivity = 52;
            public const uint cmdButtonDeveloperDataId = 53;
            public const uint cmdButtonDeviceInfo = 54;
            public const uint cmdButtonEvent = 55;
            public const uint cmdButtonFieldDescription = 56;
            public const uint cmdButtonFileId = 57;
            public const uint cmdButtonSport = 58;
            public const uint cmdButtonWorkout = 59;
            public const uint cmdButtonWahooFF00 = 60;
            public const uint cmdButtonWahooFF01 = 61;
        }

        // ContextPopup CommandName

        public Ribbon Ribbon { get; private set; }
        public RibbonApplicationMenu Application { get; private set; }
        public RibbonButton ButtonOpen { get; private set; }
        public RibbonButton ButtonSaveGpx { get; private set; }
        public RibbonButton ButtonAbout { get; private set; }
        public RibbonButton ButtonExit { get; private set; }
        public RibbonHelpButton ButtonHelp { get; private set; }
        public RibbonQuickAccessToolbar QAT { get; private set; }
        public RibbonButton ButtonSession { get; private set; }
        public RibbonButton ButtonLaps { get; private set; }
        public RibbonTab TabChart { get; private set; }
        public RibbonGroup GroupChartSelect { get; private set; }
        public RibbonComboBox ComboSelect { get; private set; }
        public RibbonToggleButton ToggleSmooth { get; private set; }
        public RibbonButton ButtonSetSettings { get; private set; }
        public RibbonGroup GroupChartSpeed { get; private set; }
        public RibbonCheckBox ButtonSpeed { get; private set; }
        public RibbonCheckBox ButtonCadence { get; private set; }
        public RibbonGroup GroupChartPower { get; private set; }
        public RibbonCheckBox ButtonPower { get; private set; }
        public RibbonCheckBox ButtonLRBalance { get; private set; }
        public RibbonCheckBox ButtonHeartRate { get; private set; }
        public RibbonGroup GroupChartPower1 { get; private set; }
        public RibbonComboBox ComboPower { get; private set; }
        public RibbonComboBox ComboLRBalance { get; private set; }
        public RibbonGroup GroupChartEffectiveness { get; private set; }
        public RibbonCheckBox ButtonLTorqueEff { get; private set; }
        public RibbonCheckBox ButtonRTorqueEff { get; private set; }
        public RibbonGroup GroupChartEffectiveness1 { get; private set; }
        public RibbonComboBox ComboLTorque { get; private set; }
        public RibbonComboBox ComboRTorque { get; private set; }
        public RibbonGroup GroupChartSmoothness { get; private set; }
        public RibbonCheckBox ButtonLSmoothness { get; private set; }
        public RibbonCheckBox ButtonRSmoothness { get; private set; }
        public RibbonGroup GroupChartSmoothness1 { get; private set; }
        public RibbonComboBox ComboLSmoothness { get; private set; }
        public RibbonComboBox ComboRSmoothness { get; private set; }
        public RibbonGroup GroupChartClimbing { get; private set; }
        public RibbonCheckBox ButtonAltitude { get; private set; }
        public RibbonCheckBox ButtonGrade { get; private set; }
        public RibbonGroup GroupChartExtras { get; private set; }
        public RibbonCheckBox ButtonTemperature { get; private set; }
        public RibbonTab TabView { get; private set; }
        public RibbonGroup GroupTotals { get; private set; }
        public RibbonGroup GroupExtras { get; private set; }
        public RibbonButton ButtonMyExtras { get; private set; }
        public RibbonGroup GroupMaps { get; private set; }
        public RibbonButton ButtonMap { get; private set; }
        public RibbonGroup GroupZones { get; private set; }
        public RibbonButton ButtonHeartRateZones { get; private set; }
        public RibbonButton ButtonPowerZones { get; private set; }
        public RibbonGroup GroupIntern { get; private set; }
        public RibbonButton ButtonActivity { get; private set; }
        public RibbonButton ButtonDeveloperDataId { get; private set; }
        public RibbonButton ButtonDeviceInfo { get; private set; }
        public RibbonButton ButtonEvent { get; private set; }
        public RibbonButton ButtonFieldDescription { get; private set; }
        public RibbonButton ButtonFileId { get; private set; }
        public RibbonButton ButtonSport { get; private set; }
        public RibbonButton ButtonWorkout { get; private set; }
        public RibbonButton ButtonWahooFF00 { get; private set; }
        public RibbonButton ButtonWahooFF01 { get; private set; }

        public RibbonItems(Ribbon ribbon)
        {
            if (ribbon == null)
                throw new ArgumentNullException(nameof(ribbon), "Parameter is null");
            this.Ribbon = ribbon;
            Application = new RibbonApplicationMenu(ribbon, Cmd.cmdApplication);
            ButtonOpen = new RibbonButton(ribbon, Cmd.cmdButtonOpen);
            ButtonSaveGpx = new RibbonButton(ribbon, Cmd.cmdButtonSaveGpx);
            ButtonAbout = new RibbonButton(ribbon, Cmd.cmdButtonAbout);
            ButtonExit = new RibbonButton(ribbon, Cmd.cmdButtonExit);
            ButtonHelp = new RibbonHelpButton(ribbon, Cmd.cmdButtonHelp);
            QAT = new RibbonQuickAccessToolbar(ribbon, Cmd.cmdQAT);
            ButtonSession = new RibbonButton(ribbon, Cmd.cmdButtonSession);
            ButtonLaps = new RibbonButton(ribbon, Cmd.cmdButtonLaps);
            TabChart = new RibbonTab(ribbon, Cmd.CmdTabChart);
            GroupChartSelect = new RibbonGroup(ribbon, Cmd.cmdGroupChartSelect);
            ComboSelect = new RibbonComboBox(ribbon, Cmd.cmdComboSelect);
            ToggleSmooth = new RibbonToggleButton(ribbon, Cmd.cmdToggleSmooth);
            ButtonSetSettings = new RibbonButton(ribbon, Cmd.cmdButtonSetSettings);
            GroupChartSpeed = new RibbonGroup(ribbon, Cmd.cmdGroupChartSpeed);
            ButtonSpeed = new RibbonCheckBox(ribbon, Cmd.cmdButtonSpeed);
            ButtonCadence = new RibbonCheckBox(ribbon, Cmd.cmdButtonCadence);
            GroupChartPower = new RibbonGroup(ribbon, Cmd.cmdGroupChartPower);
            ButtonPower = new RibbonCheckBox(ribbon, Cmd.cmdButtonPower);
            ButtonLRBalance = new RibbonCheckBox(ribbon, Cmd.cmdButtonLRBalance);
            ButtonHeartRate = new RibbonCheckBox(ribbon, Cmd.cmdButtonHeartRate);
            GroupChartPower1 = new RibbonGroup(ribbon, Cmd.cmdGroupChartPower1);
            ComboPower = new RibbonComboBox(ribbon, Cmd.cmdComboPower);
            ComboLRBalance = new RibbonComboBox(ribbon, Cmd.cmdComboLRBalance);
            GroupChartEffectiveness = new RibbonGroup(ribbon, Cmd.cmdGroupChartEffectiveness);
            ButtonLTorqueEff = new RibbonCheckBox(ribbon, Cmd.cmdButtonLTorqueEff);
            ButtonRTorqueEff = new RibbonCheckBox(ribbon, Cmd.cmdButtonRTorqueEff);
            GroupChartEffectiveness1 = new RibbonGroup(ribbon, Cmd.cmdGroupChartEffectiveness1);
            ComboLTorque = new RibbonComboBox(ribbon, Cmd.cmdComboLTorque);
            ComboRTorque = new RibbonComboBox(ribbon, Cmd.cmdComboRTorque);
            GroupChartSmoothness = new RibbonGroup(ribbon, Cmd.cmdGroupChartSmoothness);
            ButtonLSmoothness = new RibbonCheckBox(ribbon, Cmd.cmdButtonLSmoothness);
            ButtonRSmoothness = new RibbonCheckBox(ribbon, Cmd.cmdButtonRSmoothness);
            GroupChartSmoothness1 = new RibbonGroup(ribbon, Cmd.cmdGroupChartSmoothness1);
            ComboLSmoothness = new RibbonComboBox(ribbon, Cmd.cmdComboLSmoothness);
            ComboRSmoothness = new RibbonComboBox(ribbon, Cmd.cmdComboRSmoothness);
            GroupChartClimbing = new RibbonGroup(ribbon, Cmd.cmdGroupChartClimbing);
            ButtonAltitude = new RibbonCheckBox(ribbon, Cmd.cmdButtonAltitude);
            ButtonGrade = new RibbonCheckBox(ribbon, Cmd.cmdButtonGrade);
            GroupChartExtras = new RibbonGroup(ribbon, Cmd.cmdGroupChartExtras);
            ButtonTemperature = new RibbonCheckBox(ribbon, Cmd.cmdButtonTemperature);
            TabView = new RibbonTab(ribbon, Cmd.cmdTabView);
            GroupTotals = new RibbonGroup(ribbon, Cmd.cmdGroupTotals);
            GroupExtras = new RibbonGroup(ribbon, Cmd.cmdGroupExtras);
            ButtonMyExtras = new RibbonButton(ribbon, Cmd.cmdButtonMyExtras);
            GroupMaps = new RibbonGroup(ribbon, Cmd.cmdGroupMaps);
            ButtonMap = new RibbonButton(ribbon, Cmd.cmdButtonMap);
            GroupZones = new RibbonGroup(ribbon, Cmd.cmdGroupZones);
            ButtonHeartRateZones = new RibbonButton(ribbon, Cmd.cmdButtonHeartRateZones);
            ButtonPowerZones = new RibbonButton(ribbon, Cmd.cmdButtonPowerZones);
            GroupIntern = new RibbonGroup(ribbon, Cmd.cmdGroupIntern);
            ButtonActivity = new RibbonButton(ribbon, Cmd.cmdButtonActivity);
            ButtonDeveloperDataId = new RibbonButton(ribbon, Cmd.cmdButtonDeveloperDataId);
            ButtonDeviceInfo = new RibbonButton(ribbon, Cmd.cmdButtonDeviceInfo);
            ButtonEvent = new RibbonButton(ribbon, Cmd.cmdButtonEvent);
            ButtonFieldDescription = new RibbonButton(ribbon, Cmd.cmdButtonFieldDescription);
            ButtonFileId = new RibbonButton(ribbon, Cmd.cmdButtonFileId);
            ButtonSport = new RibbonButton(ribbon, Cmd.cmdButtonSport);
            ButtonWorkout = new RibbonButton(ribbon, Cmd.cmdButtonWorkout);
            ButtonWahooFF00 = new RibbonButton(ribbon, Cmd.cmdButtonWahooFF00);
            ButtonWahooFF01 = new RibbonButton(ribbon, Cmd.cmdButtonWahooFF01);
        }

    }
}
