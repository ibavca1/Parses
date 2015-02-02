using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.IonRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.IonRu
{
    internal class IonRuCatalogPageContentParser : HtmlPageContentParser, IIonRuWebPageContentParser
    {
        public IonRuWebPageType PageType
        {
            get { return IonRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts(page, content, context.Options);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            bool isEmpty =
                content.HasNode(@"//p[contains(text(),'Ничего подходящего не найдено')]");
            if (isEmpty)
                return WebPageContentParsingResult.Empty;

            HtmlNode productsList =
                content.GetSingleNode(@"//ul[@class='catalog_items']");
            HtmlNodeCollection productsNodes =
                productsList.SelectNodes(@".//div[@class='catalog_item hoverable']");

            var pages = new List<WebPage>();
            foreach (HtmlNode productNode in productsNodes)
            {
                Uri uri = productNode
                    .GetSingleNode(@".//a[@class='catalog_item_link']")
                    .GetUri(page);
                bool isVariantsUri = uri.AbsoluteUri.Contains("variants");
                IonRuWebPageType type = isVariantsUri ? IonRuWebPageType.Catalog : IonRuWebPageType.Product;
                WebPage productPage = page.Site.GetPage(uri, type, page.Path);
                pages.Add(productPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagerNode =
                document.SelectSingleNode(@"//ul[@class='pager']");
            bool noPager = pagerNode == null;
            if (noPager)
                return null;

            HtmlNode nextPageNode =
                pagerNode.SelectSingleNode(@".//a[text()='Следующая']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, IonRuWebPageType.Catalog, page.Path);
        }
    }
}