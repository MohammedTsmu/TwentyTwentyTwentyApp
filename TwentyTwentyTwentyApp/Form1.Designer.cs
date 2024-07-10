namespace TwentyTwentyTwentyApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ProgressBar progressBarBreak;
        private System.Windows.Forms.Label lblBreaksTaken;
        private System.Windows.Forms.Label lblTotalBreakTime;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblInstruction = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.progressBarBreak = new System.Windows.Forms.ProgressBar();
            this.lblBreaksTaken = new System.Windows.Forms.Label();
            this.lblTotalBreakTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblInstruction
            // 
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Location = new System.Drawing.Point(12, 9);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(158, 13);
            this.lblInstruction.TabIndex = 0;
            this.lblInstruction.Text = "التطبيق سيذكرك بأخذ فترات راحة منتظمة:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(10, 50);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(150, 30);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "بدء التذكيرات";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(200, 50);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(150, 30);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "الإعدادات";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // progressBarBreak
            // 
            this.progressBarBreak.Location = new System.Drawing.Point(10, 100);
            this.progressBarBreak.Name = "progressBarBreak";
            this.progressBarBreak.Size = new System.Drawing.Size(360, 30);
            this.progressBarBreak.TabIndex = 3;
            this.progressBarBreak.Visible = false;
            // 
            // lblBreaksTaken
            // 
            this.lblBreaksTaken.AutoSize = true;
            this.lblBreaksTaken.Location = new System.Drawing.Point(10, 140);
            this.lblBreaksTaken.Name = "lblBreaksTaken";
            this.lblBreaksTaken.Size = new System.Drawing.Size(120, 13);
            this.lblBreaksTaken.TabIndex = 4;
            this.lblBreaksTaken.Text = "عدد فترات الراحة التي تم أخذها: 0";
            // 
            // lblTotalBreakTime
            // 
            this.lblTotalBreakTime.AutoSize = true;
            this.lblTotalBreakTime.Location = new System.Drawing.Point(10, 160);
            this.lblTotalBreakTime.Name = "lblTotalBreakTime";
            this.lblTotalBreakTime.Size = new System.Drawing.Size(122, 13);
            this.lblTotalBreakTime.TabIndex = 5;
            this.lblTotalBreakTime.Text = "إجمالي وقت الراحة: 0 دقائق";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 181);
            this.Controls.Add(this.lblTotalBreakTime);
            this.Controls.Add(this.lblBreaksTaken);
            this.Controls.Add(this.progressBarBreak);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblInstruction);
            this.Name = "Form1";
            this.Text = "20-20-20 App";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
