using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    // Author Kasper,Elena and Christian
    class DataEntry
    {
        public string dt = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

        public string Dt { get; set; }
        public string Message { get; set; }

        private StreamReader reader;
        public DataEntry()
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


        public DataEntry(List<DataEntry> logs, string dt, string message)
        {


            dt = Dt;
            message = Message;
        }
    }
}
