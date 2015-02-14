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
    public partial class frmChangelog : Form
    {
        public frmChangelog()
        {
            InitializeComponent();
        }

        private void frmChangelog_Load(object sender, EventArgs e)
        {
            rtbChangelog.Rtf = Properties.Resources.Changelog;
        }
    }
}
