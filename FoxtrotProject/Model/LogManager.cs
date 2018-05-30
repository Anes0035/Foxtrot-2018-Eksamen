using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    // Author Kasper
    class LogManager
    {
          
        public List<LogReader> logs { get; set; }
        public LogManager()
        {
           logs = new List<LogReader>();
        }
    }
}
