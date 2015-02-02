using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.UtinetRu
{
    public class UtinetRuCity : WebCity
    {
        
        public static readonly UtinetRuCity Chelyabinsk;
        public static readonly UtinetRuCity Moscow;
        public static readonly UtinetRuCity StPetersburg;
        private static readonly Dictionary<WebCity, UtinetRuCity> Cities;

        static UtinetRuCity()
        {
            Chelyabinsk = new UtinetRuCity(WebCities.Chelyabinsk, "Челябинск", "chel");
            Moscow = new UtinetRuCity(WebCities.Moscow, "Москва", "");
            StPetersburg = new UtinetRuCity(WebCities.StPetersburg, "Санкт-Петербург", "spb");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private UtinetRuCity(WebCity town, string name, string uriPrefix)
            : base(town, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<UtinetRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(UtinetRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(UtinetRuCity))
                        .ToArray();
                return fields.Select(field => (UtinetRuCity)field.GetValue(null));
            }
        }

        public static UtinetRuCity Get(WebCity city)
        {
            UtinetRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(UtinetRuCompany.Instance, city);
            return result;
        }
    }
}