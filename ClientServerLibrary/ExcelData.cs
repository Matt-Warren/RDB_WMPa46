/*
*   FILE: ExcelData.cs
*   NAME: Steven Johnston & Matt Warren
*   DATE: 2015/11/27
*   DESC: This is used to transfer the excel data between the admin and the service.
*/
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
