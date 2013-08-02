namespace DisplayWindows
{
    partial class ToolForm
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
            this.Temp = new System.Windows.Forms.PictureBox();
            this.Drop = new System.Windows.Forms.PictureBox();
            this.Video = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Temp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Drop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Video)).BeginInit();
            this.SuspendLayout();
            // 
            // Temp
            // 
            this.Temp.Image = global::DisplayWindows.Properties.Resources.thermometer128;
            this.Temp.Location = new System.Drawing.Point(40, 124);
            this.Temp.Name = "Temp";
            this.Temp.Size = new System.Drawing.Size(55, 50);
            this.Temp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Temp.TabIndex = 2;
            this.Temp.TabStop = false;
            this.Temp.Click += new System.EventHandler(this.Temp_Click);
            // 
            // Drop
            // 
            this.Drop.Image = global::DisplayWindows.Properties.Resources.drop128;
            this.Drop.Location = new System.Drawing.Point(156, 30);
            this.Drop.Name = "Drop";
            this.Drop.Size = new System.Drawing.Size(55, 50);
            this.Drop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Drop.TabIndex = 1;
            this.Drop.TabStop = false;
            this.Drop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drop_MouseDown);
            // 
            // Video
            // 
            this.Video.Image = global::DisplayWindows.Properties.Resources.webcam;
            this.Video.Location = new System.Drawing.Point(40, 30);
            this.Video.Name = "Video";
            this.Video.Size = new System.Drawing.Size(55, 50);
            this.Video.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Video.TabIndex = 0;
            this.Video.TabStop = false;
            this.Video.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Video_MouseDown);
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 394);
            this.Controls.Add(this.Temp);
            this.Controls.Add(this.Drop);
            this.Controls.Add(this.Video);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ToolForm";
            this.Text = "工具箱";
            this.Load += new System.EventHandler(this.ToolForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.Temp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Drop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Video)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Video;
        private System.Windows.Forms.PictureBox Drop;
        private System.Windows.Forms.PictureBox Temp;
    }
}