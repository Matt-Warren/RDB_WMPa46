using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace QuizService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if DEBUG
            MattStevenQuizService myservice = new MattStevenQuizService();
            myservice.start();
#else

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MattStevenQuizService()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
