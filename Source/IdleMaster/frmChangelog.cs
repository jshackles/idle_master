﻿using System;
using System.Windows.Forms;
using IdleMaster.Properties;

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
      rtbChangelog.Rtf = Resources.Changelog;
    }
  }
}
