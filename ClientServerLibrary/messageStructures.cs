using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
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
    }
}
