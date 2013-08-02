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
    public partial class DesignMainForm : Form
    {
        #region 初始化设计窗口
        public DesignMainForm()
        {
            InitializeComponent();
            toolForm.Show(this.dockPanel, DockState.DockRightAutoHide);
            designNavigationForm.Show(this.dockPanel, DockState.DockLeft);
        }
        #endregion

        #region 窗体实例
        private ToolForm toolForm = new ToolForm();
        private VideoPropertyForm videoPropertyForm = new VideoPropertyForm();
        private DesignNavigationForm designNavigationForm = new DesignNavigationForm();
        #endregion

        VideoPropertiesClass[] videoPropertiesClass = new VideoPropertiesClass[20];
        PictureBox[] videoPictureBox = new PictureBox[20];

        private Control control;

        DBOperateClass DBOperate = new DBOperateClass();

        int videoCount = 0;
        int videoAddCount = 0;
        int dropCount = 0;
        int tempCount = 0;

        #region 初始化设计窗口
        private void DesignMainForm_Load(object sender, EventArgs e)
        {
           
        }
        #endregion

        #region 鼠标操控
        /*定义一个枚举类型，描述光标状态*/
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

        /*定义几个变量*/
        const int Band = 5;
        const int MinWidth = 10;
        const int MinHeight = 10;
        private EnumMousePointPosition m_MousePointPosition;
        private Point p, p1;

        /*定义自己的MyMouseDown事件*/
        private void MyMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            p.X = e.X;
            p.Y = e.Y;
            p1.X = e.X;
            p1.Y = e.Y;
        }

        /*定义自己的MyMouseLeave事件*/
        private void MyMouseLeave(object sender, System.EventArgs e)
        {
            m_MousePointPosition = EnumMousePointPosition.MouseSizeNone;
            this.Cursor = Cursors.Arrow;
        }

        /*设计一个函数，确定光标在控件不同位置的样式*/
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

        /*定义自己的MyMouseMove事件，在这个事件里，会使用上面设计的函数*/
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
                        /*在数据库里修改一条记录*/
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
                        break;
                    case EnumMousePointPosition.MouseSizeBottom:
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点 
                        /*在数据库里修改一条记录*/
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
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点 
                        /*在数据库里修改一条记录*/
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
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        //       lCtrl.Height = lCtrl.Height + e.Y - p1.Y;  
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        /*在数据库里修改一条记录*/
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
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        /*在数据库里修改一条记录*/
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
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        /*在数据库里修改一条记录*/
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
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y; p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        /*在数据库里修改一条记录*/
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
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width + (e.X - p1.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点 
                        /*在数据库里修改一条记录*/
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
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        /*在数据库里修改一条记录*/
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
                        break;
                    default:
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

        /*制作一个初始化过程，将界面panel1上的所有控件都绑定MyMouseDown、MyMouseLeave、MyMouseMove事件，记得在Form初始化或者Form_Load时先执行它*/
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

        //#region 载入窗口
        //private void DesignTempForm_Load(object sender, EventArgs e)
        //{
        //    initProperty();
        //    #region 从数据库里读取控件信息
        //    string ConnStr = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
        //    MySqlConnection Conn = DBOperateClass.Open_Conn(ConnStr);
        //    MySqlDataAdapter adapter = new MySqlDataAdapter("select * from video_design_info", Conn);
        //    DataTable dt = new DataTable();
        //    adapter.Fill(dt);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
             
        //        videoPropertiesClass[videoCount] = new VideoPropertiesClass();
        //        videoPictureBox[videoCount] = new PictureBox();
        //        videoPropertiesClass[videoCount].X = dt.Rows[i]["Control_X"].ToString();
        //        videoPropertiesClass[videoCount].Y = dt.Rows[i]["Control_Y"].ToString();
        //        videoPropertiesClass[videoCount].Width = dt.Rows[i]["Control_Width"].ToString();
        //        videoPropertiesClass[videoCount].Height = dt.Rows[i]["Control_Height"].ToString();
        //        videoPictureBox[videoCount].Name = dt.Rows[i]["Control_Name"].ToString();
        //        videoPictureBox[videoCount].Image = global::DisplayWindows.Properties.Resources.webcam;//控件图片的资源
        //        videoPictureBox[videoCount].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
        //        videoPictureBox[videoCount].Size = new System.Drawing.Size(int.Parse(dt.Rows[i]["Control_Width"].ToString()), int.Parse(dt.Rows[i]["Control_Height"].ToString())); //控件大小
        //        videoPictureBox[videoCount].Location = new Point(int.Parse(videoPropertiesClass[videoCount].X) , int.Parse(videoPropertiesClass[videoCount].Y) );  //把鼠标点减去控件宽和高的一半   
        //        videoPictureBox[videoCount].MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
        //        videoPictureBox[videoCount].MouseLeave += new System.EventHandler(MyMouseLeave);
        //        videoPictureBox[videoCount].MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
        //        videoPictureBox[videoCount].ContextMenuStrip = this.videoContextMenuStrip;   //增加鼠标右键菜单
        //        this.dockPanel.Controls.Add(videoPictureBox[videoCount]);                //在面板上增加一个控件
        //        videoCount++;   //添加完摄像头控件后videoCount++
                 
        //    }
        //    DBOperateClass.Close_Conn(Conn);
        //    #endregion
        //}
        //#endregion


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
                        string ConnStr = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        MySqlConnection Conn = DBOperateClass.Open_Conn(ConnStr);
                        MySqlDataAdapter adapter = new MySqlDataAdapter("select * from video_design_info", Conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["Control_Name"].ToString() == "摄像头" + videoAddCount)
                            {
                                videoAddCount++;
                            }
                        }
                        DBOperateClass.Close_Conn(Conn);
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
                        videoPictureBox[videoCount].ContextMenuStrip = this.videoContextMenuStrip;   //增加鼠标右键菜单
                        this.dockPanel.Controls.Add(videoPictureBox[videoCount]);                //在面板上增加一个控件
                        #endregion

                        #region 在数据库里增加一条记录
                        string conn = "server=localhost;uid=root;Password=password;database=mg;charset=utf8";
                        DBOperateClass.Run_SQL("insert into video_info(VideoDev_Name,VideoDev_Manu,VideoDev_User,VideoDev_Password,VideoDev_IP,VideoDev_Port,VideoDev_Prot,VideoDev_Stream,VideoDev_Channel,VideoDev_Remark) value ('" + videoPropertiesClass[videoCount].Name + "','" + videoPropertiesClass[videoCount].Manufacturer + "','" + videoPropertiesClass[videoCount].User + "','" + videoPropertiesClass[videoCount].Password + "','" + videoPropertiesClass[videoCount].IP + "','" + videoPropertiesClass[videoCount].Port + "','" + videoPropertiesClass[videoCount].Protocol + "','" + videoPropertiesClass[videoCount].Stream + "','" + videoPropertiesClass[videoCount].Channel + "','" + videoPropertiesClass[videoCount].Remark + "')", conn);
                        #endregion

                        #region 在本地数据库里增加一条记录
                        string localconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
                        DBOperateClass.Run_SQL("insert into video_design_info(Control_Name,Control_Type,Control_X,Control_Y,Control_Width,Control_Height,Control_Save,Control_Delete,Control_Update) value ('" + videoPictureBox[videoCount].Name + "','" + videoPropertiesClass[videoCount].Type + "','" + videoPictureBox[videoCount].Location.X.ToString() + "','" + videoPictureBox[videoCount].Location.Y.ToString() + "','" + videoPictureBox[videoCount].Size.Width.ToString() + "','" + videoPictureBox[videoCount].Size.Height.ToString() + "','" + videoPropertiesClass[videoCount].Save.ToString() + "','" + videoPropertiesClass[videoCount].Delete.ToString() + "','" + videoPropertiesClass[videoCount].Update.ToString() + "')", localconn);
                        #endregion

                        toolForm.ToolControlsName = null; //增加控件后将控件名称置空，一次增加一个控件
                        videoAddCount++; //添加完摄像头控件后videoAddCount++
                        videoCount++; //添加完摄像头控件后videoCount++
                        break;
                    #endregion

                    #region Drop
                    case "Drop":
                        PictureBox dropPictureBox = new PictureBox();
                        dropPictureBox.Image = global::DisplayWindows.Properties.Resources.drop128; //控件图片的资源
                        dropPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; //控件图片的布局方式
                        dropPictureBox.Size = new System.Drawing.Size(55, 50);      //控件大小
                        dropPictureBox.Location = new Point(e.X - 28, e.Y - 25);    //把鼠标点减去控件宽和高的一半
                        dropPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
                        dropPictureBox.MouseLeave += new System.EventHandler(MyMouseLeave);
                        dropPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
                        dropPictureBox.ContextMenuStrip = this.dropContextMenuStrip;    //增加鼠标右键菜单
                        this.dockPanel.Controls.Add(dropPictureBox);                 //在面板上增加一个控件
                        dropPictureBox.Name = "滴水" + dropCount;
                        dropCount++;
                        toolForm.ToolControlsName = null;
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
                        tempPictureBox.ContextMenuStrip = this.videoContextMenuStrip;    //增加鼠标右键菜单
                        this.dockPanel.Controls.Add(tempPictureBox);                 //在面板上增加一个控件
                        tempPictureBox.Name = "温湿度" + tempCount;
                        tempCount++;
                        toolForm.ToolControlsName = null;
                        break;
                    #endregion
                }
            }
        }
        #endregion

        #region 设置右键菜单父控件
        private void videoContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            control = ((System.Windows.Forms.ContextMenuStrip)sender).SourceControl;
        }
        #endregion

        #region 弹出右键菜单
        private void MenuToolStripMenuItem_Click(object sender, EventArgs e)
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
                    if ( videoPictureBox[i].Name == control.Name)
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

        #region 删除控件
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*在数据库里将该记录的删除标记置为1*/
            string conn = "server=localhost;uid=root;Password=password;database=mg;charset=utf8";
            DBOperateClass.Run_SQL("update video_design_info set Control_Delete = '1' where Control_Name = '" + control.Name + "'", conn);

            /*在本地数据库里将该记录的删除标记置为1*/
            string localconn = "server=localhost;uid=root;Password=password;database=local;charset=utf8";
            DBOperateClass.Run_SQL("update video_design_info set Control_Delete = '1' where Control_Name = '" + control.Name + "'", localconn);

            control.Dispose();
        }
        #endregion

        #region 设为顶层
        private void 设为顶层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.BringToFront();
        }
        #endregion

        #region 设为底层
        private void 设为底层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.SendToBack();
        }
        #endregion


        

    }
}
