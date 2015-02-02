using WebParsingFramework;

namespace Chia.WebParsing.Companies.IonRu
{
    public enum IonRuWebPageType
    {
        Main = WebPageType.Main,
        Catalog = WebPageType.Catalog,
        Product = WebPageType.Product,
        AvailabilityInShops = WebPageType.Unknown+100,
        ShopsList = WebPageType.ShopsList
    }
}