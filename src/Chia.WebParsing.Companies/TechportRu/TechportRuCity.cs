using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.TechportRu
{
    public class TechportRuCity : WebCity
    {
        public static readonly TechportRuCity Bryansk;
        public static readonly TechportRuCity Moscow;
        public static readonly TechportRuCity StPetersburg;
        private static readonly Dictionary<WebCity, TechportRuCity> Cities;

        static TechportRuCity()
        {
            Bryansk = new TechportRuCity(WebCities.Bryansk, "Брянск", "BRN");
            Moscow = new TechportRuCity(WebCities.Moscow, "Москва", "MSK");
            StPetersburg = new TechportRuCity(WebCities.StPetersburg, "Санкт-Петербург", "SPB");

            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private TechportRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }


        public static IEnumerable<TechportRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(TechportRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(TechportRuCity))
                        .ToArray();
                return fields.Select(field => (TechportRuCity)field.GetValue(null));
            }
        }

        public static TechportRuCity Get(WebCity city)
        {
            TechportRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(TechportRuCompany.Instance, city);
            
            return result;
        }
    }
}