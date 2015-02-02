using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.HolodilnikRu
{
    public class HolodilnikRuCity : WebCity
    {
        public static readonly HolodilnikRuCity Moscow;
        public static readonly HolodilnikRuCity StPetersburg;

        private static readonly Dictionary<WebCity, HolodilnikRuCity> Cities;

        static HolodilnikRuCity()
        {
            Moscow = new HolodilnikRuCity(WebCities.Moscow, "Москва и область", "1");
            StPetersburg = new HolodilnikRuCity(WebCities.StPetersburg, "Санкт-Петербург", "2");

            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private HolodilnikRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<HolodilnikRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(HolodilnikRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(HolodilnikRuCity))
                        .ToArray();
                return fields.Select(field => (HolodilnikRuCity)field.GetValue(null));
            }
        }

        public static HolodilnikRuCity Get(WebCity city)
        {
            HolodilnikRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(HolodilnikRuCompany.Instance, city);
            
            return result;
        }
    }
}