using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
    [Serializable]
    public class QACombo
    {
        public int questionNum { get; set; }
        public string question { get; set; }
        public string ans1 { get; set; }
        public string ans2 { get; set; }
        public string ans3 { get; set; }
        public string ans4 { get; set; }

        public int correctAnswer;

        public bool isSet;
        public QACombo()
        {

        }
        public QACombo(string qaString)
        {
            string[] temp = qaString.Split('|');
            questionNum = Convert.ToInt16(temp[0]);
            this.question = temp[1];
            this.ans1 = temp[2];
            this.ans2 = temp[3];
            this.ans3 = temp[4];
            this.ans4 = temp[5];
            this.correctAnswer = Convert.ToInt32(temp[6]);
            isSet = true;
        }

        public void SetQACombo(string qaString)
        {
            string[] temp = qaString.Split('|');
            questionNum = Convert.ToInt16(temp[0]);
            this.question = temp[1];
            this.ans1 = temp[2];
            this.ans2 = temp[3];
            this.ans3 = temp[4];
            this.ans4 = temp[5];
            this.correctAnswer = Convert.ToInt32(temp[6]);
            isSet = true;
        }

        public bool checkAnswer(int answer)
        {
            bool correct = false;
            if (answer == correctAnswer)
            {
                correct = true;
            }
            return correct;
        }


    }
}
