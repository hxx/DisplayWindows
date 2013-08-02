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
    public partial class DisplayVideoForm : DisplayBaseForm
    {
        VideoPropertiesClass[] videoPropertiesClass = new VideoPropertiesClass[20];
        DropPropertiesClass[] dropPropertiesClass = new DropPropertiesClass[20];
        PictureBox[] videoPictureBox = new PictureBox[20];
        PictureBox[] dropPictureBox = new PictureBox[20];

        public DisplayVideoForm()
        {
            InitializeComponent();
        }

        private void DisplayVideoForm_Load(object sender, EventArgs e)
        {
            #region 从数据库里读取控件信息
            string videoConnStr = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
            MySqlConnection videoConn = DBOperateClass.Open_Conn(videoConnStr);
            MySqlDataAdapter videoadapter = new MySqlDataAdapter("select * from video_design_info",videoConn);
            DataTable videodt = new DataTable();
            videoadapter.Fill(videodt);
            for (int i = 0; i < videodt.Rows.Count; i++)
            {
                if (videodt.Rows[i]["Control_Type"].ToString() == "video")
                {
                    videoPropertiesClass[i] = new VideoPropertiesClass();
                    videoPictureBox[i] = new PictureBox();
                    videoPropertiesClass[i].X = videodt.Rows[i]["Control_X"].ToString();
                    videoPropertiesClass[i].Y = videodt.Rows[i]["Control_Y"].ToString();
                    videoPropertiesClass[i].Width = videodt.Rows[i]["Control_Width"].ToString();
                    videoPropertiesClass[i].Height = videodt.Rows[i]["Control_Height"].ToString();
                    videoPictureBox[i].Name = videodt.Rows[i]["Control_Name"].ToString();
                    videoPictureBox[i].Image = global::DisplayWindows.Properties.Resources.webcam;//控件图片的资源
                    videoPictureBox[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                    videoPictureBox[i].Size = new System.Drawing.Size(int.Parse(videodt.Rows[i]["Control_Width"].ToString()), int.Parse(videodt.Rows[i]["Control_Height"].ToString())); //控件大小
                    videoPictureBox[i].Location = new Point(int.Parse(videoPropertiesClass[i].X)-190, int.Parse(videoPropertiesClass[i].Y));  //控件位置   
                    this.Controls.Add(videoPictureBox[i]);                //在面板上增加一个控件
                }
            }
            for (int i = 0; i < videodt.Rows.Count; i++)
            {
                if (videodt.Rows[i]["Control_Type"].ToString() == "drop")
                {
                    dropPropertiesClass[i] = new DropPropertiesClass();
                    dropPictureBox[i] = new PictureBox();
                    dropPropertiesClass[i].X = videodt.Rows[i]["Control_X"].ToString();
                    dropPropertiesClass[i].Y = videodt.Rows[i]["Control_Y"].ToString();
                    dropPropertiesClass[i].Width = videodt.Rows[i]["Control_Width"].ToString();
                    dropPropertiesClass[i].Height = videodt.Rows[i]["Control_Height"].ToString();
                    dropPictureBox[i].Name = videodt.Rows[i]["Control_Name"].ToString();
                    dropPictureBox[i].Image = global::DisplayWindows.Properties.Resources.drop128;//控件图片的资源
                    dropPictureBox[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                    dropPictureBox[i].Size = new System.Drawing.Size(int.Parse(videodt.Rows[i]["Control_Width"].ToString()), int.Parse(videodt.Rows[i]["Control_Height"].ToString())); //控件大小
                    dropPictureBox[i].Location = new Point(int.Parse(dropPropertiesClass[i].X)-190, int.Parse(dropPropertiesClass[i].Y));  //控件位置  
                    this.Controls.Add(dropPictureBox[i]);                //在面板上增加一个控件
                }
            }
            DBOperateClass.Close_Conn(videoConn);
            
            #endregion
        }
    }
}
