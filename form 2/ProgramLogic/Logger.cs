using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolGraphicsApp.ProgramLogic
{
    internal class Logger
    {
        private const string Path = "log.txt";

        public void Log(string message)
        {
            if(!File.Exists(Path))
            {
                File.Create(Path);
            }

            using(var stream = new FileStream(Path, FileMode.Append))
            {
                DateTime dateTime = DateTime.Now;
                var buffer = new UTF8Encoding(true).GetBytes($"[{dateTime.ToString()}] {message}");
                stream.Write(buffer, 0, buffer.Length);
            }

        }
    }
}
