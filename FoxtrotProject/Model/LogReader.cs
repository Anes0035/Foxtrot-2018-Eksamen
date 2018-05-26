using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class LogReader
    {

        public DateTime DT { get; set; }
        public string Message { get; set; }

        private StreamReader reader;
        public LogReader()
        {

        }

        public void ReadDateEntry(string entry)
        {
            
            using (StreamReader reader = File.OpenText(@"C:\Users\ChristianTerp\Log\DataLog.txt"))
            {
                string line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    line = reader.ReadLine();
                    
                }               
            }
        }
        public List<String> ReadLog()
        {
            throw new NotImplementedException();
        }
    }
}
