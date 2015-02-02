using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.NotikRu
{
    public class NotikRuCity : WebCity
    {
        public static readonly NotikRuCity Moscow;
        public static readonly NotikRuCity StPetersburg;

        private static readonly Dictionary<WebCity, NotikRuCity> Cities;

        static NotikRuCity()
        {
            Moscow = new NotikRuCity(WebCities.Moscow, "Москва", "1");
            StPetersburg = new NotikRuCity(WebCities.StPetersburg, "Санкт-Петербург", "2");
            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private NotikRuCity(WebCity town, string name, string code)
            : base(town, name)
        {
            Code = code;
        }

        public string Code { get; private set; }

        public static IEnumerable<NotikRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(NotikRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(NotikRuCity))
                        .ToArray();
                return fields.Select(field => (NotikRuCity)field.GetValue(null));
            }
        }

        public static NotikRuCity Get(WebCity city)
        {
            NotikRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(NotikRuCompany.Instance, city);
            
            return result;
        }
    }
}