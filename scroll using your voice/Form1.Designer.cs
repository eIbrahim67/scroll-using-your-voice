namespace ScrollUsingYourVoice
{
    partial class MainForm
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
            this.scrollUp = new System.Windows.Forms.Button();
            this.scrollDown = new System.Windows.Forms.Button();
            this.comboBoxWindows = new System.Windows.Forms.ComboBox();
            this.labelOutput = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scrollUp
            // 
            this.scrollUp.Location = new System.Drawing.Point(495, 12);
            this.scrollUp.Name = "scrollUp";
            this.scrollUp.Size = new System.Drawing.Size(293, 137);
            this.scrollUp.TabIndex = 0;
            this.scrollUp.Text = "Scroll Up";
            this.scrollUp.UseVisualStyleBackColor = true;
            this.scrollUp.Click += new System.EventHandler(this.buttonScrollUp_Click);
            // 
            // scrollDown
            // 
            this.scrollDown.Location = new System.Drawing.Point(495, 301);
            this.scrollDown.Name = "scrollDown";
            this.scrollDown.Size = new System.Drawing.Size(293, 137);
            this.scrollDown.TabIndex = 1;
            this.scrollDown.Text = "Scroll Down";
            this.scrollDown.UseVisualStyleBackColor = true;
            this.scrollDown.Click += new System.EventHandler(this.buttonScrollDown_Click);
            // 
            // comboBoxWindows
            // 
            this.comboBoxWindows.FormattingEnabled = true;
            this.comboBoxWindows.ImeMode = System.Windows.Forms.ImeMode.On;
            this.comboBoxWindows.IntegralHeight = false;
            this.comboBoxWindows.Location = new System.Drawing.Point(62, 42);
            this.comboBoxWindows.Name = "comboBoxWindows";
            this.comboBoxWindows.Size = new System.Drawing.Size(375, 24);
            this.comboBoxWindows.TabIndex = 2;
            this.comboBoxWindows.SelectedIndexChanged += new System.EventHandler(this.comboBoxWindows_SelectedIndexChanged);
            this.comboBoxWindows.Click += new System.EventHandler(this.comboBoxWindows_Click);
            // 
            // labelOutput
            // 
            this.labelOutput.Location = new System.Drawing.Point(87, 167);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(207, 170);
            this.labelOutput.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(253, 158);
            this.button1.TabIndex = 4;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.comboBoxWindows);
            this.Controls.Add(this.scrollDown);
            this.Controls.Add(this.scrollUp);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button scrollUp;
        private System.Windows.Forms.Button scrollDown;
        private System.Windows.Forms.ComboBox comboBoxWindows;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Button button1;
    }
}

