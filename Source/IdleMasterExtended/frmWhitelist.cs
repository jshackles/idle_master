﻿using System;
using System.Linq;
using System.Windows.Forms;
using IdleMasterExtended.Properties;

namespace IdleMasterExtended
{
    public partial class frmWhitelist : Form
    {
        frmMain mainForm;

        public frmWhitelist(frmMain parentForm)
        {
            this.mainForm = parentForm;
            InitializeComponent();
        }

        public void SaveWhitelist()
        {
            Settings.Default.whitelist.Clear();
            Settings.Default.whitelist.AddRange(lstWhitelist.Items.Cast<string>().ToArray());
            Settings.Default.Save();
        }

        private void frmWhitelist_Load(object sender, EventArgs e)
        {
            // Localize form
            btnAdd.Text = localization.strings.add;
            btnSave.Text = localization.strings.save;
            this.Text = localization.strings.manage_whitelist;
            grpAdd.Text = localization.strings.add_game_whitelist;

            lstWhitelist.Items.AddRange(Settings.Default.whitelist.Cast<string>().ToArray());

            if (Settings.Default.customTheme)
            {
                applyTheme();
            }
        }

        private void applyTheme()
        {
            System.Drawing.Color colorBgd = Settings.Default.colorBgd;
            System.Drawing.Color colorTxt = Settings.Default.colorTxt;

            this.BackColor = colorBgd;
            this.ForeColor = colorTxt;

            btnAdd.FlatStyle = btnSave.FlatStyle = btnRemove.FlatStyle = FlatStyle.Flat;
            btnAdd.BackColor = btnSave.BackColor = btnRemove.BackColor = colorBgd;
            btnAdd.ForeColor = btnSave.ForeColor = btnRemove.ForeColor = colorTxt;

            lstWhitelist.BackColor = colorBgd;
            lstWhitelist.ForeColor = colorTxt;

            grpAdd.BackColor = colorBgd;
            grpAdd.ForeColor = colorTxt;

            txtAppid.BackColor = colorBgd;
            txtAppid.ForeColor = colorTxt;

            btnRemove.Image = Settings.Default.whiteIcons ? Resources.imgTrash_w : Resources.imgTrash;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            SaveWhitelist();

            if (Settings.Default.IdlingModeWhitelist)
            {
                mainForm.StopIdle();
                await mainForm.LoadBadgesAsync();

                if (lstWhitelist.Items.Count == 1)
                {
                    mainForm.StartSoloIdle(
                        mainForm.AllBadges.FirstOrDefault(b => b.AppId == int.Parse(lstWhitelist.Items[0].ToString()))
                    );
                }
                else if (lstWhitelist.Items.Count > 1)
                {
                    mainForm.StartMultipleIdle();
                }

                mainForm.DisableCardDropCheckTimer();
                mainForm.UpdateStateInfo();
            }
            else
            {
                mainForm.EnableCardDropCheckTimer();
            }

            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int result;
            
            if (int.TryParse(txtAppid.Text, out result)
                && lstWhitelist.Items.Cast<string>().All(blApp => blApp != txtAppid.Text))
            {
                lstWhitelist.Items.Add(txtAppid.Text);
            }

            txtAppid.Text = string.Empty;
            txtAppid.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lstWhitelist.Items.Remove(lstWhitelist.SelectedItem);
        }
    }
}
