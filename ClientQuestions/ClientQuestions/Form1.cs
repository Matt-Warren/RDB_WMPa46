using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientQuestions
{
    public partial class mainForm : Form
    {
        public QACombo currentQA = new QACombo();

        enum checkBoxes{
            ANS_ONE = 1,
            ANS_TWO,
            ANS_THREE,
            ANS_FOUR,
            ALL
        }


        public mainForm()
        {
            InitializeComponent();
            
        }

        public void GetNextQuestion()
        {
            lblQuestionText.Text = "Getting new question...";
            tmrTimeLeft.Enabled = false;
            string newQuestion = "How much wood could a woodchuck chuck if a woodchuck could chuck wood?|A lot.|None.|A wee bit.|None of the above.|1";

            //get the next question here



            currentQA = new QACombo(newQuestion);
            lblTimeLeft.Text = "20";
            tmrTimeLeft.Enabled = true;
        }

        private void uncheckOthers(checkBoxes boxNumber)
        {
            if (boxNumber != checkBoxes.ANS_ONE)
            {
                cbAnswer_1.Checked = false;
            }
            if (boxNumber != checkBoxes.ANS_TWO)
            {
                cbAnswer_2.Checked = false;
            }
            if (boxNumber != checkBoxes.ANS_THREE)
            {
                cbAnswer_3.Checked = false;
            }
            if (boxNumber != checkBoxes.ANS_FOUR)
            {
                cbAnswer_4.Checked = false;
            }

        }
        


        private void setQAText()
        {
            if (currentQA.isSet)
            {
                lblQuestionText.Text = currentQA.question;
                cbAnswer_1.Text = currentQA.ans1;
                cbAnswer_2.Text = currentQA.ans2;
                cbAnswer_3.Text = currentQA.ans3;
                cbAnswer_4.Text = currentQA.ans4;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            bool correct = currentQA.checkAnswer(cbAnswer_1.Checked == true ? 1 : cbAnswer_2.Checked == true ? 2 : cbAnswer_3.Checked == true ? 3 : 4 );
            int timeLeft = Convert.ToInt32(lblTimeLeft.Text);

            btnSubmit.Enabled = false;
            uncheckOthers(checkBoxes.ALL);

            //send correct and timeLeft to the service to score points

            lblTimeLeft.Text = Convert.ToString(timeLeft);
            GetNextQuestion();
            setQAText();

            //send back to service here

        }

        private void cbClicked(object sender, EventArgs e)
        {
            string[] temp = ((CheckBox)sender).Name.Split('_'); //get the number of the box checked
            if (!(cbAnswer_1.Checked | cbAnswer_2.Checked | cbAnswer_3.Checked | cbAnswer_4.Checked)) //if none of the boxes are checked, 
            {
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
            }
            uncheckOthers((checkBoxes)Convert.ToInt32(temp[1])); //uncheck all boxes except for the one just checked
        }

        private void tmrTimeLeft_Tick(object sender, EventArgs e)
        {
            int timeLeft = Convert.ToInt32(lblTimeLeft.Text); //convert time to int
            if(timeLeft-1 >= 0)
            {
                timeLeft -= 1;
                lblTimeLeft.Text = Convert.ToString(timeLeft);
            }
            else
            {
                //get next question
                lblTimeLeft.Text = Convert.ToString(timeLeft);
                GetNextQuestion();
                setQAText();
            }
            
        }

        
    }
}
