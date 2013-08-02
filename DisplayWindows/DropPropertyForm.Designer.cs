namespace DisplayWindows
{
    partial class DropPropertyForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_Enter = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.textBox_Width = new System.Windows.Forms.TextBox();
            this.textBox_Height = new System.Windows.Forms.TextBox();
            this.textBox_Remark = new System.Windows.Forms.TextBox();
            this.comboBox_Manufacturer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "对象名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "坐标X：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "坐标Y：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "控件宽：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "控件高：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "设备厂家：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "备注：";
            // 
            // button_Enter
            // 
            this.button_Enter.Location = new System.Drawing.Point(84, 287);
            this.button_Enter.Name = "button_Enter";
            this.button_Enter.Size = new System.Drawing.Size(111, 32);
            this.button_Enter.TabIndex = 7;
            this.button_Enter.Text = "确定";
            this.button_Enter.UseVisualStyleBackColor = true;
            this.button_Enter.Click += new System.EventHandler(this.button_Enter_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(243, 287);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(111, 32);
            this.button_Cancel.TabIndex = 8;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(84, 32);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.ReadOnly = true;
            this.textBox_Name.Size = new System.Drawing.Size(139, 21);
            this.textBox_Name.TabIndex = 9;
            // 
            // textBox_X
            // 
            this.textBox_X.Location = new System.Drawing.Point(84, 81);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.ReadOnly = true;
            this.textBox_X.Size = new System.Drawing.Size(139, 21);
            this.textBox_X.TabIndex = 10;
            // 
            // textBox_Y
            // 
            this.textBox_Y.Location = new System.Drawing.Point(282, 81);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.ReadOnly = true;
            this.textBox_Y.Size = new System.Drawing.Size(139, 21);
            this.textBox_Y.TabIndex = 11;
            // 
            // textBox_Width
            // 
            this.textBox_Width.Location = new System.Drawing.Point(84, 130);
            this.textBox_Width.Name = "textBox_Width";
            this.textBox_Width.ReadOnly = true;
            this.textBox_Width.Size = new System.Drawing.Size(139, 21);
            this.textBox_Width.TabIndex = 12;
            // 
            // textBox_Height
            // 
            this.textBox_Height.Location = new System.Drawing.Point(282, 130);
            this.textBox_Height.Name = "textBox_Height";
            this.textBox_Height.ReadOnly = true;
            this.textBox_Height.Size = new System.Drawing.Size(139, 21);
            this.textBox_Height.TabIndex = 13;
            // 
            // textBox_Remark
            // 
            this.textBox_Remark.Location = new System.Drawing.Point(84, 228);
            this.textBox_Remark.Name = "textBox_Remark";
            this.textBox_Remark.Size = new System.Drawing.Size(337, 21);
            this.textBox_Remark.TabIndex = 15;
            // 
            // comboBox_Manufacturer
            // 
            this.comboBox_Manufacturer.FormattingEnabled = true;
            this.comboBox_Manufacturer.Location = new System.Drawing.Point(84, 179);
            this.comboBox_Manufacturer.Name = "comboBox_Manufacturer";
            this.comboBox_Manufacturer.Size = new System.Drawing.Size(337, 20);
            this.comboBox_Manufacturer.TabIndex = 16;
            // 
            // DropPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.comboBox_Manufacturer);
            this.Controls.Add(this.textBox_Remark);
            this.Controls.Add(this.textBox_Height);
            this.Controls.Add(this.textBox_Width);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.textBox_X);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Enter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DropPropertyForm";
            this.Text = "水浸属性设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Button button_Enter;
        public System.Windows.Forms.Button button_Cancel;
        public System.Windows.Forms.TextBox textBox_Name;
        public System.Windows.Forms.TextBox textBox_X;
        public System.Windows.Forms.TextBox textBox_Y;
        public System.Windows.Forms.TextBox textBox_Width;
        public System.Windows.Forms.TextBox textBox_Height;
        public System.Windows.Forms.TextBox textBox_Remark;
        public System.Windows.Forms.ComboBox comboBox_Manufacturer;
    }
}