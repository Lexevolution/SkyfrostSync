namespace SFSFront
{
    partial class SFSGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            registerButton = new Button();
            unregisterButton = new Button();
            PasswordTextbox = new TextBox();
            UsernameTextbox = new TextBox();
            LoginButton = new Button();
            SuspendLayout();
            // 
            // registerButton
            // 
            registerButton.Location = new Point(365, 150);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(75, 23);
            registerButton.TabIndex = 0;
            registerButton.Text = "Register";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // unregisterButton
            // 
            unregisterButton.Location = new Point(365, 207);
            unregisterButton.Name = "unregisterButton";
            unregisterButton.Size = new Size(75, 23);
            unregisterButton.TabIndex = 1;
            unregisterButton.Text = "Unregister";
            unregisterButton.UseVisualStyleBackColor = true;
            unregisterButton.Click += unregisterButton_Click;
            // 
            // PasswordTextbox
            // 
            PasswordTextbox.Location = new Point(113, 174);
            PasswordTextbox.Name = "PasswordTextbox";
            PasswordTextbox.PasswordChar = '*';
            PasswordTextbox.Size = new Size(100, 23);
            PasswordTextbox.TabIndex = 2;
            // 
            // UsernameTextbox
            // 
            UsernameTextbox.Location = new Point(113, 127);
            UsernameTextbox.Name = "UsernameTextbox";
            UsernameTextbox.Size = new Size(100, 23);
            UsernameTextbox.TabIndex = 2;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(113, 207);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(75, 23);
            LoginButton.TabIndex = 3;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // SFSGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LoginButton);
            Controls.Add(UsernameTextbox);
            Controls.Add(PasswordTextbox);
            Controls.Add(unregisterButton);
            Controls.Add(registerButton);
            Name = "SFSGUI";
            Text = "SkyfrostSync";
            Load += SFSGUI_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button registerButton;
        private Button unregisterButton;
        private TextBox PasswordTextbox;
        private TextBox UsernameTextbox;
        private Button LoginButton;
    }
}
