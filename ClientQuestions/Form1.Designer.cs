namespace ClientQuestions
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
            this.components = new System.ComponentModel.Container();
            this.pQuestions = new System.Windows.Forms.Panel();
            this.lblQuestionText = new System.Windows.Forms.Label();
            this.pAnswers = new System.Windows.Forms.Panel();
            this.cbAnswer_4 = new System.Windows.Forms.CheckBox();
            this.cbAnswer_3 = new System.Windows.Forms.CheckBox();
            this.cbAnswer_2 = new System.Windows.Forms.CheckBox();
            this.cbAnswer_1 = new System.Windows.Forms.CheckBox();
            this.lblTimeLeft = new System.Windows.Forms.Label();
            this.pTimeLeft = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tmrTimeLeft = new System.Windows.Forms.Timer(this.components);
            this.pQuestions.SuspendLayout();
            this.pAnswers.SuspendLayout();
            this.pTimeLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pQuestions
            // 
            this.pQuestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pQuestions.Controls.Add(this.lblQuestionText);
            this.pQuestions.Location = new System.Drawing.Point(13, 13);
            this.pQuestions.Name = "pQuestions";
            this.pQuestions.Size = new System.Drawing.Size(423, 126);
            this.pQuestions.TabIndex = 0;
            // 
            // lblQuestionText
            // 
            this.lblQuestionText.AutoSize = true;
            this.lblQuestionText.Location = new System.Drawing.Point(4, 4);
            this.lblQuestionText.Name = "lblQuestionText";
            this.lblQuestionText.Size = new System.Drawing.Size(0, 13);
            this.lblQuestionText.TabIndex = 0;
            // 
            // pAnswers
            // 
            this.pAnswers.Controls.Add(this.cbAnswer_4);
            this.pAnswers.Controls.Add(this.cbAnswer_3);
            this.pAnswers.Controls.Add(this.cbAnswer_2);
            this.pAnswers.Controls.Add(this.cbAnswer_1);
            this.pAnswers.Location = new System.Drawing.Point(13, 172);
            this.pAnswers.Name = "pAnswers";
            this.pAnswers.Size = new System.Drawing.Size(493, 176);
            this.pAnswers.TabIndex = 1;
            // 
            // cbAnswer_4
            // 
            this.cbAnswer_4.AutoSize = true;
            this.cbAnswer_4.Location = new System.Drawing.Point(8, 126);
            this.cbAnswer_4.Name = "cbAnswer_4";
            this.cbAnswer_4.Size = new System.Drawing.Size(15, 14);
            this.cbAnswer_4.TabIndex = 3;
            this.cbAnswer_4.UseVisualStyleBackColor = true;
            this.cbAnswer_4.Click += new System.EventHandler(this.cbClicked);
            // 
            // cbAnswer_3
            // 
            this.cbAnswer_3.AutoSize = true;
            this.cbAnswer_3.Location = new System.Drawing.Point(8, 83);
            this.cbAnswer_3.Name = "cbAnswer_3";
            this.cbAnswer_3.Size = new System.Drawing.Size(15, 14);
            this.cbAnswer_3.TabIndex = 2;
            this.cbAnswer_3.UseVisualStyleBackColor = true;
            this.cbAnswer_3.Click += new System.EventHandler(this.cbClicked);
            // 
            // cbAnswer_2
            // 
            this.cbAnswer_2.AutoSize = true;
            this.cbAnswer_2.Location = new System.Drawing.Point(8, 43);
            this.cbAnswer_2.Name = "cbAnswer_2";
            this.cbAnswer_2.Size = new System.Drawing.Size(15, 14);
            this.cbAnswer_2.TabIndex = 1;
            this.cbAnswer_2.UseVisualStyleBackColor = true;
            this.cbAnswer_2.Click += new System.EventHandler(this.cbClicked);
            // 
            // cbAnswer_1
            // 
            this.cbAnswer_1.AutoSize = true;
            this.cbAnswer_1.Location = new System.Drawing.Point(8, 3);
            this.cbAnswer_1.Name = "cbAnswer_1";
            this.cbAnswer_1.Size = new System.Drawing.Size(15, 14);
            this.cbAnswer_1.TabIndex = 0;
            this.cbAnswer_1.UseVisualStyleBackColor = true;
            this.cbAnswer_1.Click += new System.EventHandler(this.cbClicked);
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.AutoSize = true;
            this.lblTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeLeft.Location = new System.Drawing.Point(13, 8);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(36, 26);
            this.lblTimeLeft.TabIndex = 2;
            this.lblTimeLeft.Text = "20";
            // 
            // pTimeLeft
            // 
            this.pTimeLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTimeLeft.Controls.Add(this.lblTimeLeft);
            this.pTimeLeft.Location = new System.Drawing.Point(442, 12);
            this.pTimeLeft.Name = "pTimeLeft";
            this.pTimeLeft.Size = new System.Drawing.Size(64, 45);
            this.pTimeLeft.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(13, 145);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(423, 24);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit Answer";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // tmrTimeLeft
            // 
            this.tmrTimeLeft.Enabled = true;
            this.tmrTimeLeft.Interval = 1000;
            this.tmrTimeLeft.Tick += new System.EventHandler(this.tmrTimeLeft_Tick);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 360);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.pTimeLeft);
            this.Controls.Add(this.pAnswers);
            this.Controls.Add(this.pQuestions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "mainForm";
            this.Text = "Answer Questions!";
            this.pQuestions.ResumeLayout(false);
            this.pQuestions.PerformLayout();
            this.pAnswers.ResumeLayout(false);
            this.pAnswers.PerformLayout();
            this.pTimeLeft.ResumeLayout(false);
            this.pTimeLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pQuestions;
        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.Panel pAnswers;
        private System.Windows.Forms.CheckBox cbAnswer_4;
        private System.Windows.Forms.CheckBox cbAnswer_3;
        private System.Windows.Forms.CheckBox cbAnswer_2;
        private System.Windows.Forms.CheckBox cbAnswer_1;
        private System.Windows.Forms.Label lblTimeLeft;
        private System.Windows.Forms.Panel pTimeLeft;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Timer tmrTimeLeft;
    }
}

