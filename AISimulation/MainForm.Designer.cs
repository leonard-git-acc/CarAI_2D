namespace Simulation
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.defaultspeed_button = new System.Windows.Forms.Button();
            this.render_checkBox = new System.Windows.Forms.CheckBox();
            this.grid_checkBox = new System.Windows.Forms.CheckBox();
            this.save_button = new System.Windows.Forms.Button();
            this.datails_checkBox = new System.Windows.Forms.CheckBox();
            this.load_button = new System.Windows.Forms.Button();
            this.control_panel = new System.Windows.Forms.Panel();
            this.settings_button = new System.Windows.Forms.Button();
            this.unlimitedspeed_button = new System.Windows.Forms.Button();
            this.halfspeed_button = new System.Windows.Forms.Button();
            this.doublespeed_button = new System.Windows.Forms.Button();
            this.control_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultspeed_button
            // 
            this.defaultspeed_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultspeed_button.Location = new System.Drawing.Point(11, 33);
            this.defaultspeed_button.Name = "defaultspeed_button";
            this.defaultspeed_button.Size = new System.Drawing.Size(67, 23);
            this.defaultspeed_button.TabIndex = 1;
            this.defaultspeed_button.Text = "Default";
            this.defaultspeed_button.UseVisualStyleBackColor = true;
            this.defaultspeed_button.Click += new System.EventHandler(this.defaultspeed_button_Click);
            // 
            // render_checkBox
            // 
            this.render_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.render_checkBox.AutoSize = true;
            this.render_checkBox.Checked = true;
            this.render_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.render_checkBox.Location = new System.Drawing.Point(20, 128);
            this.render_checkBox.Name = "render_checkBox";
            this.render_checkBox.Size = new System.Drawing.Size(61, 17);
            this.render_checkBox.TabIndex = 2;
            this.render_checkBox.Text = "Render";
            this.render_checkBox.UseVisualStyleBackColor = true;
            this.render_checkBox.CheckedChanged += new System.EventHandler(this.render_checkBox_CheckedChanged);
            // 
            // grid_checkBox
            // 
            this.grid_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_checkBox.AutoSize = true;
            this.grid_checkBox.Location = new System.Drawing.Point(20, 174);
            this.grid_checkBox.Name = "grid_checkBox";
            this.grid_checkBox.Size = new System.Drawing.Size(45, 17);
            this.grid_checkBox.TabIndex = 3;
            this.grid_checkBox.Text = "Grid";
            this.grid_checkBox.UseVisualStyleBackColor = true;
            this.grid_checkBox.CheckedChanged += new System.EventHandler(this.grid_checkBox_CheckedChanged);
            // 
            // save_button
            // 
            this.save_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.save_button.Location = new System.Drawing.Point(20, 193);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(50, 23);
            this.save_button.TabIndex = 4;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // datails_checkBox
            // 
            this.datails_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.datails_checkBox.AutoSize = true;
            this.datails_checkBox.Location = new System.Drawing.Point(20, 151);
            this.datails_checkBox.Name = "datails_checkBox";
            this.datails_checkBox.Size = new System.Drawing.Size(58, 17);
            this.datails_checkBox.TabIndex = 5;
            this.datails_checkBox.Text = "Details";
            this.datails_checkBox.UseVisualStyleBackColor = true;
            this.datails_checkBox.CheckedChanged += new System.EventHandler(this.datails_checkBox_CheckedChanged);
            // 
            // load_button
            // 
            this.load_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.load_button.Location = new System.Drawing.Point(20, 222);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(50, 23);
            this.load_button.TabIndex = 6;
            this.load_button.Text = "Load";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // control_panel
            // 
            this.control_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.control_panel.Controls.Add(this.settings_button);
            this.control_panel.Controls.Add(this.unlimitedspeed_button);
            this.control_panel.Controls.Add(this.halfspeed_button);
            this.control_panel.Controls.Add(this.doublespeed_button);
            this.control_panel.Controls.Add(this.defaultspeed_button);
            this.control_panel.Controls.Add(this.load_button);
            this.control_panel.Controls.Add(this.render_checkBox);
            this.control_panel.Controls.Add(this.datails_checkBox);
            this.control_panel.Controls.Add(this.grid_checkBox);
            this.control_panel.Controls.Add(this.save_button);
            this.control_panel.Enabled = false;
            this.control_panel.Location = new System.Drawing.Point(800, 283);
            this.control_panel.Name = "control_panel";
            this.control_panel.Size = new System.Drawing.Size(84, 279);
            this.control_panel.TabIndex = 7;
            // 
            // settings_button
            // 
            this.settings_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.settings_button.Location = new System.Drawing.Point(11, 251);
            this.settings_button.Name = "settings_button";
            this.settings_button.Size = new System.Drawing.Size(67, 23);
            this.settings_button.TabIndex = 10;
            this.settings_button.Text = "Settings";
            this.settings_button.UseVisualStyleBackColor = true;
            this.settings_button.Click += new System.EventHandler(this.settings_button_Click);
            // 
            // unlimitedspeed_button
            // 
            this.unlimitedspeed_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.unlimitedspeed_button.Location = new System.Drawing.Point(11, 91);
            this.unlimitedspeed_button.Name = "unlimitedspeed_button";
            this.unlimitedspeed_button.Size = new System.Drawing.Size(67, 23);
            this.unlimitedspeed_button.TabIndex = 9;
            this.unlimitedspeed_button.Text = "Unlimited";
            this.unlimitedspeed_button.UseVisualStyleBackColor = true;
            this.unlimitedspeed_button.Click += new System.EventHandler(this.unlimitedspeed_button_Click);
            // 
            // halfspeed_button
            // 
            this.halfspeed_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.halfspeed_button.Location = new System.Drawing.Point(11, 4);
            this.halfspeed_button.Name = "halfspeed_button";
            this.halfspeed_button.Size = new System.Drawing.Size(67, 23);
            this.halfspeed_button.TabIndex = 8;
            this.halfspeed_button.Text = "x0,5";
            this.halfspeed_button.UseVisualStyleBackColor = true;
            this.halfspeed_button.Click += new System.EventHandler(this.halfspeed_button_Click);
            // 
            // doublespeed_button
            // 
            this.doublespeed_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.doublespeed_button.Location = new System.Drawing.Point(11, 62);
            this.doublespeed_button.Name = "doublespeed_button";
            this.doublespeed_button.Size = new System.Drawing.Size(67, 23);
            this.doublespeed_button.TabIndex = 7;
            this.doublespeed_button.Text = "x2";
            this.doublespeed_button.UseVisualStyleBackColor = true;
            this.doublespeed_button.Click += new System.EventHandler(this.doublespeed_button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.control_panel);
            this.Name = "MainForm";
            this.Text = "CarAI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.control_panel.ResumeLayout(false);
            this.control_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button defaultspeed_button;
        private System.Windows.Forms.CheckBox render_checkBox;
        private System.Windows.Forms.CheckBox grid_checkBox;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.CheckBox datails_checkBox;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.Button doublespeed_button;
        private System.Windows.Forms.Button halfspeed_button;
        private System.Windows.Forms.Button unlimitedspeed_button;
        private System.Windows.Forms.Button settings_button;
        public System.Windows.Forms.Panel control_panel;
    }
}

