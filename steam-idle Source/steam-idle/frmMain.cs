﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace steam_idle
{
    public partial class frmMain : Form
    {
        public frmMain(long appid)
        {
            InitializeComponent();
            picApp.Load("http://cdn.akamai.steamstatic.com/steam/apps/" + appid.ToString() + "/header_292x136.jpg");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
