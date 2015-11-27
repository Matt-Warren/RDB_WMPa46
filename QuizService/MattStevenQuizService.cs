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

namespace QuizService
{
    public partial class MattStevenQuizService : ServiceBase
    {
        struct ClientConnections
        {
            public TcpClient cSocket;
            public MyTimer rTimer;
            public MyTimer wTimer;
            public string userType;
            public int question;
            public int score;
            public string name;
        }
        EventLog eventLogger;
        List<ClientConnections> connections = new List<ClientConnections>();
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
            List<byte[]> listObject = new List<byte[]>();
            byte[] bytes = new byte[8192];
            byte[] fullObjectBytes;// = new byte[8192];
            ClientConnections connection = (ClientConnections)obj;
            // Get a stream object for reading and writing
            NetworkStream stream = connection.cSocket.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            //{
            stream.Read(bytes, 0, bytes.Length);
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

            if (objType == typeof(String))
            {

                connection.name = ((string)objFromClient);
                objectOut = ObjectToByteArray("Connected");
            }
            else if (objType == typeof(Answer))
            {
                Answer userAnswer = (Answer)objFromClient;
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
                List<Leaderboard> statusList = new List<Leaderboard>();
                foreach (var con in connections)
                {
                    Leaderboard newLeaderboard;
                    newLeaderboard.name = con.name;
                    newLeaderboard.score = con.score;
                    statusList.Add(newLeaderboard);
                }
                statusList.OrderBy(o=>o.score).ToList();
                objectOut = ObjectToByteArray(statusList);
            }
            else if(objType == typeof(QACombo))
            {
                QACombo sendCombo = new QACombo();
                ////////////////////////////////////////////////////////////
            }
            else if(objType == typeof(List<QACombo>))
            {
                List<QACombo> sendCombo = new List<QACombo>();
                ////////////////////////////////////////////////////////////


            }
            else if(objType == typeof(List<ExcelData>))
            {
                List<ExcelData> excelDataList = new List<ExcelData>();
                ////////////////////////////////////////////////////////////////
            }

            stream.Write(objectOut, 0, objectOut.Length);
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
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

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

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    ClientConnections newConnection = new ClientConnections();
                    newConnection.cSocket = client;
                    newConnection.rTimer = new MyTimer(0,readSocket,newConnection);
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
    }
}
