using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.CentrBtRu
{
    public class CentrBtRuCity : WebCity
    {
        public static readonly CentrBtRuCity Moscow;
        public static readonly CentrBtRuCity Ekaterinburg;

        static CentrBtRuCity()
        {
            Moscow = new CentrBtRuCity(WebCities.Moscow, "Москва", 1);
            Ekaterinburg = new CentrBtRuCity(WebCities.Ekaterinburg, "Екатеринбург", 2);

            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private CentrBtRuCity(WebCity city, string name, int internalId)
            : base(city, name)
        {
            InternalId = internalId.ToString();
        }

        public string InternalId { get; private set; }

        public static IEnumerable<CentrBtRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(CentrBtRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(CentrBtRuCity))
                        .ToArray();
                return fields.Select(field => (CentrBtRuCity)field.GetValue(null));
            }
        }

        public static CentrBtRuCity Get(WebCity city)
        {
            CentrBtRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(CentrBtRuCompany.Instance, city);
            return result;
        }

        private static readonly Dictionary<WebCity, CentrBtRuCity> Cities;
    }
}