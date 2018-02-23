﻿using System;
using System.Drawing;
using System.Windows.Forms;
using IdleMaster.Properties;
using System.Threading;
using System.Text.RegularExpressions;

namespace IdleMaster
{
  public partial class frmSettings : Form
  {
    public frmSettings()
    {
      InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (radIdleDefault.Checked)
        {
            Settings.Default.sort = "default";
        }
        if (radIdleLeastDrops.Checked)
        {
            Settings.Default.sort = "leastcards";
        }
        if (radIdleMostDrops.Checked)
        {
            Settings.Default.sort = "mostcards";
        }
        if (radIdleMostValue.Checked)
        {
            Settings.Default.sort = "mostvalue";
        }

        if (cboLanguage.Text != "")
        {
            if (cboLanguage.Text != Settings.Default.language)
            {
                MessageBox.Show(localization.strings.please_restart);
            }
            Settings.Default.language = cboLanguage.Text;
        }

        if (radOneThenMany.Checked)
        {
            Settings.Default.OnlyOneGameIdle = false;
            Settings.Default.OneThenMany = true;
            Settings.Default.fastMode = false; // Disable fast mode
        }
        else
        {
            Settings.Default.OnlyOneGameIdle = radOneGameOnly.Checked && !radManyThenOne.Checked;
            Settings.Default.OneThenMany = false;

            // JN: Enable/disable fast mode
            if (radFastMode.Checked)
            {
                Settings.Default.OnlyOneGameIdle = false;
                Settings.Default.fastMode = true;
            }
            else
            {
                Settings.Default.fastMode = false;
            }
        }        
        Settings.Default.minToTray = chkMinToTray.Checked;
        Settings.Default.ignoreclient = chkIgnoreClientStatus.Checked;
        Settings.Default.showUsername = chkShowUsername.Checked;
        Settings.Default.Save();
        Close();
    }

    private void frmSettings_Load(object sender, EventArgs e)
    {
        if (Settings.Default.language != "")
        {
            cboLanguage.SelectedItem = Settings.Default.language;            
        }
        else
        {
            switch (Thread.CurrentThread.CurrentUICulture.EnglishName)
            {
                case "Chinese (Simplified, China)":
                case "Chinese (Traditional, China)":
                case "Portuguese (Brazil)":
                    cboLanguage.SelectedItem = Thread.CurrentThread.CurrentUICulture.EnglishName;
                    break;
                default:
                    cboLanguage.SelectedItem = Regex.Replace(Thread.CurrentThread.CurrentUICulture.EnglishName, @"\(.+\)", "").Trim();
                    break;
            }
        }
        
        switch (Settings.Default.sort)
        {
            case "leastcards":
                radIdleLeastDrops.Checked = true;
                break;
            case "mostcards":
                radIdleMostDrops.Checked = true;
                break;
            case "mostvalue":
                radIdleMostValue.Checked = true;
                break;
            default:
                break;
        }

        // Load translation
        this.Text = localization.strings.idle_master_settings;
        grpGeneral.Text = localization.strings.general;
        grpIdlingQuantity.Text = localization.strings.idling_behavior;
        grpPriority.Text = localization.strings.idling_order;
        btnOK.Text = localization.strings.accept;
        btnCancel.Text = localization.strings.cancel;
        ttHints.SetToolTip(btnAdvanced, localization.strings.advanced_auth);
        chkMinToTray.Text = localization.strings.minimize_to_tray;
        ttHints.SetToolTip(chkMinToTray, localization.strings.minimize_to_tray);
        chkIgnoreClientStatus.Text = localization.strings.ignore_client_status;
        ttHints.SetToolTip(chkIgnoreClientStatus, localization.strings.ignore_client_status);
        chkShowUsername.Text = localization.strings.show_username;
        ttHints.SetToolTip(chkShowUsername, localization.strings.show_username);
        radOneGameOnly.Text = localization.strings.idle_individual;
        ttHints.SetToolTip(radOneGameOnly, localization.strings.idle_individual);
        radManyThenOne.Text = localization.strings.idle_simultaneous;
        ttHints.SetToolTip(radManyThenOne, localization.strings.idle_simultaneous);
        radOneThenMany.Text = localization.strings.idle_onethenmany;
        ttHints.SetToolTip(radOneThenMany, localization.strings.idle_onethenmany);
        radIdleDefault.Text = localization.strings.order_default;
        ttHints.SetToolTip(radIdleDefault, localization.strings.order_default);
        radIdleMostValue.Text = localization.strings.order_value;
        ttHints.SetToolTip(radIdleMostValue, localization.strings.order_value);
        radIdleMostDrops.Text = localization.strings.order_most;
        ttHints.SetToolTip(radIdleMostDrops, localization.strings.order_most);
        radIdleLeastDrops.Text = localization.strings.order_least;
        ttHints.SetToolTip(radIdleLeastDrops, localization.strings.order_least);
        lblLanguage.Text = localization.strings.interface_language;
        
        if (Settings.Default.fastMode)
        {
            radFastMode.Checked = true;
        }
        else if (Settings.Default.OneThenMany)
        {
            radOneThenMany.Checked = true;
        }
        else
        {
            radOneGameOnly.Checked = Settings.Default.OnlyOneGameIdle;
            radManyThenOne.Checked = !Settings.Default.OnlyOneGameIdle;
        }        

        if (Settings.Default.minToTray)
        {
            chkMinToTray.Checked = true;
        }

        if (Settings.Default.ignoreclient)
        {
            chkIgnoreClientStatus.Checked = true;
        }

        if (Settings.Default.showUsername)
        {
            chkShowUsername.Checked = true;
        }

        runtimeCustomThemeSettings(); // JN: Apply theme colors and icons
    }

    // JN: Change the colors of the form components to match the dark theme
    private void runtimeCustomThemeSettings()
    {
        // Read settings
        var customTheme = Settings.Default.customTheme;
        var whiteIcons = Settings.Default.whiteIcons;

        // Set checkboxes (Not necessary, as the checkboxes are bound to the global setting)
        //darkThemeCheckBox.Checked = customTheme;
        //whiteIconsCheckBox.Checked = whiteIcons;
        
        if (customTheme)
        {
            // Custom theme colors (could be user selected, probably)
            Settings.Default.colorBgd = Color.FromArgb(38, 38, 38);
            Settings.Default.colorTxt = Color.FromArgb(196, 196, 196);
        }

        // Define colors
        Color colorBgd = customTheme ? Settings.Default.colorBgd : Settings.Default.colorBgdOriginal;
        Color colorTxt = customTheme ? Settings.Default.colorTxt : Settings.Default.colorTxtOriginal;
        
        // Define button style
        FlatStyle buttonStyle = customTheme ? FlatStyle.Flat : FlatStyle.Standard;

        // --------------------------
        // -- APPLY THEME SETTINGS --
        // --------------------------

        // Form colors
        this.BackColor = colorBgd;
        this.ForeColor = colorTxt;

        // Group title colors
        grpGeneral.ForeColor = grpIdlingQuantity.ForeColor = grpPriority.ForeColor = colorTxt;

        // Dropdown
        cboLanguage.BackColor = colorBgd;
        cboLanguage.ForeColor = colorTxt;

        // Buttons
        btnOK.FlatStyle = btnCancel.FlatStyle = btnAdvanced.FlatStyle = buttonStyle;
        btnOK.BackColor = btnCancel.BackColor = btnAdvanced.BackColor = colorBgd;
        btnOK.ForeColor = btnCancel.ForeColor = btnAdvanced.ForeColor = colorTxt;

        // Update the icon(s)
        runtimeWhiteIconsSettings();

        // Apply to the main frame window
        //this.Parent.Refresh();
        // Save the settings
        Settings.Default.Save();
    }

    private void runtimeWhiteIconsSettings()
    {
        // Icon images
        btnAdvanced.Image = Settings.Default.whiteIcons ? Resources.imgLock_w : Resources.imgLock;
    }

    private void btnAdvanced_Click(object sender, EventArgs e)
    {
      var frm = new frmSettingsAdvanced();
      frm.ShowDialog();
    }

    private void darkThemeCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.customTheme = darkThemeCheckBox.Checked; // Save the dark theme setting
        runtimeCustomThemeSettings(); // JN: Apply the dark theme
    }

    private void whiteIconsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.whiteIcons = whiteIconsCheckBox.Checked; // Save the white icons setting
        runtimeWhiteIconsSettings(); // JN: Apply white icons
    }
  }
}
