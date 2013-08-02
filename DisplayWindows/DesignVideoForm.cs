using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using MySql.Data.MySqlClient;

namespace DisplayWindows
{


    public partial class DesignVideoForm : Form
    {
        #region 窗体实例
        private ToolForm toolForm = new ToolForm();
        private VideoPropertyForm videoPropertyForm = new VideoPropertyForm();
        private DropPropertyForm dropPropertyForm = new DropPropertyForm();
        private DesignNavigationForm designNavigationForm = new DesignNavigationForm();
        #endregion


        VideoPropertiesClass[] videoPropertiesClass = new VideoPropertiesClass[20];
        DropPropertiesClass[] dropPropertiesClass = new DropPropertiesClass[20];
        PictureBox[] videoPictureBox = new PictureBox[20];
        PictureBox[] dropPictureBox = new PictureBox[20];

        private Control control;

        DBOperateClass DBOperate = new DBOperateClass();

        bool need_Save = false;

        int controlCount = 0;
        int videoCount = 0;
        int videoAddCount = 0;
        int dropCount = 0;
        int dropAddCount = 0;
        int tempCount = 0;
        int tempAddCount = 0;

        #region 初始化设计窗口
        public DesignVideoForm()
        {
            InitializeComponent();
            toolForm.Show(this.dockPanel, DockState.DockRightAutoHide);
            designNavigationForm.Show(this.dockPanel, DockState.DockLeft);
        }
        #endregion

        #region 鼠标操控
        
        #region 定义一个枚举类型，描述光标状态
        private enum EnumMousePointPosition
        {
            MouseSizeNone = 0, //'无  
            MouseSizeRight = 1, //'拉伸右边框  
            MouseSizeLeft = 2, //'拉伸左边框  
            MouseSizeBottom = 3, //'拉伸下边框  
            MouseSizeTop = 4, //'拉伸上边框  
            MouseSizeTopLeft = 5, //'拉伸左上角  
            MouseSizeTopRight = 6, //'拉伸右上角  
            MouseSizeBottomLeft = 7, //'拉伸左下角  
            MouseSizeBottomRight = 8, //'拉伸右下角  
            MouseDrag = 9   // '鼠标拖动  
        }
        #endregion
        
        #region 定义几个变量
        const int Band = 5;
        const int MinWidth = 10;
        const int MinHeight = 10;
        private EnumMousePointPosition m_MousePointPosition;
        private Point p, p1;
        #endregion
        
        #region 定义自己的MyMouseDown事件
        private void MyMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            p.X = e.X;
            p.Y = e.Y;
            p1.X = e.X;
            p1.Y = e.Y;
        }
        #endregion

        #region 定义自己的MyMouseLeave事件
        private void MyMouseLeave(object sender, System.EventArgs e)
        {
            m_MousePointPosition = EnumMousePointPosition.MouseSizeNone;
            this.Cursor = Cursors.Arrow;
        }
        #endregion

        #region 设计一个函数，确定光标在控件不同位置的样式
        private EnumMousePointPosition MousePointPosition(Size size, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.X >= -1 * Band) | (e.X <= size.Width) | (e.Y >= -1 * Band) | (e.Y <= size.Height))
            {
                if (e.X < Band)
                {
                    if (e.Y < Band) { return EnumMousePointPosition.MouseSizeTopLeft; }
                    else
                    {
                        if (e.Y > -1 * Band + size.Height)
                        {
                            return EnumMousePointPosition.MouseSizeBottomLeft;
                        }
                        else
                        {
                            return EnumMousePointPosition.MouseSizeLeft;
                        }
                    }
                }
                else
                {
                    if (e.X > -1 * Band + size.Width)
                    {
                        if (e.Y < Band)
                        {
                            return EnumMousePointPosition.MouseSizeTopRight;
                        }
                        else
                        {
                            if (e.Y > -1 * Band + size.Height)
                            {
                                return EnumMousePointPosition.MouseSizeBottomRight;
                            }
                            else
                            {
                                return EnumMousePointPosition.MouseSizeRight;
                            }
                        }
                    }
                    else
                    {
                        if (e.Y < Band)
                        {
                            return EnumMousePointPosition.MouseSizeTop;
                        }
                        else
                        {
                            if (e.Y > -1 * Band + size.Height)
                            {
                                return EnumMousePointPosition.MouseSizeBottom;
                            }
                            else
                            {
                                return EnumMousePointPosition.MouseDrag;
                            }
                        }
                    }
                }
            }
            else
            {
                return EnumMousePointPosition.MouseSizeNone;
            }
        }
        #endregion
        
        #region 定义自己的MyMouseMove事件，在这个事件里，会使用上面设计的函数
        private void MyMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Control lCtrl = (sender as Control);
            if (e.Button == MouseButtons.Left)
            {
                switch (m_MousePointPosition)
                {
                    case EnumMousePointPosition.MouseDrag:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + e.Y - p.Y;
                        //在数据库里修改一条记录
                        string mdconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", mdconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        } 
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    case EnumMousePointPosition.MouseSizeBottom:
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点 
                        //在数据库里修改一条记录
                        string msbconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", msbconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点 
                        //在数据库里修改一条记录
                        string msbrconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", msbrconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        //       lCtrl.Height = lCtrl.Height + e.Y - p1.Y;  
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        //在数据库里修改一条记录
                        string msrconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", msrconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        //在数据库里修改一条记录
                        string mstconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", mstconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        //在数据库里修改一条记录
                        string mslconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", mslconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y; p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        //在数据库里修改一条记录
                        string msblconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", msblconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width + (e.X - p1.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点 
                        //在数据库里修改一条记录
                        string mstrconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", mstrconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        //在数据库里修改一条记录
                        string mstlconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '1' where Control_Name = '" + lCtrl.Name + "'", mstlconn);
                        for (int i = 0; i < videoCount; i++)
                        {
                            if (videoPictureBox[i].Name == lCtrl.Name)
                            {
                                videoPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                videoPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                videoPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                videoPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            if (dropPictureBox[i].Name == lCtrl.Name)
                            {
                                dropPropertiesClass[i].X = lCtrl.Location.X.ToString();
                                dropPropertiesClass[i].Y = lCtrl.Location.Y.ToString();
                                dropPropertiesClass[i].Width = lCtrl.Size.Width.ToString();
                                dropPropertiesClass[i].Height = lCtrl.Size.Height.ToString();
                            }
                        }
                        need_Save = true; //将需要保存标识置为true
                        break;
                    default:
                        need_Save = false; //将需要保存标识置为false
                        break;
                }
                if (lCtrl.Width < MinWidth) lCtrl.Width = MinWidth;
                if (lCtrl.Height < MinHeight) lCtrl.Height = MinHeight;
            }
            else
            {
                m_MousePointPosition = MousePointPosition(lCtrl.Size, e);   //'判断光标的位置状态  
                switch (m_MousePointPosition)   //'改变光标 
                {
                    case EnumMousePointPosition.MouseSizeNone:
                        this.Cursor = Cursors.Arrow;        //'箭头 
                        break;
                    case EnumMousePointPosition.MouseDrag:
                        this.Cursor = Cursors.SizeAll;      //'四方向  
                        break;
                    case EnumMousePointPosition.MouseSizeBottom:
                        this.Cursor = Cursors.SizeNS;       //'南北  
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        this.Cursor = Cursors.SizeNS;       //'南北 
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        this.Cursor = Cursors.SizeWE;       //'东西  
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        this.Cursor = Cursors.SizeWE;       //'东西  
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        this.Cursor = Cursors.SizeNESW;     //'东北到南西  
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        this.Cursor = Cursors.SizeNWSE;     //'东南到西北  
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        this.Cursor = Cursors.SizeNWSE;     //'东南到西北
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        this.Cursor = Cursors.SizeNESW;     //'东北到南西  
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 制作一个初始化过程，将界面panel1上的所有控件都绑定MyMouseDown、MyMouseLeave、MyMouseMove事件，记得在Form初始化或者Form_Load时先执行它
        private void initProperty()
        {
            for (int i = 0; i < this.dockPanel.Controls.Count; i++)
            {
                this.dockPanel.Controls[i].MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
                this.dockPanel.Controls[i].MouseLeave += new System.EventHandler(MyMouseLeave);
                this.dockPanel.Controls[i].MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
            }

        }
        #endregion
        #endregion

        #region 载入窗口
        private void DesignTempForm_Load(object sender, EventArgs e)
        {
            initProperty();
            #region 从数据库video_desgin_info表里里读取控件信息
            string videoConnStr = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
            MySqlConnection videoConn = DBOperateClass.Open_Conn(videoConnStr);
            MySqlDataAdapter videoadapter = new MySqlDataAdapter("select * from video_design_info", videoConn);
            DataTable videodt = new DataTable();
            videoadapter.Fill(videodt);
            for (int i = 0; i < videodt.Rows.Count; i++)
            {
                if (videodt.Rows[i]["Control_Type"].ToString() == "video")
                {
                    videoPropertiesClass[videoCount] = new VideoPropertiesClass();
                    videoPictureBox[videoCount] = new PictureBox();
                    videoPropertiesClass[videoCount].X = videodt.Rows[i]["Control_X"].ToString();
                    videoPropertiesClass[videoCount].Y = videodt.Rows[i]["Control_Y"].ToString();
                    videoPropertiesClass[videoCount].Width = videodt.Rows[i]["Control_Width"].ToString();
                    videoPropertiesClass[videoCount].Height = videodt.Rows[i]["Control_Height"].ToString();
                    videoPictureBox[videoCount].Name = videodt.Rows[i]["Control_Name"].ToString();
                    videoPictureBox[videoCount].Image = global::DisplayWindows.Properties.Resources.webcam;//控件图片的资源
                    videoPictureBox[videoCount].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                    videoPictureBox[videoCount].Size = new System.Drawing.Size(int.Parse(videodt.Rows[i]["Control_Width"].ToString()), int.Parse(videodt.Rows[i]["Control_Height"].ToString())); //控件大小
                    videoPictureBox[videoCount].Location = new Point(int.Parse(videoPropertiesClass[videoCount].X), int.Parse(videoPropertiesClass[videoCount].Y));  //把鼠标点减去控件宽和高的一半   
                    videoPictureBox[videoCount].MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
                    videoPictureBox[videoCount].MouseLeave += new System.EventHandler(MyMouseLeave);
                    videoPictureBox[videoCount].MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
                    videoPictureBox[videoCount].ContextMenuStrip = this.videoContextMenuStrip_video;   //增加鼠标右键菜单
                    this.dockPanel.Controls.Add(videoPictureBox[videoCount]);                //在面板上增加一个控件
                    videoCount++;   //添加完摄像头控件后videoCount++
                    controlCount++;    //添加完控件后controlCount++
                }
            }
            for (int i = 0; i < videodt.Rows.Count; i++)
            {
                if (videodt.Rows[i]["Control_Type"].ToString() == "drop")
                {
                    dropPropertiesClass[dropCount] = new DropPropertiesClass();
                    dropPictureBox[dropCount] = new PictureBox();
                    dropPropertiesClass[dropCount].X = videodt.Rows[i]["Control_X"].ToString();
                    dropPropertiesClass[dropCount].Y = videodt.Rows[i]["Control_Y"].ToString();
                    dropPropertiesClass[dropCount].Width = videodt.Rows[i]["Control_Width"].ToString();
                    dropPropertiesClass[dropCount].Height = videodt.Rows[i]["Control_Height"].ToString();
                    dropPictureBox[dropCount].Name = videodt.Rows[i]["Control_Name"].ToString();
                    dropPictureBox[dropCount].Image = global::DisplayWindows.Properties.Resources.drop128;//控件图片的资源
                    dropPictureBox[dropCount].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                    dropPictureBox[dropCount].Size = new System.Drawing.Size(int.Parse(videodt.Rows[i]["Control_Width"].ToString()), int.Parse(videodt.Rows[i]["Control_Height"].ToString())); //控件大小
                    dropPictureBox[dropCount].Location = new Point(int.Parse(dropPropertiesClass[dropCount].X), int.Parse(dropPropertiesClass[dropCount].Y));  //把鼠标点减去控件宽和高的一半   
                    dropPictureBox[dropCount].MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
                    dropPictureBox[dropCount].MouseLeave += new System.EventHandler(MyMouseLeave);
                    dropPictureBox[dropCount].MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
                    dropPictureBox[dropCount].ContextMenuStrip = this.dropContextMenuStrip_video;   //增加鼠标右键菜单
                    this.dockPanel.Controls.Add(dropPictureBox[dropCount]);                //在面板上增加一个控件
                    dropCount++;   //添加完摄像头控件后dropCount++
                    controlCount++;    //添加完控件后controlCount++
                }
            }
            DBOperateClass.Close_Conn(videoConn);
            #endregion

        }
        #endregion


        #region 鼠标点击增加控件
        private void dockPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (toolForm.ToolControlsName != null)
            {
                switch (toolForm.ToolControlsName)
                {
                    #region Video
                    case "Video":
                        #region 控件名称为数据库里已有的名称则videoCount++
                        string VideoConnStr = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        MySqlConnection VideoConn = DBOperateClass.Open_Conn(VideoConnStr);
                        MySqlDataAdapter Videoadapter = new MySqlDataAdapter("select * from video_design_info", VideoConn);
                        DataTable Videodt = new DataTable();
                        Videoadapter.Fill(Videodt);
                        for (int i = 0; i < Videodt.Rows.Count; i++)
                        {
                            if (Videodt.Rows[i]["Control_Name"].ToString() == "摄像头" + videoAddCount)
                            {
                                videoAddCount++;
                            }
                        }
                        DBOperateClass.Close_Conn(VideoConn);
                        #endregion

                        #region 添加一个摄像头控件
                        videoPropertiesClass[videoCount] = new VideoPropertiesClass();
                        videoPictureBox[videoCount] = new PictureBox();
                        videoPictureBox[videoCount].Name = "摄像头" + videoAddCount;
                        videoPictureBox[videoCount].Image = global::DisplayWindows.Properties.Resources.webcam;//控件图片的资源
                        videoPictureBox[videoCount].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                        videoPictureBox[videoCount].Size = new System.Drawing.Size(55, 50); //控件大小
                        videoPictureBox[videoCount].Location = new Point(e.X - 28, e.Y - 25);  //把鼠标点减去控件宽和高的一半   
                        videoPictureBox[videoCount].MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
                        videoPictureBox[videoCount].MouseLeave += new System.EventHandler(MyMouseLeave);
                        videoPictureBox[videoCount].MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
                        videoPictureBox[videoCount].ContextMenuStrip = this.videoContextMenuStrip_video;   //增加鼠标右键菜单
                        this.dockPanel.Controls.Add(videoPictureBox[videoCount]);                //在面板上增加一个控件
                        #endregion

                        #region 在数据库里增加一条记录
                        string Videoconn = "server=localhost;uid=root;Password=password;database=mg;charset=utf8";
                        DBOperateClass.Run_SQL("insert into video_info(VideoDev_Name,VideoDev_Manu,VideoDev_User,VideoDev_Password,VideoDev_IP,VideoDev_Port,VideoDev_Prot,VideoDev_Stream,VideoDev_Channel,VideoDev_Remark) value ('" + videoPropertiesClass[videoCount].Name + "','" + videoPropertiesClass[videoCount].Manufacturer + "','" + videoPropertiesClass[videoCount].User + "','" + videoPropertiesClass[videoCount].Password + "','" + videoPropertiesClass[videoCount].IP + "','" + videoPropertiesClass[videoCount].Port + "','" + videoPropertiesClass[videoCount].Protocol + "','" + videoPropertiesClass[videoCount].Stream + "','" + videoPropertiesClass[videoCount].Channel + "','" + videoPropertiesClass[videoCount].Remark + "')", Videoconn);
                        #endregion

                        #region 在本地数据库里增加一条记录
                        string Videolocalconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("insert into video_design_info(Control_Name,Control_Type,Control_X,Control_Y,Control_Width,Control_Height,Control_Save,Control_Delete,Control_Update) value ('" + videoPictureBox[videoCount].Name + "','" + videoPropertiesClass[videoCount].Type + "','" + videoPictureBox[videoCount].Location.X.ToString() + "','" + videoPictureBox[videoCount].Location.Y.ToString() + "','" + videoPictureBox[videoCount].Size.Width.ToString() + "','" + videoPictureBox[videoCount].Size.Height.ToString() + "','" + videoPropertiesClass[videoCount].Save.ToString() + "','" + videoPropertiesClass[videoCount].Delete.ToString() + "','" + videoPropertiesClass[videoCount].Update.ToString() + "')", Videolocalconn);
                        #endregion

                        toolForm.ToolControlsName = null; //增加控件后将控件名称置空，一次增加一个控件
                        videoAddCount++; //添加完摄像头控件后videoAddCount++
                        videoCount++; //添加完摄像头控件后videoCount++
                        controlCount++;    //添加完控件后controlCount++
                        need_Save = true; //将需要保存标识置为true
                        break;
                    #endregion

                    #region Drop
                    case "Drop":
                        #region 控件名称为数据库里已有的名称则DropCount++
                        string DropConnStr = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        MySqlConnection DropConn = DBOperateClass.Open_Conn(DropConnStr);
                        MySqlDataAdapter Dropadapter = new MySqlDataAdapter("select * from video_design_info", DropConn);
                        DataTable Dropdt = new DataTable();
                        Dropadapter.Fill(Dropdt);
                        for (int i = 0; i < Dropdt.Rows.Count; i++)
                        {
                            if (Dropdt.Rows[i]["Control_Name"].ToString() == "水浸" + dropAddCount)
                            {
                                dropAddCount++;
                            }
                        }
                        DBOperateClass.Close_Conn(DropConn);
                        #endregion

                        #region 添加一个水浸控件
                        dropPropertiesClass[dropCount] = new DropPropertiesClass();
                        dropPictureBox[dropCount] = new PictureBox();
                        dropPictureBox[dropCount].Name = "水浸" + dropAddCount;
                        dropPictureBox[dropCount].Image = global::DisplayWindows.Properties.Resources.drop128;//控件图片的资源
                        dropPictureBox[dropCount].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                        dropPictureBox[dropCount].Size = new System.Drawing.Size(55, 50); //控件大小
                        dropPictureBox[dropCount].Location = new Point(e.X - 28, e.Y - 25);  //把鼠标点减去控件宽和高的一半   
                        dropPictureBox[dropCount].MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
                        dropPictureBox[dropCount].MouseLeave += new System.EventHandler(MyMouseLeave);
                        dropPictureBox[dropCount].MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
                        dropPictureBox[dropCount].ContextMenuStrip = this.videoContextMenuStrip_video;   //增加鼠标右键菜单
                        this.dockPanel.Controls.Add(dropPictureBox[dropCount]);                //在面板上增加一个控件
                        #endregion

                        //#region 在数据库里增加一条记录
                        //string Dropconn = "server=localhost;uid=root;Password=password;database=mg;charset=utf8";
                        //DBOperateClass.Run_SQL("insert into video_info(VideoDev_Name,VideoDev_Manu,VideoDev_User,VideoDev_Password,VideoDev_IP,VideoDev_Port,VideoDev_Prot,VideoDev_Stream,VideoDev_Channel,VideoDev_Remark) value ('" + videoPropertiesClass[dropCount].Name + "','" + videoPropertiesClass[dropCount].Manufacturer + "','" + videoPropertiesClass[dropCount].User + "','" + videoPropertiesClass[dropCount].Password + "','" + videoPropertiesClass[dropCount].IP + "','" + videoPropertiesClass[dropCount].Port + "','" + videoPropertiesClass[dropCount].Protocol + "','" + videoPropertiesClass[dropCount].Stream + "','" + videoPropertiesClass[dropCount].Channel + "','" + videoPropertiesClass[dropCount].Remark + "')", Dropconn);
                        //#endregion

                        #region 在本地数据库里增加一条记录
                        string Droplocalconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("insert into video_design_info(Control_Name,Control_Type,Control_X,Control_Y,Control_Width,Control_Height,Control_Save,Control_Delete,Control_Update) value ('" + dropPictureBox[dropCount].Name + "','" + dropPropertiesClass[dropCount].Type + "','" + dropPictureBox[dropCount].Location.X.ToString() + "','" + dropPictureBox[dropCount].Location.Y.ToString() + "','" + dropPictureBox[dropCount].Size.Width.ToString() + "','" + dropPictureBox[dropCount].Size.Height.ToString() + "','" + dropPropertiesClass[dropCount].Save.ToString() + "','" + dropPropertiesClass[dropCount].Delete.ToString() + "','" + dropPropertiesClass[dropCount].Update.ToString() + "')", Droplocalconn);
                        #endregion

                        //

                        //

                        MessageBox.Show(dropPictureBox[dropCount].Name);
                        toolForm.ToolControlsName = null; //增加控件后将控件名称置空，一次增加一个控件
                        dropAddCount++; //添加完水浸控件后dropAddCount++
                        dropCount++; //添加完水浸控件后dropCount++
                        controlCount++;    //添加完控件后controlCount++
                        need_Save = true; //将需要保存标识置为true
                        break;
                    #endregion

                    #region Temp
                    case "Temp":
                        PictureBox tempPictureBox = new PictureBox();
                        tempPictureBox.Image = global::DisplayWindows.Properties.Resources.thermometer128; //控件图片的资源
                        tempPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                        tempPictureBox.Size = new System.Drawing.Size(55, 50);      //控件大小
                        tempPictureBox.Location = new Point(e.X - 28, e.Y - 25);    //把鼠标点减去控件宽和高的一半
                        tempPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
                        tempPictureBox.MouseLeave += new System.EventHandler(MyMouseLeave);
                        tempPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
                        tempPictureBox.ContextMenuStrip = this.videoContextMenuStrip_video;    //增加鼠标右键菜单
                        this.dockPanel.Controls.Add(tempPictureBox);                 //在面板上增加一个控件
                        tempPictureBox.Name = "温湿度" + tempCount;
                        tempCount++;
                        toolForm.ToolControlsName = null;
                        need_Save = true; //将需要保存标识置为true
                        break;
                    #endregion

                    #region default
                    default:
                        need_Save = false;
                        break;
                    #endregion
                }
            }
        }
        #endregion

        #region 设置右键菜单父控件
        private void videoContextMenuStrip_video_Opening(object sender, CancelEventArgs e)
        {
            control = ((System.Windows.Forms.ContextMenuStrip)sender).SourceControl;
        }

        private void dropContextMenuStrip_video_Opening(object sender, CancelEventArgs e)
        {
            control = ((System.Windows.Forms.ContextMenuStrip)sender).SourceControl;
        }
        #endregion

        #region 点击摄像头的属性
        private void video属性摄像头ToolStripMenuItem_video_Click(object sender, EventArgs e)
        {
            if (videoPropertyForm == null || videoPropertyForm.IsDisposed)
            {
                videoPropertyForm = new VideoPropertyForm();
                videoPropertyForm.textBox_Name.Text = control.Name;
                videoPropertyForm.textBox_X.Text = control.Location.X.ToString();
                videoPropertyForm.textBox_Y.Text = control.Location.Y.ToString();
                videoPropertyForm.textBox_Width.Text = control.Size.Width.ToString();
                videoPropertyForm.textBox_Height.Text = control.Size.Height.ToString();
                for (int i = 0; i < videoCount; i++)
                {
                    if (videoPictureBox[i].Name == control.Name)
                    {
                        videoPropertyForm.comboBox_Manufacturer.Text = videoPropertiesClass[i].Manufacturer;
                        videoPropertyForm.textBox_User.Text = videoPropertiesClass[i].User;
                        videoPropertyForm.textBox_Password.Text = videoPropertiesClass[i].Password;
                        videoPropertyForm.textBox_IP.Text = videoPropertiesClass[i].IP;
                        videoPropertyForm.textBox_Port.Text = videoPropertiesClass[i].Port;
                        videoPropertyForm.comboBox_Stream.Text = videoPropertiesClass[i].Stream;
                        videoPropertyForm.comboBox_Protocol.Text = videoPropertiesClass[i].Protocol;
                        videoPropertyForm.comboBox_Channel.Text = videoPropertiesClass[i].Channel;
                        videoPropertyForm.textBox_Remark.Text = videoPropertiesClass[i].Remark;
                    }
                }
                videoPropertyForm.Focus();
                videoPropertyForm.Text = control.Name + "属性设置";
                videoPropertyForm.ShowDialog(this.dockPanel);
            }
            else
            {
                videoPropertyForm.textBox_Name.Text = control.Name;
                videoPropertyForm.textBox_X.Text = control.Location.X.ToString();
                videoPropertyForm.textBox_Y.Text = control.Location.Y.ToString();
                videoPropertyForm.textBox_Width.Text = control.Size.Width.ToString();
                videoPropertyForm.textBox_Height.Text = control.Size.Height.ToString();
                for (int i = 0; i < videoCount; i++)
                {
                    if (videoPictureBox[i].Name == control.Name)
                    {
                        videoPropertyForm.comboBox_Manufacturer.Text = videoPropertiesClass[i].Manufacturer;
                        videoPropertyForm.textBox_User.Text = videoPropertiesClass[i].User;
                        videoPropertyForm.textBox_Password.Text = videoPropertiesClass[i].Password;
                        videoPropertyForm.textBox_IP.Text = videoPropertiesClass[i].IP;
                        videoPropertyForm.textBox_Port.Text = videoPropertiesClass[i].Port;
                        videoPropertyForm.comboBox_Stream.Text = videoPropertiesClass[i].Stream;
                        videoPropertyForm.comboBox_Protocol.Text = videoPropertiesClass[i].Protocol;
                        videoPropertyForm.comboBox_Channel.Text = videoPropertiesClass[i].Channel;
                        videoPropertyForm.textBox_Remark.Text = videoPropertiesClass[i].Remark;
                    }
                }
                videoPropertyForm.Focus();
                videoPropertyForm.Text = control.Name + "属性设置";
                videoPropertyForm.ShowDialog(this.dockPanel);
            }
        }
        #endregion

        #region 点击摄像头的删除
        private void video删除ToolStripMenuItem_video_Click(object sender, EventArgs e)
        {
            #region 在数据库里将该记录的删除标记置为1
            string conn = "server=localhost;uid=root;Password=password;database=mg;charset=utf8";
            DBOperateClass.Run_SQL("update video_design_info set Control_Delete = '1' where Control_Name = '" + control.Name + "'", conn);
            #endregion

            #region 在本地数据库里将该记录的删除标记置为1
            string localconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
            DBOperateClass.Run_SQL("update video_design_info set Control_Delete = '1' where Control_Name = '" + control.Name + "'", localconn);
            #endregion
            videoCount--;
            control.Dispose();
            need_Save = true; //将需要保存标识置为true
        }
        #endregion

        #region 点击摄像头的设为顶层
        private void video设为顶层ToolStripMenuItem_video_Click(object sender, EventArgs e)
        {
            control.BringToFront();
        }
        #endregion

        #region 点击摄像头的设为底层
        private void video设为底层ToolStripMenuItem_video_Click(object sender, EventArgs e)
        {
            control.SendToBack();
        }
        #endregion

        #region 点击水浸的属性
        private void drop属性滴水ToolStripMenuItem_video_Click(object sender, EventArgs e)
        {
            if (dropPropertyForm == null || dropPropertyForm.IsDisposed)
            {
                dropPropertyForm = new DropPropertyForm();
                dropPropertyForm.textBox_Name.Text = control.Name;
                dropPropertyForm.textBox_X.Text = control.Location.X.ToString();
                dropPropertyForm.textBox_Y.Text = control.Location.Y.ToString();
                dropPropertyForm.textBox_Width.Text = control.Size.Width.ToString();
                dropPropertyForm.textBox_Height.Text = control.Size.Height.ToString();
                for (int i = 0; i < dropCount; i++)
                {
                    if (dropPictureBox[i].Name == control.Name)
                    {
                        dropPropertyForm.comboBox_Manufacturer.Text = dropPropertiesClass[i].Manufacturer;
                        dropPropertyForm.textBox_Remark.Text = dropPropertiesClass[i].Remark;
                    }
                }
                dropPropertyForm.Focus();
                dropPropertyForm.Text = control.Name + "属性设置";
                dropPropertyForm.ShowDialog(this.dockPanel);
            }
            else
            {
                dropPropertyForm.textBox_Name.Text = control.Name;
                dropPropertyForm.textBox_X.Text = control.Location.X.ToString();
                dropPropertyForm.textBox_Y.Text = control.Location.Y.ToString();
                dropPropertyForm.textBox_Width.Text = control.Size.Width.ToString();
                dropPropertyForm.textBox_Height.Text = control.Size.Height.ToString();
                for (int i = 0; i < dropCount; i++)
                {
                    if (dropPictureBox[i].Name == control.Name)
                    {
                        dropPropertyForm.comboBox_Manufacturer.Text = dropPropertiesClass[i].Manufacturer;
                        dropPropertyForm.textBox_Remark.Text = dropPropertiesClass[i].Remark;
                    }
                }
                dropPropertyForm.Focus();
                dropPropertyForm.Text = control.Name + "属性设置";
                dropPropertyForm.ShowDialog(this.dockPanel);
            }
        }
        #endregion

        #region 点击水浸的删除
        private void drop删除ToolStripMenuItem_video_Click(object sender, EventArgs e)
        {
            //#region 在数据库里将该记录的删除标记置为1
            //string conn = "server=localhost;uid=root;Password=password;database=mg;charset=utf8";
            //DBOperateClass.Run_SQL("update drop_design_info set Control_Delete = '1' where Control_Name = '" + control.Name + "'", conn);
            //#endregion

            #region 在本地数据库里将该记录的删除标记置为1
            string localconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
            DBOperateClass.Run_SQL("update video_design_info set Control_Delete = '1' where Control_Name = '" + control.Name + "'", localconn);
            #endregion

            dropCount--;
            control.Dispose();
            need_Save = true; //将需要保存标识置为true
        }
        #endregion

        #region 点击水浸的设为顶层
        private void drop设为顶层ToolStripMenuItem_video_Click(object sender, EventArgs e)
        {
            control.BringToFront();
        }
        #endregion

        #region 点击水浸的设为底层
        private void drop设为底层ToolStripMenuItem_video_Click(object sender, EventArgs e)
        {
            control.SendToBack();
        }
        #endregion

        #region 关闭设计窗口时弹出是否保存的对话框
        private void DesignTempForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (need_Save == true)
            {
                DialogResult mess = MessageBox.Show("文件尚未保存,是否保存?",
                        "保存文件", MessageBoxButtons.YesNoCancel);
                switch (mess)
                {
                    #region Yes
                    case DialogResult.Yes:
                        string yesconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        #region 保存按钮，保存窗口中现有的控件
                        DBOperateClass.Run_SQL("update video_design_info set Control_Save = '1'", yesconn);
                        #endregion
                        #region 保存按钮，将删除标记为1的控件删除
                        DBOperateClass.Run_SQL("delete from video_design_info where Control_Delete = '1'", yesconn);
                        #endregion
                        #region 保存按钮，将更新标记为1的控件的信息重新保存
                        for (int i = 0; i < videoCount; i++)
                        {
                            DBOperateClass.Run_SQL("update video_design_info set Control_X = '" + videoPropertiesClass[i].X + "',Control_Y = '" + videoPropertiesClass[i].Y + "',Control_Width = '" + videoPropertiesClass[i].Width + "',Control_Height = '" + videoPropertiesClass[i].Height + "' where (Control_Update = '1' and Control_Name = '" + videoPictureBox[i].Name + "') ", yesconn);
                        }
                        for (int i = 0; i < dropCount; i++)
                        {
                            DBOperateClass.Run_SQL("update video_design_info set Control_X = '" + dropPropertiesClass[i].X + "',Control_Y = '" + dropPropertiesClass[i].Y + "',Control_Width = '" + dropPropertiesClass[i].Width + "',Control_Height = '" + dropPropertiesClass[i].Height + "' where (Control_Update = '1' and Control_Name = '" + dropPictureBox[i].Name + "') ", yesconn);
                        }

                        #region 将更新标记为1的重新置为0
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '0'", yesconn);
                        #endregion
                        #endregion
                        break;
                    #endregion

                    #region No
                    case DialogResult.No:
                        string noconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        #region 不保存按钮，将保存标记为0的控件删除
                        DBOperateClass.Run_SQL("delete from video_design_info where Control_Save = '0'",
                            noconn);
                        #endregion
                        #region 不保存按钮，将删除控件的删除标记置为0
                        DBOperateClass.Run_SQL("update video_design_info set Control_Delete = '0'", noconn);
                        #endregion
                        #region 不保存按钮，将更新标记为1的重新置为0
                        DBOperateClass.Run_SQL("update video_design_info set Control_Update = '0'", noconn);
                        #endregion
                        break;
                    #endregion

                    #region Cancel
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    #endregion
                }
            }
        }
        #endregion

      
          
        
    }
}
