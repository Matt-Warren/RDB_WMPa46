using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
    public struct Answer
    {
        public int question;
        public int answer;
        public int timeLeft;
    }
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

    public struct Result
    {
        public int questionNumber;
        public string question;
        public string actualAnswer;
        public string theirAnswer;
    }
}
