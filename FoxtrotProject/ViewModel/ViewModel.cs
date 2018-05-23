using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FoxtrotProject.Model;

namespace FoxtrotProject.ViewModel
{
    abstract class ViewModel : INotifyPropertyChanged
    {
        protected static Database db = new Database();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected string ValidateNumericParse<T>(string value, string propertyName, out T numericValue) where T : IComparable
        {
            try
            {
                numericValue = (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
                return String.Format("{0} feltet indeholder ulovelige tegn", propertyName);
            }
            catch
            {
                numericValue = default(T);
                return null;
            }
            
        }

        protected string ValidateIntegerParse(string value, string propertyName, out int integerValue)
        {
            string message = null;

            if (!Int32.TryParse(value, out integerValue))
                message = string.Format("{0} feltet indeholder ulovlige tegn", propertyName);

            return message;
        }

        protected string ValidateDoubleParse(string value, string propertyName, out double doubleValue)
        {
            string message = null;

            if (!Double.TryParse(value, out doubleValue))
                message = string.Format("{0} feltet indeholder ulovlige tegn", propertyName);

            return message;
        }

        protected string PropertyIsEmptyErrorMessage(string propertyName)
        {
            return string.Format("{0} feltet er tomt", propertyName);
        }
    }
}
