using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.RegardRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RegardRu
{
    internal class RegardRuCatalogPageContentParser : RegardRuHtmlPageContentParser, IRegardRuWebPageContentParser
    {
        public RegardRuWebPageType PageType
        {
            get { return RegardRuWebPageType.Catalog; }
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
            // название модели в списке без торговой группы, а нам нужно с ней
            // поэтому идем в карточку

            HtmlNode productsList =
                content.GetSingleNode(@"//div[@id='hits']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//table[@class='one-list-tovar']//a[@class='header']");

            var pages = new List<WebPage>();
            foreach (HtmlNode productNode in productsNodes)
            {
                Uri uri = productNode.GetUri(page);
                WebPage productPage = page.Site.GetPage(uri, RegardRuWebPageType.Product, page.Path);
                pages.Add(productPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//div[@class='pagination']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
               paginationNode.SelectSingleNode(@"a[@class='curr']/following-sibling::a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, RegardRuWebPageType.Catalog, page.Path);
        } 
    }
}