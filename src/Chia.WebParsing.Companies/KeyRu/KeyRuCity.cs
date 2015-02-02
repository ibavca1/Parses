using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.KeyRu
{
    public class KeyRuCity : WebCity
    {
        public static readonly KeyRuCity StPetersburg;
        public static readonly KeyRuCity VelikyNovgorod;
        public static readonly KeyRuCity Cherepovets;
        public static readonly KeyRuCity Voronez;
        public static readonly KeyRuCity Petrozavodsk;
        public static readonly KeyRuCity Belgorod;
        public static readonly KeyRuCity Lipetsk;
        public static readonly KeyRuCity Yaroslavl;
        public static readonly KeyRuCity Taganrog;
        public static readonly KeyRuCity Gatchina;

        static KeyRuCity()
        {
            StPetersburg = new KeyRuCity(WebCities.StPetersburg, 1063, "Санкт-Петербург");
            VelikyNovgorod = new KeyRuCity(WebCities.VelikyNovgorod, 1064, "Великий Новгород");
            Cherepovets = new KeyRuCity(WebCities.Cherepovets, 1066, "Череповец");
            Voronez = new KeyRuCity(WebCities.Voronezh, 1065, "Воронеж");
            Petrozavodsk = new KeyRuCity(WebCities.Petrozavodsk, 1067, "Петрозаводск");
            Belgorod = new KeyRuCity(WebCities.Belgorod, 1068, "Белгород");
            Lipetsk = new KeyRuCity(WebCities.Lipetsk, 1069, "Липецк");
            Yaroslavl = new KeyRuCity(WebCities.Yaroslavl, 1071, "Ярославль");
            Taganrog = new KeyRuCity(WebCities.Taganrog, 23972, "Таганрог");
            Gatchina = new KeyRuCity(WebCities.Gatchina, 3449935, "Гатчина");

            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private KeyRuCity(WebCity city, int cityId, string name)
            : base(city, name)
        {
            CityId = cityId.ToString(CultureInfo.InvariantCulture);
        }

        public string CityId { get; private set; }

        public static IEnumerable<KeyRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(KeyRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(KeyRuCity))
                        .ToArray();
                return fields.Select(field => (KeyRuCity)field.GetValue(null));
            }
        }

        public static KeyRuCity Get(WebCity city)
        {
            KeyRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(KeyRuCompany.Instance, city);

            return result;
        }

        private static readonly Dictionary<WebCity, KeyRuCity> Cities;
    }
}