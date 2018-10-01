namespace Simulation
{
    partial class SettingsForm
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
            this.simImgSearch_button = new System.Windows.Forms.Button();
            this.simImgPath_textBox = new System.Windows.Forms.TextBox();
            this.overImgPath_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.overImgSearch_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.spawnLocationY_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.spawnLocationX_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rotation_trackBar = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.targetLocationX_textBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.targetLocationY_textBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.width_textBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.length_textBox = new System.Windows.Forms.TextBox();
            this.preview_panel = new System.Windows.Forms.Panel();
            this.selectSpawn_button = new System.Windows.Forms.Button();
            this.selectTarget_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rotation_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            // 
            // simImgSearch_button
            // 
            this.simImgSearch_button.Location = new System.Drawing.Point(380, 65);
            this.simImgSearch_button.Name = "simImgSearch_button";
            this.simImgSearch_button.Size = new System.Drawing.Size(75, 23);
            this.simImgSearch_button.TabIndex = 1;
            this.simImgSearch_button.Text = "Search";
            this.simImgSearch_button.UseVisualStyleBackColor = true;
            this.simImgSearch_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // simImgPath_textBox
            // 
            this.simImgPath_textBox.Location = new System.Drawing.Point(17, 68);
            this.simImgPath_textBox.Name = "simImgPath_textBox";
            this.simImgPath_textBox.ReadOnly = true;
            this.simImgPath_textBox.Size = new System.Drawing.Size(357, 20);
            this.simImgPath_textBox.TabIndex = 2;
            // 
            // overImgPath_textBox
            // 
            this.overImgPath_textBox.Location = new System.Drawing.Point(17, 107);
            this.overImgPath_textBox.Name = "overImgPath_textBox";
            this.overImgPath_textBox.ReadOnly = true;
            this.overImgPath_textBox.Size = new System.Drawing.Size(357, 20);
            this.overImgPath_textBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Simulation Image";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Overlay Image";
            // 
            // overImgSearch_button
            // 
            this.overImgSearch_button.Location = new System.Drawing.Point(380, 104);
            this.overImgSearch_button.Name = "overImgSearch_button";
            this.overImgSearch_button.Size = new System.Drawing.Size(75, 23);
            this.overImgSearch_button.TabIndex = 6;
            this.overImgSearch_button.Text = "Search";
            this.overImgSearch_button.UseVisualStyleBackColor = true;
            this.overImgSearch_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Car Settings";
            // 
            // spawnLocationY_textBox
            // 
            this.spawnLocationY_textBox.Location = new System.Drawing.Point(122, 205);
            this.spawnLocationY_textBox.Name = "spawnLocationY_textBox";
            this.spawnLocationY_textBox.Size = new System.Drawing.Size(56, 20);
            this.spawnLocationY_textBox.TabIndex = 8;
            this.spawnLocationY_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.spawnLocationY_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Spawn Location";
            // 
            // spawnLocationX_textBox
            // 
            this.spawnLocationX_textBox.Location = new System.Drawing.Point(37, 205);
            this.spawnLocationX_textBox.Name = "spawnLocationX_textBox";
            this.spawnLocationX_textBox.Size = new System.Drawing.Size(56, 20);
            this.spawnLocationX_textBox.TabIndex = 10;
            this.spawnLocationX_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.spawnLocationX_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "X:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Y:";
            // 
            // rotation_trackBar
            // 
            this.rotation_trackBar.Location = new System.Drawing.Point(236, 200);
            this.rotation_trackBar.Maximum = 72;
            this.rotation_trackBar.Name = "rotation_trackBar";
            this.rotation_trackBar.Size = new System.Drawing.Size(219, 45);
            this.rotation_trackBar.TabIndex = 13;
            this.rotation_trackBar.Scroll += new System.EventHandler(this.rotation_trackBar_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(235, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Rotation";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(99, 247);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Y:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 247);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "X:";
            // 
            // targetLocationX_textBox
            // 
            this.targetLocationX_textBox.Location = new System.Drawing.Point(37, 244);
            this.targetLocationX_textBox.Name = "targetLocationX_textBox";
            this.targetLocationX_textBox.Size = new System.Drawing.Size(56, 20);
            this.targetLocationX_textBox.TabIndex = 17;
            this.targetLocationX_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.targetLocationX_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Target Location";
            // 
            // targetLocationY_textBox
            // 
            this.targetLocationY_textBox.Location = new System.Drawing.Point(122, 244);
            this.targetLocationY_textBox.Name = "targetLocationY_textBox";
            this.targetLocationY_textBox.Size = new System.Drawing.Size(56, 20);
            this.targetLocationY_textBox.TabIndex = 15;
            this.targetLocationY_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.targetLocationY_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(341, 251);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Length:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(235, 251);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Width:";
            // 
            // width_textBox
            // 
            this.width_textBox.Location = new System.Drawing.Point(279, 248);
            this.width_textBox.Name = "width_textBox";
            this.width_textBox.Size = new System.Drawing.Size(56, 20);
            this.width_textBox.TabIndex = 22;
            this.width_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.width_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(235, 232);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Size";
            // 
            // length_textBox
            // 
            this.length_textBox.Location = new System.Drawing.Point(390, 248);
            this.length_textBox.Name = "length_textBox";
            this.length_textBox.Size = new System.Drawing.Size(56, 20);
            this.length_textBox.TabIndex = 20;
            this.length_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.length_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // preview_panel
            // 
            this.preview_panel.BackColor = System.Drawing.Color.Black;
            this.preview_panel.Location = new System.Drawing.Point(102, 308);
            this.preview_panel.Name = "preview_panel";
            this.preview_panel.Size = new System.Drawing.Size(250, 250);
            this.preview_panel.TabIndex = 25;
            this.preview_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.preview_panel_Paint);
            // 
            // selectSpawn_button
            // 
            this.selectSpawn_button.Location = new System.Drawing.Point(184, 203);
            this.selectSpawn_button.Name = "selectSpawn_button";
            this.selectSpawn_button.Size = new System.Drawing.Size(46, 23);
            this.selectSpawn_button.TabIndex = 26;
            this.selectSpawn_button.Text = "Select";
            this.selectSpawn_button.UseVisualStyleBackColor = true;
            // 
            // selectTarget_button
            // 
            this.selectTarget_button.Location = new System.Drawing.Point(184, 243);
            this.selectTarget_button.Name = "selectTarget_button";
            this.selectTarget_button.Size = new System.Drawing.Size(46, 23);
            this.selectTarget_button.TabIndex = 27;
            this.selectTarget_button.Text = "Select";
            this.selectTarget_button.UseVisualStyleBackColor = true;
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(299, 573);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 28;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(380, 573);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 29;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 608);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.selectTarget_button);
            this.Controls.Add(this.selectSpawn_button);
            this.Controls.Add(this.preview_panel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.width_textBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.length_textBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.targetLocationX_textBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.targetLocationY_textBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rotation_trackBar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.spawnLocationX_textBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.spawnLocationY_textBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.overImgSearch_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.overImgPath_textBox);
            this.Controls.Add(this.simImgPath_textBox);
            this.Controls.Add(this.simImgSearch_button);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rotation_trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeCustomComponent()
        {
            typeof(System.Windows.Forms.Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty
            | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null,
            preview_panel, new object[] { true });

            this.TopMost = true;
            this.MaximizeBox = false;
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button simImgSearch_button;
        private System.Windows.Forms.TextBox simImgPath_textBox;
        private System.Windows.Forms.TextBox overImgPath_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button overImgSearch_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox spawnLocationY_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox spawnLocationX_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar rotation_trackBar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox targetLocationX_textBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox targetLocationY_textBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox width_textBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox length_textBox;
        private System.Windows.Forms.Panel preview_panel;
        private System.Windows.Forms.Button selectSpawn_button;
        private System.Windows.Forms.Button selectTarget_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button start_button;
    }
}