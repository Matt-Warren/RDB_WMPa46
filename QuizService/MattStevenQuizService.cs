using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using ClientServerLibrary;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using MySql.Data.MySqlClient;

namespace QuizService
{
    public partial class MattStevenQuizService : ServiceBase
    {
        class ClientConnections
        {
            public TcpClient cSocket;
            public MyTimer rTimer;
            public int question;
            public int score;
            public string name;
            public List<Result> results;
        }
        EventLog eventLogger;
        List<ClientConnections> connections = new List<ClientConnections>();
        DB db = new DB();
        public MattStevenQuizService()
        {
            InitializeComponent();

        }
        public void start()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            eventLogger = new System.Diagnostics.EventLog();
            // Turn off autologging
            this.AutoLog = false;
            // create an event source, specifying the name of a log that
            // does not currently exist to create a new, custom log
            if (!System.Diagnostics.EventLog.SourceExists("QuizSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "QuizSource", "QuizLog");
            }
            // configure the event log instance to use this source name
            eventLogger.Source = "QuizSource";
            eventLogger.Log = "QuizLog";

            eventLogger.WriteEntry("go");

            MyTimer listenThread = new MyTimer(0, listen, new object());
            
        }

        protected override void OnStop()
        {
        }
        private void readSocket(object obj)
        {
            ClientConnections connection = obj as ClientConnections;
            try
            {
                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;// = new byte[8192];
                // Get a stream object for reading and writing
                NetworkStream stream = connection.cSocket.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                //{
                if (connection.cSocket.Connected)
                {
                    stream.Read(bytes, 0, bytes.Length);
                }
                else
                {
                    connection.rTimer.stop();
                }
                // Translate data bytes to a ASCII string.
                listObject.Add(bytes);
                //data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                //Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                //data = data.ToUpper();

                //byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                // Send back a response.
                //stream.Write(msg, 0, msg.Length);
                //Console.WriteLine("Sent: {0}", data);
                //}
                var bformatter = new BinaryFormatter();
                //fullObjectBytes = listObject.ToArray().Cast<Byte>().ToArray();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromClient = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromClient.GetType();

                byte[] objectOut = new byte[0];

                if (objType == typeof(String))//client sent name
                {
                    connection.name = ((string)objFromClient);
                    objectOut = ObjectToByteArray("Connected");
                }
                else if (objType == typeof(Answer))//client gives you an answer (give them next question or their results)
                {
                    Answer userAnswer = (Answer)objFromClient;

                    if (connection.question != -1)//first question
                    {
                        List<string> queryRow = db.Select("Select questionNum,question,ans1,ans2,ans3,ans4,correctAnswer from questions where questionNum = " + connection.question).FirstOrDefault();

                        QACombo QARow = new QACombo(String.Join("|", queryRow));

                        if (userAnswer.answer == QARow.correctAnswer)
                        {
                            connection.score += userAnswer.timeLeft;
                        }
                        else
                        {
                            userAnswer.timeLeft = 0;
                        }
                        connection.results.Add(new Result(QARow.questionNum + "|" + QARow.question + "|" + queryRow[1 + QARow.correctAnswer] + "|" + queryRow[1 + userAnswer.answer]));
                        db.Insert("INSERT INTO questionattempts (questionId,timeLeft)VALUES(" + QARow.questionNum + "," + userAnswer.timeLeft + ")");
                    }
                    List<string> nextQuestion = db.Select("Select questionNum,question,ans1,ans2,ans3,ans4,correctAnswer from questions where questionNum > " + connection.question + " Order By questionNum ASC").FirstOrDefault();
                    if (nextQuestion != null)
                    {
                        QACombo nextQ = new QACombo(String.Join("|", nextQuestion));
                        objectOut = ObjectToByteArray(nextQ);

                        connection.question = nextQ.questionNum;
                    }
                    else
                    {
                        db.Insert("INSERT INTO leaderboard (name,score)VALUES('" + connection.name + "'," + connection.score + ")");
                        objectOut = ObjectToByteArray(connection.results);
                    }
                }
                else if (objType == typeof(CurrentStatus))
                {
                    List<CurrentStatus> statusList = new List<CurrentStatus>();
                    foreach (var con in connections)
                    {
                        CurrentStatus newStatus;
                        newStatus.name = con.name;
                        newStatus.score = con.score;
                        newStatus.questionNum = con.question;
                        statusList.Add(newStatus);
                    }
                    objectOut = ObjectToByteArray(statusList);
                }
                else if (objType == typeof(Leaderboard))
                {
                    List<List<string>> leader = db.Select("Select name, score from leaderboard");
                    List<Leaderboard> leaderBoard = new List<Leaderboard>();
                    foreach (var player in leader)
                    {
                        Leaderboard newLeaderboard;
                        newLeaderboard.name = player[0];
                        newLeaderboard.score = Convert.ToInt16(player[1]);
                        leaderBoard.Add(newLeaderboard);
                    }
                    leaderBoard.OrderBy(o => o.score).ToList();
                    objectOut = ObjectToByteArray(leaderBoard);
                }
                else if (objType == typeof(ExcelData))
                {
                    List<ExcelData> excelDataList = new List<ExcelData>();
                    List<List<string>> excelRecords = db.Select("Select questionId, timeLeft from questionattempts");
                    List<List<string>> questionData = db.Select("Select questionNum, question from questions");
                    foreach (List<string> questionRecord in questionData)
                    {
                        excelDataList.Add(new ExcelData(Convert.ToInt16(questionRecord[0]), questionRecord[1], ((IEnumerable<int>)(from excelRecord in excelRecords where excelRecord[0] == questionRecord[0] select Convert.ToInt16(excelRecord[1]))).Average(),
                           ((double)excelRecords.Select(o => o[0] == questionRecord[0] && o[1] != "0").Count()) / excelRecords.Count()));
                    }
                    objectOut = ObjectToByteArray(excelDataList);
                }

                else if (objType == typeof(List<QACombo>))
                {
                    List<QACombo> QAList = (List<QACombo>)objFromClient;
                    if (QAList.Any())
                    {
                        db.Delete("DELETE FROM questions");
                        foreach (var record in QAList)
                        {
                            db.Insert("INSERT INTO questionattempts (questionNum,question,ans1,ans2,ans3,ans4,correctAnswer)VALUES(" + record.questionNum + "," + record.question + "," + record.ans1 + "," + record.ans2 + "," + record.ans3 + "," + record.ans4 + "," + record.correctAnswer + ")");
                        }
                    }
                    else
                    {
                        List<QACombo> send = new List<QACombo>();

                        List<List<string>> thisQuestion = db.Select("Select questionNum,question,ans1,ans2,ans3,ans4,correctAnswer from questions");
                        foreach (List<string> record in thisQuestion)
                        {
                            send.Add(new QACombo(String.Join("|", thisQuestion.Skip(1))));
                        }
                        objectOut = ObjectToByteArray(send);
                    }

                }

                stream.Write(objectOut, 0, objectOut.Length);
            }
            catch (IOException e)
            {
                
                connection.cSocket.Close();
                connection.rTimer.stop();
            }
            //fullObjectBytes = listObject.Join();

            ////////////////////////////////////////////////////////////////////////////////////////////data = string recived
        }
        private void writeSocket(object obj)
        {

        }
        private void listen(object obj)
        {
            TcpListener server = null;
            try
            {
                eventLogger.WriteEntry("Starting service");
                // Set the TcpListener on port 13000.
                Int32 port = 53512;
                IPAddress localAddr = IPAddress.Parse(GetLocalIPAddress());

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {

                    eventLogger.WriteEntry("Waiting for connection");
                    Console.Write("Waiting for a connection... ");
                    Console.Write(GetLocalIPAddress());

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    ClientConnections newConnection = new ClientConnections();
                    newConnection.results = new List<Result>();
                    newConnection.cSocket = client;
                    newConnection.score = 0;
                    newConnection.question = -1;
                    newConnection.rTimer = new MyTimer(0, readSocket, newConnection);
                    //connections.Add(newConnection);
                    eventLogger.WriteEntry("connection made");
                    Console.WriteLine("Connected!");

                    //data = null;

                    //// Get a stream object for reading and writing
                    //NetworkStream stream = client.GetStream();

                    //int i;

                    //// Loop to receive all the data sent by the client.
                    //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    //{
                    //    // Translate data bytes to a ASCII string.
                    //    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    //    Console.WriteLine("Received: {0}", data);

                    //    // Process the data sent by the client.
                    //    data = data.ToUpper();

                    //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    //    // Send back a response.
                    //    stream.Write(msg, 0, msg.Length);
                    //    Console.WriteLine("Sent: {0}", data);
                    //}

                    // Shutdown and end connection
                    //client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
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
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
