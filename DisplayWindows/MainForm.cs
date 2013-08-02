using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisplayWindows
{
    public partial class MainForm : Form
    {
        #region 窗体实例
        private NavigationForm navigationForm = new NavigationForm();
        private TreeViewNavigationForm treeViewNavigationForm = new TreeViewNavigationForm();
        private DesignMainForm designMainForm = new DesignMainForm();
        private DesignVideoForm designTempForm = new DesignVideoForm();
        #endregion

        public MainForm()
        {
            InitializeComponent();

            navigationForm.Show(this.dockPanel, DockState.DockLeft);
            treeViewNavigationForm.Show(this.dockPanel,DockState.DockLeftAutoHide);

        }

        private void 画图设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (designMainForm == null || designMainForm.IsDisposed)
            {
                designMainForm = new DesignMainForm();
                designMainForm.Show(this.dockPanel);
                designMainForm.Focus();
            }
            else
            {
                designMainForm.Show(this.dockPanel);
                designMainForm.Focus();
            }            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (designMainForm == null || designMainForm.IsDisposed)
            {
                designMainForm = new DesignMainForm();
                designMainForm.Show(this.dockPanel);
                designMainForm.Focus();
            }
            else
            {
                designMainForm.Show(this.dockPanel);
                designMainForm.Focus();
            }
          
           
        }

        private void 树形导航面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewNavigationForm == null || treeViewNavigationForm.IsDisposed)
            {
                treeViewNavigationForm = new TreeViewNavigationForm();
                treeViewNavigationForm.Show(this.dockPanel, DockState.DockLeftAutoHide);
                treeViewNavigationForm.Focus();
            }
            else
            {
                treeViewNavigationForm.Show(this.dockPanel, DockState.DockLeftAutoHide);
                treeViewNavigationForm.Focus();
            }
        }

        private void 图形导航面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (navigationForm == null || navigationForm.IsDisposed)
            {
                navigationForm = new NavigationForm();
                navigationForm.Show(this.dockPanel, DockState.DockLeft);
                navigationForm.Focus();
            }
            else
            {
                navigationForm.Show(this.dockPanel, DockState.DockLeft);
                navigationForm.Focus();
            }
        }
        
    }
}
