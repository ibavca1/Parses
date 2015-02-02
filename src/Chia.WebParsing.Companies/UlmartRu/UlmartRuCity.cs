using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.UlmartRu
{
    public class UlmartRuCity : WebCity
    {
        public static readonly UlmartRuCity Belgorod;
        public static readonly UlmartRuCity Cheboksary;
        public static readonly UlmartRuCity Ivanovo;
        public static readonly UlmartRuCity Krasnodar;
        public static readonly UlmartRuCity Lipetsk;
        public static readonly UlmartRuCity Moscow;
        public static readonly UlmartRuCity NaberezhnyeChelny;
        public static readonly UlmartRuCity NizhnyNovgorod;
        public static readonly UlmartRuCity Petrozavodsk;
        public static readonly UlmartRuCity Ryazan;
        public static readonly UlmartRuCity RostovOnDon;
        public static readonly UlmartRuCity Samara;
        public static readonly UlmartRuCity Saratov;
        public static readonly UlmartRuCity StPetersburg;
        public static readonly UlmartRuCity Toliatty;
        public static readonly UlmartRuCity Ulyanovsk;
        public static readonly UlmartRuCity Voronezh;
        public static readonly UlmartRuCity Yaroslavl;
        public static readonly UlmartRuCity YoshkarOla;
        private static readonly Dictionary<WebCity, UlmartRuCity> Cities;

        static UlmartRuCity()
        {
            Belgorod = new UlmartRuCity(WebCities.Belgorod, "Белгород", "1722");
            Cheboksary = new UlmartRuCity(WebCities.Cheboksary, "Чебоксары", "1501");
            Ivanovo = new UlmartRuCity(WebCities.Ivanovo, "Иваново", "421");
            Krasnodar = new UlmartRuCity(WebCities.Krasnodar, "Краснодар", "281");
            Lipetsk = new UlmartRuCity(WebCities.Lipetsk, "Липецк", "1687");
            Moscow = new UlmartRuCity(WebCities.Moscow, "Москва", "18414");
            NaberezhnyeChelny = new UlmartRuCity(WebCities.NaberezhnyeChelny, "Набережные Челны", "741");
            NizhnyNovgorod = new UlmartRuCity(WebCities.NizhnyNovgorod, "Нижний Новгород", "321");
            Petrozavodsk = new UlmartRuCity(WebCities.Petrozavodsk, "Петрозаводск", "301");
            Ryazan = new UlmartRuCity(WebCities.Ryazan, "Рязань", "341");
            RostovOnDon = new UlmartRuCity(WebCities.RostovOnDon, "Ростов-на-Дону", "401");
            Samara = new UlmartRuCity(WebCities.Samara, "Самара", "681");
            Saratov = new UlmartRuCity(WebCities.Saratov, "Саратов", "881");
            StPetersburg = new UlmartRuCity(WebCities.StPetersburg, "Санкт-Петербург", "18413");
            Toliatty = new UlmartRuCity(WebCities.Tolyatti, "Тольятти", "1182");
            Ulyanovsk = new UlmartRuCity(WebCities.Ulyanovsk, "Ульяновск", "1708");
            Voronezh = new UlmartRuCity(WebCities.Voronezh, "Воронеж", "1021");
            Yaroslavl = new UlmartRuCity(WebCities.Yaroslavl, "Ярославль", "101");
            YoshkarOla = new UlmartRuCity(WebCities.YoshkarOla, "Йошкар-Ола", "1833");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private UlmartRuCity(WebCity other, string name, string cookieValue)
            : base(other, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<UlmartRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(UlmartRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(UlmartRuCity))
                        .ToArray();
                return fields.Select(field => (UlmartRuCity)field.GetValue(null));
            }
        }

        public static UlmartRuCity Get(WebCity city)
        {
            UlmartRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(UlmartRuCompany.Instance, city);
            return result;
        }
    }
}