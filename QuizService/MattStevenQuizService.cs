/*
* name: matt steven
* file: mattstevenquizservice.cs
* date: 11/27/2015
* assignment: windows/rbd combined assignment
* description: The service that runnings listining to new clients and accepting thier requests
*/
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
using System.Runtime.Serialization;

namespace QuizService
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    public partial class MattStevenQuizService : ServiceBase
    {
        /// <summary>
        /// 
        /// </summary>
        class ClientConnections
        {
            /// <summary>
            /// The current socket
            /// </summary>
            public TcpClient cSocket;
            /// <summary>
            /// The read timer
            /// </summary>
            public MyTimer rTimer;
            /// <summary>
            /// The question the user is on
            /// </summary>
            public int question;
            /// <summary>
            /// The users score
            /// </summary>
            public int score;
            /// <summary>
            /// The users name
            /// </summary>
            public string name;
            /// <summary>
            /// The results of the users questions
            /// </summary>
            public List<Result> results;
        }
        /// <summary>
        /// The event logger
        /// </summary>
        EventLog eventLogger;
        /// <summary>
        /// The connections
        /// </summary>
        List<ClientConnections> connections = new List<ClientConnections>();
        /// <summary>
        /// </summary>
        DB db = new DB();
        /// <summary>
        /// Initializes a new instance of the <see cref="MattStevenQuizService"/> class.
        /// </summary>
        Int32 port = 53512;
        public MattStevenQuizService()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Starts this instance. for degubing
        /// </summary>
        public void start()
        {
            OnStart(null);
        }
        /// <summary>
        /// Called when [start].
        /// </summary>
        /// <param name="args">The arguments.</param>
        protected override void OnStart(string[] args)
        {
            if (args.Length == 1)
            {
                port = Convert.ToInt32(args[0]);
            }
            //Creates logger 
            eventLogger = new System.Diagnostics.EventLog();
            this.AutoLog = false;
            if (!System.Diagnostics.EventLog.SourceExists("QuizSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "QuizSource", "QuizLog");
            }
            eventLogger.Source = "QuizSource";
            eventLogger.Log = "QuizLog";

            eventLogger.WriteEntry("go");
            //Create a thead for getting new connections
            MyTimer listenThread = new MyTimer(0, listen, new object());
            
        }

        /// <summary>
        /// Called when [stop].
        /// </summary>
        protected override void OnStop()
        {
            //closes all connections
            foreach (var con in connections)
            {
                con.cSocket.GetStream().Close();
            }
        }
        /// <summary>
        /// Reads the socket.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void readSocket(object obj)
        {
            //cast the obj which is a clientConnection
            ClientConnections connection = obj as ClientConnections;
            try
            {
                //size/data of read
                List<byte[]> listObject = new List<byte[]>();
                byte[] bytes = new byte[8192];
                byte[] fullObjectBytes;
                NetworkStream stream = connection.cSocket.GetStream();
                
                //check if connection is still open
                if (connection.cSocket.Connected)
                {
                    stream.Read(bytes, 0, bytes.Length);
                }
                else
                {
                    //stop timer if connection closed
                    connection.rTimer.stop();
                }
                listObject.Add(bytes);
                
                //Convert data recived to an object
                var bformatter = new BinaryFormatter();
                fullObjectBytes = bytes;
                Stream fullObjectStream = new MemoryStream(fullObjectBytes);
                object objFromClient = bformatter.Deserialize(fullObjectStream);
                Type objType = objFromClient.GetType();

                byte[] objectOut = new byte[0];//temp- the file data the will be sent to client

                if (objType == typeof(String))//client sent name
                {
                    connection.name = ((string)objFromClient);
                    objectOut = ObjectToByteArray("Connected");
                }
                else if (objType == typeof(Answer))//client gives you an answer (give them next question or their results)
                {
                    //Send client a question
                    Answer userAnswer = (Answer)objFromClient;

                    //check if first question
                    if (connection.question != -1)
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
                        //Add users answer/result to results list
                        connection.results.Add(new Result(QARow.questionNum + "|" + QARow.question + "|" + queryRow[1 + QARow.correctAnswer] + "|" + queryRow[1 + userAnswer.answer]));
                        db.Insert("INSERT INTO questionattempts (questionId,timeLeft)VALUES(" + QARow.questionNum + "," + userAnswer.timeLeft + ")");
                    }
                    //get next question data
                    List<string> nextQuestion = db.Select("Select questionNum,question,ans1,ans2,ans3,ans4,correctAnswer from questions where questionNum > " + connection.question + " Order By questionNum ASC").FirstOrDefault();
                    if (nextQuestion != null)
                    {
                        //Create object to send to client
                        QACombo nextQ = new QACombo(String.Join("|", nextQuestion));
                        objectOut = ObjectToByteArray(nextQ);

                        connection.question = nextQ.questionNum;
                    }
                    else
                    {
                        //Add user to leaderboard
                        db.Insert("INSERT INTO leaderboard (name,score)VALUES('" + connection.name + "'," + connection.score + ")");
                        objectOut = ObjectToByteArray(connection.results);
                    }
                }
                else if (objType == typeof(CurrentStatus))//
                {
                    //List of current connections
                    List<CurrentStatus> statusList = new List<CurrentStatus>();
                    foreach (var con in connections)
                    {
                        if (con.question != -1)
                        {
                            CurrentStatus newStatus;
                            newStatus.name = con.name;
                            newStatus.score = con.score;
                            newStatus.questionNum = con.question;
                            statusList.Add(newStatus);
                        }
                    }
                    //Send list of current connection to admin
                    objectOut = ObjectToByteArray(statusList);
                }
                else if (objType == typeof(Leaderboard))
                {
                    //create leaderboard 
                    List<List<string>> leader = db.Select("Select name, score from leaderboard order by score Desc");
                    List<Leaderboard> leaderBoard = new List<Leaderboard>();
                    foreach (var player in leader)
                    {
                        Leaderboard newLeaderboard;
                        newLeaderboard.name = player[0];
                        newLeaderboard.score = Convert.ToInt16(player[1]);
                        leaderBoard.Add(newLeaderboard);
                    }
                    leaderBoard.OrderBy(o => o.score).ToList();
                    //Send leaderboard to user
                    objectOut = ObjectToByteArray(leaderBoard);
                }
                else if (objType == typeof(ExcelData))
                {
                    //Create excel list for admin
                    List<ExcelData> excelDataList = new List<ExcelData>();
                    List<List<string>> excelRecords = db.Select("Select questionId, timeLeft from questionattempts");
                    List<List<string>> questionData = db.Select("Select questionNum, question from questions");
                    //Combine questionatttempts and question information to generate fields for excel
                    foreach (List<string> questionRecord in questionData)
                    {
                        int questionNumber = Convert.ToInt16(questionRecord[0]);
                        string questionText = questionRecord[1];

                        double avgTime = 0;
                        int count = 0;
                        double percentCorrect = 0;
                        
                        foreach (var excelRecord in excelRecords)
                        {
                            if (excelRecord[0] == questionRecord[0])
                            {
                                avgTime += Convert.ToDouble(excelRecord[1]);

                                if (excelRecord[1] != "0")
                                {
                                    percentCorrect++;
                                }
                                count++;
                            }
                        }
                        avgTime = count != 0 ? avgTime /= count : 0;

                        percentCorrect = count != 0 ? percentCorrect /= count : 0;
                        percentCorrect *= 100;
                        
                        excelDataList.Add(new ExcelData(questionNumber, questionText, avgTime, percentCorrect));
                    }
                    //Send excel data to admin
                    objectOut = ObjectToByteArray(excelDataList);
                }

                else if (objType == typeof(List<QACombo>))
                {
                    //Admin updates the question list
                    List<QACombo> QAList = (List<QACombo>)objFromClient;
                    if (QAList.Any())//Admin is changing questions
                    {
                        db.Delete("DELETE FROM questions");//Remove all questions
                        db.Delete("DELETE FROM questionattempts");//remove all questionattempts
                        //insert all new questions
                        foreach (var record in QAList)
                        {
                            db.Insert("INSERT INTO questions (questionNum,question,ans1,ans2,ans3,ans4,correctAnswer)VALUES(" + record.questionNum + ",'" + record.question + "','" + record.ans1 + "','" + record.ans2 + "','" + record.ans3 + "','" + record.ans4 + "'," + record.correctAnswer + ")");
                        }
                    }
                    else//Read all questions
                    {

                        List<QACombo> send = new List<QACombo>();

                        List<List<string>> thisQuestion = db.Select("Select questionNum,question,ans1,ans2,ans3,ans4,correctAnswer from questions");
                        foreach (List<string> record in thisQuestion)
                        {
                            send.Add(new QACombo(String.Join("|", record)));
                        }
                        //Send all questions to admin
                        objectOut = ObjectToByteArray(send);
                    }

                }
                //Send the data to the client
                stream.Write(objectOut, 0, objectOut.Length);
            }
            catch (IOException e)
            {
                connection.cSocket.Close();
                connection.rTimer.stop();
            }
            catch (SerializationException e)
            {
                connection.cSocket.Close();
                connection.rTimer.stop();
            }
            finally
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    //Check if any clients have disconnected from the server
                    if (connections[i].cSocket.Connected == false)
                    {
                        //Remove the client from running clients list
                        connections.RemoveAt(i);
                    }
                }
            }
        }
        /// <summary>
        /// Listens for new clients.
        /// </summary>
        /// <param name="obj">not used</param>
        private void listen(object obj)
        {
            TcpListener server = null;
            try
            {
                eventLogger.WriteEntry("Starting service");
                
                IPAddress localAddr = IPAddress.Parse(GetLocalIPAddress());
                server = new TcpListener(localAddr, port);
                server.Start();
                Byte[] bytes = new Byte[256];
                while (true)
                {

                    eventLogger.WriteEntry("Waiting for connection");
                    Console.Write("Waiting for a connection... ");
                    Console.Write(GetLocalIPAddress());//gets this computers ip address
                    TcpClient client = server.AcceptTcpClient();
                    //Create new connection for the client that just connected
                    ClientConnections newConnection = new ClientConnections();
                    newConnection.results = new List<Result>();
                    newConnection.cSocket = client;
                    newConnection.score = 0;
                    newConnection.question = -1;
                    //Create a thread the will run on loop sending this clients Connection to it
                    newConnection.rTimer = new MyTimer(0, readSocket, newConnection);
                    //Add connection to running connections list
                    connections.Add(newConnection);
                    eventLogger.WriteEntry("connection made");
                    Console.WriteLine("Connected!");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
        /// <summary>
        /// Converts object to byte array
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
        /// Gets the local ip address.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Local IP Address Not Found!</exception>
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
