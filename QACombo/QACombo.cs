using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA
{
    [Serializable]
    public class QACombo
    {

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
            this.question = temp[0];
            this.ans1 = temp[1];
            this.ans2 = temp[2];
            this.ans3 = temp[3];
            this.ans4 = temp[4];
            this.correctAnswer = Convert.ToInt32(temp[5]);
            isSet = true;
        }

        public void SetQACombo(string qaString)
        {
            string[] temp = qaString.Split('|');
            this.question = temp[0];
            this.ans1 = temp[1];
            this.ans2 = temp[2];
            this.ans3 = temp[3];
            this.ans4 = temp[4];
            this.correctAnswer = Convert.ToInt32(temp[5]);
            isSet = true;
        }

        public bool checkAnswer(int answer)
        {
            bool correct = false;
            if(answer == correctAnswer)
            {
                correct = true;
            }
            return correct;
        }


    }
}
