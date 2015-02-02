using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.TehnoshokRu
{
    public class TehnoshokRuCity : WebCity
    {
        public static readonly TehnoshokRuCity StPetersburg;
        public static readonly TehnoshokRuCity Kingisepp;
        public static readonly TehnoshokRuCity Murmansk;
        public static readonly TehnoshokRuCity Petrozavodsk; 
        private static readonly Dictionary<WebCity, TehnoshokRuCity> Cities;

        static TehnoshokRuCity()
        {
            StPetersburg = new TehnoshokRuCity(WebCities.StPetersburg, "Санкт-Петербург", "www");
            Kingisepp = new TehnoshokRuCity(WebCities.Kingisepp, "Кингисепп", "kingisepp");
            Murmansk = new TehnoshokRuCity(WebCities.Murmansk, "Мурманск", "murmansk");
            Petrozavodsk = new TehnoshokRuCity(WebCities.Petrozavodsk, "Петрозаводск", "petrozavodsk");
            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private TehnoshokRuCity(WebCity town, string name, string uriPrefix)
            : base(town, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<TehnoshokRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(TehnoshokRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(TehnoshokRuCity))
                        .ToArray();
                return fields.Select(field => (TehnoshokRuCity)field.GetValue(null));
            }
        }

        public static TehnoshokRuCity Get(WebCity city)
        {
            TehnoshokRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(TehnoshokRuCompany.Instance, city);
            return result;
        }
    }
}