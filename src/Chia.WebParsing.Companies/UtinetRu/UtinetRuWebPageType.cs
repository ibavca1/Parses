using WebParsingFramework;

namespace Chia.WebParsing.Companies.UtinetRu
{
    public enum UtinetRuWebPageType
    {
        Main = WebPageType.Main,
        Razdel = WebPageType.Razdel,
        Product = WebPageType.Product,
        Catalog = WebPageType.Catalog,
        Location = WebPageType.Unknown+100
    }
}