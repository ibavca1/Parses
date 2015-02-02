using WebParsingFramework;

namespace Chia.WebParsing.Companies.TdPoiskRu
{
    public enum TdPoiskRuWebPageType
    {
        Main = WebPageType.Main,
        Catalog = WebPageType.Catalog,
        Product = WebPageType.Product,
        Razdel = WebPageType.Razdel,
        ShopsList = WebPageType.ShopsList,
        AvailabilityInShops = WebPageType.Unknown+100
    }
}