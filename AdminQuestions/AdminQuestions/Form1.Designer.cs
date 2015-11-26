namespace AdminQuestions
{
    partial class mainForm
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
            this.btnEditQA = new System.Windows.Forms.Button();
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnLeaderboard = new System.Windows.Forms.Button();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEditQA
            // 
            this.btnEditQA.Location = new System.Drawing.Point(13, 13);
            this.btnEditQA.Name = "btnEditQA";
            this.btnEditQA.Size = new System.Drawing.Size(274, 152);
            this.btnEditQA.TabIndex = 0;
            this.btnEditQA.Text = "Edit Questions/Answers";
            this.btnEditQA.UseVisualStyleBackColor = true;
            // 
            // btnStatus
            // 
            this.btnStatus.Location = new System.Drawing.Point(304, 13);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(274, 152);
            this.btnStatus.TabIndex = 1;
            this.btnStatus.Text = "View User Status";
            this.btnStatus.UseVisualStyleBackColor = true;
            // 
            // btnLeaderboard
            // 
            this.btnLeaderboard.Location = new System.Drawing.Point(13, 187);
            this.btnLeaderboard.Name = "btnLeaderboard";
            this.btnLeaderboard.Size = new System.Drawing.Size(274, 152);
            this.btnLeaderboard.TabIndex = 2;
            this.btnLeaderboard.Text = "View Leaderboard";
            this.btnLeaderboard.UseVisualStyleBackColor = true;
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Location = new System.Drawing.Point(306, 187);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(274, 152);
            this.btnExcelExport.TabIndex = 3;
            this.btnExcelExport.Text = "Create Excel Spreadsheet";
            this.btnExcelExport.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 354);
            this.Controls.Add(this.btnExcelExport);
            this.Controls.Add(this.btnLeaderboard);
            this.Controls.Add(this.btnStatus);
            this.Controls.Add(this.btnEditQA);
            this.Name = "mainForm";
            this.Text = "Admin";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEditQA;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Button btnLeaderboard;
        private System.Windows.Forms.Button btnExcelExport;
    }
}

