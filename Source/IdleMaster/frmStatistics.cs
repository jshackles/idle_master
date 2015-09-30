﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleMaster
{
    public partial class frmStatistics : Form
    {
        private Statistics statistics;
        public frmStatistics(Statistics statistics)
        {
            InitializeComponent();
            this.statistics = statistics;
        }

        private void frmStatistics_Load(object sender, EventArgs e)
        {
            // Localize Form
            this.Text = localization.strings.statistics.Replace("&", "");
            btnOK.Text = localization.strings.accept;
            lblSessionHeader.Text = localization.strings.this_session + ":";
            lblTotalHeader.Text = localization.strings.total + ":";
            
            TimeSpan sessionMinutesIdled = TimeSpan.FromMinutes(statistics.getSessionMinutesIdled());
            TimeSpan totalMinutesIdled = TimeSpan.FromMinutes(Properties.Settings.Default.totalMinutesIdled);

            int sessionHoursIdled = (sessionMinutesIdled.Days * 24) + sessionMinutesIdled.Hours;
            int totalHoursIdled = (totalMinutesIdled.Days * 24) + totalMinutesIdled.Hours;

            //Session
            if (sessionHoursIdled > 0)
            {
                lblSessionTime.Text = String.Format("{0} hour{1}, {2} minute{3} idled",
                        sessionHoursIdled,
                        sessionHoursIdled == 1 ? "" : "s",
                        sessionMinutesIdled.Minutes,
                        sessionMinutesIdled.Minutes == 1 ? "" : "s");
            }
            else
            {
                lblSessionTime.Text = String.Format("{0} minute{1} idled",
                        sessionMinutesIdled.Minutes,
                        sessionMinutesIdled.Minutes == 1 ? "" : "s");
            }
            lblSessionCards.Text = statistics.getSessionCardIdled().ToString() + " cards idled";

            //Total
            if (totalHoursIdled > 0)
            {
                lblTotalTime.Text = String.Format("{0} hour{1}, {2} minute{3} idled",
                    totalHoursIdled,
                    totalHoursIdled == 1 ? "" : "s",
                    totalMinutesIdled.Minutes,
                    totalMinutesIdled.Minutes == 1 ? "" : "s");
            }
            else
            {
                lblTotalTime.Text = String.Format("{0} minute{1} idled",
                    totalMinutesIdled.Minutes,
                    totalMinutesIdled.Minutes == 1 ? "" : "s");
            }
            lblTotalCards.Text = Properties.Settings.Default.totalCardIdled.ToString() + " cards idled";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
