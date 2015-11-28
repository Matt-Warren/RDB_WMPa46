using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
    [Serializable]
    public class ExcelData
    {
        public int questionNumber;
        public string questionText;
        public double avgTime;
        public double percentCorrect;

        public ExcelData()
        {

        }
        public ExcelData(int questionNumber, string questionText, double avgTime, double percentCorrect)
        {
            this.questionNumber = questionNumber;
            this.questionText = questionText;
            this.avgTime = avgTime;
            this.percentCorrect = percentCorrect;
        }

    }
}
