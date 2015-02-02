using WebParsingFramework;

namespace Chia.WebParsing.Companies.EldoradoRu
{
    public enum EldoradoRuWebPageType
    {
        Main = WebPageType.Main,
        Catalog = WebPageType.Catalog,
        Product = WebPageType.Product,
        Razdel = WebPageType.Razdel,
        ShopsList = WebPageType.ShopsList,
        AvailabilityInShops = WebPageType.Unknown+100,
        Prices = WebPageType.Unknown+101
    }
}