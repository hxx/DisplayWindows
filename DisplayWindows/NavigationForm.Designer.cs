namespace DisplayWindows
{
    partial class NavigationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_Drop = new System.Windows.Forms.PictureBox();
            this.pictureBox_Temp = new System.Windows.Forms.PictureBox();
            this.pictureBox_Video = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Drop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Temp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Video)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Drop
            // 
            this.pictureBox_Drop.Image = global::DisplayWindows.Properties.Resources.drop64;
            this.pictureBox_Drop.Location = new System.Drawing.Point(102, 12);
            this.pictureBox_Drop.Name = "pictureBox_Drop";
            this.pictureBox_Drop.Size = new System.Drawing.Size(63, 63);
            this.pictureBox_Drop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Drop.TabIndex = 1;
            this.pictureBox_Drop.TabStop = false;
            this.pictureBox_Drop.Click += new System.EventHandler(this.pictureBox_Drop_Click);
            this.pictureBox_Drop.MouseEnter += new System.EventHandler(this.pictureBox_Drop_MouseEnter);
            this.pictureBox_Drop.MouseLeave += new System.EventHandler(this.pictureBox_Drop_MouseLeave);
            // 
            // pictureBox_Temp
            // 
            this.pictureBox_Temp.Image = global::DisplayWindows.Properties.Resources.thermometer64;
            this.pictureBox_Temp.Location = new System.Drawing.Point(21, 111);
            this.pictureBox_Temp.Name = "pictureBox_Temp";
            this.pictureBox_Temp.Size = new System.Drawing.Size(63, 63);
            this.pictureBox_Temp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Temp.TabIndex = 0;
            this.pictureBox_Temp.TabStop = false;
            this.pictureBox_Temp.Click += new System.EventHandler(this.pictureBox_Temp_Click);
            this.pictureBox_Temp.MouseEnter += new System.EventHandler(this.pictureBox_Temp_MouseEnter);
            this.pictureBox_Temp.MouseLeave += new System.EventHandler(this.pictureBox_Temp_MouseLeave);
            // 
            // pictureBox_Video
            // 
            this.pictureBox_Video.Image = global::DisplayWindows.Properties.Resources.webcam;
            this.pictureBox_Video.Location = new System.Drawing.Point(21, 12);
            this.pictureBox_Video.Name = "pictureBox_Video";
            this.pictureBox_Video.Size = new System.Drawing.Size(63, 63);
            this.pictureBox_Video.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Video.TabIndex = 2;
            this.pictureBox_Video.TabStop = false;
            this.pictureBox_Video.Click += new System.EventHandler(this.pictureBox_Video_Click);
            // 
            // NavigationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(224, 411);
            this.Controls.Add(this.pictureBox_Video);
            this.Controls.Add(this.pictureBox_Drop);
            this.Controls.Add(this.pictureBox_Temp);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "NavigationForm";
            this.Text = "图形导航面板";
            this.Load += new System.EventHandler(this.NavigationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Drop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Temp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Video)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Temp;
        private System.Windows.Forms.PictureBox pictureBox_Drop;
        private System.Windows.Forms.PictureBox pictureBox_Video;
    }
}