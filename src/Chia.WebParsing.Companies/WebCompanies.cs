using System.Linq;
using System.Reflection;
using Chia.WebParsing.Companies.CentrBtRu;
using Chia.WebParsing.Companies.CitilinkRu;
using Chia.WebParsing.Companies.CorpCentreRu;
using Chia.WebParsing.Companies.DnsShopRu;
using Chia.WebParsing.Companies.DomoRu;
using Chia.WebParsing.Companies.DostavkaRu;
using Chia.WebParsing.Companies.EldoradoRu;
using Chia.WebParsing.Companies.ElectrovenikRu;
using Chia.WebParsing.Companies.EnterRu;
using Chia.WebParsing.Companies.EurosetRu;
using Chia.WebParsing.Companies.HolodilnikRu;
using Chia.WebParsing.Companies.IonRu;
using Chia.WebParsing.Companies.JustRu;
using Chia.WebParsing.Companies.KeyRu;
using Chia.WebParsing.Companies.LogoRu;
using Chia.WebParsing.Companies.MtsRu;
using Chia.WebParsing.Companies.MvideoRu;
using Chia.WebParsing.Companies.NewmansRu;
using Chia.WebParsing.Companies.Nord24Ru;
using Chia.WebParsing.Companies.NotikRu;
using Chia.WebParsing.Companies.OldiRu;
using Chia.WebParsing.Companies.OnlinetradeRu;
using Chia.WebParsing.Companies.Oo3Ru;
using Chia.WebParsing.Companies.OzonRu;
using Chia.WebParsing.Companies.PultRu;
using Chia.WebParsing.Companies.RbtRu;
using Chia.WebParsing.Companies.RegardRu;
using Chia.WebParsing.Companies.SvyaznoyRu;
using Chia.WebParsing.Companies.TdPoiskRu;
using Chia.WebParsing.Companies.TechnonetRu;
using Chia.WebParsing.Companies.TechportRu;
using Chia.WebParsing.Companies.TehnoparkRu;
using Chia.WebParsing.Companies.TehnoshokRu;
using Chia.WebParsing.Companies.TehnosilaRu;
using Chia.WebParsing.Companies.TelemaksRu;
using Chia.WebParsing.Companies.TenIRu;
using Chia.WebParsing.Companies.UlmartRu;
using Chia.WebParsing.Companies.UtinetRu;
using Chia.WebParsing.Companies.VLazerCom;
using WebParsingFramework;

namespace Chia.WebParsing.Companies
{
    public class WebCompanies
    {
        public static readonly WebCompany CentrBtRu = CentrBtRuCompany.Instance; // 620
        public static readonly WebCompany CitilinkRu = CitilinkRuCompany.Instance; // 539
        public static readonly WebCompany CorpCentreRu = CorpCentreRuCompany.Instance; // 79
        public static readonly WebCompany DomoRu = DomoRuCompany.Instance; // 83
        public static readonly WebCompany DostavkaRu = DostavkaRuCompany.Instance; // 609
        public static readonly WebCompany DnsShopRu = DnsShopRuCompany.Instance; // 351
        public static readonly WebCompany EldoradoRu = EldoradoRuCompany.Instance; // 37
        public static readonly WebCompany ElectrovenikRu = ElectrovenikRuCompany.Instance; // 621
        public static readonly WebCompany EnterRu = EnterRuCompany.Instance; // 556
        public static readonly WebCompany EurosetRu = EurosetRuCompany.Instance; // 40
        public static readonly WebCompany HolodilnikRu = HolodilnikRuCompany.Instance; // 610
        public static readonly WebCompany IonRu = IonRuCompany.Instance; // 424
        public static readonly WebCompany JustRu = JustRuCompany.Instance; // 617
        public static readonly WebCompany KeyRu = KeyRuCompany.Instance; // 67
        public static readonly WebCompany LogoRu = LogoRuCompany.Instance; // !!!!!!!!!!!!!!!!!!!! нет кода
        public static readonly WebCompany MtsRu = MtsRuCompany.Instance; // 128
        public static readonly WebCompany MvideoRu = MvideoRuCompany.Instance; // 38
        public static readonly WebCompany NewmansRu = NewmansRuCompany.Instance; // 619
        public static readonly WebCompany Nord24Ru = Nord24RuCompany.Instance; // 74
        public static readonly WebCompany NotikRu = NotikRuCompany.Instance; // 508
        public static readonly WebCompany OldiRu = OldiRuCompany.Instance; // 612
        public static readonly WebCompany OnlinetradeRu = OnlinetradeRuCompany.Instance; // 557
        public static readonly WebCompany Oo3Ru = Oo3RuCompany.Instance; // 605
        public static readonly WebCompany OzonRu = OzonRuCompany.Instance; // 604
        public static readonly WebCompany PultRu = PultRuCompany.Instance; // 613
        public static readonly WebCompany RegardRu = RegardRuCompany.Instance; // 420
        public static readonly WebCompany RbtRu = RbtRuCompany.Instance; // 65
        public static readonly WebCompany SvyaznoyRu = SvyaznoyRuCompany.Instance; // 59
        public static readonly WebCompany TdPoiskRu = TdPoiskRuCompany.Instance; //84
        public static readonly WebCompany TelemaksRu = TelemaksRuCompany.Instance; // 117
        public static readonly WebCompany TenIRu = TenIRuCompany.Instance; // 603
        public static readonly WebCompany TechnonetRu = TechnonetRuCompany.Instance; // 272
        public static readonly WebCompany TechportRu = TechportRuCompany.Instance; // 614
        public static readonly WebCompany TehnoparkRu = TehnoparkRuCompany.Instance; // 62
        public static readonly WebCompany TehnosilaRu = TehnosilaRuCompany.Instance; // 69
        public static readonly WebCompany TehnoshokRu = TehnoshokRuCompany.Instance; // 119
        public static readonly WebCompany UlmartRu = UlmartRuCompany.Instance; // 534
        public static readonly WebCompany UtinetRu = UtinetRuCompany.Instance; // 583
        public static readonly WebCompany VLazerCom = VLazerComCompany.Instance; // 196

        public static WebCompany ExpertRu
        {
            get { return RbtRu; }
        }

        /// <summary>
        /// Получает список всех интернет-компаний.
        /// </summary>
        public static WebCompany[] All
        {
            get
            {
                FieldInfo[] fields =
                     typeof(WebCompanies)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(field => typeof(WebCompany).IsAssignableFrom(field.FieldType))
                        .ToArray();
                return fields.Select(field => (WebCompany)field.GetValue(null)).ToArray();
            }
        }
    }
}
