using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.EldoradoRu
{
    public class EldoradoRuCity : WebCity
    {
        public static readonly EldoradoRuCity Barnaul;
        public static readonly EldoradoRuCity Belgorod;
        public static readonly EldoradoRuCity Bryansk;
        public static readonly EldoradoRuCity Cheboksary;
        public static readonly EldoradoRuCity Chelyabinsk;
        public static readonly EldoradoRuCity Ekaterinburg;
        public static readonly EldoradoRuCity Ivanovo;
        public static readonly EldoradoRuCity Kazan;
        public static readonly EldoradoRuCity Krasnodar;
        public static readonly EldoradoRuCity Krasnoyarsk;
        public static readonly EldoradoRuCity Lipetsk;
        public static readonly EldoradoRuCity Moscow;
        public static readonly EldoradoRuCity NaberezhnyeChelny;
        public static readonly EldoradoRuCity NizhnyNovgorod;
        public static readonly EldoradoRuCity Novosibirsk;
        public static readonly EldoradoRuCity Omsk;
        public static readonly EldoradoRuCity Orenburg;
        public static readonly EldoradoRuCity RostovOnDon;
        public static readonly EldoradoRuCity Ryazan;
        public static readonly EldoradoRuCity Samara;
        public static readonly EldoradoRuCity Saratov;
        public static readonly EldoradoRuCity StPetersburg;
        public static readonly EldoradoRuCity Surgut;
        public static readonly EldoradoRuCity Toliatty;
        public static readonly EldoradoRuCity Ulyanovsk;
        public static readonly EldoradoRuCity Ufa;
        public static readonly EldoradoRuCity Volgograd;
        public static readonly EldoradoRuCity Voronezh;
        public static readonly EldoradoRuCity Yaroslavl;
        public static readonly EldoradoRuCity YoshkarOla;
        private static readonly Dictionary<WebCity, EldoradoRuCity> Cities;

        static EldoradoRuCity()
        {
            Barnaul = new EldoradoRuCity(WebCities.Barnaul, "Барнаул", "11317");
            Belgorod = new EldoradoRuCity(WebCities.Belgorod, "Белгородская область", "15556");
            Bryansk = new EldoradoRuCity(WebCities.Bryansk, "Брянская область", "15557");
            Cheboksary = new EldoradoRuCity(WebCities.Cheboksary, "Республика Чувашия", "15601");
            Chelyabinsk = new EldoradoRuCity(WebCities.Chelyabinsk, "Челябинск", "11298");
            Ekaterinburg = new EldoradoRuCity(WebCities.Ekaterinburg, "Екатеринбург", "11297");
            Ivanovo = new EldoradoRuCity(WebCities.Ivanovo, "Ивановская область", "15560");
            Kazan = new EldoradoRuCity(WebCities.Kazan, "Казань", "11281");
            Krasnodar = new EldoradoRuCity(WebCities.Krasnodar, "Краснодар", "11300");
            Krasnoyarsk = new EldoradoRuCity(WebCities.Krasnoyarsk, "Красноярск", "11294");
            Lipetsk = new EldoradoRuCity(WebCities.Lipetsk, "Липецк", "11305");
            Moscow = new EldoradoRuCity(WebCities.Moscow, "Москва и Подмосковье", "11324");
            NaberezhnyeChelny = new EldoradoRuCity(WebCities.NaberezhnyeChelny, "Набережные Челны", "11284");
            NizhnyNovgorod = new EldoradoRuCity(WebCities.NizhnyNovgorod, "Нижний Новгород", "11289");
            Novosibirsk = new EldoradoRuCity(WebCities.Novosibirsk, "Новосибирск", "11290");
            Omsk = new EldoradoRuCity(WebCities.Omsk, "Омск", "11293");
            Orenburg = new EldoradoRuCity(WebCities.Orenburg, "Оренбургская область", "15605");
            RostovOnDon = new EldoradoRuCity(WebCities.RostovOnDon, "Ростов-на-Дону", "11299");
            //Ryazan = new OldEldoradoRuCity(WebCities.Ryazan, "Рязань", "11281", "rzn");
            Ryazan = new EldoradoRuCity(WebCities.Ryazan, "Рязань", "11360");
            Samara = new EldoradoRuCity(WebCities.Samara, "Самара", "11288");
            Saratov = new EldoradoRuCity(WebCities.Saratov, "Саратов", "11325");
            StPetersburg = new EldoradoRuCity(WebCities.StPetersburg, "Санкт-Петербург", "11279");
            Surgut = new EldoradoRuCity(WebCities.Surgut, "Ханты-Мансийский АО", "15635");
            Toliatty = new EldoradoRuCity(WebCities.Tolyatti, "Тольятти", "11339");
            Ulyanovsk = new EldoradoRuCity(WebCities.Ulyanovsk, "Ульяновск", "11349");
            Ufa = new EldoradoRuCity(WebCities.Ufa, "Уфа", "11285");
            Volgograd = new EldoradoRuCity(WebCities.Volgograd, "Волгоград", "11306");
            Voronezh = new EldoradoRuCity(WebCities.Voronezh, "Воронеж", "11302");
            Yaroslavl = new EldoradoRuCity(WebCities.Yaroslavl, "Ярославль", "11372");
            YoshkarOla = new EldoradoRuCity(WebCities.YoshkarOla, "Республика Марий Эл", "15597");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        protected EldoradoRuCity(WebCity town, string name, string cookieValue)
            : base(town, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<EldoradoRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(EldoradoRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType.IsAssignableFrom(typeof(EldoradoRuCity)))
                        .ToArray();
                return fields.Select(field => (EldoradoRuCity)field.GetValue(null));
            }
        }

        public static EldoradoRuCity Get(WebCity city)
        {
            EldoradoRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(EldoradoRuCompany.Instance, city);

            return result;
        }
    }
}