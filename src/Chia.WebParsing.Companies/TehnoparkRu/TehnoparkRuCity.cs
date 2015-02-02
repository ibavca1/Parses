using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.TehnoparkRu
{
    public class TehnoparkRuCity : WebCity
    {
        public static readonly TehnoparkRuCity Moscow;
        public static readonly TehnoparkRuCity StPetersburg;
        public static readonly TehnoparkRuCity Kaluga;
        public static readonly TehnoparkRuCity Obninsk;
        public static readonly TehnoparkRuCity Vologda;
        private static readonly Dictionary<WebCity, TehnoparkRuCity> Cities;

        static TehnoparkRuCity()
        {
            Moscow = new TehnoparkRuCity(WebCities.Moscow, 36966, "www", "Москва");
            StPetersburg = new TehnoparkRuCity(WebCities.StPetersburg, 39943, "www", "Санкт-Петербург");
            Kaluga = new TehnoparkRuCity(WebCities.Kaluga, 37526, "www", "Калуга");
            Obninsk = new TehnoparkRuCity(WebCities.Obninsk, 37540, "www", "Обнинск");
            Vologda = new TehnoparkRuCity(WebCities.Vologda, 37818, "www", "Вологда");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private TehnoparkRuCity(WebCity other, int idInCookie, string uriPrefix, string name)
            : base(other, name)
        {
            UriPrefix = uriPrefix;
            IdInCookie = idInCookie;
        }

        public string UriPrefix { get; private set; }

        public int IdInCookie { get; private set; }

        public static IEnumerable<TehnoparkRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(TehnoparkRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(TehnoparkRuCity))
                        .ToArray();
                return fields.Select(field => (TehnoparkRuCity)field.GetValue(null));
            }
        }

        public static TehnoparkRuCity Get(WebCity city)
        {
            TehnoparkRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(TehnoparkRuCompany.Instance, city);
            return result;
        }
    }
}