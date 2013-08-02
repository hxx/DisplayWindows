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
    public partial class NavigationForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public NavigationForm()
        {
            InitializeComponent();
        }

        private static DisplayTempForm displayTempForm = new DisplayTempForm();
        private static DisplayDropForm displayDropForm = new DisplayDropForm();
        private static DisplayVideoForm displayVideoForm = new DisplayVideoForm();

        private void NavigationForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox_Temp_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_Temp.Image = global::DisplayWindows.Properties.Resources.thermometer64;
        }

        private void pictureBox_Temp_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_Temp.Image = global::DisplayWindows.Properties.Resources.thermometer128;
        }

        private void pictureBox_Drop_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_Drop.Image = global::DisplayWindows.Properties.Resources.drop64;
        }

        private void pictureBox_Drop_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_Drop.Image = global::DisplayWindows.Properties.Resources.drop128;
        }

        private void pictureBox_Temp_Click(object sender, EventArgs e)
        {
            if (displayTempForm == null || displayTempForm.IsDisposed)
            {
                displayTempForm = new DisplayTempForm();
                displayTempForm.Show(this.DockPanel);
            }
            else
            {
                displayTempForm.Show(this.DockPanel);
            }
        }

        private void pictureBox_Drop_Click(object sender, EventArgs e)
        {
            if (displayDropForm == null || displayDropForm.IsDisposed)
            {
                displayDropForm = new DisplayDropForm();
                displayDropForm.Show(this.DockPanel);
            }
            else
            {
                displayDropForm.Show(this.DockPanel);
            }
        }

       

        private void pictureBox_Video_Click(object sender, EventArgs e)
        {
            if (displayVideoForm == null || displayVideoForm.IsDisposed)
            {
                displayVideoForm = new DisplayVideoForm();
                displayVideoForm.Show(this.DockPanel);
            }
            else
            {
                displayVideoForm.Show(this.DockPanel);
            }
        }

       


             
    }
}
