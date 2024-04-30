using System;
using System.IO;

namespace LoggingLibrary
{
    public class Logger : IDisposable
    {
        private static Logger? s_instance;
        private string _logFilePath;
        private StreamWriter? _writer;

        private Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
            _writer = File.AppendText(logFilePath);
        }

        public static Logger Instance
        {
            get
            {
                if (s_instance == null)
                {
                    string defaultLogFilePath = "log.txt";
                    s_instance = new Logger(defaultLogFilePath);
                }
                return s_instance;
            }
        }

        public void Log(string message)
        {
            _writer?.WriteLine($"{DateTime.Now}: {message}");
        }   

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _writer?.Dispose();
            }
            s_instance = null;
        }
    }
}
