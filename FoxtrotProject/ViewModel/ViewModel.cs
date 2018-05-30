using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FoxtrotProject.Model;

namespace FoxtrotProject.ViewModel
{
    abstract class ViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        protected static Database db = new Database();

        protected static PropertyTranslator Translator = new PropertyTranslator();

        #region ErrorHandling
        public string FirstErrorMessage
        {
            get
            {
                PropertyInfo[] properties = GetType().GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    if (this[p.Name] != null)
                        return this[p.Name];
                }

                return null;
            }
        }

        public virtual string Error
        {
            get { return null; }
        }

        public virtual string this[string propertyName]
        {
            get { return null; }
        }
        #endregion

        protected string ValidateNumericParse<T>(string value, string propertyName, out T numericValue) where T : IComparable
        {
            var typeConverter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                numericValue = (T)typeConverter.ConvertFromString(value);
                return null;
            }
            catch (Exception)
            {
                numericValue = default(T);
                return String.Format("{0} feltet indeholder ulovelige tegn", Translator.GetTranslation(propertyName));
            }

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
