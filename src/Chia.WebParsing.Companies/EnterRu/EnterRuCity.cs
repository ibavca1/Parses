using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.EnterRu
{
    public class EnterRuCity : WebCity
    {
        public static readonly EnterRuCity Bryansk;
        public static readonly EnterRuCity Moscow;
        public static readonly EnterRuCity NizhnyNovgorod;
        public static readonly EnterRuCity Chelyabinsk;
        public static readonly EnterRuCity Petrozavodsk;
        public static readonly EnterRuCity Saratov;
        public static readonly EnterRuCity StPetersburg;
        public static readonly EnterRuCity Volgograd;

        private static readonly Dictionary<WebCity, EnterRuCity> Cities;

        static EnterRuCity()
        {
            Bryansk = new EnterRuCity(WebCities.Bryansk, "Брянск", "83210");
            Moscow = new EnterRuCity(WebCities.Moscow, "Москва", "14974");
            NizhnyNovgorod = new EnterRuCity(WebCities.NizhnyNovgorod, "Нижний Новгород", "99958");
            Chelyabinsk = new EnterRuCity(WebCities.Chelyabinsk, "Челябинск", "93752");
            Petrozavodsk = new EnterRuCity(WebCities.Petrozavodsk, "Петрозаводск", "124213");
            Saratov = new EnterRuCity(WebCities.Saratov, "Саратов", "124201");
            StPetersburg = new EnterRuCity(WebCities.StPetersburg, "Санкт-Петербург", "108136");
            Volgograd = new EnterRuCity(WebCities.Volgograd, "Волгоград", "143707");
            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private EnterRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<EnterRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(EnterRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(EnterRuCity))
                        .ToArray();
                return fields.Select(field => (EnterRuCity)field.GetValue(null));
            }
        }

        public static EnterRuCity Get(WebCity city)
        {
            EnterRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(EnterRuCompany.Instance, city);
            
            return result;
        }
    }
}