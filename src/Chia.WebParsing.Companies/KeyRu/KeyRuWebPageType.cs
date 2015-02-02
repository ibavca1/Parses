using WebParsingFramework;

namespace Chia.WebParsing.Companies.KeyRu
{
    public enum KeyRuWebPageType
    {
        Main = WebPageType.Main,
        Razdel = WebPageType.Razdel,
        ShopsList = WebPageType.ShopsList,
        Catalog = WebPageType.Catalog,
        CatalogAjax = WebPageType.Product+100,
        Product = WebPageType.Product
    }
}