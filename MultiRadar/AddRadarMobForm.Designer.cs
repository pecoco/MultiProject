namespace MultiRadar
{
    partial class AddRadarMobForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSelectArea = new System.Windows.Forms.RadioButton();
            this.rbNewArea = new System.Windows.Forms.RadioButton();
            this.textZone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbZone = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textMob = new System.Windows.Forms.TextBox();
            this.btOk = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMobType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textZoneJp = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSelectArea);
            this.groupBox1.Controls.Add(this.rbNewArea);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 41);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Area";
            // 
            // rbSelectArea
            // 
            this.rbSelectArea.AutoSize = true;
            this.rbSelectArea.Checked = true;
            this.rbSelectArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbSelectArea.Location = new System.Drawing.Point(9, 18);
            this.rbSelectArea.Name = "rbSelectArea";
            this.rbSelectArea.Size = new System.Drawing.Size(83, 16);
            this.rbSelectArea.TabIndex = 1;
            this.rbSelectArea.TabStop = true;
            this.rbSelectArea.Text = "Select Area";
            this.rbSelectArea.UseVisualStyleBackColor = true;
            this.rbSelectArea.CheckedChanged += new System.EventHandler(this.rbSelectArea_CheckedChanged);
            // 
            // rbNewArea
            // 
            this.rbNewArea.AutoSize = true;
            this.rbNewArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbNewArea.Location = new System.Drawing.Point(180, 18);
            this.rbNewArea.Name = "rbNewArea";
            this.rbNewArea.Size = new System.Drawing.Size(69, 16);
            this.rbNewArea.TabIndex = 0;
            this.rbNewArea.Text = "NewArea";
            this.rbNewArea.UseVisualStyleBackColor = true;
            this.rbNewArea.CheckedChanged += new System.EventHandler(this.rbNewArea_CheckedChanged);
            // 
            // textZone
            // 
            this.textZone.Location = new System.Drawing.Point(3, 77);
            this.textZone.Name = "textZone";
            this.textZone.Size = new System.Drawing.Size(269, 19);
            this.textZone.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Yu Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(75, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add Mob";
            // 
            // cbZone
            // 
            this.cbZone.FormattingEnabled = true;
            this.cbZone.Location = new System.Drawing.Point(3, 77);
            this.cbZone.Name = "cbZone";
            this.cbZone.Size = new System.Drawing.Size(269, 20);
            this.cbZone.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(6, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mob Name";
            // 
            // textMob
            // 
            this.textMob.Location = new System.Drawing.Point(3, 192);
            this.textMob.Name = "textMob";
            this.textMob.Size = new System.Drawing.Size(269, 19);
            this.textMob.TabIndex = 5;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btOk.Location = new System.Drawing.Point(48, 228);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(61, 23);
            this.btOk.TabIndex = 6;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtCancel.Location = new System.Drawing.Point(161, 228);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(61, 23);
            this.BtCancel.TabIndex = 7;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(6, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Mob Type";
            // 
            // cbMobType
            // 
            this.cbMobType.FormattingEnabled = true;
            this.cbMobType.Items.AddRange(new object[] {
            "S",
            "A",
            "B",
            "ETC"});
            this.cbMobType.Location = new System.Drawing.Point(83, 152);
            this.cbMobType.Name = "cbMobType";
            this.cbMobType.Size = new System.Drawing.Size(189, 20);
            this.cbMobType.TabIndex = 9;
            this.cbMobType.Text = "S";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Area JapanName";
            // 
            // textZoneJp
            // 
            this.textZoneJp.Location = new System.Drawing.Point(4, 122);
            this.textZoneJp.Name = "textZoneJp";
            this.textZoneJp.Size = new System.Drawing.Size(269, 19);
            this.textZoneJp.TabIndex = 11;
            // 
            // AddRadarMobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(276, 273);
            this.Controls.Add(this.textZoneJp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbMobType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.textMob);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbZone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textZone);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddRadarMobForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AddRadarMobForm";
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.AddRederMobForm_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSelectArea;
        private System.Windows.Forms.RadioButton rbNewArea;
        private System.Windows.Forms.TextBox textZone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbZone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textMob;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMobType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textZoneJp;
    }
}