using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnosilaRu
{
    public enum TehnosilaRuWebPageType
    {
        Main = WebPageType.Main,
        Catalog = WebPageType.Catalog,
        Product = WebPageType.Product,
        Razdel = WebPageType.Razdel,
        ShopsList = WebPageType.ShopsList,
        ProductAvailabilityInShops = WebPageType.Unknown+100
    }
}