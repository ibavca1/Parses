using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.RegardRu
{
    public class RegardRuCity : WebCity
    {
        public static readonly RegardRuCity Moscow;
        private static readonly Dictionary<WebCity, RegardRuCity> Cities;

        static RegardRuCity()
        {
            Moscow = new RegardRuCity(WebCities.Moscow, "МОСКВА");

            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private RegardRuCity(WebCity town, string name)
            : base(town, name)
        {
        }

        public static IEnumerable<RegardRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(RegardRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(RegardRuCity))
                        .ToArray();
                return fields.Select(field => (RegardRuCity)field.GetValue(null));
            }
        }

        public static RegardRuCity Get(WebCity city)
        {
            RegardRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(RegardRuCompany.Instance, city);
            
            return result;
        }
    }
}