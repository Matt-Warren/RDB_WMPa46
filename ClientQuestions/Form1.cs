/*
*   FILE: Form1.cs (ClientQuestions)
*   NAME: Steven Johnston & Matt Warren
*   DATE: 2015/11/27
*   DESC: This file is used connect with the service from the client side, with the utilities outlined in the assignment description.
*/
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
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace ClientQuestions
{

    public partial class mainForm : Form
    {
        // current question and answer
        public QACombo currentQA = new QACombo();
        // username
        public string name;
        // serverIP
        public string server;
        //port
        public string portStr;
        
        public int questionNumber;

        //stream for server
        NetworkStream stream;
        
        TcpClient host;
        //enumeration of the checkboxes
        enum checkBoxes
        {
            ANS_ONE = 1,
            ANS_TWO,
            ANS_THREE,
            ANS_FOUR,
            ALL
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="mainForm"/> class.
        /// </summary>
        public mainForm()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Connects to server.
        /// </summary>
        private void connectToServer()
        {
            try
            {
                Int32 port = Convert.ToInt32(portStr);
                host = new TcpClient(server, port);

                Byte[] data = ObjectToByteArray(name);

                stream = host.GetStream(); 

                stream.Write(data, 0, data.Length); //send serialized string (empty) to server


                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;
                
                stream.Read(bytes, 0, bytes.Length); //get stuff from server
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromClient = bformatter.Deserialize(fullObjectStream); //gets object, deserializes
                Type objType = objFromClient.GetType();

                if (objType == typeof(string)) //makes sure its a string
                { 
                    MessageBox.Show((string)objFromClient); //show connected
                }

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            

        }

        /// <summary>
        /// Gets the next question.
        /// </summary>
        public void GetNextQuestion()
        {
            lblQuestionText.Text = "Getting new question...";

            try //to get a new question
            {

                tmrTimeLeft.Enabled = false;
                //Int32 port = Convert.ToInt32(portStr);
                //client = new TcpClient(server, port);
                Answer ans = new Answer();
                ans.answer = cbAnswer_1.Checked == true ? 1 : cbAnswer_2.Checked == true ? 2 : cbAnswer_3.Checked == true ? 3 : 4;
                ans.question = questionNumber;
                ans.timeLeft = Convert.ToInt32(lblTimeLeft.Text);

                Byte[] data = ObjectToByteArray(ans);

                stream = host.GetStream();

                stream.Write(data, 0, data.Length); //send our answer


                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;

                stream.Read(bytes, 0, bytes.Length); //get a new question or the end of game
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromServer = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromServer.GetType();

                if (objType == typeof(QACombo)) //if new question
                {

                    currentQA = (QACombo)objFromServer;
                    setQAText();
                    tmrTimeLeft.Enabled = true;
                }
                else if (objType == typeof(List<Result>)) //if results(end of game)
                {

                    pAnswers.Visible = false;
                    pQuestions.Visible = false;
                    pEndQuestions.Visible = true;
                    pTimeLeft.Visible = false;
                    pStartScreen.Visible = false;
                    btnSubmit.Visible = false;


                    List<Result> results = (List<Result>)objFromServer;

                    foreach (var result in results)
                    { //print results
                        txtResults.Text += "\r\n#" + result.questionNumber + "\r\nQuestion: " + result.question + "\r\n Your answer: " + result.theirAnswer + "\r\n Correct answer: " + result.actualAnswer;
                    }
                    data = ObjectToByteArray(new Leaderboard());
                    stream.Write(data, 0, data.Length); //send blank leaderboard

                    stream.Read(bytes, 0, bytes.Length);  //receive leaderboard with data
                    listObject.Add(bytes);

                    fullObjectBytes = bytes;

                    fullObjectStream = new MemoryStream(fullObjectBytes);
                    objFromServer = bformatter.Deserialize(fullObjectStream);
                    objType = objFromServer.GetType();

                    txtResults.Clear();
                    List<Leaderboard> leaderboards = (List<Leaderboard>)objFromServer;
                    int count = 1;
                    foreach (var leaderboard in leaderboards)
                    { //print leaderboard
                        txtResults.Text += "#" + count++ + " " + leaderboard.name + " " + leaderboard.score + "\r\n";
                    }
                }
            }
            catch (ArgumentNullException ex)
            {

                MessageBox.Show("InvalidOperationException: {0}", ex.Message);
                Console.WriteLine("ArgumentNullException: {0}", ex);
                stream.Close();
                this.Close();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("InvalidOperationException: {0}", ex.Message);
                Console.WriteLine("SocketException: {0}", ex);
                stream.Close();
                this.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("InvalidOperationException: {0}", ex.Message);
                Console.WriteLine("IOException: {0}", ex);
                stream.Close();
                this.Close();
            }
            catch (SerializationException ex)
            {
                MessageBox.Show("InvalidOperationException: {0}", ex.Message);
                Console.WriteLine("SerializationException: {0}", ex);
                stream.Close();
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("InvalidOperationException: {0}", ex.Message);
                Console.WriteLine("InvalidOperationException: {0}", ex);
                stream.Close();
                this.Close();
            }

            lblTimeLeft.Text = "20";
            tmrTimeLeft.Enabled = true;
        }

        /// <summary>
        /// Unchecks the others.
        /// </summary>
        /// <param name="boxNumber">The box number.</param>
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
        


        //sets question/answer texts
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

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            tmrTimeLeft.Enabled = false;
            GetNextQuestion();

            uncheckOthers(checkBoxes.ALL);
        }

        /// <summary>
        /// Checkbox click event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Tick event of the tmrTimeLeft control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
            }
            
        }

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtPort.Text != "" && txtServer.Text != "" && txtUsername.Text != "")
            {
                portStr = txtPort.Text;
                server = txtServer.Text;
                name = txtUsername.Text;
                pStartScreen.Visible = false;
                connectToServer();
                tmrTimeLeft.Enabled = true;
                GetNextQuestion();
            }
        }

        /// <summary>
        /// Objects to byte array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLeader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnLeader_Click(object sender, EventArgs e)
        {
            try {
                if (btnLeader.Text == "Close")
                {
                    this.Close();
                }
                else
                {
                    List<byte[]> listObject = new List<byte[]>();
                    byte[] bytes = new byte[8192];
                    byte[] fullObjectBytes;// = new byte[8192];
                    var bformatter = new BinaryFormatter();
                    fullObjectBytes = bytes;

                    byte[] data = ObjectToByteArray(new Leaderboard());
                    stream.Write(data, 0, data.Length);




                    stream.Read(bytes, 0, bytes.Length);
                    Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                    object objFromServer = bformatter.Deserialize(fullObjectStream);
                    Type objType = objFromServer.GetType();
                    fullObjectBytes = bytes;

                    fullObjectStream = new MemoryStream(fullObjectBytes);
                    objFromServer = bformatter.Deserialize(fullObjectStream);
                    objType = objFromServer.GetType();

                    txtResults.Clear();
                    List<Leaderboard> leaderboards = (List<Leaderboard>)objFromServer;
                    int count = 1;
                    foreach (var leaderboard in leaderboards)
                    {
                        txtResults.Text += "#" + count++ + " " + leaderboard.name + " " + leaderboard.score + "\r\n";
                    }

                    stream.Close();
                    btnLeader.Text = "Close";
                }
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show("ArgumentNullException: " + ae.Message);
                stream.Close();
                this.Close();
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException: " + se.Message);
                stream.Close();
                this.Close();
            }
        }
    }
}
