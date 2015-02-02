using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.MvideoRu
{
    public class MvideoRuCity : WebCity
    {
        public static readonly MvideoRuCity Arhangelsk;
        public static readonly MvideoRuCity Barnaul;
        public static readonly MvideoRuCity Belgorod;
        public static readonly MvideoRuCity Bryansk;
        public static readonly MvideoRuCity Cheboksary;
        public static readonly MvideoRuCity Chelyabinsk;
        public static readonly MvideoRuCity Ekaterinburg;
        public static readonly MvideoRuCity Ivanovo;
        public static readonly MvideoRuCity Kaluga;
        public static readonly MvideoRuCity Kazan;
        public static readonly MvideoRuCity Krasnodar;
        public static readonly MvideoRuCity Krasnoyarsk;
        public static readonly MvideoRuCity Kirov;
        public static readonly MvideoRuCity Lipetsk;
        public static readonly MvideoRuCity Magnitogorsk;
        public static readonly MvideoRuCity Moscow;
        public static readonly MvideoRuCity Murmansk;
        public static readonly MvideoRuCity NaberezhnyeChelny;
        public static readonly MvideoRuCity Neftekamsk;
        public static readonly MvideoRuCity NizhnyNovgorod;
        public static readonly MvideoRuCity Novosibirsk;
        public static readonly MvideoRuCity Obninsk;
        public static readonly MvideoRuCity Omsk;
        public static readonly MvideoRuCity Orenburg;
        public static readonly MvideoRuCity Petrozavodsk;
        public static readonly MvideoRuCity Perm;
        public static readonly MvideoRuCity Rostov;
        public static readonly MvideoRuCity Ryazan;
        public static readonly MvideoRuCity Samara;
        public static readonly MvideoRuCity Saratov;
        public static readonly MvideoRuCity Severodvinsk;
        public static readonly MvideoRuCity StPetersburg;
        public static readonly MvideoRuCity Surgut;
        public static readonly MvideoRuCity Syktyvkar;
        public static readonly MvideoRuCity Tambov;
        public static readonly MvideoRuCity Tolyatti;
        public static readonly MvideoRuCity Tver;
        public static readonly MvideoRuCity Tyumen;
        public static readonly MvideoRuCity Voronezh;
        public static readonly MvideoRuCity Ufa;
        public static readonly MvideoRuCity Ulyanovsk;
        public static readonly MvideoRuCity Volgograd;
        public static readonly MvideoRuCity Yaroslavl;
        public static readonly MvideoRuCity YoshkarOla;

        private static readonly Dictionary<WebCity, MvideoRuCity> Cities;

        static MvideoRuCity()
        {
            Arhangelsk = new MvideoRuCity(WebCities.Arhangelsk, "Архангельск", "CityCZ_9915");
            Barnaul = new MvideoRuCity(WebCities.Barnaul, "Барнаул", "CityCZ_9912");
            Belgorod = new MvideoRuCity(WebCities.Belgorod, "Белгород", "CityCZ_15507");
            Bryansk = new MvideoRuCity(WebCities.Bryansk, "Брянск", "CityCZ_15535");
            Chelyabinsk = new MvideoRuCity(WebCities.Chelyabinsk, "Челябинск", "CityCZ_1216");
            Cheboksary = new MvideoRuCity(WebCities.Cheboksary, "Чебоксары", "CityR_67");
            Ekaterinburg = new MvideoRuCity(WebCities.Ekaterinburg, "Екатеринбург", "CityCZ_2030");
            Ivanovo = new MvideoRuCity(WebCities.Ivanovo, "Иваново", "CityCZ_15528");
            Kaluga = new MvideoRuCity(WebCities.Kaluga, "Калуга", "CityR_106");
            Kazan = new MvideoRuCity(WebCities.Kazan, "Казань", "CityCZ_1458");
            Krasnodar = new MvideoRuCity(WebCities.Krasnodar, "Краснодар", "CityCZ_2128");
            Krasnoyarsk = new MvideoRuCity(WebCities.Krasnoyarsk, "Красноярск", "CityCZ_1854");
            Kirov = new MvideoRuCity(WebCities.Kirov, "Киров", "CityCZ_15556");
            Lipetsk = new MvideoRuCity(WebCities.Lipetsk, "Липецк", "CityCZ_15500");
            Magnitogorsk = new MvideoRuCity(WebCities.Magnitogorsk, "Магнитогорск", "CityR_27");
            Moscow = new MvideoRuCity(WebCities.Moscow, "Москва", "CityCZ_975");
            Murmansk = new MvideoRuCity(WebCities.Murmansk, "Мурманск", "CityR_144");
            NaberezhnyeChelny = new MvideoRuCity(WebCities.NaberezhnyeChelny, "Набережные Челны", "CityCZ_15563");
            Neftekamsk = new MvideoRuCity(WebCities.Neftekamsk, "Нефтекамск", "CityR_96");
            NizhnyNovgorod = new MvideoRuCity(WebCities.NizhnyNovgorod, "Нижний Новгород", "CityCZ_974");
            Novosibirsk = new MvideoRuCity(WebCities.Novosibirsk, "Новосибирск", "CityCZ_2246");
            Obninsk = new MvideoRuCity(WebCities.Obninsk, "Обнинск", "CityR_82");
            Omsk = new MvideoRuCity(WebCities.Omsk, "Омск", "CityCZ_9909");
            Orenburg = new MvideoRuCity(WebCities.Orenburg, "Оренбург", "CityCZ_6276");
            Petrozavodsk = new MvideoRuCity(WebCities.Petrozavodsk, "Петрозаводск", "CityR_122");
            Perm = new MvideoRuCity(WebCities.Perm, "Пермь", "CityCZ_1250");
            Rostov = new MvideoRuCity(WebCities.RostovOnDon, "Ростов-на-Дону", "CityCZ_2446");
            Ryazan = new MvideoRuCity(WebCities.Ryazan, "Рязань", "CityCZ_7179");
            Saratov = new MvideoRuCity(WebCities.Saratov, "Саратов", "CityCZ_984");
            Samara = new MvideoRuCity(WebCities.Samara, "Самара", "CityCZ_1780");
            Severodvinsk = new MvideoRuCity(WebCities.Severodvinsk, "Северодвинск", "CityR_120");
            StPetersburg = new MvideoRuCity(WebCities.StPetersburg, "Санкт-Петербург", "CityCZ_1638");
            Surgut = new MvideoRuCity(WebCities.Surgut, "Сургут", "CityCZ_9963");
            Syktyvkar = new MvideoRuCity(WebCities.Syktyvkar, "Сыктывкар", "CityR_102");
            Tambov = new MvideoRuCity(WebCities.Tambov, "Тамбов", "CityCZ_15598");
            Tver = new MvideoRuCity(WebCities.Tver, "Тверь", "CityR_88");
            Tyumen = new MvideoRuCity(WebCities.Tyumen, "Тюмень", "CityCZ_1024");
            Tolyatti = new MvideoRuCity(WebCities.Tolyatti, "Тольятти", "CityCZ_6270");
            Voronezh = new MvideoRuCity(WebCities.Voronezh, "Воронеж", "CityCZ_7173");
            Yaroslavl = new MvideoRuCity(WebCities.Yaroslavl, "Ярославль", "CityCZ_7176");
            Ufa = new MvideoRuCity(WebCities.Ufa, "Уфа", "CityCZ_2534");
            Ulyanovsk = new MvideoRuCity(WebCities.Ulyanovsk, "Ульяновск", "CityCZ_15570");
            Volgograd = new MvideoRuCity(WebCities.Volgograd, "Волгоград", "CityCZ_1272");
            YoshkarOla = new MvideoRuCity(WebCities.YoshkarOla, "Йошкар-Ола", "CityR_72");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private MvideoRuCity(WebCity city, string name, string cookieValue)
            : base(city, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<MvideoRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(MvideoRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(MvideoRuCity))
                        .ToArray();
                return fields.Select(field => (MvideoRuCity)field.GetValue(null));
            }
        }

        public static MvideoRuCity Get(WebCity city)
        {
            MvideoRuCity result;

            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(WebCompanies.MvideoRu, city);

            return result;
        }

        public static bool TryGet(WebCity city, out MvideoRuCity result)
        {
            return Cities.TryGetValue(city, out result);
        }
    }
}