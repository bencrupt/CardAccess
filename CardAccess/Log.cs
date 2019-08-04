using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CardAccess
{
    class Log : ILog, IDisposable
    {
        private StreamWriter sw;
        public Log()
        {
            sw = File.CreateText("log.log");
        }

        public void Write(string msg)
        {
            sw.WriteLine($"{DateTime.Now.ToString("ddddd")}: {msg}");
            sw.Flush();
        }
        public void Dispose()
        {
            sw.Dispose();
        }
    }
}
