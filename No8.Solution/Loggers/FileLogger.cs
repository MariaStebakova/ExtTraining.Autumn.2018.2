using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Loggers
{
    public class FileLogger: ILogger
    {
        public void Log(string message)
        {
            using (FileStream fs = new FileStream("log.txt", FileMode.Append))
            using (var sr = new StreamWriter(fs))
                sr.WriteLine(message);
        }
    }
}
