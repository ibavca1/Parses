using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.Oo3Ru
{
    public class Oo3RuCity : WebCity
    {
        public static readonly Oo3RuCity Moscow;
        public static readonly Oo3RuCity StPetersburg;
        private static readonly Dictionary<WebCity, Oo3RuCity> Cities;

        static Oo3RuCity()
        {
            Moscow = new Oo3RuCity(WebCities.Moscow, "Москва", "e0e2eaf1eecc", 77, "e0e2eaf1eecc", 2);
            StPetersburg = new Oo3RuCity(WebCities.StPetersburg, "Санкт-Петербург", "e3f0f3e1f0e5f2e5cf2df2eaede0d1", 78, "e3f0f3e1f0e5f2e5cf2df2eaede0d1", 1);
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private Oo3RuCity(WebCity city, string name, string regionCode, int regionId, string cityCode, int cityId)
            : base(city, name)
        {
            RegionCode = regionCode;
            RegionId = regionId;
            CityCode = cityCode;
            CityId = cityId;
        }

        public string RegionCode { get; private set; }

        public int RegionId { get; private set; }

        public string CityCode { get; private set; }

        public int CityId { get; private set; }

        public static IEnumerable<Oo3RuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(Oo3RuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(Oo3RuCity))
                        .ToArray();
                return fields.Select(field => (Oo3RuCity)field.GetValue(null));
            }
        }

        public static Oo3RuCity Get(WebCity city)
        {
            Oo3RuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(Oo3RuCompany.Instance, city);
            return result;
        }
    }
}