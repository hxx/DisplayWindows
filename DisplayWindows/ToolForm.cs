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
    public partial class ToolForm : DockContent
    {
        public ToolForm()
        {
            InitializeComponent();
        }

        
        private void ToolForm_Load(object sender, EventArgs e)
        {

        }

        public string ToolControlsName = null;

        private void Video_MouseDown(object sender, MouseEventArgs e)
        {
            ToolControlsName = Video.Name;
        }
        
        private void Drop_MouseDown(object sender, MouseEventArgs e)
        {
            ToolControlsName = Drop.Name;
        }
        
        private void Temp_Click(object sender, EventArgs e)
        {
            ToolControlsName = Temp.Name;
        }

        private void ToolForm_MouseDown(object sender, MouseEventArgs e)
        {
            ToolControlsName = null;
        }

      


    }
}
