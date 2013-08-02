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
    public partial class DisplayTempForm : DisplayBaseForm
    {
        VideoPropertiesClass[] videoPropertiesClass = new VideoPropertiesClass[20];
        PictureBox[] videoPictureBox = new PictureBox[20];

        public DisplayTempForm()
        {
            InitializeComponent();
        }

        private void DisplayTempForm_Load(object sender, EventArgs e)
        {
            #region 从数据库里读取控件信息
            string ConnStr = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
            MySqlConnection Conn = DBOperateClass.Open_Conn(ConnStr);
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from video_design_info", Conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                videoPropertiesClass[i] = new VideoPropertiesClass();
                videoPictureBox[i] = new PictureBox();
                videoPropertiesClass[i].X = dt.Rows[i]["Control_X"].ToString();
                videoPropertiesClass[i].Y = dt.Rows[i]["Control_Y"].ToString();
                videoPropertiesClass[i].Width = dt.Rows[i]["Control_Width"].ToString();
                videoPropertiesClass[i].Height = dt.Rows[i]["Control_Height"].ToString();
                videoPictureBox[i].Name = dt.Rows[i]["Control_Name"].ToString();
                videoPictureBox[i].Image = global::DisplayWindows.Properties.Resources.webcam;//控件图片的资源
                videoPictureBox[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                videoPictureBox[i].Size = new System.Drawing.Size(int.Parse(dt.Rows[i]["Control_Width"].ToString()), int.Parse(dt.Rows[i]["Control_Height"].ToString())); //控件大小
                videoPictureBox[i].Location = new Point(int.Parse(videoPropertiesClass[i].X) - 190, int.Parse(videoPropertiesClass[i].Y));  //把鼠标点减去控件宽和高的一半   
                this.Controls.Add(videoPictureBox[i]);                //在面板上增加一个控件

            }
            DBOperateClass.Close_Conn(Conn);
            #endregion
        }
    }
}
