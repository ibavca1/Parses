using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.JustRu
{
    public class JustRuCity : WebCity
    {
        public static readonly JustRuCity Moscow;
        public static readonly JustRuCity StPetersburg;
        public static readonly JustRuCity Chelyabinsk;

        private static readonly Dictionary<WebCity, JustRuCity> Cities;

        static JustRuCity()
        {
            Moscow = new JustRuCity(WebCities.Moscow, "Москва", "4312;4400;613310");
            StPetersburg = new JustRuCity(WebCities.StPetersburg, "Санкт-Петербург", "4925;4962");
            Chelyabinsk = new JustRuCity(WebCities.Chelyabinsk, "Челябинск", "5507;5539;454000");
            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private JustRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = HttpUtility.UrlEncode(cookieValue);
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<JustRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(JustRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(JustRuCity))
                        .ToArray();
                return fields.Select(field => (JustRuCity)field.GetValue(null));
            }
        }

        public static JustRuCity Get(WebCity city)
        {
            JustRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(JustRuCompany.Instance, city);
            return result;
        }
    }
}