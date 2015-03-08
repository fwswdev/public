namespace KstFileGen
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
            this.btnGenerateFile = new System.Windows.Forms.Button();
            this.btnRandomData = new System.Windows.Forms.Button();
            this.btnRunContinuousStream = new System.Windows.Forms.Button();
            this.btnStopContinuousStream = new System.Windows.Forms.Button();
            this.btnSinewaveData = new System.Windows.Forms.Button();
            this.numUpDownPeriod = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerateFile
            // 
            this.btnGenerateFile.Location = new System.Drawing.Point(12, 12);
            this.btnGenerateFile.Name = "btnGenerateFile";
            this.btnGenerateFile.Size = new System.Drawing.Size(260, 23);
            this.btnGenerateFile.TabIndex = 0;
            this.btnGenerateFile.Text = "Generate Increment Data";
            this.btnGenerateFile.UseVisualStyleBackColor = true;
            this.btnGenerateFile.Click += new System.EventHandler(this.btnGenerateFile_Click);
            // 
            // btnRandomData
            // 
            this.btnRandomData.Location = new System.Drawing.Point(12, 41);
            this.btnRandomData.Name = "btnRandomData";
            this.btnRandomData.Size = new System.Drawing.Size(260, 23);
            this.btnRandomData.TabIndex = 1;
            this.btnRandomData.Text = "Generate Random Data";
            this.btnRandomData.UseVisualStyleBackColor = true;
            this.btnRandomData.Click += new System.EventHandler(this.btnRandomData_Click);
            // 
            // btnRunContinuousStream
            // 
            this.btnRunContinuousStream.Location = new System.Drawing.Point(12, 120);
            this.btnRunContinuousStream.Name = "btnRunContinuousStream";
            this.btnRunContinuousStream.Size = new System.Drawing.Size(260, 23);
            this.btnRunContinuousStream.TabIndex = 2;
            this.btnRunContinuousStream.Text = "Run Continuous Stream";
            this.btnRunContinuousStream.UseVisualStyleBackColor = true;
            this.btnRunContinuousStream.Click += new System.EventHandler(this.btnRunContinuousStream_Click);
            // 
            // btnStopContinuousStream
            // 
            this.btnStopContinuousStream.Location = new System.Drawing.Point(12, 149);
            this.btnStopContinuousStream.Name = "btnStopContinuousStream";
            this.btnStopContinuousStream.Size = new System.Drawing.Size(260, 23);
            this.btnStopContinuousStream.TabIndex = 3;
            this.btnStopContinuousStream.Text = "Stop Continuous Stream";
            this.btnStopContinuousStream.UseVisualStyleBackColor = true;
            this.btnStopContinuousStream.Click += new System.EventHandler(this.btnStopContinuousStream_Click);
            // 
            // btnSinewaveData
            // 
            this.btnSinewaveData.Location = new System.Drawing.Point(12, 70);
            this.btnSinewaveData.Name = "btnSinewaveData";
            this.btnSinewaveData.Size = new System.Drawing.Size(154, 23);
            this.btnSinewaveData.TabIndex = 4;
            this.btnSinewaveData.Text = "Generate Sinewave Data";
            this.btnSinewaveData.UseVisualStyleBackColor = true;
            this.btnSinewaveData.Click += new System.EventHandler(this.btnSinewaveData_Click);
            // 
            // numUpDownPeriod
            // 
            this.numUpDownPeriod.DecimalPlaces = 4;
            this.numUpDownPeriod.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numUpDownPeriod.Location = new System.Drawing.Point(172, 73);
            this.numUpDownPeriod.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numUpDownPeriod.Name = "numUpDownPeriod";
            this.numUpDownPeriod.Size = new System.Drawing.Size(75, 20);
            this.numUpDownPeriod.TabIndex = 5;
            this.numUpDownPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "sec";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 191);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numUpDownPeriod);
            this.Controls.Add(this.btnSinewaveData);
            this.Controls.Add(this.btnStopContinuousStream);
            this.Controls.Add(this.btnRunContinuousStream);
            this.Controls.Add(this.btnRandomData);
            this.Controls.Add(this.btnGenerateFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Kst File Generator";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateFile;
        private System.Windows.Forms.Button btnRandomData;
        private System.Windows.Forms.Button btnRunContinuousStream;
        private System.Windows.Forms.Button btnStopContinuousStream;
        private System.Windows.Forms.Button btnSinewaveData;
        private System.Windows.Forms.NumericUpDown numUpDownPeriod;
        private System.Windows.Forms.Label label1;
    }
}

