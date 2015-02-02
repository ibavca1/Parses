using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    internal class MvideoRuShopsListPageContentParser : MvideoRuHtmlPageContentParser, IMvideoRuWebPageContentParser
    {
        public MvideoRuWebPageType Type
        {
            get { return MvideoRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            WebShop[] shops = ParseShops(content).ToArray();
            var result = new WebPageContentParsingResult {Shops = shops};
            
            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
            {
                nextPage.IsPartOfShopsInformation = true;
                result.Pages.Add(nextPage);
            }

            return result;
        }

        private static IEnumerable<WebShop> ParseShops(HtmlPageContent content)
        {
            HtmlNode shopsList =
                content.SelectSingleNode(@"//div[contains(@class,'store-locator-list')]/ul");
            bool noShops = shopsList == null;
            if (noShops)
                return Enumerable.Empty<WebShop>();

            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//li[@class='store-locator-list-item']");

            var shops = new List<WebShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string name =
                    shopsNode.GetSingleNode(@".//div[@class='name']/h3").GetInnerText();
                string address =
                    shopsNode.GetSingleNode(@".//div[@class='name']/p").GetInnerText();

                var shop = new WebShop(name, address);
                shops.Add(shop);
            }

            return shops;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode paginationNode =
                content.SelectSingleNode(@"//div[@class='pagination pagination-centered']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@"a[contains(@class,'ico-pagination-next') and @href]");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, MvideoRuWebPageType.ShopsList, page.Path);
        }
    }
}