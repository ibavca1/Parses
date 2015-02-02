using System.Linq;
using System.Reflection;
using WebParsingFramework;

namespace Chia.WebParsing.Companies
{
    /// <summary>
    /// Города.
    /// </summary>
    public static class WebCities
    {
        public static readonly WebCity Astrakhan = new WebCity(92, "Астрахань");
        public static readonly WebCity Arhangelsk = new WebCity(95, "Архангельск");
        public static readonly WebCity Barnaul = new WebCity(97, "Барнаул");
        public static readonly WebCity Bryansk = new WebCity(56, "Брянск");
        public static readonly WebCity Belgorod = new WebCity(626, "Белгород");
        public static readonly WebCity Cheboksary = new WebCity(614, "Чебоксары");
        public static readonly WebCity Cherepovets = new WebCity(408, "Череповец");
        public static readonly WebCity Chelyabinsk = new WebCity(85, "Челябинск");
        public static readonly WebCity Ekaterinburg = new WebCity(60, "Екатеринбург");
        public static readonly WebCity Gatchina = new WebCity(540, "Гатчина");
        public static readonly WebCity Ivanovo = new WebCity(248, "Иваново");
        public static readonly WebCity Izhevsk = new WebCity(61, "Ижевск");
        public static readonly WebCity Krasnoyarsk = new WebCity(177, "Красноярск");
        public static readonly WebCity Kazan = new WebCity(62, "Казань");
        public static readonly WebCity Krasnodar = new WebCity(63, "Краснодар");
        public static readonly WebCity Kirov = new WebCity(195, "Киров");
        public static readonly WebCity Khabarovsk = new WebCity(235, "Хабаровск");
        public static readonly WebCity Kaluga = new WebCity(528, "Калуга");
        public static readonly WebCity Kemerovo = new WebCity(99, "Кемерово");
        public static readonly WebCity Kingisepp = new WebCity(610, "Кингисепп");
        public static readonly WebCity Kurgan = new WebCity(101, "Курган");
        public static readonly WebCity Lipetsk = new WebCity(87, "Липецк");
        public static readonly WebCity Magnitogorsk = new WebCity(64, "Магнитогорск");
        public static readonly WebCity Moscow = new WebCity(66, "Москва");
        public static readonly WebCity Monchegorsk = new WebCity(1323, "Мончегорск");
        public static readonly WebCity Murmansk = new WebCity(193, "Мурманск");
        public static readonly WebCity NaberezhnyeChelny = new WebCity(67, "Набережные Челны");
        public static readonly WebCity NizhnyNovgorod = new WebCity(68, "Нижний Новгород");
        public static readonly WebCity Novokuznetsk = new WebCity(70, "Новокузнецк");
        public static readonly WebCity Novosibirsk = new WebCity(71, "Новосибирск");
        public static readonly WebCity Neftekamsk = new WebCity(256, "Нефтекамск");
        public static readonly WebCity Nizhnekamsk = new WebCity(369, "Нижнекамск");
        public static readonly WebCity Omsk = new WebCity(72, "Омск");
        public static readonly WebCity Orenburg = new WebCity(73, "Оренбург");
        public static readonly WebCity Obninsk = new WebCity(477, "Обнинск");
        public static readonly WebCity Perm = new WebCity(74, "Пермь");
        public static readonly WebCity Penza = new WebCity(370, "Пенза");
        public static readonly WebCity Petrozavodsk = new WebCity(106, "Петрозаводск");
        public static readonly WebCity PetropavlovskKamchatsky = new WebCity(123, "Петропавловск-Камчатский");
        public static readonly WebCity RostovOnDon = new WebCity(75, "Ростов-на-Дону");
        public static readonly WebCity Ryazan = new WebCity(76, "Рязань");
        public static readonly WebCity Samara = new WebCity(77, "Самара");
        public static readonly WebCity StPetersburg = new WebCity(78, "Санкт-Петербург");
        public static readonly WebCity Saratov = new WebCity(79, "Саратов");
        public static readonly WebCity Surgut = new WebCity(89, "Сургут");
        public static readonly WebCity Serpuhov = new WebCity(752, "Серпухов");
        public static readonly WebCity Saransk = new WebCity(754, "Саранск");
        public static readonly WebCity Severodvinsk = new WebCity(217, "Северодвинск");
        public static readonly WebCity Syktyvkar = new WebCity(468, "Сыктывкар");
        public static readonly WebCity Tambov = new WebCity(81, "Тамбов");
        public static readonly WebCity Tolyatti = new WebCity(82, "Тольятти");
        public static readonly WebCity Tomsk = new WebCity(109, "Томск");
        public static readonly WebCity Tyumen = new WebCity(83, "Тюмень");
        public static readonly WebCity Tula = new WebCity(623, "Тула");
        public static readonly WebCity Taganrog = new WebCity(108, "Таганрог");
        public static readonly WebCity Tver = new WebCity(497, "Тверь");
        public static readonly WebCity Ulyanovsk = new WebCity(384, "Ульяновск");
        public static readonly WebCity Ufa = new WebCity(84, "Уфа");
        public static readonly WebCity Undefined = new WebCity(-1, "Undefined");
        public static readonly WebCity VelikyNovgorod = new WebCity(1019, "Великий Новгород");
        public static readonly WebCity VelikiyeLuki = new WebCity(753, "Великие Луки");
        public static readonly WebCity Vladimir = new WebCity(57, "Владимир");
        public static readonly WebCity Volgograd = new WebCity(58, "Волгоград");
        public static readonly WebCity Voronezh = new WebCity(59, "Воронеж");
        public static readonly WebCity Vologda = new WebCity(98, "Вологда");
        public static readonly WebCity Vladivostok = new WebCity(183, "Владивосток");
        public static readonly WebCity Yaroslavl = new WebCity(86, "Ярославль");
        public static readonly WebCity YoshkarOla = new WebCity(448, "Йошкар-Ола");


        //public static readonly WebCity Norilsk = new WebCity(754, "Саранск");

        /// <summary>
        /// Получает список всех городов в контейнере.
        /// </summary>
        public static WebCity[] All
        {
            get
            {
                FieldInfo[] fields =
                    typeof(WebCities)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fieldInfo => fieldInfo.FieldType == typeof(WebCity))
                        .ToArray();
                return fields.Select(field => (WebCity)field.GetValue(null)).ToArray();
            }
        }
    }
}