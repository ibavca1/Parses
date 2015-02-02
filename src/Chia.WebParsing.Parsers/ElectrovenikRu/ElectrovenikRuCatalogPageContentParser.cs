using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.ElectrovenikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.ElectrovenikRu
{
    internal class ElectrovenikRuCatalogPageContentParser : ElectrovenikRuHtmlPageContentParser, IElectrovenikRuWebPageContentParser
    {
        public ElectrovenikRuWebPageType PageType
        {
            get { return ElectrovenikRuWebPageType.Catalog; }
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

        private static WebPageContentParsingResult ParseProducts(
            WebPage page, HtmlPageContent content)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//ul[contains(@class,'catalog list-catalog')]");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@"//li[contains(@class,'catalog-item')]");

            var positions = new List<WebMonitoringPosition>();
            foreach (HtmlNode productNode in productsNodes)
            {
                string article = productNode
                    .GetSingleNode(@".//div[@class='product__item__art']")
                    .GetInnerText()
                    .Replace("Артикул:", "").Trim();
                string name = productNode
                    .GetSingleNode(@".//a[contains(@class,'catalog-item__name')]")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//a[contains(@class,'catalog-item__name')]")
                    .GetUri(page);
                decimal price = productNode
                    .GetSingleNode(@".//div[@class='new-price']")
                    .GetPrice();

                var position = new WebMonitoringPosition(article, name, price, uri);
                positions.Add(position);
            }

            return new WebPageContentParsingResult {Positions = positions};
        }

         private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
         {
             HtmlNode paginatorNode =
                 content.SelectSingleNode(@"//div[@class='section_catalog_paginator']");
             if (paginatorNode == null)
                 return null;

             HtmlNode nextPageNode =
                 paginatorNode.SelectSingleNode(@"//a[contains(@class,'js-pagination-next')]");
             if (nextPageNode == null)
                 return null;

             Uri nextPageUri = nextPageNode.GetUri(page);
             return page.Site.GetPage(nextPageUri, ElectrovenikRuWebPageType.Catalog, page.Path);
         }
    }
}