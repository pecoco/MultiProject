namespace ACT.RadarForm
{
    partial class RadarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RadarForm));
            this.lbSwitch = new System.Windows.Forms.Label();
            this.btFlagOn = new System.Windows.Forms.Button();
            this.btZoomMainus = new System.Windows.Forms.Button();
            this.btZoomPlus = new System.Windows.Forms.Button();
            this.btAllModeSwitch = new System.Windows.Forms.Button();
            this.btResize = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btInterpersonal = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.radarWPF1 = new MultiRadar.RadarWPF();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbSwitch
            // 
            this.lbSwitch.AutoSize = true;
            this.lbSwitch.Font = new System.Drawing.Font("Yu Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbSwitch.ForeColor = System.Drawing.Color.White;
            this.lbSwitch.Location = new System.Drawing.Point(3, 0);
            this.lbSwitch.Name = "lbSwitch";
            this.lbSwitch.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbSwitch.Size = new System.Drawing.Size(26, 19);
            this.lbSwitch.TabIndex = 8;
            this.lbSwitch.Text = "■";
            this.lbSwitch.Click += new System.EventHandler(this.lbSwitch_Click);
            // 
            // btFlagOn
            // 
            this.btFlagOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btFlagOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFlagOn.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btFlagOn.ForeColor = System.Drawing.Color.White;
            this.btFlagOn.Location = new System.Drawing.Point(183, 0);
            this.btFlagOn.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btFlagOn.Name = "btFlagOn";
            this.btFlagOn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.btFlagOn.Size = new System.Drawing.Size(26, 21);
            this.btFlagOn.TabIndex = 11;
            this.btFlagOn.Text = "F";
            this.btFlagOn.UseVisualStyleBackColor = false;
            this.btFlagOn.Click += new System.EventHandler(this.btFlagOn_Click);
            // 
            // btZoomMainus
            // 
            this.btZoomMainus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btZoomMainus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btZoomMainus.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btZoomMainus.ForeColor = System.Drawing.Color.White;
            this.btZoomMainus.Location = new System.Drawing.Point(153, 0);
            this.btZoomMainus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btZoomMainus.Name = "btZoomMainus";
            this.btZoomMainus.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.btZoomMainus.Size = new System.Drawing.Size(24, 21);
            this.btZoomMainus.TabIndex = 12;
            this.btZoomMainus.Text = "-";
            this.btZoomMainus.UseVisualStyleBackColor = false;
            this.btZoomMainus.Click += new System.EventHandler(this.btZoomMainus_Click);
            // 
            // btZoomPlus
            // 
            this.btZoomPlus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btZoomPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btZoomPlus.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btZoomPlus.ForeColor = System.Drawing.Color.White;
            this.btZoomPlus.Location = new System.Drawing.Point(125, 0);
            this.btZoomPlus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btZoomPlus.Name = "btZoomPlus";
            this.btZoomPlus.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.btZoomPlus.Size = new System.Drawing.Size(24, 21);
            this.btZoomPlus.TabIndex = 13;
            this.btZoomPlus.Text = "+";
            this.btZoomPlus.UseVisualStyleBackColor = false;
            this.btZoomPlus.Click += new System.EventHandler(this.btZoomPlus_Click);
            // 
            // btAllModeSwitch
            // 
            this.btAllModeSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btAllModeSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAllModeSwitch.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btAllModeSwitch.ForeColor = System.Drawing.Color.White;
            this.btAllModeSwitch.Location = new System.Drawing.Point(215, 0);
            this.btAllModeSwitch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btAllModeSwitch.Name = "btAllModeSwitch";
            this.btAllModeSwitch.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.btAllModeSwitch.Size = new System.Drawing.Size(26, 21);
            this.btAllModeSwitch.TabIndex = 14;
            this.btAllModeSwitch.Text = "A";
            this.btAllModeSwitch.UseVisualStyleBackColor = false;
            this.btAllModeSwitch.Click += new System.EventHandler(this.btAllModeSwitch_Click);
            // 
            // btResize
            // 
            this.btResize.BackColor = System.Drawing.Color.Silver;
            this.btResize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btResize.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btResize.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btResize.Location = new System.Drawing.Point(276, 0);
            this.btResize.Margin = new System.Windows.Forms.Padding(0);
            this.btResize.Name = "btResize";
            this.btResize.Size = new System.Drawing.Size(24, 23);
            this.btResize.TabIndex = 9;
            this.btResize.UseVisualStyleBackColor = false;
            this.btResize.Click += new System.EventHandler(this.btResize_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(35, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 12);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadarForm_MouseDown);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.button1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RadarForm_MouseMove);
            // 
            // btInterpersonal
            // 
            this.btInterpersonal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btInterpersonal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btInterpersonal.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btInterpersonal.ForeColor = System.Drawing.Color.White;
            this.btInterpersonal.Location = new System.Drawing.Point(247, 0);
            this.btInterpersonal.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btInterpersonal.Name = "btInterpersonal";
            this.btInterpersonal.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.btInterpersonal.Size = new System.Drawing.Size(26, 21);
            this.btInterpersonal.TabIndex = 15;
            this.btInterpersonal.Text = "M";
            this.btInterpersonal.UseVisualStyleBackColor = false;
            this.btInterpersonal.Click += new System.EventHandler(this.btInterpersonal_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btResize);
            this.panel1.Controls.Add(this.lbSwitch);
            this.panel1.Controls.Add(this.btZoomPlus);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btAllModeSwitch);
            this.panel1.Controls.Add(this.btZoomMainus);
            this.panel1.Controls.Add(this.btInterpersonal);
            this.panel1.Controls.Add(this.btFlagOn);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 26);
            this.panel1.TabIndex = 17;
            // 
            // elementHost1
            // 
            this.elementHost1.BackColor = System.Drawing.Color.Transparent;
            this.elementHost1.BackColorTransparent = true;
            this.elementHost1.Location = new System.Drawing.Point(41, 182);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(200, 100);
            this.elementHost1.TabIndex = 18;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.radarWPF1;
            // 
            // RadarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(300, 305);
            this.ControlBox = false;
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RadarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RadarForm_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbSwitch;
        private System.Windows.Forms.Button btResize;
        private System.Windows.Forms.Button btFlagOn;
        private System.Windows.Forms.Button btZoomMainus;
        private System.Windows.Forms.Button btZoomPlus;
        private System.Windows.Forms.Button btAllModeSwitch;
        private System.Windows.Forms.Button btInterpersonal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private MultiRadar.RadarWPF radarWPF1;
    }


}
