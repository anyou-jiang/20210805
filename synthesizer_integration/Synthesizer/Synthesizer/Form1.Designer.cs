namespace Synthesizer
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
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StopBtn = new System.Windows.Forms.Button();
            this.timeTxt = new System.Windows.Forms.TextBox();
            this.FrequencyForLeftSpeakerTxt = new System.Windows.Forms.TextBox();
            this.AmplitudeTxt = new System.Windows.Forms.TextBox();
            this.HearingTestPlayBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.FrequencyForRightSpeakerTxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(180, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 20);
            this.label9.TabIndex = 46;
            this.label9.Text = "Seconds";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(58, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 20);
            this.label7.TabIndex = 45;
            this.label7.Text = "Left Speaker";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(63, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 20);
            this.label6.TabIndex = 44;
            this.label6.Text = "Amplitude";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 39;
            this.label3.Text = "Synthesizer:";
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(335, 235);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 21);
            this.StopBtn.TabIndex = 32;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // timeTxt
            // 
            this.timeTxt.Location = new System.Drawing.Point(62, 175);
            this.timeTxt.Name = "timeTxt";
            this.timeTxt.Size = new System.Drawing.Size(100, 21);
            this.timeTxt.TabIndex = 31;
            this.timeTxt.Text = "1";
            // 
            // FrequencyForLeftSpeakerTxt
            // 
            this.FrequencyForLeftSpeakerTxt.Location = new System.Drawing.Point(177, 103);
            this.FrequencyForLeftSpeakerTxt.Name = "FrequencyForLeftSpeakerTxt";
            this.FrequencyForLeftSpeakerTxt.Size = new System.Drawing.Size(100, 21);
            this.FrequencyForLeftSpeakerTxt.TabIndex = 30;
            this.FrequencyForLeftSpeakerTxt.Text = "500";
            this.FrequencyForLeftSpeakerTxt.TextChanged += new System.EventHandler(this.FrequencyForLeftSpeakerTxt_TextChanged);
            // 
            // AmplitudeTxt
            // 
            this.AmplitudeTxt.Location = new System.Drawing.Point(177, 60);
            this.AmplitudeTxt.Name = "AmplitudeTxt";
            this.AmplitudeTxt.Size = new System.Drawing.Size(100, 21);
            this.AmplitudeTxt.TabIndex = 29;
            this.AmplitudeTxt.Text = "0.5";
            // 
            // HearingTestPlayBtn
            // 
            this.HearingTestPlayBtn.Location = new System.Drawing.Point(146, 235);
            this.HearingTestPlayBtn.Name = "HearingTestPlayBtn";
            this.HearingTestPlayBtn.Size = new System.Drawing.Size(75, 21);
            this.HearingTestPlayBtn.TabIndex = 27;
            this.HearingTestPlayBtn.Text = "Play";
            this.HearingTestPlayBtn.UseVisualStyleBackColor = true;
            this.HearingTestPlayBtn.Click += new System.EventHandler(this.RainFallPlayBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(50, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 20);
            this.label8.TabIndex = 48;
            this.label8.Text = "Right speaker";
            // 
            // FrequencyForRightSpeakerTxt
            // 
            this.FrequencyForRightSpeakerTxt.Location = new System.Drawing.Point(177, 139);
            this.FrequencyForRightSpeakerTxt.Name = "FrequencyForRightSpeakerTxt";
            this.FrequencyForRightSpeakerTxt.Size = new System.Drawing.Size(100, 21);
            this.FrequencyForRightSpeakerTxt.TabIndex = 47;
            this.FrequencyForRightSpeakerTxt.Text = "45";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 49;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 415);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.FrequencyForRightSpeakerTxt);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.timeTxt);
            this.Controls.Add(this.FrequencyForLeftSpeakerTxt);
            this.Controls.Add(this.AmplitudeTxt);
            this.Controls.Add(this.HearingTestPlayBtn);
            this.Name = "Form1";
            this.Text = "Synthesizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.TextBox timeTxt;
        private System.Windows.Forms.TextBox FrequencyForLeftSpeakerTxt;
        private System.Windows.Forms.TextBox AmplitudeTxt;
        private System.Windows.Forms.Button HearingTestPlayBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox FrequencyForRightSpeakerTxt;
        private System.Windows.Forms.Button button1;
    }
}

