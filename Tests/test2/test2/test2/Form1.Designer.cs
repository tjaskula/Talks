namespace test2
{
    partial class Form1
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
            this.button_SingleTarget = new System.Windows.Forms.Button();
            this.button_MultiTarget = new System.Windows.Forms.Button();
            this.button_MultiTarget500 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button_SingleTarget
            // 
            this.button_SingleTarget.Location = new System.Drawing.Point(12, 12);
            this.button_SingleTarget.Name = "button_SingleTarget";
            this.button_SingleTarget.Size = new System.Drawing.Size(145, 27);
            this.button_SingleTarget.TabIndex = 0;
            this.button_SingleTarget.Text = "Single Target";
            this.button_SingleTarget.UseVisualStyleBackColor = true;
            this.button_SingleTarget.Click += new System.EventHandler(this.button_SingleTarget_Click);
            // 
            // button_MultiTarget
            // 
            this.button_MultiTarget.Location = new System.Drawing.Point(12, 45);
            this.button_MultiTarget.Name = "button_MultiTarget";
            this.button_MultiTarget.Size = new System.Drawing.Size(145, 27);
            this.button_MultiTarget.TabIndex = 1;
            this.button_MultiTarget.Text = "MultiTarget 1";
            this.button_MultiTarget.UseVisualStyleBackColor = true;
            this.button_MultiTarget.Click += new System.EventHandler(this.button_MultiTarget_Click);
            // 
            // button_MultiTarget500
            // 
            this.button_MultiTarget500.Location = new System.Drawing.Point(12, 78);
            this.button_MultiTarget500.Name = "button_MultiTarget500";
            this.button_MultiTarget500.Size = new System.Drawing.Size(145, 27);
            this.button_MultiTarget500.TabIndex = 2;
            this.button_MultiTarget500.Text = "MultiTarget 500";
            this.button_MultiTarget500.UseVisualStyleBackColor = true;
            this.button_MultiTarget500.Click += new System.EventHandler(this.button_MultiTarget500_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(182, 14);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(249, 227);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 253);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button_MultiTarget500);
            this.Controls.Add(this.button_MultiTarget);
            this.Controls.Add(this.button_SingleTarget);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_SingleTarget;
        private System.Windows.Forms.Button button_MultiTarget;
        private System.Windows.Forms.Button button_MultiTarget500;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

