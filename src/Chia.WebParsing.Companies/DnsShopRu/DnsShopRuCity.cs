using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.DnsShopRu
{
    public class DnsShopRuCity : WebCity
    {
        public static readonly DnsShopRuCity Barnaul;
        public static readonly DnsShopRuCity Belgorod;
        public static readonly DnsShopRuCity Chelyabinsk;
        public static readonly DnsShopRuCity Ekaterinburg;
        public static readonly DnsShopRuCity Ivanovo;
        public static readonly DnsShopRuCity Kazan;
        public static readonly DnsShopRuCity Krasnodar;
        public static readonly DnsShopRuCity Krasnoyarsk;
        public static readonly DnsShopRuCity Lipetsk;
        public static readonly DnsShopRuCity Moscow;
        public static readonly DnsShopRuCity NaberezhnyeChelny;
        public static readonly DnsShopRuCity NizhnyNovgorod;
        public static readonly DnsShopRuCity Novosibirsk;
        public static readonly DnsShopRuCity Omsk;
        public static readonly DnsShopRuCity Orenburg;
        public static readonly DnsShopRuCity Petrozavodsk;
        public static readonly DnsShopRuCity Ryazan;
        public static readonly DnsShopRuCity RostovOnDon;
        public static readonly DnsShopRuCity Samara;
        public static readonly DnsShopRuCity Saratov;
        public static readonly DnsShopRuCity Surgut;
        public static readonly DnsShopRuCity StPetersburg;
        public static readonly DnsShopRuCity Tolyatty;
        public static readonly DnsShopRuCity Ulyanovsk;
        public static readonly DnsShopRuCity Ufa;
        public static readonly DnsShopRuCity Voronezh;
        public static readonly DnsShopRuCity Volgograd;
        public static readonly DnsShopRuCity Yaroslavl;
        public static readonly DnsShopRuCity YoshkarOla;
        
        private static readonly Dictionary<WebCity, DnsShopRuCity> Cities;

        static DnsShopRuCity()
        {
            Barnaul = new DnsShopRuCity(WebCities.Barnaul, "Барнаул", "barnaul");
            Belgorod = new DnsShopRuCity(WebCities.Belgorod, "Белгород", "belgorod");
            Chelyabinsk = new DnsShopRuCity(WebCities.Chelyabinsk, "Челябинск", "chelyabinsk");
            Ekaterinburg = new DnsShopRuCity(WebCities.Ekaterinburg, "Екатеринбург", "ekaterinburg");
            Ivanovo = new DnsShopRuCity(WebCities.Ivanovo, "Иваново", "ivanovo");
            Kazan = new DnsShopRuCity(WebCities.Kazan, "Казань", "kazan");
            Krasnodar = new DnsShopRuCity(WebCities.Krasnodar, "Краснодар", "krasnodar");
            Krasnoyarsk = new DnsShopRuCity(WebCities.Krasnoyarsk, "Красноярск", "krasnoyarsk");
            Lipetsk = new DnsShopRuCity(WebCities.Lipetsk, "Липецк", "lipetsk");
            Moscow = new DnsShopRuCity(WebCities.Moscow, "Москва", "moscow");
            NaberezhnyeChelny = new DnsShopRuCity(WebCities.NaberezhnyeChelny, "Набережные Челны", "naberezhnye-chelny");
            NizhnyNovgorod = new DnsShopRuCity(WebCities.NizhnyNovgorod, "Нижний Новгород", "nizhniy-novgorod");
            Novosibirsk = new DnsShopRuCity(WebCities.Novosibirsk, "Новосибирск", "novosibirsk");
            Omsk = new DnsShopRuCity(WebCities.Omsk, "Омск", "omsk");
            Orenburg = new DnsShopRuCity(WebCities.Orenburg, "Оренбург", "orenburg");
            Petrozavodsk = new DnsShopRuCity(WebCities.Petrozavodsk, "Петрозаводск", "petrozavodsk");
            Ryazan = new DnsShopRuCity(WebCities.Ryazan, "Рязань", "ryazan");
            RostovOnDon = new DnsShopRuCity(WebCities.RostovOnDon, "Ростов-на-Дону", "rostov-na-donu");
            Samara = new DnsShopRuCity(WebCities.Samara, "Самара", "samara");
            Saratov = new DnsShopRuCity(WebCities.Saratov, "Саратов", "saratov");
            StPetersburg = new DnsShopRuCity(WebCities.StPetersburg, "Санкт-Петербург", "spb");
            Surgut = new DnsShopRuCity(WebCities.Surgut, "Сургут", "surgut");
            Tolyatty = new DnsShopRuCity(WebCities.Tolyatti, "Тольятти", "tolyatti");
            Ulyanovsk = new DnsShopRuCity(WebCities.Ulyanovsk, "Ульяновск", "ulyanovsk");
            Ufa = new DnsShopRuCity(WebCities.Ufa, "Уфа", "ufa");
            Voronezh = new DnsShopRuCity(WebCities.Voronezh, "Воронеж", "voronezh");
            Volgograd = new DnsShopRuCity(WebCities.Volgograd, "Волгоград", "volgograd");
            Yaroslavl = new DnsShopRuCity(WebCities.Yaroslavl, "Ярославль", "yaroslavl");
            YoshkarOla = new DnsShopRuCity(WebCities.YoshkarOla, "Йошкар-Ола", "yoshkar-ola");

            Cities = All.ToDictionary(c => (WebCity) c, c => c);
        }

        private DnsShopRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<DnsShopRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(DnsShopRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(DnsShopRuCity))
                        .ToArray();
                return fields.Select(field => (DnsShopRuCity)field.GetValue(null));
            }
        }

        public static DnsShopRuCity Get(WebCity city)
        {
            DnsShopRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(DnsShopRuCompany.Instance, city);
            
            return result;
        }
    }
}