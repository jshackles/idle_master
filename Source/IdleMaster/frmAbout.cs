using System;
using System.Deployment.Application;
using System.Reflection;
using System.Windows.Forms;

namespace IdleMaster
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icologo;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                lblVersion.Text = "Idle Master v" + version;
            }
            else
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                lblVersion.Text = "Idle Master v" + version;
            }
        }
    }
}
