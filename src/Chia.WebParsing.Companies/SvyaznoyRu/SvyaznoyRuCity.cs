using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.SvyaznoyRu
{
    public class SvyaznoyRuCity : WebCity
    {
        public static readonly SvyaznoyRuCity Barnaul;
        public static readonly SvyaznoyRuCity Bryansk;
        public static readonly SvyaznoyRuCity Cheboksary;
        public static readonly SvyaznoyRuCity Chelyabinsk;
        public static readonly SvyaznoyRuCity Ekaterinburg;
        public static readonly SvyaznoyRuCity Krasnoyarsk;
        public static readonly SvyaznoyRuCity Orenburg;
        public static readonly SvyaznoyRuCity Petrozavodsk;
        public static readonly SvyaznoyRuCity Moscow;
        public static readonly SvyaznoyRuCity NizhnyNovgorod;
        public static readonly SvyaznoyRuCity Novosibirsk;
        public static readonly SvyaznoyRuCity StPetersburg;
        public static readonly SvyaznoyRuCity Saratov;
        public static readonly SvyaznoyRuCity Surgut;
        public static readonly SvyaznoyRuCity Volgograd;
        public static readonly SvyaznoyRuCity YoshkarOla;
        private static readonly Dictionary<WebCity, SvyaznoyRuCity> Cities;

        static SvyaznoyRuCity()
        {
            Barnaul = new SvyaznoyRuCity(WebCities.Barnaul, "Барнаул", "64");
            Bryansk = new SvyaznoyRuCity(WebCities.Bryansk, "Брянск", "67");
            Cheboksary = new SvyaznoyRuCity(WebCities.Cheboksary, "Чебоксары", "193");
            Chelyabinsk = new SvyaznoyRuCity(WebCities.Chelyabinsk, "Челябинск", "194");
            Ekaterinburg = new SvyaznoyRuCity(WebCities.Ekaterinburg, "Екатеринбург", "90");
            Krasnoyarsk = new SvyaznoyRuCity(WebCities.Krasnoyarsk, "Красноярск",  "220");
            Orenburg = new SvyaznoyRuCity(WebCities.Orenburg, "Оренбург", "248");
            Petrozavodsk = new SvyaznoyRuCity(WebCities.Petrozavodsk, "Петрозаводск", "159");
            Moscow = new SvyaznoyRuCity(WebCities.Moscow, "Москва", "133");
            NizhnyNovgorod = new SvyaznoyRuCity(WebCities.NizhnyNovgorod, "Нижний Новгород",  "140");
            Novosibirsk = new SvyaznoyRuCity(WebCities.Novosibirsk, "Новосибирск",  "143");
            StPetersburg = new SvyaznoyRuCity(WebCities.StPetersburg, "Санкт-Петербург",  "171");
            Saratov = new SvyaznoyRuCity(WebCities.Saratov, "Саратов", "173");
            Surgut = new SvyaznoyRuCity(WebCities.Surgut, "Сургут",  "313");
            Volgograd = new SvyaznoyRuCity(WebCities.Volgograd, "Волгоград",  "71");
            YoshkarOla = new SvyaznoyRuCity(WebCities.YoshkarOla, "Йошкар-Ола", "101");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private SvyaznoyRuCity(WebCity city, string name, string cityId)
            : base(city, name)
        {
            CityId = cityId;
        }

        public string CityId { get; private set; }

        public static IEnumerable<SvyaznoyRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(SvyaznoyRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(SvyaznoyRuCity))
                        .ToArray();
                return fields.Select(field => (SvyaznoyRuCity)field.GetValue(null));
            }
        }

        public static SvyaznoyRuCity Get(WebCity city)
        {
            SvyaznoyRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(SvyaznoyRuCompany.Instance, city);

            return result;
        }

        
    }
}