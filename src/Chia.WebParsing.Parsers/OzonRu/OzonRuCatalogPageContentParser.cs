using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.OzonRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.OzonRu
{
    internal class OzonRuCatalogPageContentParser : OzonRuHtmlPageContentParser, IOzonRuWebPageContentParser
    {
        public OzonRuWebPageType PageType
        {
            get { return OzonRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool hasCategories = content.HasNode(@"//div[@id='facetControl_catalog']");
            if (hasCategories)
                return ParseCategories(page, content);

            WebPageContentParsingResult result = ParseProducts(page, content);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection productNodes =
               content.SelectNodes(@"//div[@id='bTilesModeShow']//div[@itemtype='http://schema.org/Product']");
            if (productNodes == null)
                return WebPageContentParsingResult.Empty;

            var pages = new List<WebPage>();
            foreach (HtmlNode productNode in productNodes)
            {
                Uri productUri =
                    productNode.GetSingleNode("a").GetUri(page);
                WebPage productPage =
                    page.Site.GetPage(productUri, OzonRuWebPageType.Product, page.Path);
                pages.Add(productPage);
            }

            return new WebPageContentParsingResult{Pages = pages};
        }

        private static WebPageContentParsingResult ParseCategories(WebPage page, HtmlPageContent content)
        {
            HtmlNode categoriesList =
               content.GetSingleNode(@"//div[@id='facetControl_catalog']");
            HtmlNodeCollection categoriesNodes =
                categoriesList.GetNodes(@".//a");

            var pages = new List<WebPage>();
            
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                Uri itemUri = categoryNode.GetUri(page).RemoveQuery();
                string itemName = categoryNode.GetSingleNode("text()").GetInnerText();
                var itemPath = new WebSiteMapPath(itemName);

                var categoryPage = page.Site.GetPage(itemUri, OzonRuWebPageType.Catalog, itemPath);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode pagingNode =
                content.SelectSingleNode(@"//p[@class='Pages']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
                pagingNode.SelectSingleNode(@"span[@class='Active']/following-sibling::a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);

            WebPage nextPage = page.Site.GetPage(nextPageUri, OzonRuWebPageType.Catalog, page.Path);
            return nextPage;
        }
    }
}