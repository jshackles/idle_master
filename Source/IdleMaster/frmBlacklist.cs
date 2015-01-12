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
    public partial class frmBlacklist : Form
    {
        public frmBlacklist()
        {
            InitializeComponent();
        }

        public void LoadBlacklist()
        {
            foreach (String appid in Properties.Settings.Default.blacklist)
            {
                lstBlacklist.Items.Add(appid);
            }
        }

        public void SaveBlacklist()
        {
            System.Collections.Specialized.StringCollection blacklist = new System.Collections.Specialized.StringCollection();
            foreach (String appid in lstBlacklist.Items)
            {
                blacklist.Add(appid);
            }
            Properties.Settings.Default.blacklist = blacklist;
            Properties.Settings.Default.Save();
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
                foreach (String blApp in lstBlacklist.Items)
                {
                    if (blApp == txtAppid.Text)
                    {
                        onBlacklist = true;
                    }
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
