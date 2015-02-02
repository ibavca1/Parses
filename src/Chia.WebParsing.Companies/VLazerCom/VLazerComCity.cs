using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Companies.VLazerCom
{
    public class VLazerComCity : WebCity
    {
        public static readonly VLazerComCity Khabarovsk;
        public static readonly VLazerComCity Vladivostok;
        private static readonly Dictionary<WebCity, VLazerComCity> Cities;

        static VLazerComCity()
        {
           Khabarovsk = new VLazerComCity(WebCities.Khabarovsk, "Хабаровск", "khb");
           Vladivostok = new VLazerComCity(WebCities.Vladivostok, "Владивосток", "");

            Cities = All.ToDictionary(c => (WebCity)c, c => c);
        }

        private VLazerComCity(WebCity other, string name, string uriPrefix)
            : base(other, name)
        {
            UriPrefix = uriPrefix;
        }

        public string UriPrefix { get; private set; }

        public static IEnumerable<VLazerComCity> All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(VLazerComCity)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(VLazerComCity))
                        .ToArray();
                return fields.Select(field => (VLazerComCity)field.GetValue(null));
            }
        }

        public static VLazerComCity Get(WebCity city)
        {
            VLazerComCity result;
            if (!Cities.TryGetValue(city, out result))
                throw new UnsupporedWebCityException(VLazerComCompany.Instance, city);
            return result;
        }
    }
}