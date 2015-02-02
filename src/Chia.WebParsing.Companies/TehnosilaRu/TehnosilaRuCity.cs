using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.TehnosilaRu
{
    public class TehnosilaRuCity : WebCity
    {
        public static readonly TehnosilaRuCity Belgorod;
        public static readonly TehnosilaRuCity Ivanovo;
        public static readonly TehnosilaRuCity Kaluga;
        public static readonly TehnosilaRuCity Krasnodar;
        public static readonly TehnosilaRuCity Lipetsk;
        public static readonly TehnosilaRuCity Moscow;
        public static readonly TehnosilaRuCity Novosibirsk;
        public static readonly TehnosilaRuCity Nizhnekamsk;
        public static readonly TehnosilaRuCity NaberezhnyeChelny;
        public static readonly TehnosilaRuCity Omsk;
        public static readonly TehnosilaRuCity Perm;
        public static readonly TehnosilaRuCity RostovNaDonu;
        public static readonly TehnosilaRuCity Ryazan;
        public static readonly TehnosilaRuCity Samara;
        public static readonly TehnosilaRuCity StPetersburg;
        public static readonly TehnosilaRuCity Serpuhov;
        public static readonly TehnosilaRuCity Tambov;
        public static readonly TehnosilaRuCity Tyumen;
        public static readonly TehnosilaRuCity Tver;
        public static readonly TehnosilaRuCity Ulyanovsk;
        public static readonly TehnosilaRuCity Ufa;
        public static readonly TehnosilaRuCity Voronezh;
        public static readonly TehnosilaRuCity Yaroslavl;
        private static readonly Dictionary<WebCity, TehnosilaRuCity> Cities;

        static TehnosilaRuCity()
        {
            Belgorod = new TehnosilaRuCity(WebCities.Belgorod, "belgorod", "Белгород");
            Lipetsk = new TehnosilaRuCity(WebCities.Lipetsk, "lipetsk", "Липецк");
            Moscow = new TehnosilaRuCity(WebCities.Moscow, "www", "Москва");
            Ivanovo = new TehnosilaRuCity(WebCities.Ivanovo, "ivanovo", "Иваново");
            Kaluga = new TehnosilaRuCity(WebCities.Kaluga, "kaluga", "Калуга");
            StPetersburg = new TehnosilaRuCity(WebCities.StPetersburg, "sankt-peterburg", "Санкт-Петербург");
            Serpuhov = new TehnosilaRuCity(WebCities.Serpuhov, "serpuhov", "Серпухов");
            Tambov = new TehnosilaRuCity(WebCities.Tambov, "tambov", "Тамбов");
            Tver = new TehnosilaRuCity(WebCities.Tver, "tver", "Тверь");
            Krasnodar = new TehnosilaRuCity(WebCities.Krasnodar, "krasnodar", "Краснодар");
            RostovNaDonu = new TehnosilaRuCity(WebCities.RostovOnDon, "rostov-na-donu", "Ростов-на-Дону");
            Novosibirsk = new TehnosilaRuCity(WebCities.Novosibirsk, "novosibirsk", "Новосибирск");
            NaberezhnyeChelny = new TehnosilaRuCity(WebCities.NaberezhnyeChelny, "chelny", "Набережные Челны");
            Nizhnekamsk = new TehnosilaRuCity(WebCities.Nizhnekamsk, "enizhnekamsk", "Нижнекамск");
            Omsk = new TehnosilaRuCity(WebCities.Omsk, "omsk", "Омск");
            Perm = new TehnosilaRuCity(WebCities.Perm, "perm", "Пермь");
            Voronezh = new TehnosilaRuCity(WebCities.Voronezh, "voronezh", "Воронеж");
            Ryazan = new TehnosilaRuCity(WebCities.Ryazan, "ryazan", "Рязань");
            Tyumen = new TehnosilaRuCity(WebCities.Tyumen, "tyumen", "Тюмень");
            Ufa = new TehnosilaRuCity(WebCities.Ufa, "ufa1", "Уфа");
            Ulyanovsk = new TehnosilaRuCity(WebCities.Ulyanovsk, "ulyanovsk", "Ульяновск");
            Samara = new TehnosilaRuCity(WebCities.Samara, "samara", "Самара");
            Yaroslavl = new TehnosilaRuCity(WebCities.Yaroslavl, "yaroslavl", "Ярославль");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private TehnosilaRuCity(WebCity town, string uriPrefix, string name)
            : base(town, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<TehnosilaRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(TehnosilaRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(TehnosilaRuCity))
                        .ToArray();
                return fields.Select(field => (TehnosilaRuCity)field.GetValue(null));
            }
        }

        public static TehnosilaRuCity Get(WebCity city)
        {
            TehnosilaRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(TehnosilaRuCompany.Instance, city);
            return result;
        }
    }
}