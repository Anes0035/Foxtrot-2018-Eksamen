using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.ViewModel
{
    class PropertyTranslator
    {
        // Author Christian
        private Dictionary<string, string> Translations;

        public PropertyTranslator()
        {
            Translations = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            TranslationList();
        }

        private void TranslationList()
        {
            Translations.Add("Name", "Navn");
            Translations.Add("Address", "Adresse");
            Translations.Add("TelephoneNumber", "Tlf. nummer");
            Translations.Add("ContactPerson", "Kontakt person");
            Translations.Add("GrossIncome", "Årlig omsætning");
        }

        /// <summary>
        /// Get english property name translated to danish
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns>Default return string is the parameter propertyName</returns>
        public string GetTranslation(string propertyName)
        {
            if (Translations.ContainsKey(propertyName))
                return Translations[propertyName];
            else
                return propertyName;
        }
    }
}
