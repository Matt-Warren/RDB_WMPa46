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
using System.Net.Sockets;

namespace AdminQuestions
{
    public partial class mainForm : Form
    {

        public string name;
        public string serverIP;
        public string portStr;
        NetworkStream stream;
        TcpClient server;

        // The current question and answer combo
        public QACombo currentQA = new QACombo();

        // The list of all the questions in the database
        public List<QACombo> questionsList = new List<QACombo>();
        List<Leaderboard> currLeaderList = new List<Leaderboard>();
        List<CurrentStatus> currStatList = new List<CurrentStatus>();

        // Enumeration of the possible check boxes for question answers
        enum checkBoxes
        {
            ANS_ONE = 1,
            ANS_TWO,
            ANS_THREE,
            ANS_FOUR,
            ALL
        }

        // Initializes a new instance of the form
        public mainForm()
        {
            InitializeComponent();
        }

        private void connectToServer()
        {
            try
            {
                Int32 port = Convert.ToInt32(portStr);
                server = new TcpClient(serverIP, port);

                Byte[] data = ObjectToByteArray(name);

                stream = server.GetStream();

                stream.Write(data, 0, data.Length);


                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;

                // Loop to receive all the data sent by the client.
                stream.Read(bytes, 0, bytes.Length);
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromServer = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromServer.GetType();

                if (objType == typeof(string))
                {
                    MessageBox.Show((string)objFromServer);
                }
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show("ArgumentNullException: " + ae.Message);
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException:" + se.Message);
            }
        }

        /// <summary>
        /// Gets the lowest question number.
        /// </summary>
        /// <returns></returns>
        public int GetLowestQNumber()
        {
            int lowQNumber = 0;
            int index = 0;
            int numberOfQuestions = questionsList.Count;
            int[] usedNumbers = new int[numberOfQuestions];
            bool valid = false;
            foreach(QACombo qac in questionsList)
            {
                usedNumbers[index] = qac.questionNum;
                index++;
            }

            for(int x = 0; x<numberOfQuestions; x++)
            {
                for(int y = 0; y<numberOfQuestions; y++)
                {
                    if(x == usedNumbers[y])
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        valid = true;
                    }
                    
                }
                if (valid)
                {
                    lowQNumber = x;
                    break;
                }
                
            }
            if (!valid)
            {
                lowQNumber = numberOfQuestions;
            }
            return lowQNumber;
        }

        /// <summary>
        /// Handles the Click event of the btnEditQA control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEditQA_Click(object sender, EventArgs e)
        { //Edit Questions/Answers menu selected

            ///////////////////////////////////////////////////////////get the list of questions from the db here
            /////////////////////////stream isn't set yet, use that

            try
            {

                Byte[] data = ObjectToByteArray(new List<QACombo>());

                stream = server.GetStream();

                stream.Write(data, 0, data.Length);


                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;

                // Loop to receive all the data sent by the client.
                stream.Read(bytes, 0, bytes.Length);
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;

                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromServer = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromServer.GetType();

                if (objType == typeof(List<QACombo>))
                {
                    questionsList = (List<QACombo>)objFromServer;
                }
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show("ArgumentNullException: " + ae.Message);
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException: " +  se.Message);
            }

            lbQuestions.Items.Clear(); //remove all items each time
            lbQuestions.Items.Add("Add a new question..."); //set the first thing in the listbox to this to allow the user to reset fields

            foreach (QACombo qac in questionsList)
            {
                lbQuestions.Items.Add(qac.question); //add the list of questions to the listbox
            }
            pEditQA.Visible = true; //show the edit qa panel
            pEditQA.BringToFront();
        }

        /// <summary>
        /// Handles the Click event of the btnQABack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnQABack_Click(object sender, EventArgs e)
        {
            try //to send the QACombo list back to the server
            { 

                Byte[] data = ObjectToByteArray(questionsList);

                stream = server.GetStream();

                stream.Write(data, 0, data.Length);

            }
            catch(ArgumentNullException ae)
            {
                MessageBox.Show("ArgumentNullException: " + ae.Message);
            }
            catch(SocketException se)
            {
                MessageBox.Show("SocketException: " + se.Message);
            }

            pEditQA.Visible = false;
        }

        /// <summary>
        /// Uncheck checkboxes
        /// </summary>
        /// <param name="boxNumber">The box number to leave on.</param>
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

        /// <summary>
        /// Click event for check boxes
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lbQuestions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lbQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbQuestions.SelectedItem != null) { 
                string questionText = lbQuestions.SelectedItem.ToString(); //gets the text of the question
                if (questionText == "Add a new question...")
                {
                    clearFields(); //clears all fields so a new question can be made
                }
                else
                {
                    currentQA = questionsList.ElementAt((lbQuestions.SelectedIndex) - 1); //get the question from the question list
                    txtQuestion.Text = currentQA.question; //set all the text
                    txtAns1.Text = currentQA.ans1;
                    txtAns2.Text = currentQA.ans2;
                    txtAns3.Text = currentQA.ans3;
                    txtAns4.Text = currentQA.ans4;
                    switch ((checkBoxes)currentQA.correctAnswer)
                    { //set the checkbox
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

        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtQuestion.Text != "" & txtAns1.Text != "" & txtAns2.Text != "" & txtAns3.Text != "" & txtAns4.Text != "" & (cbAns_1.Checked | cbAns_2.Checked | cbAns_3.Checked | cbAns_4.Checked))
            { //if none of the fields are blank
                if (!txtQuestion.Text.Contains('|') & !txtAns1.Text.Contains('|') & !txtAns2.Text.Contains('|') & !txtAns3.Text.Contains('|') & !txtAns4.Text.Contains('|'))
                { //if none of the fields contain a pipe symbol
                    int qid = (lbQuestions.SelectedIndex == -1) ? 0 : lbQuestions.SelectedIndex; //if the question id is -1, sets to 0

                    if (qid == 0)
                    { //make a new question
                        currentQA = new QACombo(Convert.ToString(GetLowestQNumber()) + "|" + txtQuestion.Text + "|" + txtAns1.Text + "|" + txtAns2.Text + "|" + txtAns3.Text + "|" + txtAns4.Text + "|" + (cbAns_1.Checked == true ? 1 : cbAns_2.Checked == true ? 2 : cbAns_3.Checked == true ? 3 : 4));
                        questionsList.Add(currentQA);
                        lbQuestions.Items.Add(currentQA.question);

                    }
                    else
                    { //edit an older question

                        currentQA = questionsList.ElementAt(qid - 1);
                        currentQA.question = txtQuestion.Text;
                        currentQA.ans1 = txtAns1.Text;
                        currentQA.ans2 = txtAns2.Text;
                        currentQA.ans3 = txtAns3.Text;
                        currentQA.ans4 = txtAns4.Text;
                        currentQA.correctAnswer = (cbAns_1.Checked == true ? 1 : cbAns_2.Checked == true ? 2 : cbAns_3.Checked == true ? 3 : 4);

                        lbQuestions.Items.RemoveAt(qid); //remove the older question
                        lbQuestions.Items.Add(currentQA.question); //add the new question

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

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbQuestions.SelectedItem != null) { 
                string toDelete = lbQuestions.SelectedItem.ToString(); //get the question to delete
                if (toDelete != "Add a new question...")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete question: '" + toDelete + "'?", "Delete?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int qid = lbQuestions.SelectedIndex; 
                        questionsList.RemoveAt(qid - 1); //remove from list
                        lbQuestions.Items.RemoveAt(qid); //remove from listbox
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
        }

        /// <summary>
        /// Clears all the fields.
        /// </summary>
        public void clearFields()
        {
            txtQuestion.Text = "";
            txtAns1.Text = "";
            txtAns2.Text = "";
            txtAns3.Text = "";
            txtAns4.Text = "";
            uncheckOthers(checkBoxes.ALL);
        }

        /// <summary>
        /// Handles the Click event of the btnLeaderboard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnLeaderboard_Click(object sender, EventArgs e)
        {
            
            try
            {


                Byte[] data = ObjectToByteArray(new Leaderboard());

                stream = server.GetStream();

                stream.Write(data, 0, data.Length);

                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;

                // Loop to receive all the data sent by the client.
                stream.Read(bytes, 0, bytes.Length);
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromServer = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromServer.GetType();

                if (objType == typeof(List<Leaderboard>))
                {
                    currLeaderList = (List<Leaderboard>)objFromServer;
                }
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show("ArgumentNullException: " + ae.Message);
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException: " + se.Message);
            }
            dgLeader.Rows.Clear();
            foreach (Leaderboard current in currLeaderList)
            {
                dgLeader.Rows.Add(current.name, current.score); //sets up the datagrid
            }


            pLeaderboard.Visible = true;
            pLeaderboard.BringToFront();


        }

        public void UpdateStatus(object sender, EventArgs e)
        {
            try
            {

                Byte[] data = ObjectToByteArray(new CurrentStatus());

                stream = server.GetStream();

                stream.Write(data, 0, data.Length);

                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;

                // Loop to receive all the data sent by the client.
                stream.Read(bytes, 0, bytes.Length);
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromServer = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromServer.GetType();

                if (objType == typeof(List<CurrentStatus>))
                {
                    currStatList = (List<CurrentStatus>)objFromServer;
                }
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show("ArgumentNullException: " + ae.Message);
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException: " + se.Message);
            }
            dgStatus.Rows.Clear();
            foreach (CurrentStatus current in currStatList)
            {
                dgStatus.Rows.Add(current.name, current.questionNum, current.score); //sets up the datagrid
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLeaderboardBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnLeaderboardBack_Click(object sender, EventArgs e)
        {
            tmrRefreshStatus.Enabled = false;
            pLeaderboard.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the btnStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStatus_Click(object sender, EventArgs e)
        {
            tmrRefreshStatus.Enabled = true;
            UpdateStatus(sender, e);

           
            pStatus.Visible = true;
            pStatus.BringToFront();
        }

        /// <summary>
        /// Handles the Click event of the btnExcelExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            List<ExcelData> excelList = new List<ExcelData>(); //store the list of excel data in here
            try
            {

                Byte[] data = ObjectToByteArray(new ExcelData());

                stream = server.GetStream();

                stream.Write(data, 0, data.Length);

                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;

                // Loop to receive all the data sent by the client.
                stream.Read(bytes, 0, bytes.Length);
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromServer = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromServer.GetType();

                if (objType == typeof(List<ExcelData>))
                {
                    excelList = (List<ExcelData>)objFromServer;
                }
            }
            catch(ArgumentNullException ae)
            {
                MessageBox.Show("ArgumentNullException: ", ae.Message);
            }
            catch(SocketException se)
            {
                MessageBox.Show("SocketException: ", se.Message);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            
            object missing = Type.Missing;
            Excel.Application oXL = null;
            Excel.Workbooks oWBs = null;
            Excel.Workbook oWB = null;
            Excel.Worksheet oSheet = null;
            Excel.Range oCells = null;
            Excel.Range chartRange = null; //range for the chart
            Excel.ChartObjects xlCharts = null;
            Excel.ChartObject myChart = null;
            Excel.Chart chartPage = null;

            try
            {
                
                

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
                { //set data for the cells
                    count++;
                    oCells[count, 1] = ed.questionNumber;
                    oCells[count, 2] = ed.questionText;
                    oCells[count, 3] = ed.avgTime;
                    oCells[count, 4] = ed.percentCorrect;
                }
                
                
                xlCharts = (Excel.ChartObjects)oSheet.ChartObjects(Type.Missing); //create the chart
                myChart = (Excel.ChartObject)xlCharts.Add(700, 10, 300, 250); //where to make the chart
                chartPage = myChart.Chart; 

                chartRange = oSheet.get_Range("A1", ("C"+Convert.ToString(count-1))); //set the range for the chart
               
                chartPage.SetSourceData(chartRange, Excel.XlRowCol.xlColumns); //set source data for chart
                
                chartPage.ChartType = Excel.XlChartType.xlColumnClustered; //set type
                chartPage.HasLegend = false; //no legend

                // Save the workbook as a xlsx file and close it
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Clean up the unmanaged Excel COM resources 
                if(chartRange != null)
                {
                    Marshal.FinalReleaseComObject(chartRange);
                    chartRange = null;
                }

                if(xlCharts != null)
                {
                    Marshal.FinalReleaseComObject(xlCharts);
                    xlCharts = null;
                }
                if(myChart = null)
                {
                    Marshal.FinalReleaseComObject(myChart);
                    myChart = null;
                }
                if(chartPage = null)
                {
                    Marshal.FinalReleaseComObject(chartPage);
                    chartPage = null;
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

        /// <summary>
        /// Handles the Click event of the btnStatusBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStatusBack_Click(object sender, EventArgs e)
        {
            tmrRefreshStatus.Enabled = false;
            pStatus.Visible = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtPort.Text != "" && txtServer.Text != "")
            {
                portStr = txtPort.Text;
                serverIP = txtServer.Text;
                name = "admin";
                pStartScreen.Visible = false;
                connectToServer();
            }
        }

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
