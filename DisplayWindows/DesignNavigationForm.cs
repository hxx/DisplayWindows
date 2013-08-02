using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DisplayWindows
{
    public partial class DesignNavigationForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public DesignNavigationForm()
        {
            InitializeComponent();
        }

        private static DesignVideoForm designVideoForm = new DesignVideoForm();
        private static DesignDropForm designDropForm = new DesignDropForm();

        private void pictureBox_DesignTemp_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_DesignTemp.Image = global::DisplayWindows.Properties.Resources.thermometer64;
        }

        private void pictureBox_DesignTemp_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_DesignTemp.Image = global::DisplayWindows.Properties.Resources.thermometer128;
        }

        private void pictureBox_DesignDrop_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_DesignDrop.Image = global::DisplayWindows.Properties.Resources.drop64;
        }

        private void pictureBox_DesignDrop_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_DesignDrop.Image = global::DisplayWindows.Properties.Resources.drop128;
        }

        
        private void DesignNavigationForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox_Designvideo_Click(object sender, EventArgs e)
        {
            if (designVideoForm == null || designVideoForm.IsDisposed)
            {
                designVideoForm = new DesignVideoForm();
                designVideoForm.Show();
            }
            else
            {
                designVideoForm.TopMost = true;
                designVideoForm.Show();
            }
        }

        private void pictureBox_DesignDrop_Click(object sender, EventArgs e)
        {
            if (designDropForm == null || designDropForm.IsDisposed)
            {
                designDropForm = new DesignDropForm();
                designDropForm.Show();
            }
            else
            {
                designDropForm.TopMost = true;
                designDropForm.Show();
            }
        }

      
    }
}
