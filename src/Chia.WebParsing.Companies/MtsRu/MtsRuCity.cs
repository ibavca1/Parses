using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.MtsRu
{
    public class MtsRuCity : WebCity
    {
        public static readonly MtsRuCity Chelyabinsk;
        public static readonly MtsRuCity Moscow;
        private static readonly Dictionary<WebCity, MtsRuCity> Cities;

        static MtsRuCity()
        {
            Chelyabinsk = new MtsRuCity(WebCities.Chelyabinsk, "Челябинская обл.", "1821");
            Moscow = new MtsRuCity(WebCities.Moscow, "Москва и Подмосковье", "1826");

            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private MtsRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<MtsRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(MtsRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(MtsRuCity))
                        .ToArray();
                return fields.Select(field => (MtsRuCity)field.GetValue(null));
            }
        }

        public static MtsRuCity Get(WebCity city)
        {
            MtsRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(MtsRuCompany.Instance, city);
            
            return result;
        }
    }
}