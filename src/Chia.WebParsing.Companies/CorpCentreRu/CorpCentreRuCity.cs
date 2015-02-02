using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.CorpCentreRu
{
    public class CorpCentreRuCity : WebCity
    {
        public static readonly CorpCentreRuCity Barnaul;
        public static readonly CorpCentreRuCity Cheboksary;
        public static readonly CorpCentreRuCity Ekaterinburg;
        public static readonly CorpCentreRuCity Izhevsk;
        public static readonly CorpCentreRuCity Kemerovo;
        public static readonly CorpCentreRuCity Kirov;
        public static readonly CorpCentreRuCity Kurgan;
        public static readonly CorpCentreRuCity Novosibirsk;
        public static readonly CorpCentreRuCity Orenburg;
        public static readonly CorpCentreRuCity Penza;
        public static readonly CorpCentreRuCity Perm;
        public static readonly CorpCentreRuCity Syktyvkar;
        public static readonly CorpCentreRuCity Saransk;
        public static readonly CorpCentreRuCity Samara;
        public static readonly CorpCentreRuCity Tomsk;
        public static readonly CorpCentreRuCity Tolyatti;
        public static readonly CorpCentreRuCity Tyumen;
        public static readonly CorpCentreRuCity Ufa;
        public static readonly CorpCentreRuCity Ulyanovsk;

        static CorpCentreRuCity()
        {
            Barnaul = new CorpCentreRuCity(WebCities.Barnaul, "Барнаул", "2");
            Cheboksary = new CorpCentreRuCity(WebCities.Cheboksary, "Чебоксары", "125");
            Ekaterinburg = new CorpCentreRuCity(WebCities.Ekaterinburg, "Екатеринбург", "76");
            Izhevsk = new CorpCentreRuCity(WebCities.Izhevsk, "Ижевск", "108");
            Kemerovo = new CorpCentreRuCity(WebCities.Kemerovo, "Кемерово", "24");
            Kirov = new CorpCentreRuCity(WebCities.Kirov, "Киров", "30");
            Kurgan = new CorpCentreRuCity(WebCities.Kurgan, "Курган", "33");
            Novosibirsk = new CorpCentreRuCity(WebCities.Novosibirsk, "Новосибирск", "37");
            Orenburg = new CorpCentreRuCity(WebCities.Orenburg, "Оренбург", "42");
            Penza = new CorpCentreRuCity(WebCities.Penza, "Пенза", "48");
            Perm = new CorpCentreRuCity(WebCities.Perm, "Пермь", "56");
            Syktyvkar = new CorpCentreRuCity(WebCities.Syktyvkar, "Сыктывкар", "62");
            Saransk = new CorpCentreRuCity(WebCities.Saransk, "Саранск", "65");
            Samara = new CorpCentreRuCity(WebCities.Samara, "Самара", "68");
            Tomsk = new CorpCentreRuCity(WebCities.Tomsk, "Томск", "99");
            Tolyatti = new CorpCentreRuCity(WebCities.Tolyatti, "Тольятти", "69");
            Tyumen = new CorpCentreRuCity(WebCities.Tyumen, "Тюмень", "103");
            Ufa = new CorpCentreRuCity(WebCities.Ufa, "Уфа", "20");
            Ulyanovsk = new CorpCentreRuCity(WebCities.Ulyanovsk, "Улья", "114");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private CorpCentreRuCity(WebCity city, string name, string cookieValue) 
            : base(city, name)
        {
            CookieValue = cookieValue;
        }

        public string CookieValue { get; private set; }

        public static IEnumerable<CorpCentreRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(CorpCentreRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(CorpCentreRuCity))
                        .ToArray();
                return fields.Select(field => (CorpCentreRuCity)field.GetValue(null));
            }
        }

        public static CorpCentreRuCity Get(WebCity city)
        {
            CorpCentreRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(CorpCentreRuCompany.Instance, city);
            return result;
        }

        private static readonly Dictionary<WebCity, CorpCentreRuCity> Cities;
    }
}