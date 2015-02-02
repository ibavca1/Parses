using System;
using Chia.WebParsing.Companies.KeyRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.KeyRu
{
    internal class KeyRuCatalogPageContentParser : KeyRuHtmlPageContentParser, IKeyRuWebPageContentParser
    {
        public KeyRuWebPageType Type
        {
            get { return KeyRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            bool isEmpty = content.HasNode(@"//div[@class='catalog_items_count']/span[text()='0']");
            if (isEmpty)
                return WebPageContentParsingResult.Empty;

            WebPageContentParsingResult result = ParseProducts(page, content, context.Options);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        internal static WebPageContentParsingResult ParseProductsNodes(WebPage page, HtmlNode productsListNode,
            WebSiteParsingOptions options)
        {
            HtmlNodeCollection productsNodes =
               productsListNode.SelectNodes(@".//div[@data-item_id]");
            if (productsNodes == null)
                return WebPageContentParsingResult.Empty;

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productsNode in productsNodes)
            {
                // для ключей нам требуются характеристики
                // поэтому идем в карточку
                Uri uri = productsNode
                    .GetSingleNode(@".//div[@class='cell cell_name']//a")
                    .GetUri(page);

                WebPage productPage = page.Site.GetPage(uri, KeyRuWebPageType.Product, page.Path);
                result.Pages.Add(productPage);
            }

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content,
            WebSiteParsingOptions options)
        {
            HtmlNode productsListNode =
                content.GetSingleNode(@"//div[@id='items_goods']");
            return ParseProductsNodes(page, productsListNode, options);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            string categoryId = content
                .GetSingleNode(@"//input[@id='category_id']")
                .GetAttributeText("value");

            Uri nextPageUri = page.Uri
                .AddQueryParam("p", "1")
                .AddQueryParam("category_id", categoryId);
            return page.Site.GetPage(nextPageUri, KeyRuWebPageType.CatalogAjax, page.Path);
        }
    }
}