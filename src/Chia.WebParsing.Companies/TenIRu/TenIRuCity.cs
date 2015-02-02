using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.TenIRu
{
    public class TenIRuCity : WebCity
    {
        public static readonly TenIRuCity Moscow;

        static TenIRuCity()
        {
            Moscow = new TenIRuCity(WebCities.Moscow, "Москва", "259749");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private TenIRuCity(WebCity town, string name, string internalId)
            : base(town, name)
        {
            InternalId = internalId;
        }

        public string InternalId { get; private set; }

        public static IEnumerable<TenIRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(TenIRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(TenIRuCity))
                        .ToArray();
                return fields.Select(field => (TenIRuCity)field.GetValue(null));
            }
        }

        public static TenIRuCity Get(WebCity city)
        {
            TenIRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(TenIRuCompany.Instance, city);
            return result;
        }

        private static readonly Dictionary<WebCity, TenIRuCity> Cities;
    }
}