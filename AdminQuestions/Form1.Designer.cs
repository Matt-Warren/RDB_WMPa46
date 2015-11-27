﻿namespace AdminQuestions
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
            this.pEditQA = new System.Windows.Forms.Panel();
            this.btnQABack = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblQuestionList = new System.Windows.Forms.Label();
            this.cbAns_4 = new System.Windows.Forms.CheckBox();
            this.cbAns_3 = new System.Windows.Forms.CheckBox();
            this.cbAns_2 = new System.Windows.Forms.CheckBox();
            this.cbAns_1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAns4 = new System.Windows.Forms.TextBox();
            this.lblAns4 = new System.Windows.Forms.Label();
            this.txtAns3 = new System.Windows.Forms.TextBox();
            this.lblAns3 = new System.Windows.Forms.Label();
            this.txtAns2 = new System.Windows.Forms.TextBox();
            this.lblAns2 = new System.Windows.Forms.Label();
            this.txtAns1 = new System.Windows.Forms.TextBox();
            this.lblAns1 = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.lbQuestions = new System.Windows.Forms.ListBox();
            this.pLeaderboard = new System.Windows.Forms.Panel();
            this.btnLeaderboardBack = new System.Windows.Forms.Button();
            this.pStatus = new System.Windows.Forms.Panel();
            this.btnStatusBack = new System.Windows.Forms.Button();
            this.pEditQA.SuspendLayout();
            this.pLeaderboard.SuspendLayout();
            this.pStatus.SuspendLayout();
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
            this.btnEditQA.Click += new System.EventHandler(this.btnEditQA_Click);
            // 
            // btnStatus
            // 
            this.btnStatus.Location = new System.Drawing.Point(304, 13);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(274, 152);
            this.btnStatus.TabIndex = 1;
            this.btnStatus.Text = "View User Status";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // btnLeaderboard
            // 
            this.btnLeaderboard.Location = new System.Drawing.Point(13, 187);
            this.btnLeaderboard.Name = "btnLeaderboard";
            this.btnLeaderboard.Size = new System.Drawing.Size(274, 152);
            this.btnLeaderboard.TabIndex = 2;
            this.btnLeaderboard.Text = "View Leaderboard";
            this.btnLeaderboard.UseVisualStyleBackColor = true;
            this.btnLeaderboard.Click += new System.EventHandler(this.btnLeaderboard_Click);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Location = new System.Drawing.Point(306, 187);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(274, 152);
            this.btnExcelExport.TabIndex = 3;
            this.btnExcelExport.Text = "Create Excel Spreadsheet";
            this.btnExcelExport.UseVisualStyleBackColor = true;
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // pEditQA
            // 
            this.pEditQA.Controls.Add(this.btnQABack);
            this.pEditQA.Controls.Add(this.btnDelete);
            this.pEditQA.Controls.Add(this.lblQuestionList);
            this.pEditQA.Controls.Add(this.cbAns_4);
            this.pEditQA.Controls.Add(this.cbAns_3);
            this.pEditQA.Controls.Add(this.cbAns_2);
            this.pEditQA.Controls.Add(this.cbAns_1);
            this.pEditQA.Controls.Add(this.label1);
            this.pEditQA.Controls.Add(this.btnSave);
            this.pEditQA.Controls.Add(this.txtAns4);
            this.pEditQA.Controls.Add(this.lblAns4);
            this.pEditQA.Controls.Add(this.txtAns3);
            this.pEditQA.Controls.Add(this.lblAns3);
            this.pEditQA.Controls.Add(this.txtAns2);
            this.pEditQA.Controls.Add(this.lblAns2);
            this.pEditQA.Controls.Add(this.txtAns1);
            this.pEditQA.Controls.Add(this.lblAns1);
            this.pEditQA.Controls.Add(this.lblQuestion);
            this.pEditQA.Controls.Add(this.txtQuestion);
            this.pEditQA.Controls.Add(this.lbQuestions);
            this.pEditQA.Location = new System.Drawing.Point(12, 3);
            this.pEditQA.Name = "pEditQA";
            this.pEditQA.Size = new System.Drawing.Size(568, 406);
            this.pEditQA.TabIndex = 4;
            this.pEditQA.Visible = false;
            // 
            // btnQABack
            // 
            this.btnQABack.Location = new System.Drawing.Point(476, 3);
            this.btnQABack.Name = "btnQABack";
            this.btnQABack.Size = new System.Drawing.Size(87, 23);
            this.btnQABack.TabIndex = 11;
            this.btnQABack.Text = "Save and Quit";
            this.btnQABack.UseVisualStyleBackColor = true;
            this.btnQABack.Click += new System.EventHandler(this.btnQABack_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(316, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblQuestionList
            // 
            this.lblQuestionList.AutoSize = true;
            this.lblQuestionList.Location = new System.Drawing.Point(3, 10);
            this.lblQuestionList.Name = "lblQuestionList";
            this.lblQuestionList.Size = new System.Drawing.Size(71, 13);
            this.lblQuestionList.TabIndex = 18;
            this.lblQuestionList.Text = "Question List:";
            // 
            // cbAns_4
            // 
            this.cbAns_4.AutoSize = true;
            this.cbAns_4.Location = new System.Drawing.Point(497, 367);
            this.cbAns_4.Name = "cbAns_4";
            this.cbAns_4.Size = new System.Drawing.Size(15, 14);
            this.cbAns_4.TabIndex = 17;
            this.cbAns_4.UseVisualStyleBackColor = true;
            this.cbAns_4.Click += new System.EventHandler(this.cbClicked);
            // 
            // cbAns_3
            // 
            this.cbAns_3.AutoSize = true;
            this.cbAns_3.Location = new System.Drawing.Point(497, 299);
            this.cbAns_3.Name = "cbAns_3";
            this.cbAns_3.Size = new System.Drawing.Size(15, 14);
            this.cbAns_3.TabIndex = 16;
            this.cbAns_3.UseVisualStyleBackColor = true;
            this.cbAns_3.Click += new System.EventHandler(this.cbClicked);
            // 
            // cbAns_2
            // 
            this.cbAns_2.AutoSize = true;
            this.cbAns_2.Location = new System.Drawing.Point(497, 229);
            this.cbAns_2.Name = "cbAns_2";
            this.cbAns_2.Size = new System.Drawing.Size(15, 14);
            this.cbAns_2.TabIndex = 15;
            this.cbAns_2.UseVisualStyleBackColor = true;
            this.cbAns_2.Click += new System.EventHandler(this.cbClicked);
            // 
            // cbAns_1
            // 
            this.cbAns_1.AutoSize = true;
            this.cbAns_1.Location = new System.Drawing.Point(497, 161);
            this.cbAns_1.Name = "cbAns_1";
            this.cbAns_1.Size = new System.Drawing.Size(15, 14);
            this.cbAns_1.TabIndex = 14;
            this.cbAns_1.UseVisualStyleBackColor = true;
            this.cbAns_1.Click += new System.EventHandler(this.cbClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(485, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Correct Answer";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(397, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtAns4
            // 
            this.txtAns4.Location = new System.Drawing.Point(5, 350);
            this.txtAns4.Multiline = true;
            this.txtAns4.Name = "txtAns4";
            this.txtAns4.Size = new System.Drawing.Size(485, 44);
            this.txtAns4.TabIndex = 10;
            // 
            // lblAns4
            // 
            this.lblAns4.AutoSize = true;
            this.lblAns4.Location = new System.Drawing.Point(5, 333);
            this.lblAns4.Name = "lblAns4";
            this.lblAns4.Size = new System.Drawing.Size(54, 13);
            this.lblAns4.TabIndex = 9;
            this.lblAns4.Text = "Answer 4:";
            // 
            // txtAns3
            // 
            this.txtAns3.Location = new System.Drawing.Point(5, 283);
            this.txtAns3.Multiline = true;
            this.txtAns3.Name = "txtAns3";
            this.txtAns3.Size = new System.Drawing.Size(486, 44);
            this.txtAns3.TabIndex = 8;
            // 
            // lblAns3
            // 
            this.lblAns3.AutoSize = true;
            this.lblAns3.Location = new System.Drawing.Point(5, 266);
            this.lblAns3.Name = "lblAns3";
            this.lblAns3.Size = new System.Drawing.Size(54, 13);
            this.lblAns3.TabIndex = 7;
            this.lblAns3.Text = "Answer 3:";
            // 
            // txtAns2
            // 
            this.txtAns2.Location = new System.Drawing.Point(5, 212);
            this.txtAns2.Multiline = true;
            this.txtAns2.Name = "txtAns2";
            this.txtAns2.Size = new System.Drawing.Size(486, 44);
            this.txtAns2.TabIndex = 6;
            // 
            // lblAns2
            // 
            this.lblAns2.AutoSize = true;
            this.lblAns2.Location = new System.Drawing.Point(5, 195);
            this.lblAns2.Name = "lblAns2";
            this.lblAns2.Size = new System.Drawing.Size(54, 13);
            this.lblAns2.TabIndex = 5;
            this.lblAns2.Text = "Answer 2:";
            // 
            // txtAns1
            // 
            this.txtAns1.Location = new System.Drawing.Point(4, 148);
            this.txtAns1.Multiline = true;
            this.txtAns1.Name = "txtAns1";
            this.txtAns1.Size = new System.Drawing.Size(486, 44);
            this.txtAns1.TabIndex = 4;
            // 
            // lblAns1
            // 
            this.lblAns1.AutoSize = true;
            this.lblAns1.Location = new System.Drawing.Point(4, 131);
            this.lblAns1.Name = "lblAns1";
            this.lblAns1.Size = new System.Drawing.Size(54, 13);
            this.lblAns1.TabIndex = 3;
            this.lblAns1.Text = "Answer 1:";
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new System.Drawing.Point(126, 10);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(52, 13);
            this.lblQuestion.TabIndex = 2;
            this.lblQuestion.Text = "Question:";
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(129, 29);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(435, 95);
            this.txtQuestion.TabIndex = 1;
            // 
            // lbQuestions
            // 
            this.lbQuestions.FormattingEnabled = true;
            this.lbQuestions.Items.AddRange(new object[] {
            "Add a new question..."});
            this.lbQuestions.Location = new System.Drawing.Point(3, 29);
            this.lbQuestions.Name = "lbQuestions";
            this.lbQuestions.Size = new System.Drawing.Size(120, 95);
            this.lbQuestions.TabIndex = 0;
            this.lbQuestions.SelectedIndexChanged += new System.EventHandler(this.lbQuestions_SelectedIndexChanged);
            // 
            // pLeaderboard
            // 
            this.pLeaderboard.Controls.Add(this.btnLeaderboardBack);
            this.pLeaderboard.Location = new System.Drawing.Point(12, 3);
            this.pLeaderboard.Name = "pLeaderboard";
            this.pLeaderboard.Size = new System.Drawing.Size(568, 406);
            this.pLeaderboard.TabIndex = 20;
            this.pLeaderboard.Visible = false;
            // 
            // btnLeaderboardBack
            // 
            this.btnLeaderboardBack.Location = new System.Drawing.Point(488, 10);
            this.btnLeaderboardBack.Name = "btnLeaderboardBack";
            this.btnLeaderboardBack.Size = new System.Drawing.Size(75, 23);
            this.btnLeaderboardBack.TabIndex = 0;
            this.btnLeaderboardBack.Text = "Back";
            this.btnLeaderboardBack.UseVisualStyleBackColor = true;
            this.btnLeaderboardBack.Click += new System.EventHandler(this.btnLeaderboardBack_Click);
            // 
            // pStatus
            // 
            this.pStatus.Controls.Add(this.btnStatusBack);
            this.pStatus.Location = new System.Drawing.Point(12, 7);
            this.pStatus.Name = "pStatus";
            this.pStatus.Size = new System.Drawing.Size(568, 406);
            this.pStatus.TabIndex = 21;
            this.pStatus.Visible = false;
            // 
            // btnStatusBack
            // 
            this.btnStatusBack.Location = new System.Drawing.Point(488, 10);
            this.btnStatusBack.Name = "btnStatusBack";
            this.btnStatusBack.Size = new System.Drawing.Size(75, 23);
            this.btnStatusBack.TabIndex = 0;
            this.btnStatusBack.Text = "Back";
            this.btnStatusBack.UseVisualStyleBackColor = true;
            this.btnStatusBack.Click += new System.EventHandler(this.btnStatusBack_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 421);
            this.Controls.Add(this.pStatus);
            this.Controls.Add(this.pEditQA);
            this.Controls.Add(this.pLeaderboard);
            this.Controls.Add(this.btnExcelExport);
            this.Controls.Add(this.btnLeaderboard);
            this.Controls.Add(this.btnStatus);
            this.Controls.Add(this.btnEditQA);
            this.Name = "mainForm";
            this.Text = "Admin";
            this.pEditQA.ResumeLayout(false);
            this.pEditQA.PerformLayout();
            this.pLeaderboard.ResumeLayout(false);
            this.pStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEditQA;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Button btnLeaderboard;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.Panel pEditQA;
        private System.Windows.Forms.ListBox lbQuestions;
        private System.Windows.Forms.CheckBox cbAns_4;
        private System.Windows.Forms.CheckBox cbAns_3;
        private System.Windows.Forms.CheckBox cbAns_2;
        private System.Windows.Forms.CheckBox cbAns_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnQABack;
        private System.Windows.Forms.TextBox txtAns4;
        private System.Windows.Forms.Label lblAns4;
        private System.Windows.Forms.TextBox txtAns3;
        private System.Windows.Forms.Label lblAns3;
        private System.Windows.Forms.TextBox txtAns2;
        private System.Windows.Forms.Label lblAns2;
        private System.Windows.Forms.TextBox txtAns1;
        private System.Windows.Forms.Label lblAns1;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Label lblQuestionList;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pLeaderboard;
        private System.Windows.Forms.Button btnLeaderboardBack;
        private System.Windows.Forms.Panel pStatus;
        private System.Windows.Forms.Button btnStatusBack;
    }
}

