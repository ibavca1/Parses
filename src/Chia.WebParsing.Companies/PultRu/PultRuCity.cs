using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.PultRu
{
    public class PultRuCity : WebCity
    {
        public static readonly PultRuCity Moscow;
        public static readonly PultRuCity StPetersburg;
        private static readonly Dictionary<WebCity, PultRuCity> Cities;

        static PultRuCity()
        {
            Moscow = new PultRuCity(WebCities.Moscow, "moskva", "Москва");
            StPetersburg = new PultRuCity(WebCities.StPetersburg, "st-petersburg", "Санкт-Петербург");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private PultRuCity(WebCity town, string cookieValue, string name)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        /// <summary>
        /// Значение-ключ, которое задает город в cookie-файле.
        /// </summary>
        public string CookieValue { get; private set; }

        public static IEnumerable<PultRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(PultRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(PultRuCity))
                        .ToArray();
                return fields.Select(field => (PultRuCity)field.GetValue(null));
            }
        }

        public static PultRuCity Get(WebCity city)
        {
            PultRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(PultRuCompany.Instance, city);
            return result;
        }
    }
}