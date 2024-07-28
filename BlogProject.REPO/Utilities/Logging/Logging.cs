using BlogProject.SERVICE.Utilities.ILogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Utilities.Logging
{
    public class Logging : ILogging
    {
        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Info: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
