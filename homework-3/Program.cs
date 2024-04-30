using System;

namespace LoggingLibrary
{
    public class Program
    {
        public static void Main()
        {
            Logger.Instance.Log("Log message.");
            Logger.Instance.Dispose();
        }
    }
}
