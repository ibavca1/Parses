using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.CitilinkRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CitilinkRu
{
    internal class CitilinkRuCatalogPageContentParser : CitilinkRuHtmlPageContentParser, ICitilinkRuWebPageContentParser
    {
        public CitilinkRuWebPageType PageType
        {
            get { return CitilinkRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts(page, content);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
        {
            // в списке укороченные ключи, нормальные ключи только в карточке

            HtmlNodeCollection goodsNodes =
                content.GetNodes(@"//table[contains(@class,'item')]");
            var pages = new List<WebPage>();
            foreach (HtmlNode node in goodsNodes)
            {
                Uri uri = 
                    node.GetSingleNode(@".//tr/td[@class='l']/a").GetUri(page);
                WebPage productPage = page.Site.GetPage(uri, CitilinkRuWebPageType.Product, page.Path);
                pages.Add(productPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode pagingNode =
                content.SelectSingleNode(@"//div[@class='cat_nav top']/div[@class='b']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
                pagingNode.SelectSingleNode(@"span[@class='active']/following-sibling::a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            WebPage nextPage = page.Site.GetPage(nextPageUri, CitilinkRuWebPageType.Catalog, page.Path);
            return nextPage;
        }
    }
}