using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.ElectrovenikRu
{
    public class ElectrovenikRuCity : WebCity
    {
        public static readonly ElectrovenikRuCity Moscow;
        public static readonly ElectrovenikRuCity Chelyabinsk;

        static ElectrovenikRuCity()
        {
            Moscow = new ElectrovenikRuCity(WebCities.Moscow, "Москва и МО", "www");
            Chelyabinsk = new ElectrovenikRuCity(WebCities.Chelyabinsk, "Челябинск", "chelyabinsk");

            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        public ElectrovenikRuCity(WebCity city, string name, string uriPrefix)
            : base(city, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<ElectrovenikRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(ElectrovenikRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(ElectrovenikRuCity))
                        .ToArray();
                return fields.Select(field => (ElectrovenikRuCity)field.GetValue(null));
            }
        }

        public static ElectrovenikRuCity Get(WebCity city)
        {
            ElectrovenikRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(ElectrovenikRuCompany.Instance, city);
            return result;
        }

        private static readonly Dictionary<WebCity, ElectrovenikRuCity> Cities;
    }
}