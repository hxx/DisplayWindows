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
    public partial class TreeViewNavigationForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public TreeViewNavigationForm()
        {
            InitializeComponent();
        }

        private static DisplayTempForm displayTempForm = new DisplayTempForm();
        private static DisplayDropForm displayDropForm = new DisplayDropForm();
        private static DisplayVideoForm displayVideoForm = new DisplayVideoForm();

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode a = e.Node;
            switch (a.Text)
            {
                case "摄像头监控":
                    if (displayVideoForm == null || displayVideoForm.IsDisposed)
                    {
                        displayVideoForm = new DisplayVideoForm();
                        displayVideoForm.Show(this.DockPanel);
                    }
                    else
                    {
                        displayVideoForm.Show(this.DockPanel);
                    }
                    break;
                case "水浸监控":
                    if (displayDropForm == null || displayDropForm.IsDisposed)
                    {
                        displayDropForm = new DisplayDropForm();
                        displayDropForm.Show(this.DockPanel);
                    }
                    else
                    {
                        displayDropForm.Show(this.DockPanel);
                    }
                    break;
                case "温湿度监控":
                    if (displayTempForm == null || displayTempForm.IsDisposed)
                    {
                        displayTempForm = new DisplayTempForm();
                        displayTempForm.Show(this.DockPanel);
                    }
                    else
                    {
                        displayTempForm.Show(this.DockPanel);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
