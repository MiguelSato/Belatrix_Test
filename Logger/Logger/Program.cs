

namespace Logger
{
    class Program
    {
        static bool a;

        static void Main(string[] args)
        {

            JobLoggerTester loggerTester = new JobLoggerTester();

            loggerTester.Run();

        }
    }
}
