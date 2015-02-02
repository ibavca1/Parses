using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.Oo3Ru;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Oo3Ru
{
    internal class Oo3RuCatalogPageContentParser : Oo3RuHtmlPageContentParser, IOo3RuWebPageContentParser
    {
        public Oo3RuWebPageType PageType
        {
            get { return Oo3RuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPage[] categoriesPages = ParseCategories(page, content).ToArray();
            if (categoriesPages.Any())
                return new WebPageContentParsingResult {Pages = categoriesPages};


            WebPageContentParsingResult result = ParseProducts(page, content);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static IEnumerable<WebPage> ParseCategories(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection categoriesNodes =
                content.SelectNodes(@"//div[@class='top_category']//a[@itemprop='itemListElement']");
            if (categoriesNodes == null)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                Uri uri = categoryNode.GetUri(page);
                string name = categoryNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, Oo3RuWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return pages;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
        {
            // здесь мы парсим только ссылки на страницы товаров.
            // можно было бы сразу забирать товары, но нет информации о возможности доставки в выбранный город
            // она есть только на странице с товаром

            HtmlNode productsList =
                content.GetSingleNode(@"//table[@class='product_pl']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//a[@itemprop='name']");

            var pages = new List<WebPage>();
            foreach (HtmlNode productNode in productsNodes)
            {
                Uri uri = productNode.GetUri(page);
                WebPage productPage = page.Site.GetPage(uri, Oo3RuWebPageType.Product, page.Path);
                pages.Add(productPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagerNode =
               document.SelectSingleNode(@"//ul[@id='pager']");
            bool noPaging = pagerNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
                pagerNode.SelectSingleNode(@"li[@class='active']/following-sibling::li[not(@class='last')]/a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, Oo3RuWebPageType.Catalog, page.Path);
        } 
    }
}