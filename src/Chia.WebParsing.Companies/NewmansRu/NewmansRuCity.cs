using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.NewmansRu
{
    public class NewmansRuCity : WebCity
    {
        public static readonly NewmansRuCity Krasnodar;
        public static readonly NewmansRuCity Moscow;
        public static readonly NewmansRuCity StPetersburg;
        public static readonly NewmansRuCity Voronez;

        static NewmansRuCity()
        {
            Krasnodar = new NewmansRuCity(WebCities.Krasnodar, "Краснодар", "krasnodar");
            Moscow = new NewmansRuCity(WebCities.Moscow, "Москва", "msk");
            StPetersburg = new NewmansRuCity(WebCities.StPetersburg, "Санкт-Петербург", "spb");
            Voronez = new NewmansRuCity(WebCities.Voronezh, "Воронеж", "voronezh");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private NewmansRuCity(WebCity city, string name, string uriPrefix) 
            : base(city, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<NewmansRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(NewmansRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(NewmansRuCity))
                        .ToArray();
                return fields.Select(field => (NewmansRuCity)field.GetValue(null));
            }
        }

        public static NewmansRuCity Get(WebCity city)
        {
            NewmansRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(NewmansRuCompany.Instance, city);
            return result;
        }

        private static readonly Dictionary<WebCity, NewmansRuCity> Cities;
    }
}