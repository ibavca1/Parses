using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.TelemaksRu
{
    public class TelemaksRuCity : WebCity
    {
        public static readonly TelemaksRuCity Arhangelsk;
        public static readonly TelemaksRuCity Ivanovo;
        public static readonly TelemaksRuCity Kaluga;
        public static readonly TelemaksRuCity Kingisepp;
        public static readonly TelemaksRuCity Monchegorsk;
        public static readonly TelemaksRuCity Murmansk;
        public static readonly TelemaksRuCity Obninsk;
        public static readonly TelemaksRuCity Petrozavodsk;
        public static readonly TelemaksRuCity StPetersburg;
        public static readonly TelemaksRuCity Severodvinsk;
        public static readonly TelemaksRuCity Syktyvkar;
        public static readonly TelemaksRuCity Tambov;
        public static readonly TelemaksRuCity Tver;
        public static readonly TelemaksRuCity VelikiyeLuki;
        private static readonly Dictionary<WebCity, TelemaksRuCity> Cities;

        static TelemaksRuCity()
        {
            Arhangelsk = new TelemaksRuCity(WebCities.Arhangelsk, "arh", "Архангельск");
            Ivanovo = new TelemaksRuCity(WebCities.Ivanovo, "ivn", "Иваново");
            Kaluga = new TelemaksRuCity(WebCities.Kaluga, "kal", "Калуга");
            Kingisepp = new TelemaksRuCity(WebCities.Kingisepp, "king", "Кингисепп");
            Monchegorsk = new TelemaksRuCity(WebCities.Monchegorsk, "monch", "Мончегорск");
            Murmansk = new TelemaksRuCity(WebCities.Murmansk, "murm", "Мурманск");
            Obninsk = new TelemaksRuCity(WebCities.Obninsk, "obn", "Обнинск");
            Petrozavodsk = new TelemaksRuCity(WebCities.Petrozavodsk, "petr", "Петрозаводск");
            StPetersburg = new TelemaksRuCity(WebCities.StPetersburg, "www", "Санкт-Петербург");
            Severodvinsk = new TelemaksRuCity(WebCities.Severodvinsk, "sev", "Северодвинск");
            Syktyvkar = new TelemaksRuCity(WebCities.Syktyvkar, "sykt", "Сыктывкар");
            Tambov = new TelemaksRuCity(WebCities.Tambov, "tamb", "Тамбов");
            Tver = new TelemaksRuCity(WebCities.Tver, "tver", "Тверь");
            VelikiyeLuki = new TelemaksRuCity(WebCities.VelikiyeLuki, "vluki", "Великие Луки");

            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private TelemaksRuCity(WebCity other, string uriPrefix, string name)
            : base(other, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<TelemaksRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(TelemaksRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(TelemaksRuCity))
                        .ToArray();
                return fields.Select(field => (TelemaksRuCity)field.GetValue(null));
            }
        }

        public static TelemaksRuCity Get(WebCity city)
        {
            TelemaksRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(TelemaksRuCompany.Instance, city);
            return result;
        }
    }
}