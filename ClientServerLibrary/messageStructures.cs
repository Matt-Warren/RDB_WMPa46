using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
<<<<<<< HEAD
    public struct Answer
    {
        public int question;
        public int answer;
    }s
    public struct CurrentStatus
    {
        public string name;
        public int score;
        public int questionNum;
    }
    public struct Leaderboard
    {
        public string name;
        public int score;

    }
    public class messageStructures
    {
        
=======
    public class messageStructures
    {
        public struct Answer
        {
            public int question;
            public int answer;
        }
        public struct CurrentStatus
        {
            public string name;
            public int score;
        }
        public struct Leaderboard
        {
            public string name;
            public int score;
        }
>>>>>>> 1f8670714014fc953e94f43913e8f7d941785364
    }
}
