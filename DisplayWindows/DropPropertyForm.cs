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
    public partial class DropPropertyForm : Form
    {
        public DropPropertyForm()
        {
            InitializeComponent();
        }

        private void button_Enter_Click(object sender, EventArgs e)
        {
            /*在本地数据库里修改一条记录*/
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
