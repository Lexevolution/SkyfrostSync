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
            components = new System.ComponentModel.Container();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            registerButton = new Button();
            unregisterButton = new Button();
            PasswordTextbox = new TextBox();
            UsernameTextbox = new TextBox();
            LoginButton = new Button();
            bindingSource1 = new BindingSource(components);
            GenPicture = new Button();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
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
            // bindingSource1
            // 
            bindingSource1.AllowNew = true;
            bindingSource1.DataSource = typeof(SFFileLib.AccountInfo);
            // 
            // GenPicture
            // 
            GenPicture.Location = new Point(244, 272);
            GenPicture.Name = "GenPicture";
            GenPicture.Size = new Size(75, 23);
            GenPicture.TabIndex = 4;
            GenPicture.Text = "Picture :)";
            GenPicture.UseVisualStyleBackColor = true;
            GenPicture.Click += GenPicture_Click;
            // 
            // SFSGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GenPicture);
            Controls.Add(LoginButton);
            Controls.Add(UsernameTextbox);
            Controls.Add(PasswordTextbox);
            Controls.Add(unregisterButton);
            Controls.Add(registerButton);
            Name = "SFSGUI";
            Text = "SkyfrostSync";
            Load += SFSGUI_Load;
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
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
        public BindingSource bindingSource1;
        private Button GenPicture;
    }
}
