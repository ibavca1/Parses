using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.OldiRu
{
    public class OldiRuCity : WebCity
    {
        public static readonly OldiRuCity Astrakhan;
        public static readonly OldiRuCity Ekaterinburg;
        public static readonly OldiRuCity Kaluga;
        public static readonly OldiRuCity Khabarovsk;
        public static readonly OldiRuCity Kirov;
        public static readonly OldiRuCity Moscow;
        public static readonly OldiRuCity Novosibirsk;
        public static readonly OldiRuCity Novokuznetsk;
        //public static readonly OldiRuCity Norilsk;
        public static readonly OldiRuCity PetropavlovskKamchatsky;
        public static readonly OldiRuCity Saransk;
        public static readonly OldiRuCity StPetersburg;
        public static readonly OldiRuCity Syktyvkar;
        public static readonly OldiRuCity Vladivostok;
        public static readonly OldiRuCity Vladimir;

        static OldiRuCity()
        {
            Moscow = new OldiRuCity(WebCities.Moscow, "Москва", "259749");
            StPetersburg = new OldiRuCity(WebCities.StPetersburg, "Санкт-Петербург", "269480");
            Ekaterinburg = new OldiRuCity(WebCities.Ekaterinburg, "Екатеринбург", "259907");
            Vladivostok = new OldiRuCity(WebCities.Vladivostok, "Владивосток", "259958");
            Novosibirsk = new OldiRuCity(WebCities.Novosibirsk, "Новосибирск", "259900");
            Syktyvkar = new OldiRuCity(WebCities.Syktyvkar, "Сыктывкар", "259926");
            Saransk = new OldiRuCity(WebCities.Saransk, "Саранск", "259911");
            //Norilsk = new OldiRuCity(WebCities.Norilsk, "Норильск", "260001");
            Khabarovsk = new OldiRuCity(WebCities.Khabarovsk, "Хабаровск", "259777");
            Astrakhan = new OldiRuCity(WebCities.Astrakhan, "Астрахань", "259893");
            Vladimir = new OldiRuCity(WebCities.Vladimir, "Владимир", "259824");
            Kaluga = new OldiRuCity(WebCities.Kaluga, "Калуга", "259796");
            PetropavlovskKamchatsky = new OldiRuCity(WebCities.PetropavlovskKamchatsky, "Петропавловск-Камчатский", "259781");
            Novokuznetsk = new OldiRuCity(WebCities.Novokuznetsk, "Новокузнецк", "259866");
            Kirov = new OldiRuCity(WebCities.Kirov, "Киров", "264970");
            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private OldiRuCity(WebCity town, string name, string internalId)
            : base(town, name)
        {
            InternalId = internalId;
        }

        public string InternalId { get; private set; }

        public static IEnumerable<OldiRuCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(OldiRuCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(OldiRuCity))
                        .ToArray();
                return fields.Select(field => (OldiRuCity)field.GetValue(null));
            }
        }

        public static OldiRuCity Get(WebCity city)
        {
            OldiRuCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(OldiRuCompany.Instance, city);
            return result;
        }

        private static readonly Dictionary<WebCity, OldiRuCity> Cities;
    }
}