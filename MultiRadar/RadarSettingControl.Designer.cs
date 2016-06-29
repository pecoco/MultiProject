namespace MultiRadar
{
    partial class RadarSettingControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label30 = new System.Windows.Forms.Label();
            this.btSePath = new System.Windows.Forms.Button();
            this.textSePath = new System.Windows.Forms.TextBox();
            this.ckRadarVisible = new System.Windows.Forms.CheckBox();
            this.textRadarYpos = new System.Windows.Forms.TextBox();
            this.textRadarXpos = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbRederModeSelect = new System.Windows.Forms.RadioButton();
            this.rbRederModeFull = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControlMob = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listMobSS = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listMobA = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listMobB = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listMobETC = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textAreaJp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btAddAction = new System.Windows.Forms.Button();
            this.ComboRadarZone = new System.Windows.Forms.ComboBox();
            this.btOpenRadar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textRederDataPath = new System.Windows.Forms.TextBox();
            this.ofdRederPath = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.tabControlMob.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.label30.Location = new System.Drawing.Point(266, 415);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(46, 12);
            this.label30.TabIndex = 100;
            this.label30.Text = "SE Path";
            // 
            // btSePath
            // 
            this.btSePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btSePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btSePath.Location = new System.Drawing.Point(373, 406);
            this.btSePath.Name = "btSePath";
            this.btSePath.Size = new System.Drawing.Size(30, 26);
            this.btSePath.TabIndex = 99;
            this.btSePath.Text = "...";
            this.btSePath.UseVisualStyleBackColor = false;
            // 
            // textSePath
            // 
            this.textSePath.BackColor = System.Drawing.Color.Black;
            this.textSePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSePath.ForeColor = System.Drawing.Color.White;
            this.textSePath.Location = new System.Drawing.Point(210, 433);
            this.textSePath.Name = "textSePath";
            this.textSePath.Size = new System.Drawing.Size(193, 19);
            this.textSePath.TabIndex = 98;
            // 
            // ckRadarVisible
            // 
            this.ckRadarVisible.AutoSize = true;
            this.ckRadarVisible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ckRadarVisible.Location = new System.Drawing.Point(38, 434);
            this.ckRadarVisible.Name = "ckRadarVisible";
            this.ckRadarVisible.Size = new System.Drawing.Size(15, 14);
            this.ckRadarVisible.TabIndex = 97;
            this.ckRadarVisible.UseVisualStyleBackColor = true;
            // 
            // textRadarYpos
            // 
            this.textRadarYpos.BackColor = System.Drawing.Color.Black;
            this.textRadarYpos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textRadarYpos.ForeColor = System.Drawing.Color.White;
            this.textRadarYpos.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textRadarYpos.Location = new System.Drawing.Point(152, 433);
            this.textRadarYpos.MaxLength = 5;
            this.textRadarYpos.Name = "textRadarYpos";
            this.textRadarYpos.Size = new System.Drawing.Size(45, 19);
            this.textRadarYpos.TabIndex = 96;
            this.textRadarYpos.Text = "0";
            this.textRadarYpos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Setting_KeyPress);
            // 
            // textRadarXpos
            // 
            this.textRadarXpos.BackColor = System.Drawing.Color.Black;
            this.textRadarXpos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textRadarXpos.ForeColor = System.Drawing.Color.White;
            this.textRadarXpos.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textRadarXpos.Location = new System.Drawing.Point(89, 433);
            this.textRadarXpos.MaxLength = 5;
            this.textRadarXpos.Name = "textRadarXpos";
            this.textRadarXpos.Size = new System.Drawing.Size(43, 19);
            this.textRadarXpos.TabIndex = 95;
            this.textRadarXpos.Text = "0";
            this.textRadarXpos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Setting_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.label19.Location = new System.Drawing.Point(28, 415);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(39, 12);
            this.label19.TabIndex = 94;
            this.label19.Text = "Enable";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.label13.Location = new System.Drawing.Point(148, 415);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 12);
            this.label13.TabIndex = 93;
            this.label13.Text = "Y Position";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.label17.Location = new System.Drawing.Point(84, 415);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 12);
            this.label17.TabIndex = 92;
            this.label17.Text = "X Position";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRederModeSelect);
            this.groupBox1.Controls.Add(this.rbRederModeFull);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(34, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 34);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            // 
            // rbRederModeSelect
            // 
            this.rbRederModeSelect.AutoSize = true;
            this.rbRederModeSelect.Location = new System.Drawing.Point(169, 13);
            this.rbRederModeSelect.Name = "rbRederModeSelect";
            this.rbRederModeSelect.Size = new System.Drawing.Size(144, 16);
            this.rbRederModeSelect.TabIndex = 2;
            this.rbRederModeSelect.Text = "Only Select Radar Data";
            this.rbRederModeSelect.UseVisualStyleBackColor = true;
            // 
            // rbRederModeFull
            // 
            this.rbRederModeFull.AutoSize = true;
            this.rbRederModeFull.Checked = true;
            this.rbRederModeFull.Location = new System.Drawing.Point(103, 13);
            this.rbRederModeFull.Name = "rbRederModeFull";
            this.rbRederModeFull.Size = new System.Drawing.Size(42, 16);
            this.rbRederModeFull.TabIndex = 1;
            this.rbRederModeFull.TabStop = true;
            this.rbRederModeFull.Text = "Full";
            this.rbRederModeFull.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "Radar Mode";
            // 
            // tabControlMob
            // 
            this.tabControlMob.Controls.Add(this.tabPage1);
            this.tabControlMob.Controls.Add(this.tabPage2);
            this.tabControlMob.Controls.Add(this.tabPage3);
            this.tabControlMob.Controls.Add(this.tabPage4);
            this.tabControlMob.Location = new System.Drawing.Point(34, 201);
            this.tabControlMob.Name = "tabControlMob";
            this.tabControlMob.SelectedIndex = 0;
            this.tabControlMob.Size = new System.Drawing.Size(369, 117);
            this.tabControlMob.TabIndex = 90;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.listMobSS);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(361, 91);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "S";
            // 
            // listMobSS
            // 
            this.listMobSS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listMobSS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMobSS.ForeColor = System.Drawing.Color.White;
            this.listMobSS.FormattingEnabled = true;
            this.listMobSS.ItemHeight = 12;
            this.listMobSS.Location = new System.Drawing.Point(3, 3);
            this.listMobSS.Name = "listMobSS";
            this.listMobSS.Size = new System.Drawing.Size(355, 85);
            this.listMobSS.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.listMobA);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(361, 91);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "A";
            // 
            // listMobA
            // 
            this.listMobA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listMobA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMobA.ForeColor = System.Drawing.Color.White;
            this.listMobA.FormattingEnabled = true;
            this.listMobA.ItemHeight = 12;
            this.listMobA.Location = new System.Drawing.Point(3, 3);
            this.listMobA.Name = "listMobA";
            this.listMobA.Size = new System.Drawing.Size(355, 85);
            this.listMobA.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.listMobB);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(361, 91);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "B";
            // 
            // listMobB
            // 
            this.listMobB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listMobB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMobB.ForeColor = System.Drawing.Color.White;
            this.listMobB.FormattingEnabled = true;
            this.listMobB.ItemHeight = 12;
            this.listMobB.Location = new System.Drawing.Point(3, 3);
            this.listMobB.Name = "listMobB";
            this.listMobB.Size = new System.Drawing.Size(355, 85);
            this.listMobB.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Controls.Add(this.listMobETC);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(361, 91);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "ETC";
            // 
            // listMobETC
            // 
            this.listMobETC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listMobETC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMobETC.ForeColor = System.Drawing.Color.White;
            this.listMobETC.FormattingEnabled = true;
            this.listMobETC.ItemHeight = 12;
            this.listMobETC.Location = new System.Drawing.Point(3, 3);
            this.listMobETC.Name = "listMobETC";
            this.listMobETC.Size = new System.Drawing.Size(355, 85);
            this.listMobETC.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.label9.Location = new System.Drawing.Point(32, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 12);
            this.label9.TabIndex = 89;
            this.label9.Text = "Mob Lank";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.label8.Location = new System.Drawing.Point(217, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 12);
            this.label8.TabIndex = 88;
            this.label8.Text = "Area Japanese Name";
            // 
            // textAreaJp
            // 
            this.textAreaJp.BackColor = System.Drawing.Color.Black;
            this.textAreaJp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAreaJp.ForeColor = System.Drawing.Color.White;
            this.textAreaJp.Location = new System.Drawing.Point(219, 161);
            this.textAreaJp.Name = "textAreaJp";
            this.textAreaJp.ReadOnly = true;
            this.textAreaJp.Size = new System.Drawing.Size(158, 19);
            this.textAreaJp.TabIndex = 87;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.label7.Location = new System.Drawing.Point(37, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 86;
            this.label7.Text = "Select Area";
            // 
            // btAddAction
            // 
            this.btAddAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btAddAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAddAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btAddAction.Location = new System.Drawing.Point(36, 114);
            this.btAddAction.Name = "btAddAction";
            this.btAddAction.Size = new System.Drawing.Size(147, 23);
            this.btAddAction.TabIndex = 85;
            this.btAddAction.Text = "Add New Data or Update";
            this.btAddAction.UseVisualStyleBackColor = false;
            this.btAddAction.Click += new System.EventHandler(this.btAddAction_Click);
            // 
            // ComboRadarZone
            // 
            this.ComboRadarZone.BackColor = System.Drawing.Color.Black;
            this.ComboRadarZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboRadarZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboRadarZone.ForeColor = System.Drawing.Color.White;
            this.ComboRadarZone.FormattingEnabled = true;
            this.ComboRadarZone.Location = new System.Drawing.Point(35, 160);
            this.ComboRadarZone.Name = "ComboRadarZone";
            this.ComboRadarZone.Size = new System.Drawing.Size(156, 20);
            this.ComboRadarZone.TabIndex = 84;
            this.ComboRadarZone.SelectedIndexChanged += new System.EventHandler(this.ComboRadarZone_SelectedIndexChanged);
            // 
            // btOpenRadar
            // 
            this.btOpenRadar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btOpenRadar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOpenRadar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btOpenRadar.Location = new System.Drawing.Point(369, 17);
            this.btOpenRadar.Name = "btOpenRadar";
            this.btOpenRadar.Size = new System.Drawing.Size(30, 26);
            this.btOpenRadar.TabIndex = 103;
            this.btOpenRadar.Text = "...";
            this.btOpenRadar.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.label6.Location = new System.Drawing.Point(28, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 12);
            this.label6.TabIndex = 102;
            this.label6.Text = "Setting Path";
            // 
            // textRederDataPath
            // 
            this.textRederDataPath.BackColor = System.Drawing.Color.Black;
            this.textRederDataPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textRederDataPath.ForeColor = System.Drawing.Color.White;
            this.textRederDataPath.Location = new System.Drawing.Point(30, 45);
            this.textRederDataPath.Name = "textRederDataPath";
            this.textRederDataPath.Size = new System.Drawing.Size(369, 19);
            this.textRederDataPath.TabIndex = 101;
            // 
            // RadarSettingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.btOpenRadar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textRederDataPath);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.btSePath);
            this.Controls.Add(this.textSePath);
            this.Controls.Add(this.ckRadarVisible);
            this.Controls.Add(this.textRadarYpos);
            this.Controls.Add(this.textRadarXpos);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControlMob);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textAreaJp);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btAddAction);
            this.Controls.Add(this.ComboRadarZone);
            this.Name = "RadarSettingControl";
            this.Size = new System.Drawing.Size(441, 503);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControlMob.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btSePath;
        private System.Windows.Forms.TextBox textSePath;
        private System.Windows.Forms.CheckBox ckRadarVisible;
        private System.Windows.Forms.TextBox textRadarYpos;
        private System.Windows.Forms.TextBox textRadarXpos;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbRederModeSelect;
        private System.Windows.Forms.RadioButton rbRederModeFull;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabControl tabControlMob;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox listMobSS;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listMobA;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listMobB;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox listMobETC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textAreaJp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btAddAction;
        private System.Windows.Forms.ComboBox ComboRadarZone;
        private System.Windows.Forms.Button btOpenRadar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textRederDataPath;
        private System.Windows.Forms.FolderBrowserDialog ofdRederPath;
    }
}
