namespace Ambalama_HMS_with_MetroUI
{
    partial class Login
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
            this.unameTxt = new MetroFramework.Controls.MetroTextBox();
            this.loginBtn = new MetroFramework.Controls.MetroButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.passwordTxt = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // unameTxt
            // 
            // 
            // 
            // 
            this.unameTxt.CustomButton.Image = null;
            this.unameTxt.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.unameTxt.CustomButton.Name = "";
            this.unameTxt.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.unameTxt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.unameTxt.CustomButton.TabIndex = 1;
            this.unameTxt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.unameTxt.CustomButton.UseSelectable = true;
            this.unameTxt.CustomButton.Visible = false;
            this.unameTxt.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.unameTxt.Lines = new string[0];
            this.unameTxt.Location = new System.Drawing.Point(267, 196);
            this.unameTxt.MaxLength = 32767;
            this.unameTxt.Name = "unameTxt";
            this.unameTxt.PasswordChar = '\0';
            this.unameTxt.PromptText = "Username";
            this.unameTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.unameTxt.SelectedText = "";
            this.unameTxt.SelectionLength = 0;
            this.unameTxt.SelectionStart = 0;
            this.unameTxt.ShortcutsEnabled = true;
            this.unameTxt.Size = new System.Drawing.Size(267, 35);
            this.unameTxt.TabIndex = 0;
            this.unameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.unameTxt.UseSelectable = true;
            this.unameTxt.WaterMark = "Username";
            this.unameTxt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.unameTxt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // loginBtn
            // 
            this.loginBtn.BackColor = System.Drawing.Color.White;
            this.loginBtn.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.loginBtn.Location = new System.Drawing.Point(328, 331);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(150, 45);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseCustomBackColor = true;
            this.loginBtn.UseSelectable = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ambalama_HMS_with_MetroUI.Properties.Resources.site_logo_white;
            this.pictureBox1.Location = new System.Drawing.Point(203, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(392, 97);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // passwordTxt
            // 
            // 
            // 
            // 
            this.passwordTxt.CustomButton.Image = null;
            this.passwordTxt.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.passwordTxt.CustomButton.Name = "";
            this.passwordTxt.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.passwordTxt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.passwordTxt.CustomButton.TabIndex = 1;
            this.passwordTxt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.passwordTxt.CustomButton.UseSelectable = true;
            this.passwordTxt.CustomButton.Visible = false;
            this.passwordTxt.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.passwordTxt.Lines = new string[0];
            this.passwordTxt.Location = new System.Drawing.Point(267, 251);
            this.passwordTxt.MaxLength = 32767;
            this.passwordTxt.Name = "passwordTxt";
            this.passwordTxt.PasswordChar = '●';
            this.passwordTxt.PromptText = "Password";
            this.passwordTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.passwordTxt.SelectedText = "";
            this.passwordTxt.SelectionLength = 0;
            this.passwordTxt.SelectionStart = 0;
            this.passwordTxt.ShortcutsEnabled = true;
            this.passwordTxt.Size = new System.Drawing.Size(267, 35);
            this.passwordTxt.TabIndex = 2;
            this.passwordTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.passwordTxt.UseSelectable = true;
            this.passwordTxt.UseSystemPasswordChar = true;
            this.passwordTxt.WaterMark = "Password";
            this.passwordTxt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.passwordTxt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passwordTxt_KeyDown);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 465);
            this.Controls.Add(this.passwordTxt);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.unameTxt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "Login";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Style = MetroFramework.MetroColorStyle.White;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox unameTxt;
        private MetroFramework.Controls.MetroButton loginBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroTextBox passwordTxt;
    }
}