using WebParsingFramework;

namespace Chia.WebParsing.Companies.DomoRu
{
    public enum DomoRuWebPageType
    {
        Main = WebPageType.Main,
        MainMenuAjax = WebPageType.Unknown+100,
        Catalog = WebPageType.Catalog,
        CatalogAjax = WebPageType.Unknown+101,
        Product = WebPageType.Product,
        Razdel = WebPageType.Razdel
    }
}