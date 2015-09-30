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
      // Localize Form
      this.Text = localization.strings.release_notes_title;

      rtbChangelog.Rtf = Resources.Changelog;
    }
  }
}
