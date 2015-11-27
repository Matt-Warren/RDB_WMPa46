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
            this.pStartScreen = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.pEndQuestions = new System.Windows.Forms.Panel();
            this.pQuestions.SuspendLayout();
            this.pAnswers.SuspendLayout();
            this.pTimeLeft.SuspendLayout();
            this.pStartScreen.SuspendLayout();
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
            // pStartScreen
            // 
            this.pStartScreen.Controls.Add(this.btnStart);
            this.pStartScreen.Controls.Add(this.lblPort);
            this.pStartScreen.Controls.Add(this.lblServer);
            this.pStartScreen.Controls.Add(this.lblUsername);
            this.pStartScreen.Controls.Add(this.txtPort);
            this.pStartScreen.Controls.Add(this.txtServer);
            this.pStartScreen.Controls.Add(this.txtUsername);
            this.pStartScreen.Location = new System.Drawing.Point(3, 3);
            this.pStartScreen.Name = "pStartScreen";
            this.pStartScreen.Size = new System.Drawing.Size(511, 353);
            this.pStartScreen.TabIndex = 5;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(158, 112);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(218, 41);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "START!";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(155, 89);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port:";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(155, 63);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(41, 13);
            this.lblServer.TabIndex = 4;
            this.lblServer.Text = "Server:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(155, 37);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(38, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Name:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(246, 86);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(130, 20);
            this.txtPort.TabIndex = 2;
            this.txtPort.Text = "53512";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(246, 60);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(130, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "192.168.0.10";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(246, 34);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(130, 20);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Text = "ddd";
            // 
            // pEndQuestions
            // 
            this.pEndQuestions.Location = new System.Drawing.Point(4, 4);
            this.pEndQuestions.Name = "pEndQuestions";
            this.pEndQuestions.Size = new System.Drawing.Size(511, 353);
            this.pEndQuestions.TabIndex = 6;
            this.pEndQuestions.Visible = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 360);
            this.Controls.Add(this.pStartScreen);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.pTimeLeft);
            this.Controls.Add(this.pAnswers);
            this.Controls.Add(this.pQuestions);
            this.Controls.Add(this.pEndQuestions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "mainForm";
            this.Text = "Answer Questions!";
            this.pQuestions.ResumeLayout(false);
            this.pQuestions.PerformLayout();
            this.pAnswers.ResumeLayout(false);
            this.pAnswers.PerformLayout();
            this.pTimeLeft.ResumeLayout(false);
            this.pTimeLeft.PerformLayout();
            this.pStartScreen.ResumeLayout(false);
            this.pStartScreen.PerformLayout();
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
        private System.Windows.Forms.Panel pStartScreen;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel pEndQuestions;
    }
}

