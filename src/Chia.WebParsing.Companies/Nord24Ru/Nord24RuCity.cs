using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.Nord24Ru
{
    public class Nord24RuCity : WebCity
    {
        public static readonly Nord24RuCity Chelyabinsk;
        public static readonly Nord24RuCity Ekaterinburg;
        public static readonly Nord24RuCity Surgut;
        public static readonly Nord24RuCity Tyumen;

        private static readonly Dictionary<WebCity, Nord24RuCity> Cities;

        static Nord24RuCity()
        {
            Chelyabinsk = new Nord24RuCity(WebCities.Chelyabinsk, "Челябинск", "f064e8e0876d2d924010a93e90aa4926");
            Ekaterinburg = new Nord24RuCity(WebCities.Ekaterinburg, "Екатеринбург", "a4eb7bd26e0e5a3a35431f1e890429b0");
            Surgut = new Nord24RuCity(WebCities.Surgut, "Сургут", "91164d222057e3ef2841d0ed0f408c0a");
            Tyumen = new Nord24RuCity(WebCities.Tyumen, "Тюмень", "08f4d1f6b599c3e04c055f13c8032002");
            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private Nord24RuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<Nord24RuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(Nord24RuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(Nord24RuCity))
                        .ToArray();
                return fields.Select(field => (Nord24RuCity)field.GetValue(null));
            }
        }

        public static Nord24RuCity Get(WebCity city)
        {
            Nord24RuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(Nord24RuCompany.Instance, city);
            
            return result;
        }
    }
}