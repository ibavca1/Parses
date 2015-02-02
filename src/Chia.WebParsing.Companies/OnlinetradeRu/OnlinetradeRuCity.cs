using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.OnlinetradeRu
{
    public class OnlinetradeRuCity : WebCity
    {
        public static readonly OnlinetradeRuCity Moscow;
        public static readonly OnlinetradeRuCity StPetersburg;
        public static readonly OnlinetradeRuCity Tula;
        public static readonly OnlinetradeRuCity Tver;
        public static readonly OnlinetradeRuCity NizhnyNovgorod;
        public static readonly OnlinetradeRuCity Ekaterinburg;
        public static readonly OnlinetradeRuCity Chelyabinsk;
        public static readonly OnlinetradeRuCity Krasnodar;
        private static readonly Dictionary<WebCity, OnlinetradeRuCity> Cities;

        static OnlinetradeRuCity()
        {
            Moscow = new OnlinetradeRuCity(WebCities.Moscow, "Москва и МО", "www");
            StPetersburg = new OnlinetradeRuCity(WebCities.StPetersburg, "Санкт-Петербург", "spb");
            Tver = new OnlinetradeRuCity(WebCities.Tver, "Тверь", "tver");
            Tula = new OnlinetradeRuCity(WebCities.Tula, "Тула", "tula");
            NizhnyNovgorod = new OnlinetradeRuCity(WebCities.NizhnyNovgorod, "Нижний Новгород", "nn");
            Ekaterinburg = new OnlinetradeRuCity(WebCities.Ekaterinburg, "Екатеринбург", "ekb");
            Chelyabinsk = new OnlinetradeRuCity(WebCities.Chelyabinsk, "Челябинск", "chel");
            Krasnodar = new OnlinetradeRuCity(WebCities.Krasnodar, "Краснодар", "krasnodar");

            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private OnlinetradeRuCity(WebCity town, string name, string uriPrefix)
            : base(town, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<OnlinetradeRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(OnlinetradeRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(OnlinetradeRuCity))
                        .ToArray();
                return fields.Select(field => (OnlinetradeRuCity)field.GetValue(null));
            }
        }

        public static OnlinetradeRuCity Get(WebCity city)
        {
            OnlinetradeRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(OnlinetradeRuCompany.Instance, city);
            
            return result;
        }
    }
}