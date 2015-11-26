using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QA;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using ClientServerLibrary;

namespace AdminQuestions
{
    public partial class mainForm : Form
    {
        public QACombo currentQA = new QACombo();
        public List<QACombo> questionsList = new List<QACombo>();
        enum checkBoxes
        {
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

        private void btnEditQA_Click(object sender, EventArgs e)
        {
            //get the list of questions from the db here
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    questionsList = (List<QACombo>)formatter.Deserialize(stream);
                }
                catch (SerializationException se)
                {
                    MessageBox.Show(("Failed to deserialize. Reason: " + se.Message));
                }
            }
            lbQuestions.Items.Clear();
            lbQuestions.Items.Add("Add a new question...");
            int count = 0;
            foreach (QACombo qac in questionsList)
            {
                count++;
                lbQuestions.Items.Add(qac.question);
            }
            pEditQA.Visible = true;
        }

        private void btnQABack_Click(object sender, EventArgs e)
        {
            //save stuff to database
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(stream, questionsList);
                }
                catch (SerializationException se)
                {
                    MessageBox.Show(("Failed to serialize. Reason: " + se.Message));
                }
            }

            pEditQA.Visible = false;
        }

        private void uncheckOthers(checkBoxes boxNumber)
        {
            if (boxNumber != checkBoxes.ANS_ONE)
            {
                cbAns_1.Checked = false;
            }
            if (boxNumber != checkBoxes.ANS_TWO)
            {
                cbAns_2.Checked = false;
            }
            if (boxNumber != checkBoxes.ANS_THREE)
            {
                cbAns_3.Checked = false;
            }
            if (boxNumber != checkBoxes.ANS_FOUR)
            {
                cbAns_4.Checked = false;
            }

        }

        private void cbClicked(object sender, EventArgs e)
        {
            string[] temp = ((CheckBox)sender).Name.Split('_'); //get the number of the box checked
            if (!(cbAns_1.Checked | cbAns_2.Checked | cbAns_3.Checked | cbAns_4.Checked)) //if none of the boxes are checked, 
            {
                //btnSubmit.Enabled = false;
            }
            else
            {
                //btnSubmit.Enabled = true;
            }
            uncheckOthers((checkBoxes)Convert.ToInt32(temp[1])); //uncheck all boxes except for the one just checked
        }

        private void lbQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                string questionNumber = lbQuestions.SelectedItem.ToString();
                if (questionNumber == "Add a new question...")
                {
                    clearFields();
                }
                else
                {
                    currentQA = questionsList.ElementAt((lbQuestions.SelectedIndex)-1);
                    txtQuestion.Text = currentQA.question;
                    txtAns1.Text = currentQA.ans1;
                    txtAns2.Text = currentQA.ans2;
                    txtAns3.Text = currentQA.ans3;
                    txtAns4.Text = currentQA.ans4;
                    switch ((checkBoxes)currentQA.correctAnswer)
                    {
                        case checkBoxes.ANS_ONE:
                            cbAns_1.Checked = true;
                            break;
                        case checkBoxes.ANS_TWO:
                            cbAns_2.Checked = true;
                            break;
                        case checkBoxes.ANS_THREE:
                            cbAns_3.Checked = true;
                            break;
                        case checkBoxes.ANS_FOUR:
                            cbAns_4.Checked = true;
                            break;
                    }
                    uncheckOthers((checkBoxes)currentQA.correctAnswer);
                }
            }
            catch
            {
                //null string do nothing
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtQuestion.Text!="" & txtAns1.Text!="" & txtAns2.Text!="" & txtAns3.Text!="" & txtAns4.Text!="" & (cbAns_1.Checked | cbAns_2.Checked | cbAns_3.Checked | cbAns_4.Checked))
            { //if none of the fields are blank
                if (!txtQuestion.Text.Contains('|') & !txtAns1.Text.Contains('|') & !txtAns2.Text.Contains('|') & !txtAns3.Text.Contains('|') & !txtAns4.Text.Contains('|'))
                { //if none of the fields contain a pipe symbol
                    int qid = (lbQuestions.SelectedIndex == -1) ? 0 : lbQuestions.SelectedIndex;

                    if (qid == 0)
                    {
                        currentQA = new QACombo(txtQuestion.Text + "|" + txtAns1.Text + "|" + txtAns2.Text + "|" + txtAns3.Text + "|" + txtAns4.Text + "|" + (cbAns_1.Checked == true ? 1 : cbAns_2.Checked == true ? 2 : cbAns_3.Checked == true ? 3 : 4));
                        questionsList.Add(currentQA);
                        lbQuestions.Items.Add(currentQA.question);
                        
                    }
                    else
                    {
                        
                        currentQA = questionsList.ElementAt(qid-1);
                        currentQA.question = txtQuestion.Text;
                        currentQA.ans1 = txtAns1.Text;
                        currentQA.ans2 = txtAns2.Text;
                        currentQA.ans3 = txtAns3.Text;
                        currentQA.ans4 = txtAns4.Text;
                        currentQA.correctAnswer = (cbAns_1.Checked == true ? 1 : cbAns_2.Checked == true ? 2 : cbAns_3.Checked == true ? 3 : 4);

                        lbQuestions.Items.RemoveAt(qid);
                        lbQuestions.Items.Add(currentQA.question);

                        clearFields();
                    }
                    MessageBox.Show("Saved the question!");

                }
                else
                {
                    MessageBox.Show("Question not saved, invalid characters found in a text box. (No '|' allowed.)");
                }
            }
            else
            {
                MessageBox.Show("Question not saved. Must complete all fields.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try {
                string toDelete = lbQuestions.SelectedItem.ToString();
                if (toDelete != "Add a new question...")
                {
                    string[] number = toDelete.Split(' '); //number[1] will be deleted

                    DialogResult result = MessageBox.Show("Are you sure you want to delete question: '" + toDelete + "'?", "Delete?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int qid = lbQuestions.SelectedIndex;
                        questionsList.Remove(questionsList.ElementAt(qid-1));
                        lbQuestions.Items.RemoveAt(qid);
                        clearFields();

                        MessageBox.Show("Question was deleted.");

                    }
                    else
                    {
                        //don't delete it
                        MessageBox.Show("Question was not deleted.");
                    }
                }
            }
            catch
            {
                //tostring would have failed
            }
        }
        public void clearFields()
        {
            txtQuestion.Text = "";
            txtAns1.Text = "";
            txtAns2.Text = "";
            txtAns3.Text = "";
            txtAns4.Text = "";
            uncheckOthers(checkBoxes.ALL);
        }

        private void btnLeaderboard_Click(object sender, EventArgs e)
        {
            //request for leaderboard stuff here!
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(stream, questionsList);
                }
                catch (SerializationException se)
                {
                    MessageBox.Show(("Failed to serialize. Reason: " + se.Message));
                }
            }

            pLeaderboard.Visible = true;
            
            
        }

        private void btnLeaderboardBack_Click(object sender, EventArgs e)
        {
            pLeaderboard.Visible = false;
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {

        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //get list of questions and such from the server
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            try {
                stream.Seek(0, SeekOrigin.Begin);
                List<ExcelData> excelList = (List<ExcelData>)formatter.Deserialize(stream);





            }
            catch
            {

            }

        }
    }
}
