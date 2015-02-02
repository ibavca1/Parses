using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.CentrBtRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.CentrBtRu
{
    internal class CentrBtRuCatalogPageContentParser : CentrBtRuMainPageContentParser
    {
        public override CentrBtRuWebPageType PageType
        {
            get { return CentrBtRuWebPageType.Catalog; }
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

        private static WebPageContentParsingResult ParseProducts(
            WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            HtmlNodeCollection namesNodes =
                content.GetNodes(@"//span[@class='tov_title']");
            HtmlNodeCollection pricesNodes =
                content.GetNodes(@"//span[@class='cena']");
            bool areNamesAndPricesCountEqual = namesNodes.Count == pricesNodes.Count;
            if (!areNamesAndPricesCountEqual)
                throw new InvalidWebPageMarkupException();

            bool needToGoToProductPage = options.ProductArticle;

            var result = new WebPageContentParsingResult();
            for (int i = 0; i < namesNodes.Count; i++)
            {
                string name = namesNodes[i].GetInnerText();
                Uri uri = namesNodes[i].ParentNode.GetUri(page);
                decimal price = pricesNodes[i].GetPrice();

                if (needToGoToProductPage)
                {
                    WebPage productPage = page.Site.GetPage(uri, CentrBtRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                }
                else
                {
                    var product = new WebMonitoringPosition(null, name, price, uri);
                    result.Positions.Add(product);
                }
            }

            return result;
        }

         private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
         {
             HtmlNode nextPageNode =
               content.SelectSingleNode(@"//span[@class='NavBarItem' and text()='дальше']");
             bool noNextPage = nextPageNode == null;
             if (noNextPage)
                 return null;

             Uri nextPageUri = nextPageNode.ParentNode.GetUri(page);
             return page.Site.GetPage(nextPageUri, CentrBtRuWebPageType.Catalog, page.Path);
         }
    }
}