using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.CitilinkRu
{
    public class CitilinkRuCity : WebCity
    {
        public static readonly CitilinkRuCity Moscow;
        public static readonly CitilinkRuCity StPetersburg;
        private static readonly Dictionary<WebCity, CitilinkRuCity> Cities;

        static CitilinkRuCity()
        {
            Moscow = new CitilinkRuCity(WebCities.Moscow, "msk_cl", "Москва");
            StPetersburg = new CitilinkRuCity(WebCities.StPetersburg, "spb_cl", "Санкт-Петербург");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private CitilinkRuCity(WebCity other, string cookieValue, string name)
            : base(other, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<CitilinkRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(CitilinkRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(CitilinkRuCity))
                        .ToArray();
                return fields.Select(field => (CitilinkRuCity)field.GetValue(null));
            }
        }

        public static CitilinkRuCity Get(WebCity city)
        {
            CitilinkRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(CitilinkRuCompany.Instance, city);
            return result;
        }
    }
}