using WebParsingFramework;

namespace Chia.WebParsing.Companies.MvideoRu
{
    public enum MvideoRuWebPageType
    {
        Main = WebPageType.Main,
        Product = WebPageType.Product,
        Catalog = WebPageType.Catalog,
        Razdel = WebPageType.Razdel,
        RazdelsList = WebPageType.RazdelsList,
        ShopsList = WebPageType.ShopsList,
        Shop = WebPageType.Shop,
        AvailabilityInShops = WebPageType.Unknown+100
    }
}