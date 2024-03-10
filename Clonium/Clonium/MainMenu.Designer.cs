namespace Clonium
{
    partial class Settings
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
            this.OnePlayer = new System.Windows.Forms.RadioButton();
            this.TwoPlayers = new System.Windows.Forms.RadioButton();
            this.ThreePlayers = new System.Windows.Forms.RadioButton();
            this.FourPlayers = new System.Windows.Forms.RadioButton();
            this.Field1 = new System.Windows.Forms.RadioButton();
            this.Field2 = new System.Windows.Forms.RadioButton();
            this.Field3 = new System.Windows.Forms.RadioButton();
            this.Field4 = new System.Windows.Forms.RadioButton();
            this.Field5 = new System.Windows.Forms.RadioButton();
            this.numberOfPlayers = new System.Windows.Forms.GroupBox();
            this.Fields = new System.Windows.Forms.GroupBox();
            this.OK = new System.Windows.Forms.Button();
            this.numberOfPlayers.SuspendLayout();
            this.Fields.SuspendLayout();
            this.SuspendLayout();
            // 
            // OnePlayer
            // 
            this.OnePlayer.AutoSize = true;
            this.OnePlayer.Checked = true;
            this.OnePlayer.Location = new System.Drawing.Point(6, 19);
            this.OnePlayer.Name = "OnePlayer";
            this.OnePlayer.Size = new System.Drawing.Size(31, 17);
            this.OnePlayer.TabIndex = 0;
            this.OnePlayer.TabStop = true;
            this.OnePlayer.Text = "1";
            this.OnePlayer.UseVisualStyleBackColor = true;
            // 
            // TwoPlayers
            // 
            this.TwoPlayers.AutoSize = true;
            this.TwoPlayers.Location = new System.Drawing.Point(43, 19);
            this.TwoPlayers.Name = "TwoPlayers";
            this.TwoPlayers.Size = new System.Drawing.Size(31, 17);
            this.TwoPlayers.TabIndex = 0;
            this.TwoPlayers.TabStop = true;
            this.TwoPlayers.Text = "2";
            this.TwoPlayers.UseVisualStyleBackColor = true;
            // 
            // ThreePlayers
            // 
            this.ThreePlayers.AutoSize = true;
            this.ThreePlayers.Location = new System.Drawing.Point(80, 19);
            this.ThreePlayers.Name = "ThreePlayers";
            this.ThreePlayers.Size = new System.Drawing.Size(31, 17);
            this.ThreePlayers.TabIndex = 0;
            this.ThreePlayers.TabStop = true;
            this.ThreePlayers.Text = "3";
            this.ThreePlayers.UseVisualStyleBackColor = true;
            // 
            // FourPlayers
            // 
            this.FourPlayers.AutoSize = true;
            this.FourPlayers.Location = new System.Drawing.Point(117, 19);
            this.FourPlayers.Name = "FourPlayers";
            this.FourPlayers.Size = new System.Drawing.Size(31, 17);
            this.FourPlayers.TabIndex = 0;
            this.FourPlayers.TabStop = true;
            this.FourPlayers.Text = "4";
            this.FourPlayers.UseVisualStyleBackColor = true;
            // 
            // Field1
            // 
            this.Field1.Appearance = System.Windows.Forms.Appearance.Button;
            this.Field1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Field1.Checked = true;
            this.Field1.Location = new System.Drawing.Point(6, 19);
            this.Field1.Name = "Field1";
            this.Field1.Size = new System.Drawing.Size(60, 60);
            this.Field1.TabIndex = 0;
            this.Field1.TabStop = true;
            this.Field1.UseVisualStyleBackColor = true;
            this.Field1.CheckedChanged += new System.EventHandler(this.FieldRadioButtons_CheckedChanged);
            // 
            // Field2
            // 
            this.Field2.Appearance = System.Windows.Forms.Appearance.Button;
            this.Field2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Field2.Location = new System.Drawing.Point(72, 19);
            this.Field2.Name = "Field2";
            this.Field2.Size = new System.Drawing.Size(60, 60);
            this.Field2.TabIndex = 0;
            this.Field2.TabStop = true;
            this.Field2.UseVisualStyleBackColor = true;
            this.Field2.CheckedChanged += new System.EventHandler(this.FieldRadioButtons_CheckedChanged);
            // 
            // Field3
            // 
            this.Field3.Appearance = System.Windows.Forms.Appearance.Button;
            this.Field3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Field3.Location = new System.Drawing.Point(138, 19);
            this.Field3.Name = "Field3";
            this.Field3.Size = new System.Drawing.Size(60, 60);
            this.Field3.TabIndex = 0;
            this.Field3.TabStop = true;
            this.Field3.UseVisualStyleBackColor = true;
            this.Field3.CheckedChanged += new System.EventHandler(this.FieldRadioButtons_CheckedChanged);
            // 
            // Field4
            // 
            this.Field4.Appearance = System.Windows.Forms.Appearance.Button;
            this.Field4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Field4.Location = new System.Drawing.Point(204, 19);
            this.Field4.Name = "Field4";
            this.Field4.Size = new System.Drawing.Size(60, 60);
            this.Field4.TabIndex = 0;
            this.Field4.TabStop = true;
            this.Field4.UseVisualStyleBackColor = true;
            this.Field4.CheckedChanged += new System.EventHandler(this.FieldRadioButtons_CheckedChanged);
            // 
            // Field5
            // 
            this.Field5.Appearance = System.Windows.Forms.Appearance.Button;
            this.Field5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Field5.Location = new System.Drawing.Point(270, 19);
            this.Field5.Name = "Field5";
            this.Field5.Size = new System.Drawing.Size(60, 60);
            this.Field5.TabIndex = 0;
            this.Field5.TabStop = true;
            this.Field5.UseVisualStyleBackColor = true;
            // 
            // numberOfPlayers
            // 
            this.numberOfPlayers.Controls.Add(this.OnePlayer);
            this.numberOfPlayers.Controls.Add(this.FourPlayers);
            this.numberOfPlayers.Controls.Add(this.TwoPlayers);
            this.numberOfPlayers.Controls.Add(this.ThreePlayers);
            this.numberOfPlayers.Location = new System.Drawing.Point(12, 12);
            this.numberOfPlayers.Name = "numberOfPlayers";
            this.numberOfPlayers.Size = new System.Drawing.Size(338, 51);
            this.numberOfPlayers.TabIndex = 1;
            this.numberOfPlayers.TabStop = false;
            this.numberOfPlayers.Text = "Количество игроков";
            // 
            // Fields
            // 
            this.Fields.Controls.Add(this.Field1);
            this.Fields.Controls.Add(this.Field2);
            this.Fields.Controls.Add(this.Field5);
            this.Fields.Controls.Add(this.Field3);
            this.Fields.Controls.Add(this.Field4);
            this.Fields.Location = new System.Drawing.Point(12, 69);
            this.Fields.Name = "Fields";
            this.Fields.Size = new System.Drawing.Size(338, 92);
            this.Fields.TabIndex = 2;
            this.Fields.TabStop = false;
            this.Fields.Text = "Игровое поле";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(274, 167);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 3;
            this.OK.Text = "ОК";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 202);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Fields);
            this.Controls.Add(this.numberOfPlayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметры";
            this.numberOfPlayers.ResumeLayout(false);
            this.numberOfPlayers.PerformLayout();
            this.Fields.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton OnePlayer;
        private System.Windows.Forms.RadioButton TwoPlayers;
        private System.Windows.Forms.RadioButton ThreePlayers;
        private System.Windows.Forms.RadioButton FourPlayers;
        private System.Windows.Forms.RadioButton Field1;
        private System.Windows.Forms.RadioButton Field2;
        private System.Windows.Forms.RadioButton Field3;
        private System.Windows.Forms.RadioButton Field4;
        private System.Windows.Forms.RadioButton Field5;
        private System.Windows.Forms.GroupBox numberOfPlayers;
        private System.Windows.Forms.GroupBox Fields;
        private System.Windows.Forms.Button OK;
    }
}