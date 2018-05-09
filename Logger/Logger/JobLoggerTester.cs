using Logger.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class JobLoggerTester
    {

        public void Run()
        {

            //Creating a JobLogger with incorrect configuration
            Console.Out.WriteLine("Creating a JobLogger with incorrect configuration:");
            

            try
            {
                Console.Out.Write("With nothing enable: ");
                JobLogger jobLogger = new JobLogger(false, false, false, false, false, false);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }



            try
            {
                Console.Out.Write("Without any message type enable: ");
                JobLogger jobLogger = new JobLogger(true, true, true, false, false, false);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

            try
            {
                Console.Out.Write("Without any log output enable: ");
                JobLogger jobLogger = new JobLogger(false, false, false, true, true, true);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

            //Logging only to File
            try
            {
                Console.Out.WriteLine("Logging only to a file");

                JobLogger jobLogger = new JobLogger(true, false, false, true, true, true);

                NormalMessage normalMessage = new NormalMessage("This is a NORMAL message to the file");
                jobLogger.LogMessage(normalMessage);

                WarningMessage warningMessage = new WarningMessage("This is a WARNING message to the file");
                jobLogger.LogMessage(warningMessage);

                ErrorMessage errorMessage = new ErrorMessage("This is an ERROR message to the file");
                jobLogger.LogMessage(errorMessage);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

            //Logging only to Database
            try
            {
                Console.Out.WriteLine("Logging only to the data base");

                JobLogger jobLogger = new JobLogger(false, false, true, true, true, true);

                NormalMessage normalMessage = new NormalMessage("This is a NORMAL message to the data base");
                jobLogger.LogMessage(normalMessage);

                WarningMessage warningMessage = new WarningMessage("This is a WARNING message to the data base");
                jobLogger.LogMessage(warningMessage);

                ErrorMessage errorMessage = new ErrorMessage("This is an ERROR message to the data base");
                jobLogger.LogMessage(errorMessage);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

            //Logging only to Console
            try
            {
                Console.Out.WriteLine("Logging only to the console");

                JobLogger jobLogger = new JobLogger(false, true, false, true, true, true);

                NormalMessage normalMessage = new NormalMessage("This is a NORMAL message to the console");
                jobLogger.LogMessage(normalMessage);

                WarningMessage warningMessage = new WarningMessage("This is a WARNING message to the console");
                jobLogger.LogMessage(warningMessage);

                ErrorMessage errorMessage = new ErrorMessage("This is an ERROR message to the console");
                jobLogger.LogMessage(errorMessage);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

            

            Console.ReadLine();
        }
     }
}
