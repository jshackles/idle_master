using System;
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

        public void SaveBlacklist()
        {
            Settings.Default.blacklist.Clear();
            Settings.Default.blacklist.AddRange(lstBlacklist.Items.Cast<string>().ToArray());
            Settings.Default.Save();
        }

        private void frmBlacklist_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icologo;
            lstBlacklist.Items.AddRange(Settings.Default.blacklist.Cast<string>().ToArray());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBlacklist();
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int result;
            if (int.TryParse(txtAppid.Text, out result))
            {
                if (lstBlacklist.Items.Cast<string>().All(blApp => blApp != txtAppid.Text))
                    lstBlacklist.Items.Add(txtAppid.Text);
            }
            txtAppid.Text = string.Empty;
            txtAppid.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lstBlacklist.Items.Remove(lstBlacklist.SelectedItem);
        }
    }
}
