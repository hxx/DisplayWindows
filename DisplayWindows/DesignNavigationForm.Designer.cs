namespace DisplayWindows
{
    partial class DesignNavigationForm
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
            this.pictureBox_DesignDrop = new System.Windows.Forms.PictureBox();
            this.pictureBox_DesignTemp = new System.Windows.Forms.PictureBox();
            this.pictureBox_Designvideo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DesignDrop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DesignTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Designvideo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_DesignDrop
            // 
            this.pictureBox_DesignDrop.Image = global::DisplayWindows.Properties.Resources.drop64;
            this.pictureBox_DesignDrop.Location = new System.Drawing.Point(102, 12);
            this.pictureBox_DesignDrop.Name = "pictureBox_DesignDrop";
            this.pictureBox_DesignDrop.Size = new System.Drawing.Size(63, 63);
            this.pictureBox_DesignDrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_DesignDrop.TabIndex = 1;
            this.pictureBox_DesignDrop.TabStop = false;
            this.pictureBox_DesignDrop.Click += new System.EventHandler(this.pictureBox_DesignDrop_Click);
            this.pictureBox_DesignDrop.MouseEnter += new System.EventHandler(this.pictureBox_DesignDrop_MouseEnter);
            this.pictureBox_DesignDrop.MouseLeave += new System.EventHandler(this.pictureBox_DesignDrop_MouseLeave);
            // 
            // pictureBox_DesignTemp
            // 
            this.pictureBox_DesignTemp.Image = global::DisplayWindows.Properties.Resources.thermometer64;
            this.pictureBox_DesignTemp.Location = new System.Drawing.Point(21, 99);
            this.pictureBox_DesignTemp.Name = "pictureBox_DesignTemp";
            this.pictureBox_DesignTemp.Size = new System.Drawing.Size(63, 63);
            this.pictureBox_DesignTemp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_DesignTemp.TabIndex = 0;
            this.pictureBox_DesignTemp.TabStop = false;
            this.pictureBox_DesignTemp.MouseEnter += new System.EventHandler(this.pictureBox_DesignTemp_MouseEnter);
            this.pictureBox_DesignTemp.MouseLeave += new System.EventHandler(this.pictureBox_DesignTemp_MouseLeave);
            // 
            // pictureBox_Designvideo
            // 
            this.pictureBox_Designvideo.Image = global::DisplayWindows.Properties.Resources.webcam;
            this.pictureBox_Designvideo.Location = new System.Drawing.Point(21, 12);
            this.pictureBox_Designvideo.Name = "pictureBox_Designvideo";
            this.pictureBox_Designvideo.Size = new System.Drawing.Size(63, 63);
            this.pictureBox_Designvideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Designvideo.TabIndex = 2;
            this.pictureBox_Designvideo.TabStop = false;
            this.pictureBox_Designvideo.Click += new System.EventHandler(this.pictureBox_Designvideo_Click);
            // 
            // DesignNavigationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 411);
            this.Controls.Add(this.pictureBox_Designvideo);
            this.Controls.Add(this.pictureBox_DesignDrop);
            this.Controls.Add(this.pictureBox_DesignTemp);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DesignNavigationForm";
            this.Text = "图形导航面板";
            this.Load += new System.EventHandler(this.DesignNavigationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DesignDrop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DesignTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Designvideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_DesignTemp;
        private System.Windows.Forms.PictureBox pictureBox_DesignDrop;
        private System.Windows.Forms.PictureBox pictureBox_Designvideo;
    }
}