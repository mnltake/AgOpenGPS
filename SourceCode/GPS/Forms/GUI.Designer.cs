﻿//Please, if you use this, share the improvements

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AgOpenGPS.Properties;
using Microsoft.Win32;
using System.Collections.Generic;

namespace AgOpenGPS
{
    public partial class FormGPS
    {
        //ABLines directory
        public string ablinesDirectory;

        //colors for sections and field background
        private byte redSections, grnSections, bluSections;
        public byte redField, grnField, bluField;
        public byte flagColor = 0;

        //how many cm off line per big pixel
        public int lightbarCmPerPixel;

        //polygon mode for section drawing
        private bool isDrawPolygons;

        //Is it in 2D or 3D, metric or imperial, display lightbar, display grid etc
        public bool isIn3D = true, isMetric = true, isLightbarOn = true, isGridOn, isSideGuideLines = true;
        public bool isPureDisplayOn = true, isSkyOn = true, isBigAltitudeOn = false;

        //master Manual and Auto, 3 states possible
        public enum btnStates { Off, Auto, On }
        public btnStates manualBtnState = btnStates.Off;
        public btnStates autoBtnState = btnStates.Off;

        //section button states
        public enum manBtn { Off, Auto, On }

        private void LoadGUI()
        {
            //set the language to last used
            SetLanguage(Settings.Default.set_culture);

            //set the flag mark button to red dot
            btnFlag.Image = Properties.Resources.FlagRed;

            //metric settings
            isMetric = Settings.Default.setMenu_isMetric;
            metricToolStrip.Checked = isMetric;

            if (isMetric)
            {
                lblSpeedUnits.Text = gStr.gsKMH;
                metricToolStrip.Checked = true;
                imperialToolStrip.Checked = false;
                //lblFlowLeft.Text = "LPM"; lblFlowRight.Text = "LPM";
            }
            else
            {
                lblSpeedUnits.Text = gStr.gsMPH;
                metricToolStrip.Checked = false;
                imperialToolStrip.Checked = true;
                //lblFlowLeft.Text = "GPM"; lblFlowRight.Text = "GPM";

            }

            //load up colors
            redField = (Settings.Default.setF_FieldColorR);
            grnField = (Settings.Default.setF_FieldColorG);
            bluField = (Settings.Default.setF_FieldColorB);

            redSections = Settings.Default.setF_SectionColorR;
            grnSections = Settings.Default.setF_SectionColorG;
            bluSections = Settings.Default.setF_SectionColorB;

            //turn off the turn signals lol
            btnRightYouTurn.Visible = false;
            btnLeftYouTurn.Visible = false;

            //area side settings
            isAreaOnRight = Settings.Default.setMenu_isAreaRight;
            //toolStripMenuAreaSide.Checked = isAreaOnRight;

            //set up grid and lightbar
            isGridOn = Settings.Default.setMenu_isGridOn;
            gridToolStripMenuItem.Checked = isGridOn;

            //log NMEA 
            isLogNMEA = Settings.Default.setMenu_isLogNMEA;
            logNMEAMenuItem.Checked = isLogNMEA;

            isLightbarOn = Settings.Default.setMenu_isLightbarOn;
            lightbarToolStripMenuItem.Checked = isLightbarOn;

            isSideGuideLines = Settings.Default.setMenu_isSideGuideLines;
            sideGuideLines.Checked = isSideGuideLines;

            isPureDisplayOn = Settings.Default.setMenu_isPureOn;
            pursuitLineToolStripMenuItem.Checked = isPureDisplayOn;

            isSkyOn = Settings.Default.setMenu_isSkyOn;
            skyToolStripMenu.Checked = isSkyOn;

            simulatorOnToolStripMenuItem.Checked = Settings.Default.setMenu_isSimulatorOn;
            if (simulatorOnToolStripMenuItem.Checked)
            {
                panelSim.Visible = true;
                timerSim.Enabled = true;
            }
            else
            {
                panelSim.Visible = false;
                timerSim.Enabled = false;
            }

            LineUpManualBtns();

            yt.rowSkipsWidth = Properties.Vehicle.Default.set_youSkipWidth;
            cboxpRowWidth.SelectedIndex = yt.rowSkipsWidth - 1;

            //default to come up in mini panel, exit remembers 

            SwapBatmanPanels();

            if (Properties.Settings.Default.setAS_isAutoSteerAutoOn) btnAutoSteer.Text = "A";
            else btnAutoSteer.Text = "M";

            cboxTramPassEvery.Text = Properties.Vehicle.Default.setTram_Skips.ToString();
            ABLine.tramPassEvery = Properties.Vehicle.Default.setTram_Skips;
            cboxTramBasedOn.Text = Properties.Vehicle.Default.setTram_BasedOn.ToString();
            ABLine.passBasedOn = Properties.Vehicle.Default.setTram_BasedOn;

            cboxTramPassEvery.Text = "0";
            ABLine.tramPassEvery = 0;
            cboxTramBasedOn.Text = "0";
            ABLine.passBasedOn = 0;

            //panelSnap.Location = Settings.Default.setDisplay_panelSnapLocation;
            panelSim.Location = Settings.Default.setDisplay_panelSimLocation;
            panelTurn.Location = Settings.Default.setDisplay_panelTurnLocation;

            //panelSnap.Visible = false;
            panelTurn.Visible = false;

            //if (Properties.Settings.Default.setNTRIP_isOn) panelNTRIP.Visible = true;
            //else panelNTRIP.Visible = false;

            FixPanelsAndMenus();
        }

        //force all the buttons same according to two main buttons
        private void ManualAllBtnsUpdate()
        {
            ManualBtnUpdate(0, btnSection1Man);
            ManualBtnUpdate(1, btnSection2Man);
            ManualBtnUpdate(2, btnSection3Man);
            ManualBtnUpdate(3, btnSection4Man);
            ManualBtnUpdate(4, btnSection5Man);
            ManualBtnUpdate(5, btnSection6Man);
            ManualBtnUpdate(6, btnSection7Man);
            ManualBtnUpdate(7, btnSection8Man);
            ManualBtnUpdate(8, btnSection9Man);
            ManualBtnUpdate(9, btnSection10Man);
            ManualBtnUpdate(10, btnSection11Man);
            ManualBtnUpdate(11, btnSection12Man);
        }

        private void FixPanelsAndMenus()
        {
            //keep snap in view on resizing
            //if (panelSnap.Top > Height - 170)
            //{
            //    if (panelSnap.Left + 342 > Width - 5) panelSnap.Left = Width - 15 - 342;
            //}
            //else
            //{
            //    if (panelSnap.Left + 342 > Width - 200) panelSnap.Left = Width - 200 - 342;
            //}
            //if (panelSnap.Top < 1) panelSnap.Top = 1;
            //if (panelSnap.Top > Height - 100) panelSnap.Top = Height - 100;

            if (panelSim.Left + 443 > Width - 200) panelSim.Left = Width - 200 - 443;
            if (panelSim.Top < 80) panelSim.Top = 80;
            if (panelSim.Top > Height - 150) panelSim.Top = Height - 150;


            if (panelTurn.Top < 48) panelTurn.Top = 48;
            if (panelTurn.Left + 252 > Width - 200) panelTurn.Left = Width - 200 - 252;
            if (panelTurn.Top > Height - 180) panelTurn.Top = Height - 180;

            //if (panelBatman.Visible)
            //{
            //    if (panelTurn.Left < 260) panelTurn.Left = 260;
            //    if (panelSim.Left < 260) panelSim.Left = 260;
            //    if (panelSnap.Left < 260) panelSnap.Left = 260;
            //}
            //else
            //{
            if (panelTurn.Left < 75) panelTurn.Left = 75;
            if (panelSim.Left < 75) panelSim.Left = 75;
            //if (panelSnap.Left < 75) panelSnap.Left = 75;
            //}

            if (Width > 1100)
            {
                youTurnStripBtn.Visible = true;
            }
            else
            {
                youTurnStripBtn.Visible = false;
            }

            if (Width > 1250)
            {
                distanceToolBtn.Visible = true;
            }
            else
            {
                distanceToolBtn.Visible = false;
            }

            if (Width > 1390)
            {
                USBPortsToolBtn.Visible = true;
            }
            else
            {
                USBPortsToolBtn.Visible = false;
            }


        }
        public string FindDirection(double heading)
        {
            if (heading < 0) heading += glm.twoPI;

            heading = glm.toDegrees(heading);

            if (heading > 337.5 || heading < 22.5)
            {
                return (" " +  gStr.gsNorth + " ");
            }
            if (heading > 22.5 && heading < 67.5)
            {
                return (" " +  gStr.gsN_East + " ");
            }
            if (heading > 67.5 && heading < 111.5)
            {
                return (" " +  gStr.gsEast + " ");
            }
            if (heading > 111.5 && heading < 157.5)
            {
                return (" " +  gStr.gsS_East + " ");
            }
            if (heading > 157.5 && heading < 202.5)
            {
                return (" " +  gStr.gsSouth + " ");
            }
            if (heading > 202.5 && heading < 247.5)
            {
                return (" " +  gStr.gsS_West + " ");
            }
            if (heading > 247.5 && heading < 292.5)
            {
                return (" " +  gStr.gsWest + " ");
            }
            if (heading > 292.5 && heading < 337.5)
            {
                return (" " +  gStr.gsN_West + " ");
            }
            return (" " +  gStr.gsLost + " ");
        }

        //hide the left panel
        public void SwapBatmanPanels()
        {
            //Properties.Settings.Default.Save();
            if (Properties.Settings.Default.setDisplay_isBatmanOn)
            {
                //Batman mini-panel shows
                //if (panelSim.Left < 390) panelSim.Left = 390;
                oglMain.Left = statusStripLeft.Width + panelBatman.Width + 1;
                oglMain.Width = Width - (statusStripLeft.Width + panelBatman.Width) - 200;

                panelBatman.Left = statusStripLeft.Width;
                //tableLayoutPanelDisplay.Left = 181;
                //panelSim.Left = 350;

                panelBatman.Visible = true;
                //statusStripLeft.Left = 8;

                lblDistanceOffLine.Left = (Width - 25) / 2;
                LineUpManualBtns();
            }
            else
            {
                //no side panel
                //panelSim.Location = Properties.Settings.Default.setDisplay_panelSimLocation;
                oglMain.Left = 72;
                oglMain.Width = Width - 72 - 200;
                //tableLayoutPanelDisplay.Left = 8;
                //panelSnap.Left = 80;


                panelBatman.Visible = false;
                //statusStripLeft.Left = 8;

                lblDistanceOffLine.Left = (Width - 270) / 2;
                lblDistanceOffLine.Top = -1;
                panelBatman.Visible = false;
                LineUpManualBtns();
            }
        }

        //line up section On Off Auto buttons based on how many there are
        public void LineUpManualBtns()
        {
            int first2Thirds = 0;

            if (panelBatman.Visible)
            {
                btnRightYouTurn.Left = (Width+350) / 2 ;
                btnLeftYouTurn.Left = (Width-240) / 2;
                btnSwapDirection.Left = (Width - 340) / 2 + 200;
                first2Thirds = (Width - 395) / 2 + 260;
            }

            else
            {
                btnRightYouTurn.Left = (Width+140) / 2;
                btnLeftYouTurn.Left = (Width-500) / 2;
                btnSwapDirection.Left = (Width-185) / 2;
                first2Thirds = (Width - 118) / 2;
            }

            int top = 0;
            //if (panelSim.Visible == true)
            {
                top = 210;
                if (vehicle.numOfSections > 8) top = 250;
            }

            btnSection1Man.Top = Height - top;
            btnSection2Man.Top = Height - top;
            btnSection3Man.Top = Height - top;
            btnSection4Man.Top = Height - top;
            btnSection5Man.Top = Height - top;
            btnSection6Man.Top = Height - top;
            btnSection7Man.Top = Height - top;
            btnSection8Man.Top = Height - top;


            int even = 60;
            int offset = 7;

            switch (vehicle.numOfSections)
            {
                case 1:
                    btnSection1Man.Left = (first2Thirds) - 20;
                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = false;
                    btnSection3Man.Visible = false;
                    btnSection4Man.Visible = false;
                    btnSection5Man.Visible = false;
                    btnSection6Man.Visible = false;
                    btnSection7Man.Visible = false;
                    btnSection8Man.Visible = false;
                    btnSection9Man.Visible = false;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 2:
                    btnSection1Man.Left = (first2Thirds)+ offset - even;
                    btnSection2Man.Left = (first2Thirds)+ offset + 0;
                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = false;
                    btnSection4Man.Visible = false;
                    btnSection5Man.Visible = false;
                    btnSection6Man.Visible = false;
                    btnSection7Man.Visible = false;
                    btnSection8Man.Visible = false;
                    btnSection9Man.Visible = false;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 3:
                    btnSection1Man.Left = (first2Thirds) - 80;
                    btnSection2Man.Left = (first2Thirds) - 20;
                    btnSection3Man.Left = (first2Thirds) + 40;
                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = false;
                    btnSection5Man.Visible = false;
                    btnSection6Man.Visible = false;
                    btnSection7Man.Visible = false;
                    btnSection8Man.Visible = false;
                    btnSection9Man.Visible = false;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 4:
                    btnSection1Man.Left = (first2Thirds)+ offset - even*2;
                    btnSection2Man.Left = (first2Thirds)+ offset - even;
                    btnSection3Man.Left = (first2Thirds)+ offset + 0;
                    btnSection4Man.Left = (first2Thirds)+ offset + even;
                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = false;
                    btnSection6Man.Visible = false;
                    btnSection7Man.Visible = false;
                    btnSection8Man.Visible = false;
                    btnSection9Man.Visible = false;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 5:
                    btnSection1Man.Left = (first2Thirds) - 140;
                    btnSection2Man.Left = (first2Thirds) - 80;
                    btnSection3Man.Left = (first2Thirds) - 20;
                    btnSection4Man.Left = (first2Thirds) + 40;
                    btnSection5Man.Left = (first2Thirds) + 100;
                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = true;
                    btnSection6Man.Visible = false;
                    btnSection7Man.Visible = false;
                    btnSection8Man.Visible = false;
                    btnSection9Man.Visible = false;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 6:
                    btnSection1Man.Left = (first2Thirds)+ offset - even*3;
                    btnSection2Man.Left = (first2Thirds)+ offset - even*2;
                    btnSection3Man.Left = (first2Thirds)+ offset - even;
                    btnSection4Man.Left = (first2Thirds)+ offset + 0;
                    btnSection5Man.Left = (first2Thirds)+ offset+ even;
                    btnSection6Man.Left = (first2Thirds) + offset + even*2;
                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = true;
                    btnSection6Man.Visible = true;
                    btnSection7Man.Visible = false;
                    btnSection8Man.Visible = false;
                    btnSection9Man.Visible = false;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 7:
                    btnSection1Man.Left = (first2Thirds) - 200;
                    btnSection2Man.Left = (first2Thirds) - 140;
                    btnSection3Man.Left = (first2Thirds) - 80;
                    btnSection4Man.Left = (first2Thirds) - 20;
                    btnSection5Man.Left = (first2Thirds) + 40;
                    btnSection6Man.Left = (first2Thirds) + 100;
                    btnSection7Man.Left = (first2Thirds) + 160;
                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = true;
                    btnSection6Man.Visible = true;
                    btnSection7Man.Visible = true;
                    btnSection8Man.Visible = false;
                    btnSection9Man.Visible = false;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;

                    break;

                case 8:
                    btnSection1Man.Left = (first2Thirds)+ offset - even*4;           //390;
                    btnSection2Man.Left = (first2Thirds)+ offset - even*3;           //290;
                    btnSection3Man.Left = (first2Thirds)+ offset - even*2;           //190;
                    btnSection4Man.Left = (first2Thirds)+ offset - even;           //90;
                    btnSection5Man.Left = (first2Thirds)+ offset + 0;           //10;
                    btnSection6Man.Left = (first2Thirds)+ offset + even;           //110;
                    btnSection7Man.Left = (first2Thirds)+ offset + even*2;           //210;
                    btnSection8Man.Left = (first2Thirds)+ offset + even*3;           //310;
                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = true;
                    btnSection6Man.Visible = true;
                    btnSection7Man.Visible = true;
                    btnSection8Man.Visible = true;
                    btnSection9Man.Visible = false;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 9:
                    btnSection1Man.Top = Height - top;
                    btnSection2Man.Top = Height - top;
                    btnSection3Man.Top = Height - top + 42;
                    btnSection4Man.Top = Height - top;
                    btnSection5Man.Top = Height - top + 42;
                    btnSection6Man.Top = Height - top;
                    btnSection7Man.Top = Height - top + 42;
                    btnSection8Man.Top = Height - top;
                    btnSection9Man.Top = Height - top;

                    even = 80; offset = 14;
                    btnSection1Man.Left = (first2Thirds) + offset - even * 3;
                    btnSection2Man.Left = (first2Thirds) + offset - even * 2;
                    btnSection4Man.Left = (first2Thirds) + offset - even;
                    btnSection6Man.Left = (first2Thirds) + offset + 0;
                    btnSection8Man.Left = (first2Thirds) + offset + even;
                    btnSection9Man.Left = (first2Thirds) + offset + even * 2;

                    btnSection3Man.Left = (first2Thirds) - 100;
                    btnSection5Man.Left = (first2Thirds) - 20;
                    btnSection7Man.Left = (first2Thirds) + 60;

                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = true;
                    btnSection6Man.Visible = true;
                    btnSection7Man.Visible = true;
                    btnSection8Man.Visible = true;
                    btnSection9Man.Visible = true;
                    btnSection10Man.Visible = false;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 10:
                    btnSection1Man.Top = Height - top;
                    btnSection2Man.Top = Height - top + 42;
                    btnSection3Man.Top = Height - top;
                    btnSection4Man.Top = Height - top + 42;
                    btnSection5Man.Top = Height - top;
                    btnSection6Man.Top = Height - top + 42;
                    btnSection7Man.Top = Height - top;
                    btnSection8Man.Top = Height - top + 42;
                    btnSection9Man.Top = Height - top;
                    btnSection10Man.Top = Height - top + 42;


                    btnSection1Man.Left = (first2Thirds) - 200;
                    btnSection3Man.Left = (first2Thirds) - 120;
                    btnSection5Man.Left = (first2Thirds) - 40;
                    btnSection7Man.Left = (first2Thirds) + 40;
                    btnSection9Man.Left = (first2Thirds) + 120;

                    btnSection2Man.Left = (first2Thirds) - 160;
                    btnSection4Man.Left = (first2Thirds) - 80;  
                    btnSection6Man.Left = (first2Thirds) - 0; 
                    btnSection8Man.Left = (first2Thirds) + 80;  
                    btnSection10Man.Left = (first2Thirds) + 160;        


                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = true;
                    btnSection6Man.Visible = true;
                    btnSection7Man.Visible = true;
                    btnSection8Man.Visible = true;
                    btnSection9Man.Visible = true;
                    btnSection10Man.Visible = true;
                    btnSection11Man.Visible = false;
                    btnSection12Man.Visible = false;
                    break;

                case 11:
                    btnSection1Man.Top = Height - top;
                    btnSection2Man.Top = Height - top;
                    btnSection3Man.Top = Height - top + 42;
                    btnSection4Man.Top = Height - top;
                    btnSection5Man.Top = Height - top + 42;
                    btnSection6Man.Top = Height - top;
                    btnSection7Man.Top = Height - top + 42;
                    btnSection8Man.Top = Height - top;
                    btnSection9Man.Top = Height - top + 42;
                    btnSection10Man.Top = Height - top;
                    btnSection11Man.Top = Height - top;


                    btnSection1Man.Left = (first2Thirds) - 200;
                    btnSection2Man.Left = (first2Thirds) - 140;
                    btnSection4Man.Left = (first2Thirds) - 80;
                    btnSection6Man.Left = (first2Thirds) - 20;
                    btnSection8Man.Left = (first2Thirds) + 40;
                    btnSection10Man.Left = (first2Thirds) + 100;
                    btnSection11Man.Left = (first2Thirds) + 160;

                    btnSection3Man.Left = (first2Thirds) - 110;
                    btnSection5Man.Left = (first2Thirds) - 50;
                    btnSection7Man.Left = (first2Thirds) +10;
                    btnSection9Man.Left = (first2Thirds) + 70;


                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = true;
                    btnSection6Man.Visible = true;
                    btnSection7Man.Visible = true;
                    btnSection8Man.Visible = true;
                    btnSection9Man.Visible = true;
                    btnSection10Man.Visible = true;
                    btnSection11Man.Visible = true;
                    btnSection12Man.Visible = false;
                    break;

                case 12:
                    btnSection1Man.Top = Height - top;
                    btnSection2Man.Top = Height - top + 42;
                    btnSection3Man.Top = Height - top;
                    btnSection4Man.Top = Height - top + 42;
                    btnSection5Man.Top = Height - top;
                    btnSection6Man.Top = Height - top + 42;
                    btnSection7Man.Top = Height - top;
                    btnSection8Man.Top = Height - top + 42;
                    btnSection9Man.Top = Height - top;
                    btnSection10Man.Top = Height - top + 42;
                    btnSection11Man.Top = Height - top;
                    btnSection12Man.Top = Height - top + 42;

                    offset = -9; even = 70;
                    btnSection1Man.Left = (first2Thirds)  + offset - even*3;
                    btnSection3Man.Left = (first2Thirds)  + offset - even*2;
                    btnSection5Man.Left = (first2Thirds)  + offset - even;
                    btnSection7Man.Left = (first2Thirds)  + offset + 0;
                    btnSection9Man.Left = (first2Thirds)  + offset+ even;                     
                    btnSection11Man.Left = (first2Thirds) + offset + even * 2;

                    offset = 22;
                    btnSection2Man.Left = (first2Thirds)  + offset - even*3;
                    btnSection4Man.Left = (first2Thirds)  + offset - even*2;
                    btnSection6Man.Left = (first2Thirds)  + offset - even;
                    btnSection8Man.Left = (first2Thirds)  + offset + 0;
                    btnSection10Man.Left = (first2Thirds) + offset + even;
                    btnSection12Man.Left = (first2Thirds) + offset + even * 2;

                    btnSection1Man.Visible = true;
                    btnSection2Man.Visible = true;
                    btnSection3Man.Visible = true;
                    btnSection4Man.Visible = true;
                    btnSection5Man.Visible = true;
                    btnSection6Man.Visible = true;
                    btnSection7Man.Visible = true;
                    btnSection8Man.Visible = true;
                    btnSection9Man.Visible = true;
                    btnSection10Man.Visible = true;
                    btnSection11Man.Visible = true;
                    btnSection12Man.Visible = true;
                    break;
            }

            if (isJobStarted)
            {
                switch (vehicle.numOfSections)
                {
                    case 1:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = false;
                        btnSection3Man.Enabled = false;
                        btnSection4Man.Enabled = false;
                        btnSection5Man.Enabled = false;
                        btnSection6Man.Enabled = false;
                        btnSection7Man.Enabled = false;
                        btnSection8Man.Enabled = false;
                        btnSection9Man.Enabled = false;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 2:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = false;
                        btnSection4Man.Enabled = false;
                        btnSection5Man.Enabled = false;
                        btnSection6Man.Enabled = false;
                        btnSection7Man.Enabled = false;
                        btnSection8Man.Enabled = false;
                        btnSection9Man.Enabled = false;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 3:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = false;
                        btnSection5Man.Enabled = false;
                        btnSection6Man.Enabled = false;
                        btnSection7Man.Enabled = false;
                        btnSection8Man.Enabled = false;
                        btnSection9Man.Enabled = false;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 4:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = false;
                        btnSection6Man.Enabled = false;
                        btnSection7Man.Enabled = false;
                        btnSection8Man.Enabled = false;
                        btnSection9Man.Enabled = false;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 5:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = true;
                        btnSection6Man.Enabled = false;
                        btnSection7Man.Enabled = false;
                        btnSection8Man.Enabled = false;
                        btnSection9Man.Enabled = false;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 6:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = true;
                        btnSection6Man.Enabled = true;
                        btnSection7Man.Enabled = false;
                        btnSection8Man.Enabled = false;
                        btnSection9Man.Enabled = false;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 7:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = true;
                        btnSection6Man.Enabled = true;
                        btnSection7Man.Enabled = true;
                        btnSection8Man.Enabled = false;
                        btnSection9Man.Enabled = false;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 8:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = true;
                        btnSection6Man.Enabled = true;
                        btnSection7Man.Enabled = true;
                        btnSection8Man.Enabled = true;
                        btnSection9Man.Enabled = false;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 9:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = true;
                        btnSection6Man.Enabled = true;
                        btnSection7Man.Enabled = true;
                        btnSection8Man.Enabled = true;
                        btnSection9Man.Enabled = true;
                        btnSection10Man.Enabled = false;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 10:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = true;
                        btnSection6Man.Enabled = true;
                        btnSection7Man.Enabled = true;
                        btnSection8Man.Enabled = true;
                        btnSection9Man.Enabled = true;
                        btnSection10Man.Enabled = true;
                        btnSection11Man.Enabled = false;
                        btnSection12Man.Enabled = false;
                        break;

                    case 11:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = true;
                        btnSection6Man.Enabled = true;
                        btnSection7Man.Enabled = true;
                        btnSection8Man.Enabled = true;
                        btnSection9Man.Enabled = true;
                        btnSection10Man.Enabled = true;
                        btnSection11Man.Enabled = true;
                        btnSection12Man.Enabled = false;
                        break;

                    case 12:
                        btnSection1Man.Enabled = true;
                        btnSection2Man.Enabled = true;
                        btnSection3Man.Enabled = true;
                        btnSection4Man.Enabled = true;
                        btnSection5Man.Enabled = true;
                        btnSection6Man.Enabled = true;
                        btnSection7Man.Enabled = true;
                        btnSection8Man.Enabled = true;
                        btnSection9Man.Enabled = true;
                        btnSection10Man.Enabled = true;
                        btnSection11Man.Enabled = true;
                        btnSection12Man.Enabled = true;
                        break;
                }
            }
        }

        //update individual btn based on state after push
        private void ManualBtnUpdate(int sectNumber, Button btn)
        {
            switch (section[sectNumber].manBtnState)
            {
                case manBtn.Off:
                    section[sectNumber].manBtnState = manBtn.Auto;
                    btn.BackColor = Color.Lime;
                    break;

                case manBtn.Auto:
                    section[sectNumber].manBtnState = manBtn.On;
                    btn.BackColor = Color.Yellow;
                    break;

                case manBtn.On:
                    section[sectNumber].manBtnState = manBtn.Off;
                    btn.BackColor = Color.Red;
                    break;
            }
        }
        
        //Function to delete flag
        private void DeleteSelectedFlag()
        {
            //delete selected flag and set selected to none
            flagPts.RemoveAt(flagNumberPicked - 1);
            flagNumberPicked = 0;

            // re-sort the id's based on how many flags left
            int flagCnt = flagPts.Count;
            if (flagCnt > 0)
            {
                for (int i = 0; i < flagCnt; i++) flagPts[i].ID = i + 1;
            }
        }

        private void DoNTRIPSecondRoutine()
        {
            //count up the ntrip clock only if everything is alive
            if (startCounter > 50 && recvCounter < 20 && isNTRIP_RequiredOn)
            {
                IncrementNTRIPWatchDog();
            }

            //Have we connection
            if (isNTRIP_RequiredOn && !isNTRIP_Connected && !isNTRIP_Connecting)
            {
                if (!isNTRIP_Starting && ntripCounter > 20)
                {
                    StartNTRIP();
                }
            }

            if (isNTRIP_Connecting)
            {
                if (ntripCounter > 28)
                {
                    TimedMessageBox(2000, gStr.gsSocketConnectionProblem, gStr.gsNotConnectingToCaster);
                    ReconnectRequest();
                }
                if (clientSocket != null && clientSocket.Connected)
                {
                    //TimedMessageBox(2000, "NTRIP Not Connected", " At the StartNTRIP() ");
                    //ReconnectRequest();
                    //return;
                    SendAuthorization();
                }

            }

            if (isNTRIP_RequiredOn)
            {
                //update byte counter and up counter
                if (ntripCounter > 59) lblNTRIPSeconds.Text = (ntripCounter / 60) + " Mins";
                else if (ntripCounter < 60 && ntripCounter > 22) lblNTRIPSeconds.Text = ntripCounter + " Secs";
                else lblNTRIPSeconds.Text = gStr.gsConnectingIn + " " + (Math.Abs(ntripCounter - 22));

                pbarNtripMenu.Value = unchecked((byte)(tripBytes * 0.02));
                NTRIPBytesMenu.Text = ((tripBytes) * 0.001).ToString("###,###,###") + " kb";

                //watchdog for Ntrip
                if (isNTRIP_Connecting) lblWatch.Text = gStr.gsAuthourizing;
                else
                {
                    if (NTRIP_Watchdog > 10) lblWatch.Text = gStr.gsWaiting;
                    else lblWatch.Text = gStr.gsListening;
                }

                if (sendGGAInterval > 0 && isNTRIP_Sending)
                {
                    lblWatch.Text = gStr.gsSendingGGA;
                    isNTRIP_Sending = false;
                }
            }
        }

        // Buttons //-----------------------------------------------------------------------

        private void btnStartStopNtrip_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.setNTRIP_isOn)
            {
                if (isNTRIP_RequiredOn)
                {
                    ShutDownNTRIP();
                    btnStartStopNtrip.Text = gStr.gsStart;
                    lblWatch.Text = gStr.gsStopped;
                    lblNTRIPSeconds.Text = gStr.gsOffline;
                    isNTRIP_RequiredOn = false;
                }
                else
                {
                    isNTRIP_RequiredOn = true;
                    btnStartStopNtrip.Text = gStr.gsStop;
                    lblWatch.Text = gStr.gsWaiting;
                }
            }
            else
            {
                TimedMessageBox(2000, gStr.gsTurnONNtripClient, gStr.gsNTRIPClientNotSetUp);
            }
        }

        private void btnManualAutoDrive_Click(object sender, EventArgs e)
        {
            //if (isInAutoDrive)
            //{
            //    isInAutoDrive = false;
            //    btnManualAutoDrive.Image = Properties.Resources.Cancel64;
            //    btnManualAutoDrive.Text = gStr.gsManual;
            //}
            //else
            //{
            //    isInAutoDrive = true;
            //    btnManualAutoDrive.Image = Properties.Resources.OK64;
            //    btnManualAutoDrive.Text = gStr.gsAuto;
            //}
        }

        private void goPathMenu_Click(object sender, EventArgs e)
        {
            if (bnd.bndArr.Count == 0)
            {
                TimedMessageBox(2000, gStr.gsNoBoundary, gStr.gsCreateABoundaryFirst);
                return;
            }

            //if contour is on, turn it off
            if (ct.isContourBtnOn) { if (ct.isContourBtnOn) btnContour.PerformClick(); }
            //btnContourPriority.Enabled = true;

            if (yt.isYouTurnBtnOn) btnEnableAutoYouTurn.PerformClick();
            if (isAutoSteerBtnOn) btnAutoSteer.PerformClick();

            DisableYouTurnButtons();

            //if ABLine isn't set, turn off the YouTurn
            if (ABLine.isABLineSet)
            {
                //ABLine.DeleteAB();
                ABLine.isABLineBeingSet = false;
                ABLine.isABLineSet = false;
                lblDistanceOffLine.Visible = false;

                //change image to reflect on off
                btnABLine.Image = Properties.Resources.ABLineOff;
                ABLine.isBtnABLineOn = false;
            }

            if (curve.isCurveSet)
            {

                //make sure the other stuff is off
                curve.isOkToAddPoints = false;
                curve.isCurveSet = false;
                //btnContourPriority.Enabled = false;
                curve.isCurveBtnOn = false;
                btnCurve.Image = Properties.Resources.CurveOff;
            }

            if (!recPath.isPausedDrivingRecordedPath)
            {
                //already running?
                if (recPath.isDrivingRecordedPath)
                {
                    recPath.StopDrivingRecordedPath();
                    return;
                }

                //start the recorded path driving process



                if (!recPath.StartDrivingRecordedPath())
                {
                    //Cancel the recPath - something went seriously wrong
                    recPath.StopDrivingRecordedPath();
                    TimedMessageBox(1500, gStr.gsProblemMakingPath, gStr.gsCouldntGenerateValidPath);
                }
                else
                {
                    goPathMenu.Image = Properties.Resources.AutoStop;
                }
            }
            else
            {
                recPath.isPausedDrivingRecordedPath = false;
                pausePathMenu.BackColor = Color.Lime;
            }
        }

        private void pausePathMenu_Click(object sender, EventArgs e)
        {
            if (recPath.isPausedDrivingRecordedPath)
            {
                pausePathMenu.BackColor = Color.Lime;
            }
            else
            {
                pausePathMenu.BackColor = Color.OrangeRed;
            }

            recPath.isPausedDrivingRecordedPath = !recPath.isPausedDrivingRecordedPath;
        }


        private void RecordPathMenu_Click(object sender, EventArgs e)
        {
            if (recPath.isRecordOn)
            {
                FileSaveRecPath();
                recPath.isRecordOn = false;
                recordPathMenu.Image = Properties.Resources.BoundaryRecord;
            }
            else if (isJobStarted)
            {
                recPath.recList.Clear();
                recPath.isRecordOn = true;
                recordPathMenu.Image = Properties.Resources.boundaryStop;
            }
        }

        private void DeletePathMenu_Click(object sender, EventArgs e)
        {
            recPath.recList.Clear();
            recPath.StopDrivingRecordedPath();
            FileSaveRecPath();

        }

        //LIDAR control
        private void btnAutoSteer_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Question.Play();

            //new direction so reset where to put turn diagnostic
            yt.ResetCreatedYouTurn();

            if (isAutoSteerBtnOn)
            {
                isAutoSteerBtnOn = false;
                btnAutoSteer.Image = Properties.Resources.AutoSteerOff;
                if (yt.isYouTurnBtnOn) btnEnableAutoYouTurn.PerformClick();
            }
            else
            {
                if (ABLine.isBtnABLineOn | ct.isContourBtnOn | curve.isCurveBtnOn)
                {
                    isAutoSteerBtnOn = true;
                    btnAutoSteer.Image = Properties.Resources.AutoSteerOn;
                }
                else
                {
                    var form = new FormTimedMessage(2000,(gStr.gsNoGuidanceLines),(gStr.gsTurnOnContourOrMakeABLine));
                    form.Show();
                }
            }
        }

        private void BtnMakeLinesFromBoundary_Click(object sender, EventArgs e)
        {
            if (ct.isContourBtnOn)
            {
                var form = new FormTimedMessage(2000, (gStr.gsContourOn), (gStr.gsTurnOffContourFirst));
                form.Show();
                return;
            }

            if (bnd.bndArr.Count == 0)
            {
                TimedMessageBox(2000, gStr.gsNoBoundary, gStr.gsCreateABoundaryFirst);
                return;
            }

            GetAB();
        }
        private void btnCycleLines_Click(object sender, EventArgs e)
        {
            if (ABLine.isBtnABLineOn && ABLine.numABLines > 0)
            {
                ABLine.moveDistance = 0;

                ABLine.numABLineSelected++;
                if (ABLine.numABLineSelected > ABLine.numABLines) ABLine.numABLineSelected = 1;
                ABLine.refPoint1 = ABLine.lineArr[ABLine.numABLineSelected - 1].origin;
                //ABLine.refPoint2 = ABLine.lineArr[ABLine.numABLineSelected - 1].ref2;
                ABLine.abHeading = ABLine.lineArr[ABLine.numABLineSelected - 1].heading;
                ABLine.SetABLineByHeading();
                ABLine.isABLineSet = true;
                ABLine.isABLineLoaded = true;
                yt.ResetYouTurn();
                btnCycleLines.Text = "AB-" + ABLine.numABLineSelected;
            }
            else if (curve.isCurveBtnOn && curve.numCurveLines > 0)
            {
                curve.moveDistance = 0;

                curve.numCurveLineSelected++;
                if (curve.numCurveLineSelected > curve.numCurveLines) curve.numCurveLineSelected = 1;

                int idx = curve.numCurveLineSelected - 1;
                curve.aveLineHeading = curve.curveArr[idx].aveHeading;
                curve.refList?.Clear();
                for (int i = 0; i < curve.curveArr[idx].curvePts.Count; i++)
                {
                    curve.refList.Add(curve.curveArr[idx].curvePts[i]);
                }
                curve.isCurveSet = true;
                yt.ResetYouTurn();
                btnCycleLines.Text = "Cur-" + curve.numCurveLineSelected;
            }
        }


        private void btnABLine_Click(object sender, EventArgs e)
        {
            btnCycleLines.Text = "AB-" + ABLine.numABLineSelected;

            //check if window already exists
            Form f = Application.OpenForms["FormABCurve"];

            if (f != null)
            {
                f.Focus();
                return;
            }

            Form af = Application.OpenForms["FormABLine"];

            if (af != null)
            {
                af.Close();
                return;
            }


            //if contour is on, turn it off
            if (ct.isContourBtnOn) { if (ct.isContourBtnOn) btnContour.PerformClick(); }
            //btnContourPriority.Enabled = true;

            //make sure the other stuff is off
            curve.isOkToAddPoints = false;
                
            curve.isCurveBtnOn = false;
            btnCurve.Image = Properties.Resources.CurveOff;

            //if there is a line in memory, just use it.
            if (ABLine.isBtnABLineOn == false && ABLine.isABLineLoaded)
            {                
                ABLine.isABLineSet = true;
                EnableYouTurnButtons();
                btnABLine.Image = Properties.Resources.ABLineOn;
                ABLine.isBtnABLineOn = true;
                return;
            }
            
            //check if window already exists, return if true
            Form fc = Application.OpenForms["FormABLine"];

            if (fc != null)
            {
                fc.Focus();
                return;
            }

            //Bring up the form
            ABLine.isBtnABLineOn = true;
            btnABLine.Image = Properties.Resources.ABLineOn;

            //turn off youturn...
            //DisableYouTurnButtons();
            //yt.ResetYouTurn();

            var form = new FormABLine(this);
                form.Show();
        }

        private void btnCurve_Click(object sender, EventArgs e)
        {
            btnCycleLines.Text = "Cur-" + curve.numCurveLineSelected;

            //check if window already exists, return if true

            Form f = Application.OpenForms["FormABLine"];

            if (f != null)
            {
                f.Focus();
                return;
            }

            //check if window already exists
            Form cf = Application.OpenForms["FormABCurve"];

            if (cf != null)
            {
                cf.Close();
                return;
            }


            //if contour is on, turn it off
            if (ct.isContourBtnOn) { if (ct.isContourBtnOn) btnContour.PerformClick(); }

            //turn off ABLine 
            ABLine.isABLineBeingSet = false;
            ABLine.isABLineSet = false;
            lblDistanceOffLine.Visible = false;

            //change image to reflect on off
            btnABLine.Image = Properties.Resources.ABLineOff;
            ABLine.isBtnABLineOn = false;

            //new direction so reset where to put turn diagnostic
            //yt.ResetCreatedYouTurn();

            if (curve.isCurveBtnOn == false && curve.isCurveSet)
            {
                //display the curve
                curve.isCurveSet = true;
                EnableYouTurnButtons();
                btnCurve.Image = Properties.Resources.CurveOn;
                curve.isCurveBtnOn = true;
                return;
            }


            //check if window already exists
            Form fc = Application.OpenForms["FormABCurve"];

            if (fc != null)
            {
                fc.Focus();
                return;
            }

            curve.isCurveBtnOn = true;
            btnCurve.Image = Properties.Resources.CurveOn;

            EnableYouTurnButtons();
            //btnContourPriority.Enabled = true;

            Form form = new FormABCurve(this);
            form.Show();
        }

        private void btnContour_Click(object sender, EventArgs e)
        {
            ct.isContourBtnOn = !ct.isContourBtnOn;
            btnContour.Image = ct.isContourBtnOn ? Properties.Resources.ContourOn : Properties.Resources.ContourOff;

            if (ct.isContourBtnOn)
            {
                //turn off youturn...
                btnRightYouTurn.Enabled = false;
                btnLeftYouTurn.Enabled = false;
                btnRightYouTurn.Visible = false;
                btnLeftYouTurn.Visible = false;

                btnEnableAutoYouTurn.Enabled = false;
                yt.isYouTurnBtnOn = false;
                btnEnableAutoYouTurn.Image = Properties.Resources.YouTurnNo;
                //btnContourPriority.Enabled = true;

                if (ct.isRightPriority)
                {
                    btnContourPriority.Image = Properties.Resources.ContourPriorityRight;
                }
                else
                {
                    btnContourPriority.Image = Properties.Resources.ContourPriorityLeft;
                }
            }

            else
            {
                if (ABLine.isABLineSet | curve.isCurveSet)
                {
                    btnRightYouTurn.Enabled = true;
                    btnLeftYouTurn.Enabled = true;
                    btnRightYouTurn.Visible = true;
                    btnLeftYouTurn.Visible = true;

                    //auto YouTurn shutdown
                    yt.isYouTurnBtnOn = false;
                    yt.ResetYouTurn();

                    //turn off youturn...
                    btnEnableAutoYouTurn.Enabled = true;
                    btnEnableAutoYouTurn.Image = Properties.Resources.YouTurnNo;
                }
                //btnContourPriority.Enabled = false;

                btnContourPriority.Image = Properties.Resources.Snap2;
            }
        }
        private void btnContourPriority_Click(object sender, EventArgs e)
        {
            if (ct.isContourBtnOn)
            {

                ct.isRightPriority = !ct.isRightPriority;

                if (ct.isRightPriority)
                {
                    btnContourPriority.Image = Properties.Resources.ContourPriorityRight;
                }
                else
                {
                    btnContourPriority.Image = Properties.Resources.ContourPriorityLeft;
                }
            }
            else
            {
                if (ABLine.isABLineSet)
                {
                    ABLine.SnapABLine();
                }
                else if (curve.isCurveSet)
                {
                    curve.SnapABCurve();
                }
                else
                {
                    var form = new FormTimedMessage(2000, (gStr.gsNoGuidanceLines), (gStr.gsTurnOnContourOrMakeABLine));
                    form.Show();
                }
            }
        }

        //Snaps
        private void SnapSmallLeft()
        {
            if (!ct.isContourBtnOn)
            {
                if (ABLine.isABLineSet)
                {
                    //snap distance is in cm
                    yt.ResetCreatedYouTurn();
                    double dist = 0.01 * Properties.Settings.Default.setDisplay_snapDistanceSmall;

                    ABLine.MoveABLine(-dist);

                    //FileSaveABLine();
                }
                else if (curve.isCurveSet)
                {
                    //snap distance is in cm
                    yt.ResetCreatedYouTurn();
                    double dist = 0.01 * Properties.Settings.Default.setDisplay_snapDistanceSmall;

                    curve.MoveABCurve(-dist);
                }
                else
                {
                    var form = new FormTimedMessage(2000, (gStr.gsNoGuidanceLines), (gStr.gsTurnOnContourOrMakeABLine));
                    form.Show();
                }
            }
        }
        private void SnapSmallRight()
        {
            if (!ct.isContourBtnOn)
            {
                if (ABLine.isABLineSet)
                {
                    //snap distance is in cm
                    yt.ResetCreatedYouTurn();
                    double dist = 0.01 * Properties.Settings.Default.setDisplay_snapDistanceSmall;
                    ABLine.MoveABLine(dist);

                    //FileSaveABLine();
                }
                else if (curve.isCurveSet)
                {
                    //snap distance is in cm
                    yt.ResetCreatedYouTurn();
                    double dist = 0.01 * Properties.Settings.Default.setDisplay_snapDistanceSmall;
                    curve.MoveABCurve(dist);

                }
                else
                {
                    var form = new FormTimedMessage(2000, (gStr.gsNoGuidanceLines), (gStr.gsTurnOnContourOrMakeABLine));
                    form.Show();
                }
            }
        }
        private void SnapRight()
        {
            if (!ct.isContourBtnOn)
            {
                if (ABLine.isABLineSet)
                {
                    //snap distance is in cm
                    yt.ResetCreatedYouTurn();
                    double dist = 0.01 * Properties.Settings.Default.setDisplay_snapDistance;

                    ABLine.MoveABLine(dist);
                }
                else if (curve.isCurveSet)
                {
                    //snap distance is in cm
                    yt.ResetCreatedYouTurn();
                    double dist = 0.01 * Properties.Settings.Default.setDisplay_snapDistance;
                    curve.MoveABCurve(dist);

                }
                else
                {
                    var form = new FormTimedMessage(2000, (gStr.gsNoGuidanceLines), (gStr.gsTurnOnContourOrMakeABLine));
                    form.Show();
                }
            }

        }
        private void SnapLeft()
        {
            if (!ct.isContourBtnOn)
            {
                if (ABLine.isABLineSet)
                {
                    //snap distance is in cm
                    yt.ResetCreatedYouTurn();
                    double dist = 0.01 * Properties.Settings.Default.setDisplay_snapDistance;

                    ABLine.MoveABLine(-dist);

                    //FileSaveABLine();
                }
                else if (curve.isCurveSet)
                {
                    //snap distance is in cm
                    yt.ResetCreatedYouTurn();
                    double dist = 0.01 * Properties.Settings.Default.setDisplay_snapDistance;

                    curve.MoveABCurve(-dist);

                }
                else
                {
                    var form = new FormTimedMessage(2000, (gStr.gsNoGuidanceLines), (gStr.gsTurnOnContourOrMakeABLine));
                    form.Show();
                }
            }
        }

        private void btnSmallSnapLeft_Click(object sender, EventArgs e)
        { 
                SnapSmallLeft();
        }
        private void btnSmallSnapRight_Click(object sender, EventArgs e)
        {
            SnapSmallRight();
        }
        private void btnSnapRight_Click(object sender, EventArgs e)
        {
            SnapRight();
        }
        private void btnSnapLeft_Click(object sender, EventArgs e)
        {
            SnapLeft();
        }


        //Section Manual and Auto
        private void btnManualOffOn_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Asterisk.Play();

            switch (manualBtnState)
            {
                case btnStates.Off:
                    manualBtnState = btnStates.On;
                    btnManualOffOn.Image = Properties.Resources.ManualOn;

                    //if Auto is on, turn it off
                    autoBtnState = btnStates.Off;
                    btnSectionOffAutoOn.Image = Properties.Resources.SectionMasterOff;

                    //turn all the sections allowed and update to ON!! Auto changes to ON
                    for (int j = 0; j < vehicle.numOfSections; j++)
                    {
                        section[j].isAllowedOn = true;
                        section[j].manBtnState = manBtn.Auto;
                    }

                    ManualAllBtnsUpdate();
                    break;

                case btnStates.On:
                    manualBtnState = btnStates.Off;
                    btnManualOffOn.Image = Properties.Resources.ManualOff;

                    //turn section buttons all OFF or Auto if SectionAuto was on or off
                    for (int j = 0; j < vehicle.numOfSections; j++)
                    {
                        section[j].isAllowedOn = false;
                        section[j].manBtnState = manBtn.On;
                    }

                    //Update the button colors and text
                    ManualAllBtnsUpdate();
                    break;
            }
        }
        private void btnSectionOffAutoOn_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Exclamation.Play();

            switch (autoBtnState)
            {
                case btnStates.Off:

                    autoBtnState = btnStates.Auto;
                    btnSectionOffAutoOn.Image = Properties.Resources.SectionMasterOn;

                    //turn off manual if on
                    manualBtnState = btnStates.Off;
                    btnManualOffOn.Image = Properties.Resources.ManualOff;

                    //turn all the sections allowed and update to ON!! Auto changes to ON
                    for (int j = 0; j < vehicle.numOfSections; j++)
                    {
                        section[j].isAllowedOn = true;
                        section[j].manBtnState = manBtn.Off;
                    }

                    ManualAllBtnsUpdate();
                    break;

                case btnStates.Auto:
                    autoBtnState = btnStates.Off;

                    btnSectionOffAutoOn.Image = Properties.Resources.SectionMasterOff;

                    //turn section buttons all OFF or Auto if SectionAuto was on or off
                    for (int j = 0; j < vehicle.numOfSections; j++)
                    {
                        section[j].isAllowedOn = false;
                        section[j].manBtnState = manBtn.On;
                    }

                    //Update the button colors and text
                    ManualAllBtnsUpdate();
                    break;
            }
        }

        //individual buttons for sections
        private void btnSection1Man_Click(object sender, EventArgs e)
        {
            if (autoBtnState != btnStates.Auto)
            {
                //if auto is off just have on-off for choices of section buttons
                if (section[0].manBtnState == manBtn.Off) section[0].manBtnState = manBtn.Auto;
                ManualBtnUpdate(0, btnSection1Man);
                return;
            }

            ManualBtnUpdate(0, btnSection1Man);
        }
        private void btnSection2Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[1].manBtnState == manBtn.Off) section[1].manBtnState = manBtn.Auto;
                ManualBtnUpdate(1, btnSection2Man);
                return;
            }

            ManualBtnUpdate(1, btnSection2Man);
        }
        private void btnSection3Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[2].manBtnState == manBtn.Off) section[2].manBtnState = manBtn.Auto;
                ManualBtnUpdate(2, btnSection3Man);
                return;
            }

            ManualBtnUpdate(2, btnSection3Man);
        }
        private void btnSection4Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[3].manBtnState == manBtn.Off) section[3].manBtnState = manBtn.Auto;
                ManualBtnUpdate(3, btnSection4Man);
                return;
            }
            ManualBtnUpdate(3, btnSection4Man);
        }
        private void btnSection5Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[4].manBtnState == manBtn.Off) section[4].manBtnState = manBtn.Auto;
                ManualBtnUpdate(4, btnSection5Man);
                return;
            }

            ManualBtnUpdate(4, btnSection5Man);
        }
        private void btnSection6Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[5].manBtnState == manBtn.Off) section[5].manBtnState = manBtn.Auto;
                ManualBtnUpdate(5, btnSection6Man);
                return;
            }

            ManualBtnUpdate(5, btnSection6Man);
        }
        private void btnSection7Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[6].manBtnState == manBtn.Off) section[6].manBtnState = manBtn.Auto;
                ManualBtnUpdate(6, btnSection7Man);
                return;
            }

            ManualBtnUpdate(6, btnSection7Man);
        }
        private void btnSection8Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[7].manBtnState == manBtn.Off) section[7].manBtnState = manBtn.Auto;
                ManualBtnUpdate(7, btnSection8Man);
                return;
            }

            ManualBtnUpdate(7, btnSection8Man);
        }
        private void btnSection9Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[8].manBtnState == manBtn.Off) section[8].manBtnState = manBtn.Auto;
                ManualBtnUpdate(8, btnSection9Man);
                return;
            }

            ManualBtnUpdate(8, btnSection9Man);

        }
        private void btnSection10Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[9].manBtnState == manBtn.Off) section[9].manBtnState = manBtn.Auto;
                ManualBtnUpdate(9, btnSection10Man);
                return;
            }

            ManualBtnUpdate(9, btnSection10Man);

        }
        private void btnSection11Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[10].manBtnState == manBtn.Off) section[10].manBtnState = manBtn.Auto;
                ManualBtnUpdate(10, btnSection11Man);
                return;
            }

            ManualBtnUpdate(10, btnSection11Man);

        }
        private void btnSection12Man_Click(object sender, EventArgs e)
        {
            //if auto is off just have on-off for choices of section buttons
            if (autoBtnState != btnStates.Auto)
            {
                if (section[11].manBtnState == manBtn.Off) section[11].manBtnState = manBtn.Auto;
                ManualBtnUpdate(11, btnSection12Man);
                return;
            }

            ManualBtnUpdate(11, btnSection12Man);
        }
        
        //The zoom tilt buttons
        private void btnZoomIn_MouseDown(object sender, MouseEventArgs e)
        {
            if (camera.zoomValue <= 20) camera.zoomValue += camera.zoomValue * 0.1;
            else camera.zoomValue += camera.zoomValue * 0.05;
            if (camera.zoomValue > 220) camera.zoomValue = 220;
            camera.camSetDistance = camera.zoomValue * camera.zoomValue * -1;
            SetZoom();
        }
        private void btnZoomOut_MouseDown(object sender, MouseEventArgs e)
        {
            if (camera.zoomValue <= 20)
            { if ((camera.zoomValue -= camera.zoomValue * 0.1) < 6.0) camera.zoomValue = 6.0; }
            else { if ((camera.zoomValue -= camera.zoomValue * 0.05) < 6.0) camera.zoomValue = 6.0; }
            camera.camSetDistance = camera.zoomValue * camera.zoomValue * -1;
            SetZoom();
        }
        private void btnpTiltUp_MouseDown(object sender, MouseEventArgs e)
        {
            camera.camPitch -= ((camera.camPitch * 0.02) - 1);
            if (camera.camPitch > 0) camera.camPitch = 0;
        }
        private void btnpTiltDown_MouseDown(object sender, MouseEventArgs e)
        {
            camera.camPitch += ((camera.camPitch * 0.02) - 1);
            if (camera.camPitch < -80) camera.camPitch = -80;
        }
        private void btnZoomExtents_Click(object sender, EventArgs e)
        {
            //if (isJobStarted)
            {
                if (camera.camSetDistance < -400) camera.camSetDistance = -250;
                else camera.camSetDistance = -5 * maxFieldDistance;
                if (camera.camSetDistance == 0) camera.camSetDistance = -2000;
                SetZoom();
            }
        }

        private void btnFlag_Click(object sender, EventArgs e)
        {
            int nextflag = flagPts.Count + 1;
            CFlag flagPt = new CFlag(pn.latitude, pn.longitude, pn.fix.easting, pn.fix.northing, flagColor, nextflag);
            flagPts.Add(flagPt);
            FileSaveFlags();
        }

        private void btnSmoothAB_Click(object sender, EventArgs e)
        {
            if (isJobStarted && curve.isCurveBtnOn)
            {
                using (var form = new FormSmoothAB(this))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK) { }
                }
            }

            else
            {
                TimedMessageBox(2000, gStr.gsFieldNotOpen, gStr.gsStartNewField);
            }
        }

        //YouTurn on off
        private void btnLeftYouTurn_Click(object sender, EventArgs e)
        {
            if (yt.isYouTurnTriggered)
            {
                //is it turning left already?
                if (!yt.isYouTurnRight)
                {
                    yt.ResetYouTurn();
                    AutoYouTurnButtonsReset();
                }
                else
                {
                    yt.isYouTurnRight = false;
                    AutoYouTurnButtonsLeftTurn();
                }
            }
            else
            {
                if (yt.isYouTurnTriggered)
                {
                    yt.ResetYouTurn();
                    AutoYouTurnButtonsReset();
                }
                else
                {
                    yt.isYouTurnTriggered = true;
                    yt.BuildManualYouTurn(false, true);
                    AutoYouTurnButtonsLeftTurn();
                }
            }
        }
        private void btnRightYouTurn_Click(object sender, EventArgs e)
        {
            //is it already turning right, then cancel autoturn
            if (yt.isYouTurnTriggered)
            {
                //is it turning right already?
                if (yt.isYouTurnRight)
                {
                    yt.ResetYouTurn();
                    AutoYouTurnButtonsReset();
                }
                else
                {
                    //make it turn the other way
                    yt.isYouTurnRight = true;
                    AutoYouTurnButtonsRightTurn();
                }
            }
            else
            {
                if (yt.isYouTurnTriggered)
                {
                    yt.ResetYouTurn();
                    AutoYouTurnButtonsReset();
                }
                else
                {
                    yt.isYouTurnTriggered = true;
                    yt.BuildManualYouTurn(true, true);
                    AutoYouTurnButtonsRightTurn();
                }
            }
        }
        private void btnSwapDirection_Click_1(object sender, EventArgs e)
        {
            if (!yt.isYouTurnTriggered)
            {
                //is it turning right already?
                if (yt.isYouTurnRight)
                {
                    yt.isYouTurnRight = false;
                    yt.isLastYouTurnRight = !yt.isLastYouTurnRight;
                    AutoYouTurnButtonsReset();
                }
                else
                {
                    //make it turn the other way
                    yt.isYouTurnRight = true;
                    yt.isLastYouTurnRight = !yt.isLastYouTurnRight;
                    AutoYouTurnButtonsReset();
                }
            }
        }

        private void btnEnableAutoYouTurn_Click(object sender, EventArgs e)
        {
            if (bnd.bndArr.Count == 0)
            {
                TimedMessageBox(2000, gStr.gsNoBoundary, gStr.gsCreateABoundaryFirst);
                return;
            }

            if (!yt.isYouTurnBtnOn)
            {
                //new direction so reset where to put turn diagnostic
                yt.ResetCreatedYouTurn();

                if (!isAutoSteerBtnOn) return;

                yt.isYouTurnBtnOn = true;
                yt.isYouTurnBtnOn = true;
                yt.isTurnCreationTooClose = false;
                yt.isTurnCreationNotCrossingError = false;
                yt.ResetYouTurn();
                //mc.autoSteerData[mc.sdX] = 0;
                mc.machineControlData[mc.cnYouTurn] = 0;
                btnEnableAutoYouTurn.Image = Properties.Resources.Youturn80;
            }
            else
            {
                yt.isYouTurnBtnOn = false;
                yt.rowSkipsWidth = Properties.Vehicle.Default.set_youSkipWidth;
                btnEnableAutoYouTurn.Image = Properties.Resources.YouTurnNo;
                yt.ResetYouTurn();

                //new direction so reset where to put turn diagnostic
                yt.ResetCreatedYouTurn();

                //mc.autoSteerData[mc.sdX] = 0;
                mc.machineControlData[mc.cnYouTurn] = 0;
            }
        }
        public void AutoYouTurnButtonsRightTurn()
        {
            btnRightYouTurn.BackColor = Color.Yellow;
            btnRightYouTurn.Height = 95;
            btnRightYouTurn.Width = 95;
            btnLeftYouTurn.Height = 66;
            btnLeftYouTurn.Width = 80;
            btnLeftYouTurn.Text = "";
            btnLeftYouTurn.BackColor = Color.Transparent;
        }
        public void AutoYouTurnButtonsLeftTurn()
        {
            btnRightYouTurn.BackColor = Color.Transparent;
            btnRightYouTurn.Height = 66;
            btnRightYouTurn.Width = 80;
            btnRightYouTurn.Text = "";
            btnLeftYouTurn.Height = 95;
            btnLeftYouTurn.Width = 95;
            btnLeftYouTurn.BackColor = Color.Yellow;
        }
        public void AutoYouTurnButtonsReset()
        {
            //new direction so reset where to put turn diagnostic
            yt.ResetCreatedYouTurn();

            //fix the buttons
            btnLeftYouTurn.BackColor = Color.Transparent;
            btnRightYouTurn.BackColor = Color.Transparent;
            btnLeftYouTurn.Height = 66;
            btnLeftYouTurn.Width = 80;
            btnRightYouTurn.Height = 66;
            btnRightYouTurn.Width = 80;
            btnLeftYouTurn.Text = "";
            btnRightYouTurn.Text = "";

            // why yes it is backwards, puzzling
            if (!yt.isYouTurnRight)
            {
                btnLeftYouTurn.BackColor = Color.Transparent;
                btnRightYouTurn.BackColor = Color.LightGreen;
            }
            else
            {
                btnLeftYouTurn.BackColor = Color.LightGreen;
                btnRightYouTurn.BackColor = Color.Transparent;
            }
        }
        public void EnableYouTurnButtons()
        {
            btnRightYouTurn.Enabled = true;
            btnLeftYouTurn.Enabled = true;
            btnRightYouTurn.Visible = true;
            btnLeftYouTurn.Visible = true;
            btnSwapDirection.Visible = true;

            //auto YouTurn disabled
            yt.isYouTurnBtnOn = false;
            yt.ResetYouTurn();

            //turn off youturn...
            btnEnableAutoYouTurn.Enabled = true;
            yt.isYouTurnBtnOn = false;
            btnEnableAutoYouTurn.Image = Properties.Resources.YouTurnNo;
        }
        public void DisableYouTurnButtons()
        {
            btnRightYouTurn.Enabled = false;
            btnLeftYouTurn.Enabled = false;
            btnRightYouTurn.Visible = false;
            btnLeftYouTurn.Visible = false;
            btnSwapDirection.Visible = false;

            btnEnableAutoYouTurn.Enabled = false;
            yt.isYouTurnBtnOn = false;
            btnEnableAutoYouTurn.Image = Properties.Resources.YouTurnNo;
            yt.ResetYouTurn();
        }

        //Options
        private void btnFlagsGoogleEarth_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                //save new copy of flags
                FileSaveFlagsKML();

                //Process.Start(@"C:\Program Files (x86)\Google\Google Earth\client\googleearth", workingDirectory + currentFieldDirectory + "\\Flags.KML");
                Process.Start(fieldsDirectory + currentFieldDirectory + "\\Flags.KML");
            }
            else
            {
                var form = new FormTimedMessage(1500, gStr.gsFieldNotOpen, gStr.gsStartNewField);
                form.Show();
            }
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process.Start("http://AgOpenGPS.gh-ortner.com/doku.php");
        }
        private void btnDeleteAllData_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                DialogResult result3 = MessageBox.Show(gStr.gsDeleteAllContoursAndSections,
                    gStr.gsDeleteForSure,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (result3 == DialogResult.Yes)
                {
                    FileCreateContour();
                    FileCreateSections();
                    FileCreateElevation();

                    //turn auto button off
                    autoBtnState = btnStates.Off;
                    btnSectionOffAutoOn.Image = Properties.Resources.SectionMasterOff;

                    //turn section buttons all OFF and zero square meters
                    for (int j = 0; j < MAXSECTIONS; j++)
                    {
                        section[j].isAllowedOn = false;
                        section[j].manBtnState = manBtn.On;
                    }

                    //turn manual button off
                    manualBtnState = btnStates.Off;
                    btnManualOffOn.Image = Properties.Resources.ManualOff;

                    //Update the button colors and text
                    ManualAllBtnsUpdate();

                    //enable disable manual buttons
                    LineUpManualBtns();

                    //clear the section lists
                    for (int j = 0; j < MAXSECTIONS; j++)
                    {
                        //clean out the lists
                        section[j].patchList?.Clear();
                        section[j].triangleList?.Clear();
                    }

                    //clear out the contour Lists
                    ct.StopContourLine(pivotAxlePos);
                    ct.ResetContour();
                    fd.workedAreaTotal = 0;
                }
                else TimedMessageBox(1500, gStr.gsNothingDeleted, gStr.gsActionHasBeenCancelled);
            }
        }
        private void cboxpRowWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            yt.rowSkipsWidth = cboxpRowWidth.SelectedIndex + 1;
            yt.ResetCreatedYouTurn();
            Properties.Vehicle.Default.set_youSkipWidth = yt.rowSkipsWidth;
            Properties.Vehicle.Default.Save();
        }

        // Menu Items ------------------------------------------------------------------

        //File drop down items
        private void loadVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }
            if (FileOpenVehicle())
            {
                using (var form = new FormSettings(this, 0))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK) { }
                }
                using (var form = new FormIMU(this))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK) { }
                }

                TimedMessageBox(3000, gStr.gsDidyoumakechangestothevehicle, gStr.gsBesuretosavevehicleifyoudid);
            }
        }
        private void saveVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileSaveVehicle();
        }
        private void fieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JobNewOpenResume();
        }
        private void setWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Currently: " + Settings.Default.setF_workingDirectory;

            if (Settings.Default.setF_workingDirectory == "Default") fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else fbd.SelectedPath = Settings.Default.setF_workingDirectory;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AgOpenGPS",true);

                if (fbd.SelectedPath != Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
                {
                    //save the user set directory in Registry
                    regKey.SetValue("Directory", fbd.SelectedPath);
                    regKey.Close();
                    Settings.Default.setF_workingDirectory = fbd.SelectedPath;
                    Settings.Default.Save();
                }
                else
                {
                    regKey.SetValue("Directory", "Default");
                    regKey.Close();
                    Settings.Default.setF_workingDirectory = "Default";
                    Settings.Default.Save();
                }

                //restart program
                MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
                Close();
            }
        }
        private void enterSimCoordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = DialogResult.Cancel;
            using (var form = new FormSimCoords(this))
            {
                result = form.ShowDialog();
            }

            if (result == DialogResult.OK)
            {
                MessageBox.Show(gStr.gsProgramWillExitPleaseRestart, gStr.gsProgramWillExitPleaseRestart);
                if (isJobStarted) JobClose();
                Application.Exit();
            }
        }

        //Languages
        private void menuLanguageEnglish_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }
            SetLanguage("en");
            MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
            Close();

        }
        private void menuLanguageDeutsch_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }
            SetLanguage("de");
            MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
            Close();

        }
        private void menuLanguageRussian_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }
            SetLanguage("ru");
            MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
            Close();
        }
        private void menuLanguageDutch_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }
            SetLanguage("nl");
            MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
            Close();
        }
        private void menuLanguageSpanish_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }
            SetLanguage("es");
            MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
            Close();
        }
        private void menuLanguageFrench_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }
            SetLanguage("fr");
            MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
            Close();
        }
        private void menuLanguageItalian_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                var form = new FormTimedMessage(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                form.Show();
                return;
            }
            SetLanguage("it");
            MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
            Close();
        }
        private void SetLanguage(string lang)
        {
            //reset them all to false
            menuLanguageEnglish.Checked = false;
            menuLanguageDeutsch.Checked = false;
            menuLanguageRussian.Checked = false;
            menuLanguageDutch.Checked = false;
            menuLanguageSpanish.Checked = false;
            menuLanguageFrench.Checked = false;
            menuLanguageItalian.Checked = false;

            switch (lang)
            {
                case "en":
                    menuLanguageEnglish.Checked = true;
                    break;

                case "ru":
                    menuLanguageRussian.Checked = true;
                    break;

                case "de":
                    menuLanguageDeutsch.Checked = true;
                    break;

                case "nl":
                    menuLanguageDutch.Checked = true;
                    break;

                case "it":
                    menuLanguageItalian.Checked = true;
                    break;

                case "es":
                    menuLanguageSpanish.Checked = true;
                    break;

                case "fr":
                    menuLanguageFrench.Checked = true;
                    break;
            }

            //adding or editing "Language" subkey to the "SOFTWARE" subkey  
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AgOpenGPS");

            //storing the values  
            key.SetValue("Language", lang);
            key.Close();
        }

        //Help menu drop down items
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new Form_About())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) { }
            }
        }

        //Shortcut keys
        private void shortcutKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FormShortcutKeys())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) { }
            }
        }

        //Options Drop down menu items
        private void resetALLToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                MessageBox.Show(gStr.gsCloseFieldFirst);
            }
            else
            {
                DialogResult result2 = MessageBox.Show(gStr.gsReallyResetEverything, gStr.gsResetAll,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result2 == DialogResult.Yes)
                {
                    Settings.Default.Reset();
                    Settings.Default.Save();
                    Vehicle.Default.Reset();
                    Vehicle.Default.Save();
                    MessageBox.Show(gStr.gsProgramWillExitPleaseRestart);
                    Application.Exit();
                }
            }
        }
        private void logNMEAMenuItem_Click(object sender, EventArgs e)
        {
            isLogNMEA = !isLogNMEA;
            logNMEAMenuItem.Checked = isLogNMEA;
            Settings.Default.setMenu_isLogNMEA = isLogNMEA;
            Settings.Default.Save();
        }
        private void lightbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isLightbarOn = !isLightbarOn;
            lightbarToolStripMenuItem.Checked = isLightbarOn;
            Settings.Default.setMenu_isLightbarOn = isLightbarOn;
            Settings.Default.Save();
        }
        private void polygonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isDrawPolygons = !isDrawPolygons;
            polygonsToolStripMenuItem.Checked = !polygonsToolStripMenuItem.Checked;
        }
        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isGridOn = !isGridOn;
            gridToolStripMenuItem.Checked = isGridOn;
            Settings.Default.setMenu_isGridOn = isGridOn;
            Settings.Default.Save();
        }
        private void sideGuideLines_Click(object sender, EventArgs e)
        {
            isSideGuideLines = !isSideGuideLines;
            sideGuideLines.Checked = isSideGuideLines;
            Settings.Default.setMenu_isSideGuideLines = isSideGuideLines;
            Settings.Default.Save();
        }
        private void pursuitLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isPureDisplayOn = !isPureDisplayOn;
            pursuitLineToolStripMenuItem.Checked = isPureDisplayOn;
            Settings.Default.setMenu_isPureOn = isPureDisplayOn;
            Settings.Default.Save();
        }
        private void metricToolStrip_Click(object sender, EventArgs e)
        {
            metricToolStrip.Checked = true;
            imperialToolStrip.Checked = false;
            isMetric = true;
            Settings.Default.setMenu_isMetric = isMetric;
            Settings.Default.Save();
            lblSpeedUnits.Text = gStr.gsKMH;
        }
        private void skyToolStripMenu_Click(object sender, EventArgs e)
        {
            isSkyOn = !isSkyOn;
            skyToolStripMenu.Checked = isSkyOn;
            Settings.Default.setMenu_isSkyOn = isSkyOn;
            Settings.Default.Save();
        }
        private void imperialToolStrip_Click(object sender, EventArgs e)
        {
            metricToolStrip.Checked = false;
            imperialToolStrip.Checked = true;
            isMetric = false;
            Settings.Default.setMenu_isMetric = isMetric;
            Settings.Default.Save();
            lblSpeedUnits.Text = gStr.gsMPH;
        }
        private void simulatorOnToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (spGPS.IsOpen)
            {
                simulatorOnToolStripMenuItem.Checked = false;
                panelSim.Visible = false;
                timerSim.Enabled = false;

                TimedMessageBox(2000, gStr.gsGPSConnected, gStr.gsSimulatorForcedOff);
            }
            else
            {
                if (isJobStarted)
                {
                    TimedMessageBox(2000, gStr.gsFieldIsOpen, gStr.gsCloseFieldFirst);
                    return;
                }
                if (simulatorOnToolStripMenuItem.Checked)
                {
                    panelSim.Visible = true;
                    timerSim.Enabled = true;
                    DialogResult result3 = MessageBox.Show(gStr.gsAgOpenGPSWillExitPlzRestart, gStr.gsTurningOnSimulator ,MessageBoxButtons.OK);
                    Application.Exit();

                }
                else
                {
                    panelSim.Visible = false;
                    timerSim.Enabled = false;
                    //TimedMessageBox(3000, "Simulator Turning Off", "Application will Exit");
                    DialogResult result3 = MessageBox.Show(gStr.gsAgOpenGPSWillExitPlzRestart, gStr.gsTurningOffSimulator, MessageBoxButtons.OK);
                    Application.Exit();
                }
            }

            Settings.Default.setMenu_isSimulatorOn = simulatorOnToolStripMenuItem.Checked;
            Settings.Default.Save();
            LineUpManualBtns();
        }

        //setting color off Options Menu
        private void sectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //color picker for sections
            ColorDialog colorDlg = new ColorDialog
            {
                FullOpen = true,
                AnyColor = true,
                SolidColorOnly = false,
                Color = Color.FromArgb(255, redSections, grnSections, bluSections)
            };

            if (colorDlg.ShowDialog() != DialogResult.OK) return;

            redSections = colorDlg.Color.R;
            if (redSections > 253) redSections = 253;
            grnSections = colorDlg.Color.G;
            if (grnSections > 253) grnSections = 253;
            bluSections = colorDlg.Color.B;
            if (bluSections > 253) bluSections = 253;

            Settings.Default.setF_SectionColorR = redSections;
            Settings.Default.setF_SectionColorG = grnSections;
            Settings.Default.setF_SectionColorB = bluSections;
            Settings.Default.Save();
        }
        private void fieldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //color picker for fields

            ColorDialog colorDlg = new ColorDialog
            {
                FullOpen = true,
                AnyColor = true,
                SolidColorOnly = false,
                Color = Color.FromArgb(255, Settings.Default.setF_FieldColorR,
                Settings.Default.setF_FieldColorG, Settings.Default.setF_FieldColorB)
            };

            if (colorDlg.ShowDialog() != DialogResult.OK) return;

            redField = colorDlg.Color.R;
            if (redField > 253) redField = 253;
            grnField = colorDlg.Color.G;
            if (grnField > 253) grnField = 253;
            bluField = colorDlg.Color.B;
            if (bluField > 253) bluField = 253;

            Settings.Default.setF_FieldColorR = redField;
            Settings.Default.setF_FieldColorG = grnField;
            Settings.Default.setF_FieldColorB = bluField;
            Settings.Default.Save();
        }

        //Area button context menu items
        private void toolStripMenuAreaSide_Click(object sender, EventArgs e)
        {
            isAreaOnRight = !isAreaOnRight;
            Settings.Default.setMenu_isAreaRight = isAreaOnRight;
            Settings.Default.Save();
        }

        //The flag context menus
        private void toolStripMenuItemFlagRed_Click(object sender, EventArgs e)
        {
            flagColor = 0;
            btnFlag.Image = Properties.Resources.FlagRed;
        }
        private void toolStripMenuGrn_Click(object sender, EventArgs e)
        {
            flagColor = 1;
            btnFlag.Image = Properties.Resources.FlagGrn;
        }
        private void toolStripMenuYel_Click(object sender, EventArgs e)
        {
            flagColor = 2;
            btnFlag.Image = Properties.Resources.FlagYel;
        }
        private void toolStripMenuFlagDelete_Click(object sender, EventArgs e)
        {
            //delete selected flag and set selected to none
            DeleteSelectedFlag();
            FileSaveFlags();
        }
        private void toolStripMenuFlagDeleteAll_Click(object sender, EventArgs e)
        {
            flagNumberPicked = 0;
            flagPts.Clear();
            FileSaveFlags();
        }
        private void contextMenuStripFlag_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            toolStripMenuFlagDelete.Enabled = flagNumberPicked != 0;

            toolStripMenuFlagDeleteAll.Enabled = flagPts.Count > 0;
        }


        //OpenGL Window context Menu and functions
        private void deleteFlagToolOpenGLContextMenu_Click(object sender, EventArgs e)
        {
            //delete selected flag and set selected to none
            DeleteSelectedFlag();
        }
        private void contextMenuStripOpenGL_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //dont bring up menu if no flag selected
            if (flagNumberPicked == 0) e.Cancel = true;
        }
        private void googleEarthOpenGLContextMenu_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                //save new copy of kml with selected flag and view in GoogleEarth
                FileSaveSingleFlagKML(flagNumberPicked);

                //Process.Start(@"C:\Program Files (x86)\Google\Google Earth\client\googleearth", workingDirectory + currentFieldDirectory + "\\Flags.KML");
                Process.Start(fieldsDirectory + currentFieldDirectory + "\\Flag.KML");
            }
        }

        private void oglMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //0 at bottom for opengl, 0 at top for windows, so invert Y value
                Point point = oglMain.PointToClient(Cursor.Position);
                mouseX = point.X;
                mouseY = oglMain.Height - point.Y;
                leftMouseDownOnOpenGL = true;
            }
        }

        //taskbar buttons
        private void ToolstripExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void toolStripBtnSmoothABCurve_Click(object sender, EventArgs e)
        {
            if (isJobStarted && curve.isCurveBtnOn)
            {
                using (var form = new FormSmoothAB(this))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK) { }
                }
            }
            else
            {
                if (!isJobStarted)  TimedMessageBox(2000, gStr.gsFieldNotOpen, gStr.gsStartNewField);
                else TimedMessageBox(2000, gStr.gsCurveNotOn, gStr.gsTurnABCurveOn);
            }
        }
        private void toolStripAreYouSure_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                DialogResult result3 = MessageBox.Show(gStr.gsDeleteAllContoursAndSections,
                    gStr.gsDeleteForSure,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (result3 == DialogResult.Yes)
                {
                    FileCreateContour();
                    FileCreateSections();
                    //FileCreateElevation();

                    //turn auto button off
                    autoBtnState = btnStates.Off;
                    btnSectionOffAutoOn.Image = Properties.Resources.SectionMasterOff;

                    //turn section buttons all OFF and zero square meters
                    for (int j = 0; j < MAXSECTIONS; j++)
                    {
                        section[j].isAllowedOn = false;
                        section[j].manBtnState = manBtn.On;
                    }

                    //turn manual button off
                    manualBtnState = btnStates.Off;
                    btnManualOffOn.Image = Properties.Resources.ManualOff;

                    //Update the button colors and text
                    ManualAllBtnsUpdate();

                    //enable disable manual buttons
                    LineUpManualBtns();

                    //clear out the contour Lists
                    ct.StopContourLine(pivotAxlePos);
                    ct.ResetContour();
                    fd.workedAreaTotal = 0;

                    //clear the section lists
                    for (int j = 0; j < MAXSECTIONS; j++)
                    {
                        //clean out the lists
                        section[j].patchList?.Clear();
                        section[j].triangleList?.Clear();
                    }
                    patchSaveList?.Clear();
                }
                else TimedMessageBox(1500, gStr.gsNothingDeleted, gStr.gsActionHasBeenCancelled);
            }
        }
        private void toolStripBtnMakeBndContour_Click(object sender, EventArgs e)
        {
            //build all the contour guidance lines from boundaries, all of them. 
            using (var form = new FormMakeBndCon(this))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) { }
            }
        }
        private void toolstripYouTurnConfig_Click(object sender, EventArgs e)
        {
            var form = new FormYouTurn(this);
            form.ShowDialog();
            cboxpRowWidth.SelectedIndex = yt.rowSkipsWidth - 1;
        }
        private void treePlanterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check if window already exists
            Form fc = Application.OpenForms["FormTreePlant"];

            if (fc != null)
            {
                fc.Focus();
                return;
            }

            //
            Form form = new FormTreePlant(this);
            form.Show();

        }
        
        private void toolstripAutoSteerConfig_Click(object sender, EventArgs e)
        {
            //check if window already exists
            Form fc = Application.OpenForms["FormSteer"];

            if (fc != null)
            {
                fc.Focus();
                return;
            }

            //
            Form form = new FormSteer(this);
            form.Show();
        }
        private void toolStripAutoSteerChart_Click(object sender, EventArgs e)
        {
            //check if window already exists
            Form fcg = Application.OpenForms["FormSteerGraph"];

            if (fcg != null)
            {
                fcg.Focus();
                return;
            }

            //
            Form formG = new FormSteerGraph(this);
            formG.Show();
        }
        private void toolStripNTRIPConfig_Click(object sender, EventArgs e)
        {
            SettingsNTRIP();
        }
        private void deleteContourPathsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ct.stripList?.Clear();
            ct.ptList?.Clear();
            ct.ctList?.Clear();
            contourSaveList?.Clear();
        }
        
        private void toolstripVehicleConfig_Click(object sender, EventArgs e)
        {
            using (var form = new FormSettings(this, 0))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (Properties.Settings.Default.setAS_isAutoSteerAutoOn) btnAutoSteer.Text = "A";
                    else btnAutoSteer.Text = "M";
                }
            }
        }
        private void toolstripDisplayConfig_Click(object sender, EventArgs e)
        {
            using (var form = new FormIMU(this))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) { }
            }

            if (Properties.Settings.Default.setAS_isAutoSteerAutoOn) btnAutoSteer.Text = "A";
            else btnAutoSteer.Text = "M";

        }
        private void toolstripUSBPortsConfig_Click(object sender, EventArgs e)
        {
            SettingsCommunications();
        }
        private void toolstripUDPConfig_Click(object sender, EventArgs e)
        {
            SettingsUDP();
        }
        private void toolstripResetTrip_Click_1(object sender, EventArgs e)
        {
            fd.distanceUser = 0;
            fd.workedAreaTotalUser = 0;
        }
        private void toolstripVR_Click(object sender, EventArgs e)
        {
            if (!isJobStarted)
            {
                TimedMessageBox(1000, gStr.gsFieldNotOpen, gStr.gsStartNewField);
                return;
            }

            //if (bnd.bndArr[0].isSet && (ABLine.isABLineSet | curve.isCurveSet))
            //{
            //    //field too small
            //    //if (bnd.bndArr[0].bndLine.Count < 4) { TimedMessageBox(3000, "!!!!", gStr.gsBoundaryTooSmall); return; }
            //    //using (var form = new FormHeadland(this))
            //    //{
            //    //    var result = form.ShowDialog();
            //    //    if (result == DialogResult.OK)
            //    //    {

            //    //    }
            //    //}
            //}
            //else { TimedMessageBox(3000, gStr.gsBoundaryNotSet, gStr.gsCreateBoundaryFirst); }
            //TimedMessageBox(1500, "Headlands not Implemented", "Some time soon they will be functional");
        }
        private void toolstripBoundary_Click(object sender, EventArgs e)
        {
            if (isJobStarted)
            {
                using (var form = new FormBoundary(this))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Form form2 = new FormBoundaryPlayer(this);
                        form2.Show();
                    }
                }
            }
            else { TimedMessageBox(3000, gStr.gsFieldNotOpen, gStr.gsStartNewField); }
        }
        private void toolstripField_Click(object sender, EventArgs e)
        {
            JobNewOpenResume();
        }
        private void toolStripBtnHideTabs_Click(object sender, EventArgs e)
        {
            SwapBatmanPanels();
        }
        private void toolStripBatman_Click(object sender, EventArgs e)
        {
            SwapBatmanPanels();
        }

        //camera tool buttons
        private void CameraFollowingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            camera.camFollowing = true;
            camera.camPitch = -70;
        }
        private void CameraNorthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            camera.camFollowing = false;
        }
        private void CameraTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            camera.camFollowing = true;
            camera.camPitch = 0;
        }

        //Sim controls
        private void timerSim_Tick(object sender, EventArgs e)
        {
            //if a GPS is connected disable sim
            if (!spGPS.IsOpen)
            {
                if (isAutoSteerBtnOn && (guidanceLineDistanceOff != 32000)) sim.DoSimTick(guidanceLineSteerAngle * 0.01);
                else if (recPath.isDrivingRecordedPath) sim.DoSimTick(guidanceLineSteerAngle * 0.01);
                //else if (self.isSelfDriving) sim.DoSimTick(guidanceLineSteerAngle * 0.01);
                else sim.DoSimTick(sim.steerAngleScrollBar);
            }
        }
        private void hsbarSteerAngle_Scroll(object sender, ScrollEventArgs e)
        {
            sim.steerAngleScrollBar = (hsbarSteerAngle.Value - 300) * 0.1;
            btnResetSteerAngle.Text = sim.steerAngleScrollBar.ToString("N1");
        }
        private void hsbarStepDistance_Scroll(object sender, ScrollEventArgs e)
        {
            sim.stepDistance = ((double)(hsbarStepDistance.Value)) / 10.0 / (double)fixUpdateHz;
        }
        private void btnResetSteerAngle_Click(object sender, EventArgs e)
        {
            sim.steerAngleScrollBar = 0;
            hsbarSteerAngle.Value = 300;
            btnResetSteerAngle.Text = sim.steerAngleScrollBar.ToString("N1");
        }
        private void btnResetSim_Click(object sender, EventArgs e)
        {
            sim.latitude = Properties.Settings.Default.setGPS_Latitude;
            sim.longitude = Properties.Settings.Default.setGPS_Longitude;
        }

        #region Properties // ---------------------------------------------------------------------

        public string Zone { get { return Convert.ToString(pn.zone); } }
        public string FixNorthing { get { return Convert.ToString(Math.Round(pn.fix.northing + pn.utmNorth, 2)); } }
        public string FixEasting { get { return Convert.ToString(Math.Round(pn.fix.easting + pn.utmEast, 2)); } }
        public string Latitude { get { return Convert.ToString(Math.Round(pn.latitude, 7)); } }
        public string Longitude { get { return Convert.ToString(Math.Round(pn.longitude, 7)); } }

        public string SatsTracked { get { return Convert.ToString(pn.satellitesTracked); } }
        public string HDOP { get { return Convert.ToString(pn.hdop); } }
        public string NMEAHz { get { return Convert.ToString(fixUpdateHz); } }
        public string PassNumber { get { return Convert.ToString(ABLine.passNumber); } }
        public string CurveNumber { get { return Convert.ToString(curve.curveNumber); } }
        public string Heading { get { return Convert.ToString(Math.Round(glm.toDegrees(fixHeading), 1)) + "\u00B0"; } }
        public string GPSHeading { get { return (Math.Round(glm.toDegrees(gpsHeading), 1)) + "\u00B0"; } }
        public string Status { get { if (pn.status == "A") return "Active"; else return "Void"; } }
        public string FixQuality
        {
            get
            {
                if (timerSim.Enabled)
                    return "Sim: ";
                else if (pn.fixQuality == 0) return "Invalid: ";
                else if (pn.fixQuality == 1) return "GPS single: ";
                else if (pn.fixQuality == 2) return "DGPS : ";
                else if (pn.fixQuality == 3) return "PPS : ";
                else if (pn.fixQuality == 4) return "RTK fix: ";
                else if (pn.fixQuality == 5) return "Float: ";
                else if (pn.fixQuality == 6) return "Estimate: ";
                else if (pn.fixQuality == 7) return "Man IP: ";
                else if (pn.fixQuality == 8) return "Sim: ";
                else return "Unknown: ";
            }
        }

        public string GyroInDegrees
        {
            get
            {
                if (ahrs.correctionHeadingX16 != 9999)
                    return Math.Round(ahrs.correctionHeadingX16 * 0.0625, 1) + "\u00B0";
                else return "-";
            }
        }
        public string RollInDegrees
        {
            get
            {
                if (ahrs.rollX16 != 9999)
                    return Math.Round((ahrs.rollX16 - ahrs.rollZeroX16) * 0.0625, 1) + "\u00B0";
                else return "-";
            }
        }
        public string SetSteerAngle { get { return ((double)(guidanceLineSteerAngle) * 0.01).ToString("N1") + "\u00B0"; } }
        public string ActualSteerAngle { get { return ((double)(actualSteerAngleDisp) * 0.01).ToString("N1") + "\u00B0"; } }

        public string FixHeading { get { return Math.Round(fixHeading, 4).ToString(); } }

        public string LookAhead { get { return ((int)(section[0].sectionLookAhead)).ToString(); } }
        public string StepFixNum { get { return (currentStepFix).ToString(); } }
        public string CurrentStepDistance { get { return Math.Round(distanceCurrentStepFix, 3).ToString(); } }
        public string TotalStepDistance { get { return Math.Round(fixStepDist, 3).ToString(); } }

        public string WorkSwitchValue { get { return mc.workSwitchValue.ToString(); } }
        public string AgeDiff { get { return pn.ageDiff.ToString(); } }

        //Metric and Imperial Properties
        public string SpeedMPH
        {
            get
            {
                double spd = 0;
                for (int c = 0; c < 10; c++) spd += avgSpeed[c];
                spd *= 0.0621371;
                return Convert.ToString(Math.Round(spd, 1));
            }
        }
        public string SpeedKPH
        {
            get
            {
                double spd = 0;
                for (int c = 0; c < 10; c++) spd += avgSpeed[c];
                spd *= 0.1;
                return Convert.ToString(Math.Round(spd, 1));
            }
        }

        public string XTE
        {
            get
            {
                //double spd = 0;
                //for (int c = 0; c < 20; c++) spd += avgXTE[c];
                //spd *= 0.1;
                //return ((int)(spd * 0.05) + " cm");
                return (crossTrackError/10 + gStr.gsCM);
            }
        }
        public string InchXTE
        {
            get
            {
                //double spd = 0;
                //for (int c = 0; c < 20; c++) spd += avgXTE[c];
                //spd *= 0.1;
                //return ((int)(spd * 0.019685) + " in");
                return ((int)(crossTrackError/25.54) + " in");
            }
        }

        public string FixOffset { get { return (pn.fixOffset.easting.ToString("N2") + ", " + pn.fixOffset.northing.ToString("N2")); } }
        public string FixOffsetInch { get { return ((pn.fixOffset.easting*glm.m2in).ToString("N0")+ ", " + (pn.fixOffset.northing*glm.m2in).ToString("N0")); } }

        public string Altitude { get { return Convert.ToString(Math.Round(pn.altitude,1)); } }
        public string AltitudeFeet { get { return Convert.ToString((Math.Round((pn.altitude * 3.28084),1))); } }

        public string PeriAreaAcres { get { return Math.Round(periArea.area * 0.000247105, 2).ToString(); } }
        public string PeriAreaHectares { get { return Math.Round(periArea.area * 0.0001, 2).ToString(); } }
        public string DistPivotM
        {
            get
            {
                if (distancePivotToTurnLine > 0 )
                    return ((int)(distancePivotToTurnLine)) + " m";
                else return "--";
            }
        }
        public string DistPivotFt
        {
            get
            {
                if (distancePivotToTurnLine > 0 ) return (((int)(glm.m2ft * (distancePivotToTurnLine))) + " ft");
                else return "--";
            }
        }

        #endregion properties 

        //Timer triggers at 15 msec, and is THE clock of the whole program
        //Timer stopped while parsing nmea
        private void tmrWatchdog_tick(object sender, EventArgs e)
        {

            //Check for a newline char, if none then just return
            //if (Properties.Settings.Default.setGPS_fixFromWhichSentence != "UBX")
            //{
                int cr = pn.rawBuffer.IndexOf("\n", StringComparison.Ordinal);
                if (cr == -1) return; // No end found, wait for more data
            //}



            //go see if data ready for draw and position updates
            tmrWatchdog.Enabled = false;


            //0.01 second here



            //did we get a new fix position?
            if (ScanForNMEA())//5 times second?
            {
                if (twoSecondCounter++ >= fixUpdateHz * 2)
                {
                    twoSecondCounter = 0;
                    twoSeconds = true;
                    zoomUpdateCounter = true;
                }
                if (oneSecondCounter++ >= fixUpdateHz)
                {
                    oneSecondCounter = 0;
                    oneSecond = true;
                }
                if (oneHalfSecondCounter++ >= fixUpdateHz / 2)
                {
                    oneHalfSecondCounter = 0;
                    oneHalfSecond = true;
                }
                if (oneFifthSecondCounter++ >= fixUpdateHz / 5)
                {
                    oneFifthSecondCounter = 0;
                    oneFifthSecond = true;
                }

                /////////////////////////////////////////////////////////   2222222222222222  ////////////////////////////////////////
                //every 2 second update status
                if (twoSeconds == true)
                {
                    //reset the counter
                    twoSeconds = false;

                    if (panelBatman.Visible)
                    {
                        if (isMetric)
                        {
                            lblAltitude.Text = Altitude;
                        }
                        else //imperial
                        {
                            lblAltitude.Text = AltitudeFeet;
                        }       
                        
                        lblSats.Text = SatsTracked;
                        lblZone.Text = pn.zone.ToString();
                    }

                    if (isMetric)
                    {
                        btnFlag.Text = fd.AreaBoundaryLessInnersHectares;
                        //lblpAreaWorked.Text = fd.WorkedHectares;
                        toolStripLblFieldFinish.Text = fd.WorkedAreaRemainPercentage + " \r\n" +
                            fd.WorkedAreaRemainHectares + " \r\n" + fd.TimeTillFinished;
                        //status strip values
                        stripEqWidth.Text = vehiclefileName + "\r\n" + (Math.Round(vehicle.toolWidth, 2)).ToString() + " m";
                    }
                    else //imperial
                    {
                        btnFlag.Text = fd.AreaBoundaryLessInnersAcres;
                        //lblpAreaWorked.Text = fd.WorkedAcres;
                        toolStripLblFieldFinish.Text = fd.WorkedAreaRemainPercentage + " \r\n" +
                            fd.WorkedAreaRemainAcres + " \r\n" + fd.TimeTillFinished;
                        stripEqWidth.Text = vehiclefileName + "\r\n" + (Math.Round(vehicle.toolWidth * glm.m2ft, 2)).ToString() + " ft";
                    }

                    //not Metric/Standard units sensitive
                    if (ABLine.isBtnABLineOn) btnABLine.Text = "# " + PassNumber;
                    else btnABLine.Text = "";

                    if (curve.isCurveBtnOn) btnCurve.Text = "# " + CurveNumber;
                    else btnCurve.Text = "";


                    //update the online indicator 37 green red 38
                    if (recvCounter > 20 && toolStripBtnGPSStength.Image.Height != 38)
                    {
                        //stripOnlineGPS.Value = 1;
                        lblEasting.Text = "-";
                        lblNorthing.Text = gStr.gsNoGPS;
                        //lblZone.Text = "-";
                        toolStripBtnGPSStength.Image = Resources.GPSSignalPoor;
                    }
                    else if (recvCounter < 20 && toolStripBtnGPSStength.Image.Height != 37)
                    {
                        //stripOnlineGPS.Value = 100;
                        toolStripBtnGPSStength.Image = Resources.GPSSignalGood;
                    }

                }//end every 3 seconds

                //every second update all status ///////////////////////////   1 1 1 1 1 1 ////////////////////////////
                if (oneSecond == true)
                {
                    //reset the counter
                    oneSecond = false;

                    //counter used for saving field in background
                    saveCounter++;//every 60 seconds

                    if (ABLine.isBtnABLineOn && !ct.isContourBtnOn)
                    {
                        btnABMenu.Text = ((int)(ABLine.moveDistance * 100)).ToString();
                    }
                    if (curve.isCurveBtnOn && !ct.isContourBtnOn)
                    {
                        btnABMenu.Text = ((int)(curve.moveDistance * 100)).ToString();
                    }


                    if (ABLine.isBtnABLineOn || curve.isCurveBtnOn) 
                    {
                        if (ct.isContourBtnOn)
                        {
                            //panelSnap.Visible = true;
                            panelTurn.Visible = false;
                        }
                        else
                        {
                            //panelSnap.Visible = true;
                            panelTurn.Visible = true;
                        }
                    }
                    else if (ct.isContourBtnOn)
                    {
                        //panelSnap.Visible = true;
                        panelTurn.Visible = false;
                    }
                    else
                    {
                        //panelSnap.Visible = false;
                        panelTurn.Visible = false;
                    }
                    

                    if (mc.steerSwitchValue == 0)
                    {
                        this.AutoSteerToolBtn.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        this.AutoSteerToolBtn.BackColor = System.Drawing.Color.Transparent;
                    }

                    if (panelBatman.Visible)
                    {
                        //both
                        lblLatitude.Text = Latitude;
                        lblLongitude.Text = Longitude;

                        pbarAutoSteerComm.Value = pbarSteer;
                        pbarRelayComm.Value = pbarRelay;
                        pbarUDPComm.Value = pbarUDP;

                        lblRoll.Text = RollInDegrees;
                        lblYawHeading.Text = GyroInDegrees;
                        lblGPSHeading.Text = GPSHeading;
                        lblHeading2.Text = lblHeading.Text;


                        //Low means steer switch on

                        //up in the menu a few pieces of info
                        if (isJobStarted)
                        {
                            lblEasting.Text = "E:" + Math.Round(pn.fix.easting, 1).ToString();
                            lblNorthing.Text = "N:" + Math.Round(pn.fix.northing, 1).ToString();
                        }
                        else
                        {
                            lblEasting.Text = "E:" + ((int)pn.actualEasting).ToString();
                            lblNorthing.Text = "N:" + ((int)pn.actualNorthing).ToString();
                        }

                        //tboxSentence.Text = recvSentenceSettings;
                        //display items
                        lblUturnByte.Text = Convert.ToString(mc.autoSteerData[mc.sdYouTurnByte], 2).PadLeft(6, '0');
                    }
                    
                    //AutoSteerAuto button enable - Ray Bear inspired code - Thx Ray!
                    if (isJobStarted && ahrs.isAutoSteerAuto && !recPath.isDrivingRecordedPath && 
                        (ABLine.isABLineSet || ct.isContourBtnOn || curve.isCurveSet))
                    {
                        if (mc.steerSwitchValue == 0)
                        {
                            if (!isAutoSteerBtnOn) btnAutoSteer.PerformClick();
                        }
                        else
                        {
                            if ( isAutoSteerBtnOn) btnAutoSteer.PerformClick();
                        }
                    }

                    //Make sure it is off when it should
                    if ((!ABLine.isABLineSet && !ct.isContourBtnOn && !curve.isCurveSet && isAutoSteerBtnOn) || (recPath.isDrivingRecordedPath && isAutoSteerBtnOn)) btnAutoSteer.PerformClick();

                    //do all the NTRIP routines
                    DoNTRIPSecondRoutine();

                    //the main formgps window
                    if (isMetric)  //metric or imperial
                    {
                        //Hectares on the master section soft control and sections
                        btnSectionOffAutoOn.Text = fd.WorkedHectares;
                        lblSpeed.Text = SpeedKPH;

                        //status strip values
                        distanceToolBtn.Text = fd.DistanceUserMeters + "\r\n" + fd.WorkedUserHectares2;

                        btnContour.Text = XTE; //cross track error

                    }
                    else  //Imperial Measurements
                    {
                        //acres on the master section soft control and sections
                        btnSectionOffAutoOn.Text = fd.WorkedAcres;
                        lblSpeed.Text = SpeedMPH;

                        //status strip values
                        distanceToolBtn.Text = fd.DistanceUserFeet + "\r\n" + fd.WorkedUserAcres2;
                        btnContour.Text = InchXTE; //cross track error
                    }

                    //statusbar flash red undefined headland
                    if (mc.isOutOfBounds && statusStrip1.BackColor == Color.Azure
                        || !mc.isOutOfBounds && statusStrip1.BackColor == Color.Tomato)
                    {
                        if (!mc.isOutOfBounds)
                        {
                            statusStrip1.BackColor = Color.Azure;
                        }
                        else
                        {
                            statusStrip1.BackColor = Color.Tomato;
                        }
                    }

                    lblHz.Text = NMEAHz + "Hz " + (int)(frameTime) + "\r\n" + FixQuality + HzTime.ToString("N1") + " Hz";
                }

                //every half of a second update all status  ////////////////    0.5  0.5   0.5    0.5    /////////////////
                if (oneHalfSecond == true)
                {
                    //reset the counter
                    oneHalfSecond = false;

                    if (isMetric)
                    {
                        if (bnd.bndArr.Count > 0)
                        {
                            if (yt.isYouTurnRight)
                            {
                                if (!yt.isYouTurnTriggered) btnLeftYouTurn.Text = DistPivotM;
                                else { btnLeftYouTurn.Text = ""; btnRightYouTurn.Text = gStr.gsCancel + "\r\n" + yt.onA; }
                            }
                            else
                            {
                                if (!yt.isYouTurnTriggered) btnRightYouTurn.Text = DistPivotM;
                                else { btnRightYouTurn.Text = ""; btnLeftYouTurn.Text = gStr.gsCancel + "\r\n" + yt.onA; }
                            }
                        }
                    }
                    else
                    {

                        if (bnd.bndArr.Count > 0)
                        {
                            if (yt.isYouTurnRight)
                            {
                                if (!yt.isYouTurnTriggered) btnLeftYouTurn.Text = DistPivotFt;
                                else { btnLeftYouTurn.Text = ""; btnRightYouTurn.Text = gStr.gsCancel + "\r\n" + yt.onA; }
                            }
                            else
                            {
                                if (!yt.isYouTurnTriggered) btnRightYouTurn.Text = DistPivotFt;
                                else { btnRightYouTurn.Text = ""; btnLeftYouTurn.Text = gStr.gsCancel + "\r\n" + yt.onA; }
                            }
                        }
                    }

                } //end every 1/2 second

                //every fifth second update  ///////////////////////////   0.2  FIFTH Fifth ////////////////////////////
                if (oneFifthSecond == true)
                {
                    //reset the counter
                    oneFifthSecond = false;

                    lblHeading.Text = Math.Round(fixHeading * 57.295779513, 1) + "\u00B0";

                    if (guidanceLineDistanceOff == 32020 | guidanceLineDistanceOff == 32000)
                    {
                        steerAnglesToolStripDropDownButton1.Text = "Off \r\n" + ActualSteerAngle;
                    }
                    else
                    {
                        steerAnglesToolStripDropDownButton1.Text = SetSteerAngle + "\r\n" + ActualSteerAngle;
                    }
                }

            } //there was a new GPS update

            //start timer again and wait for new fix
            tmrWatchdog.Enabled = true;

        }//wait till timer fires again.  
    }//end class
}//end namespace