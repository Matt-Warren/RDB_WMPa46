using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientServerLibrary;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Reflection;

namespace AdminQuestions
{
    public partial class mainForm : Form
    {
        public MemoryStream stream1 = new MemoryStream();

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
            //using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(stream1, questionsList);
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
            try
            {
                string questionNumber = lbQuestions.SelectedItem.ToString();
                if (questionNumber == "Add a new question...")
                {
                    clearFields();
                }
                else
                {
                    currentQA = questionsList.ElementAt((lbQuestions.SelectedIndex) - 1);
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
            if (txtQuestion.Text != "" & txtAns1.Text != "" & txtAns2.Text != "" & txtAns3.Text != "" & txtAns4.Text != "" & (cbAns_1.Checked | cbAns_2.Checked | cbAns_3.Checked | cbAns_4.Checked))
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

                        currentQA = questionsList.ElementAt(qid - 1);
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
            try
            {
                string toDelete = lbQuestions.SelectedItem.ToString();
                if (toDelete != "Add a new question...")
                {
                    string[] number = toDelete.Split(' '); //number[1] will be deleted

                    DialogResult result = MessageBox.Show("Are you sure you want to delete question: '" + toDelete + "'?", "Delete?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int qid = lbQuestions.SelectedIndex;
                        questionsList.Remove(questionsList.ElementAt(qid - 1));
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
            //get list of questions and such from the server *******************
            //MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            object missing = Type.Missing;
            Excel.Application oXL = null;
            Excel.Workbooks oWBs = null;
            Excel.Workbook oWB = null;
            Excel.Worksheet oSheet = null;
            Excel.Range oCells = null;
            Excel.Range oRng1 = null;
            Excel.Range oRng2 = null;

            try
            {
                //stream1.Seek(0, SeekOrigin.Begin);
                //List<ExcelData> excelList = (List<ExcelData>)formatter.Deserialize(stream1);
                List<ExcelData> excelList = new List<ExcelData>();
                excelList.Add(new ExcelData(1, "How does this look?", 13.2, 70.3));
                excelList.Add(new ExcelData(2, "Q2?", 2.2, 70.3));
                excelList.Add(new ExcelData(3, "q3?", 3.2, 70.3));
                excelList.Add(new ExcelData(4, "q44?", 13.2, 70.3));
                excelList.Add(new ExcelData(5, "q5!?", 19.3, 70.3));

                // Create an instance of Microsoft Excel and make it invisible. 
                oXL = new Excel.Application();
                oXL.Visible = false;

                // Create a new Workbook. 
                oWBs = oXL.Workbooks;
                oWB = oWBs.Add(missing);

                // Get the active Worksheet and set its name.
                oSheet = oWB.ActiveSheet as Excel.Worksheet;
                oSheet.Name = "Question Report";

                // Set the column headers
                oSheet.Cells.EntireColumn.ColumnWidth = 30;
                oCells = oSheet.Cells;
                oCells[1, 1] = "Question Number";
                oCells[1, 2] = "Question";
                oCells[1, 3] = "Average Completion Time";
                oCells[1, 4] = "Percent Correct Answers";


                int count = 1;
                foreach (ExcelData ed in excelList)
                {
                    count++;
                    oCells[count, 1] = ed.questionNumber;
                    oCells[count, 2] = ed.questionText;
                    oCells[count, 3] = ed.avgTime;
                    oCells[count, 4] = ed.percentCorrect;
                }
                

                Excel.Range chartRange;
                Excel.ChartObjects xlCharts = (Excel.ChartObjects)oSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(700, 10, 300, 250);
                Excel.Chart chartPage = myChart.Chart;

                chartRange = oSheet.get_Range("A1", ("C"+Convert.ToString(count-1)));
               
                chartPage.SetSourceData(chartRange, Excel.XlRowCol.xlColumns);
                
                chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
                chartPage.HasLegend = false;

                // Save the workbook as a xlsx file and close it. 


                string fileName = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location) + "\\QuestionReport.xlsx";
                oWB.SaveAs(fileName, Excel.XlFileFormat.xlOpenXMLWorkbook,
                    missing, missing, missing, missing,
                    Excel.XlSaveAsAccessMode.xlNoChange,
                    missing, missing, missing, missing, missing);
                oWB.Close(missing, missing, missing);

                // Quit the Excel application
                oXL.UserControl = true;

                oXL.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Solution1.AutomateExcel throws the error: {0}",
                    ex.Message);
            }
            finally
            {
                // Clean up the unmanaged Excel COM resources by explicitly  
                // calling Marshal.FinalReleaseComObject on all accessor objects.  
                // See http://support.microsoft.com/kb/317109. 

                if (oRng2 != null)
                {
                    Marshal.FinalReleaseComObject(oRng2);
                    oRng2 = null;
                }
                if (oRng1 != null)
                {
                    Marshal.FinalReleaseComObject(oRng1);
                    oRng1 = null;
                }
                if (oCells != null)
                {
                    Marshal.FinalReleaseComObject(oCells);
                    oCells = null;
                }
                if (oSheet != null)
                {
                    Marshal.FinalReleaseComObject(oSheet);
                    oSheet = null;
                }
                if (oWB != null)
                {
                    Marshal.FinalReleaseComObject(oWB);
                    oWB = null;
                }
                if (oWBs != null)
                {
                    Marshal.FinalReleaseComObject(oWBs);
                    oWBs = null;
                }
                if (oXL != null)
                {
                    Marshal.FinalReleaseComObject(oXL);
                    oXL = null;
                }
            }

        }
    }
}
