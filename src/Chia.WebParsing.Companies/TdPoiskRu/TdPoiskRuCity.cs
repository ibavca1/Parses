using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.TdPoiskRu
{
    public class TdPoiskRuCity : WebCity
    {
        public static readonly TdPoiskRuCity Krasnodar;
        public static readonly TdPoiskRuCity RostovOnDon;

        static TdPoiskRuCity()
        {
            Krasnodar = new TdPoiskRuCity(WebCities.Krasnodar, "Краснодар", "krasnodar");
            RostovOnDon = new TdPoiskRuCity(WebCities.RostovOnDon, "Ростов-на-Дону", "rostov");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private TdPoiskRuCity(WebCity city, string name, string uriPrefix) 
            : base(city, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<TdPoiskRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(TdPoiskRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(TdPoiskRuCity))
                        .ToArray();
                return fields.Select(field => (TdPoiskRuCity)field.GetValue(null));
            }
        }

        public static TdPoiskRuCity Get(WebCity city)
        {
            TdPoiskRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(TdPoiskRuCompany.Instance, city);
            return result;
        }

        private static readonly Dictionary<WebCity, TdPoiskRuCity> Cities;
    }
}