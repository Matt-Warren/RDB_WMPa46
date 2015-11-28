/*
*   FILE: DB.cs
*   NAME: Steven Johnston & Matt Warren
*   DATE: 2015/11/27
*   DESC: This file is used to connect with SQL database.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace QuizService
{
    class DB
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DB()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "mattstevenquiz";
            uid = "root";
            password = "root";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connected To Database");
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("connection to DB failed");
                        break;

                    case 1045:
                        Console.WriteLine("Username/pass failed");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        //Insert statement
        public void Insert(string query)
        {
            
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                
                cmd.ExecuteNonQuery();
                
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string query)
        {

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<List<string>> Select(string query)
        {

            //Create a list to store the result
            List<List<string>> list = new List<List<string>>();

            //Open connection
            if (this.OpenConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    List<string> record = new List<string>();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        record.Add(dataReader[i].ToString());
                    }
                    list.Add(record);
                }
                
                dataReader.Close();
                
                this.CloseConnection();
                
                return list;
            }
            else
            {
                return list;
            }
        }
    }
}