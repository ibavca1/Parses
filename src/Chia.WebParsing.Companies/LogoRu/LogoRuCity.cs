using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.LogoRu
{
    public class LogoRuCity : WebCity
    {
        public static readonly LogoRuCity Ekaterinburg;
        public static readonly LogoRuCity Perm;
        
        private static readonly Dictionary<WebCity, LogoRuCity> Cities;

        static LogoRuCity()
        {
            Ekaterinburg = new LogoRuCity(WebCities.Ekaterinburg, "Екатеринбург", "a4eb7bd26e0e5a3a35431f1e890429b0");
            Perm = new LogoRuCity(WebCities.Perm, "Пермь", "b2666f0c172b78f437d39e4464bbe45b");
            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private LogoRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<LogoRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(LogoRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(LogoRuCity))
                        .ToArray();
                return fields.Select(field => (LogoRuCity)field.GetValue(null));
            }
        }

        public static LogoRuCity Get(WebCity city)
        {
            LogoRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(LogoRuCompany.Instance, city);
            
            return result;
        }
    }
}