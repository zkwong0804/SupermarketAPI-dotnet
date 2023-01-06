﻿namespace DapperLearningAPI.Helpers
{
    public class LogHelper
    {
        public void Info(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }
        public void Warning(string message)
        {
            Console.WriteLine($"WARN: {message}");
        }
        public void Error(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }
        public void Debug(string message)
        {
            Console.WriteLine($"DEBUG: {message}");
        }
    }
}
