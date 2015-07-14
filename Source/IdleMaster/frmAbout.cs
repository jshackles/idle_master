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
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                string version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                lblVersion.Text = "Idle Master v" + version;
            }
            else
            {
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                lblVersion.Text = "Idle Master v" + version;
            }
        }
    }
}
