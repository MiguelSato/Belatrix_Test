using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Threading;
using Logger.Message;

namespace Logger
{
    class JobLogger
    {
        private bool _logToFile;
        private bool _logToConsole;
        private bool _logToDatabase; //Correction

        private bool _logMessage;
        private bool _logWarning;
        private bool _logError;
        
        //Change order
        public JobLogger(bool logToFile, bool logToConsole, bool logToDataBase, bool logMessage, bool logWarning, bool logError)
        {
            if (!logToConsole && !logToFile && !logToDataBase) throw new Exception("Invalid configuration");

            if (!logError && !logMessage && !logWarning) throw new Exception("Error, Warning or Message logging must be specified");
            
            _logToFile = logToFile;
            _logToConsole = logToConsole;
            _logToDatabase = logToDataBase;

            _logMessage = logMessage;
            _logWarning = logWarning;
            _logError = logError;
        }

        public void LogMessage(JobMessage message)
        {

            if (message.getMessage().Length == 0) return;
            
            if (!(message.IsAnError() && _logError) && !(message.IsAWarning() && _logWarning) && !(message.IsAMessage() && _logMessage)) return;

            if (_logToDatabase) LogToDataBase(message);

            if (_logToFile) LogToFile(message);
            
            if (_logToConsole) LogToConsole(message);
            
        }

        private void LogToDataBase(JobMessage message)
        {
            string conectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            connection.Open();

            int messageType;

            if (message.IsAMessage()) messageType = 1;
            else if (message.IsAnError()) messageType = 2;
            else messageType = 3;

            string query = "INSERT INTO LOG VALUES('" + message.getMessage() + "', " + messageType.ToString() + ")";

            SqlCommand command = new SqlCommand(query, connection);

            command.ExecuteNonQuery();
        }

        private void LogToFile(JobMessage message)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
            string fileMessage = String.Empty;

            if (File.Exists(ConfigurationManager.AppSettings["LogFileDirectory"]  + DateTime.Now.ToShortDateString() + ".txt"))
            {
                fileMessage = File.ReadAllText(ConfigurationManager.AppSettings["LogFileDirectory"] + DateTime.Now.ToShortDateString() + ".txt");
            }

            fileMessage = fileMessage + DateTime.Now.ToLongTimeString() + " : " + message.getMessage() + "\n";

            File.WriteAllText(ConfigurationManager.AppSettings["LogFileDirectory"] + DateTime.Now.ToShortDateString() + ".txt", fileMessage);

        }

        private void LogToConsole(JobMessage message)
        {
            
            if (message.IsAnError())Console.ForegroundColor = ConsoleColor.Red;
            else if (message.IsAWarning()) Console.ForegroundColor = ConsoleColor.Yellow;
            else if (message.IsAMessage()) Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine(DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToLongTimeString() + " : " + message.getMessage());
        }
    }
}
