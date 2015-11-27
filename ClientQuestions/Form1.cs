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

namespace ClientQuestions
{
    public partial class mainForm : Form
    {
        public QACombo currentQA = new QACombo();
        public string name;
        public string server;
        public string portStr;
        public int questionNumber;

        NetworkStream stream;
        TcpClient client;
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

        private void connectToServer()
        {
            try
            {
                Int32 port = Convert.ToInt32(portStr);
                client = new TcpClient(server, port);

                Byte[] data = ObjectToByteArray(name);

                stream = client.GetStream();

                stream.Write(data, 0, data.Length);


                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;// = new byte[8192];
                
                // Loop to receive all the data sent by the client.
                stream.Read(bytes, 0, bytes.Length);
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromClient = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromClient.GetType();

                if (objType == typeof(string))
                { 
                    MessageBox.Show((string)objFromClient);
                }

                // Close everything.
                //stream.Close();
                //client.Close();
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

        public void GetNextQuestion()
        {
            lblQuestionText.Text = "Getting new question...";

            try //to get a new question
            {
                Int32 port = Convert.ToInt32(portStr);
                client = new TcpClient(server, port);

                Answer ans = new Answer();
                ans.answer = cbAnswer_1.Checked == true ? 1 : cbAnswer_2.Checked == true ? 2 : cbAnswer_3.Checked == true ? 3 : 4;
                ans.question = questionNumber;
                ans.timeLeft = Convert.ToInt32(lblTimeLeft.Text);

                Byte[] data = ObjectToByteArray(ans);

                stream = client.GetStream();

                stream.Write(data, 0, data.Length);


                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;// = new byte[8192];
                
                // Loop to receive all the data sent by the client.
                stream.Read(bytes, 0, bytes.Length);
                listObject.Add(bytes);

                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromClient = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromClient.GetType();

                if (objType == typeof(QACombo))
                {
                    currentQA.question = ((QACombo)objFromClient).question;
                    currentQA.ans1 = ((QACombo)objFromClient).ans1;
                    currentQA.ans2 = ((QACombo)objFromClient).ans2;
                    currentQA.ans3 = ((QACombo)objFromClient).ans3;
                    currentQA.ans4 = ((QACombo)objFromClient).ans4;
                    currentQA.correctAnswer = ((QACombo)objFromClient).correctAnswer;
                    setQAText();
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
            btnSubmit.Enabled = false;
            uncheckOthers(checkBoxes.ALL);
            tmrTimeLeft.Enabled = false;
            GetNextQuestion();

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
            }
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtPort.Text != "" && txtServer.Text != "" && txtUsername.Text != "")
            {
                portStr = txtPort.Text;
                server = txtServer.Text;
                name = txtUsername.Text;
                pStartScreen.Visible = false;
                connectToServer();
                GetNextQuestion();
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
