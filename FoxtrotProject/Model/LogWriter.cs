using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoxtrotProject.Model
{
    class LogWriter
    {
        public DateTime dt = DateTime.Now;
        private StreamWriter writer;

      
        public LogWriter()
        {
            
        }

       
        public void WriteDateEntry(string entry)
        {
            writer = new StreamWriter(@"C:\Users\ChristianTerp\Log\DataLog.txt", true);
            writer.WriteLine(entry);
            writer.Close();
        }

        public void WriteErrorMessage(string message, DateTime dt)
        {
            throw new NotImplementedException();
        }


    }
}
