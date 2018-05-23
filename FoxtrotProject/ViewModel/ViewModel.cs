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

        protected static PropertyTranslator Translator = new PropertyTranslator();

        protected string ValidateNumericParse<T>(string value, string propertyName, out T numericValue) where T : IComparable
        {
            var typeConverter = TypeDescriptor.GetConverter(typeof(T));

            if (typeConverter != null && typeConverter.IsValid(value))
            {
                numericValue = (T)typeConverter.ConvertFromString(value);
                return null;
            }

            numericValue = default(T);
            return String.Format("{0} feltet indeholder ulovelige tegn", Translator.GetTranslation(propertyName));
        }

        protected string PropertyIsEmptyErrorMessage(string propertyName)
        {
            return string.Format("{0} feltet er tomt", Translator.GetTranslation(propertyName));
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
