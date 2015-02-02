using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.RbtRu
{
    public class RbtRuCity : WebCity
    {
        public static readonly RbtRuCity Barnaul;
        public static readonly RbtRuCity Chelyabinsk;
        public static readonly RbtRuCity Ekaterinburg;
        public static readonly RbtRuCity Krasnoyarsk;
        public static readonly RbtRuCity Orenburg;
        public static readonly RbtRuCity Surgut;

        private static readonly Dictionary<WebCity, RbtRuCity> Cities;

        static RbtRuCity()
        {
            Barnaul = new RbtRuCity(WebCities.Barnaul, "Барнаул", "67");
            Chelyabinsk = new RbtRuCity(WebCities.Chelyabinsk, "Челябинск", "15");
            Ekaterinburg = new RbtRuCity(WebCities.Ekaterinburg, "Екатеринбург", "3");
            Krasnoyarsk = new RbtRuCity(WebCities.Krasnoyarsk, "Красноярск", "6");
            Orenburg = new RbtRuCity(WebCities.Orenburg, "Оренбург", "12");
            Surgut = new RbtRuCity(WebCities.Surgut, "Сургут", "13");
            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private RbtRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<RbtRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(RbtRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(RbtRuCity))
                        .ToArray();
                return fields.Select(field => (RbtRuCity)field.GetValue(null));
            }
        }

        public static RbtRuCity Get(WebCity city)
        {
            RbtRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(RbtRuCompany.Instance, city);
            
            return result;
        }
    }
}