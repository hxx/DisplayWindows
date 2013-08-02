using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DisplayWindows
{
    public partial class VideoPropertyForm : Form
    {
        DBOperateClass DBOperate = new DBOperateClass();

        public VideoPropertyForm()
        {
            InitializeComponent();
        }

        private void button_Enter_Click(object sender, EventArgs e)
        {

            //"update 資料表 set 欄位1='{0}', 欄位2='{1}'", 值1,值2);
            ///*在数据库里修改一条记录*/
            //string conn = "server=localhost;uid=root;Password=password;database=mg;charset=utf8";
            //DBOperateClass.Run_SQL("update video_info set VideoDev_Manu = '" + comboBox_Manufacturer.Text + "',VideoDev_User = '" + textBox_User.Text + "',VideoDev_Password = '" + textBox_Password.Text + "',VideoDev_IP= '" + textBox_IP.Text + "',VideoDev_Port = '" + textBox_Port.Text + "',VideoDev_Stream = '" + comboBox_Stream.Text + "',VideoDev_Prot = '" + comboBox_Protocol.Text + "',VideoDev_Channel= '" + comboBox_Channel.Text + "',VideoDev_Remark = '" + textBox_Remark.Text + "' where VideoDev_Name = '" + VideoPropertiesClass.Name + "'", conn);
            
        
            /*在本地数据库里修改一条记录*/
            this.Close(); 
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        
    }
}
