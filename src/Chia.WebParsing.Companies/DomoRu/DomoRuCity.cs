using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.DomoRu
{
    public class DomoRuCity : WebCity
    {
        public static readonly DomoRuCity Cheboksary;
        public static readonly DomoRuCity Kazan;
        public static readonly DomoRuCity NaberezhnyeChelny;

        private static readonly Dictionary<WebCity, DomoRuCity> Cities;

        static DomoRuCity()
        {
            Cheboksary = new DomoRuCity(WebCities.Cheboksary, "Чебоксары", "cheboksary", 10652);
            Kazan = new DomoRuCity(WebCities.Kazan, "Казань", "www", 10151);
            NaberezhnyeChelny = new DomoRuCity(WebCities.NaberezhnyeChelny, "Набережные Челны", "chelny", 10656);
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private DomoRuCity(WebCity town, string name, string uriPrefix, int storeId)
            : base(town, name)
        {
            StoreId = storeId;
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public int StoreId { get; private set; }

        public static IEnumerable<DomoRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(DomoRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(DomoRuCity))
                        .ToArray();
                return fields.Select(field => (DomoRuCity)field.GetValue(null));
            }
        }

        public static DomoRuCity Get(WebCity city)
        {
            DomoRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(DomoRuCompany.Instance, city);

            return result;
        }
    }
}