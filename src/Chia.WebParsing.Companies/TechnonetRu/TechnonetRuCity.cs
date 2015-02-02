using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.TechnonetRu
{
    public class TechnonetRuCity : WebCity
    {
        public static readonly TechnonetRuCity Sterlitamak;
        public static readonly TechnonetRuCity Ufa;

        static TechnonetRuCity()
        {
            Sterlitamak = new TechnonetRuCity(WebCities.Undefined, "Стерлитамак", "0");
            Ufa = new TechnonetRuCity(WebCities.Ufa, "Уфа", "113");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private TechnonetRuCity(WebCity city, string name, string cookieValue) 
            : base(city, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<TechnonetRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(TechnonetRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(TechnonetRuCity))
                        .ToArray();
                return fields.Select(field => (TechnonetRuCity)field.GetValue(null));
            }
        }

        public static TechnonetRuCity Get(WebCity city)
        {
            TechnonetRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(TechnonetRuCompany.Instance, city);
            return result;
        }

        private static readonly Dictionary<WebCity, TechnonetRuCity> Cities;
    }
}