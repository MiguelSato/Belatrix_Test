
using System;

namespace Logger.Message
{
    class JobMessage
    {
        string _message = "";
        string _type;

        public JobMessage(string message, string type)
        {
            if(message == null || type == null) throw new Exception("Parameters can't be null");

            _message = message;
            _type = type;
        }

        public bool IsAMessage()
        {
            return _type.Equals("MESSAGE");
        }

        public bool IsAWarning()
        {
            return _type.Equals("WARNING");
        }

        public bool IsAnError()
        {
            return _type.Equals("ERROR");
        }

        public string getMessage()
        {
            return _message.Trim();
        }
    }
}
