using System;
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
            TimeSpan sessionMinutesIdled = TimeSpan.FromMinutes(statistics.getSessionMinutesIdled());
            TimeSpan totalMinutesIdled = TimeSpan.FromMinutes(Properties.Settings.Default.totalMinutesIdled);
            //Session
            lblSessionTime.Text = sessionMinutesIdled.ToString("%m") + " minutes (" + sessionMinutesIdled.ToString("%h") +" hours) idled";
            lblSessionCards.Text = statistics.getSessionCardIdled().ToString() + " cards idled";

            //Total
            lblTotalTime.Text = totalMinutesIdled.ToString("%m") + " minutes (" + totalMinutesIdled.ToString("%h") + " hours) idled";
            lblTotalCards.Text = Properties.Settings.Default.totalCardIdled.ToString() + " cards idled";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
