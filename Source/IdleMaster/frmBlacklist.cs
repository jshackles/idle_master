using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using IdleMaster.Properties;

namespace IdleMaster
{
    public partial class frmBlacklist : Form
    {
        public frmBlacklist()
        {
            InitializeComponent();
        }

        private void LoadBlacklist()
        {
            foreach (String appid in Settings.Default.blacklist)
            {
                lstBlacklist.Items.Add(appid);
            }
        }

        private void SaveBlacklist()
        {
            StringCollection blacklist = new StringCollection();
            foreach (String appid in lstBlacklist.Items)
            {
                blacklist.Add(appid);
            }
            Settings.Default.blacklist = blacklist;
            Settings.Default.Save();
        }

        private void frmBlacklist_Load(object sender, EventArgs e)
        {
            // Loads the blacklist from settings
            LoadBlacklist();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBlacklist();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int result;
            if (Int32.TryParse(txtAppid.Text, out result) == true)
            {
                Boolean onBlacklist = false;
                foreach (string blApp in lstBlacklist.Items.Cast<string>().Where(blApp => blApp == txtAppid.Text))
                {
                    onBlacklist = true;
                }
                if (onBlacklist == false)
                {
                    lstBlacklist.Items.Add(txtAppid.Text);
                }
            }
            txtAppid.Text = "";
            txtAppid.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lstBlacklist.Items.Remove(lstBlacklist.SelectedItem);
        }
    }
}
