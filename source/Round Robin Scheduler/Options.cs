using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SomeTechie.RoundRobinScheduler
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void oKBtn_Click(object sender, EventArgs e)
        {
            global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default.Save();
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default.Reload();
            this.Close();
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default.Save();
        }

        private void accessCodeMessageLine1Txt_TextChanged(object sender, EventArgs e) { applyBtn.Enabled = true; }
        private void accessCodeMessageLine2Txt_TextChanged(object sender, EventArgs e) { applyBtn.Enabled = true; }
        private void accessCodeMessageMakeBoldChk_CheckedChanged(object sender, EventArgs e) { applyBtn.Enabled = true; }

        private void Options_Load(object sender, EventArgs e)
        {
            applyBtn.Enabled = false;
        }
    }
}
