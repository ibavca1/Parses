using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.EurosetRu
{
    public class EurosetRuCity : WebCity
    {
        public static readonly EurosetRuCity Barnaul;
        public static readonly EurosetRuCity Bryansk;
        public static readonly EurosetRuCity Chelyabinsk;
        public static readonly EurosetRuCity Moscow;
        public static readonly EurosetRuCity Petrozavodsk;

        private static readonly Dictionary<WebCity, EurosetRuCity> Cities;

        static EurosetRuCity()
        {
            Barnaul = new EurosetRuCity(WebCities.Barnaul, "Барнаул", "45");
            Bryansk = new EurosetRuCity(WebCities.Bryansk, "Брянск", "76");
            Chelyabinsk = new EurosetRuCity(WebCities.Chelyabinsk, "Челябинск", "627");
            Moscow = new EurosetRuCity(WebCities.Moscow, "Москва", "358");
            Petrozavodsk = new EurosetRuCity(WebCities.Petrozavodsk, "Петрозаводск", "449");
            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private EurosetRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<EurosetRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(EurosetRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(EurosetRuCity))
                        .ToArray();
                return fields.Select(field => (EurosetRuCity)field.GetValue(null));
            }
        }

        public static EurosetRuCity Get(WebCity city)
        {
            EurosetRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(EurosetRuCompany.Instance, city);
            
            return result;
        }
    }
}