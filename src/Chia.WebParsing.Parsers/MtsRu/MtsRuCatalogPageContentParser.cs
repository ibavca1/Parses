using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.MtsRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.MtsRu
{
    internal class MtsRuCatalogPageContentParser : HtmlPageContentParser, IMtsRuWebPageContentParser
    {
        public MtsRuWebPageType PageType
        {
            get { return MtsRuWebPageType.Catalog; }
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
            HtmlNode productsBlock =
                content.GetSingleNode(@"//div[@class='products_block']");
            HtmlNodeCollection namesCells =
                productsBlock.GetNodes(@"//div[@class='element_right_part' or @class='element_lower_part']");
            HtmlNodeCollection pricesCells =
                productsBlock.GetNodes(@"//div[@class='spec_basket_button']");

            if (namesCells.Count != pricesCells.Count)
                throw new InvalidWebPageMarkupException();

            var products = new List<WebMonitoringPosition>();
            for (int j = 0; j < namesCells.Count; j++)
            {
                HtmlNode nameCell = namesCells[j];
                HtmlNode priceCell = pricesCells[j];
                WebMonitoringPosition product = ParseProductNode(page, nameCell, priceCell);
                products.Add(product);
            }

            return new WebPageContentParsingResult {Positions = products};
        }

        private static WebMonitoringPosition ParseProductNode(WebPage page, HtmlNode nameCell, HtmlNode priceCell)
        {
            //string group = nameCell
            //    .GetSingleNode(@".//span[@class='type_of_good']")
            //    .GetInnerText();
            string name = nameCell
                .GetSingleNode(@".//h4/a")
                .GetInnerText();
            Uri uri = nameCell
                .GetSingleNode(@".//h4/a")
                .GetUri(page);
            //name = string.Join(" ", group, name);
            decimal price = 0;

            bool outOfStock = priceCell.HasNode(@".//span[contains(@class,'outofstock')]");
            if (!outOfStock)
            {
                price = priceCell
                    .SelectSingleNode(@".//div[@class='new_price']")
                    .GetPrice();
            }


            return new WebMonitoringPosition(null, name, price, uri);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagingNode =
                document.SelectSingleNode(@"//div[contains(@class,'page_navigator')]//ul");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
              pagingNode.SelectSingleNode(@"li[@class='active_page']/following-sibling::li/a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            WebPage nextPage = page.Site.GetPage(nextPageUri, MtsRuWebPageType.Catalog, page.Path);
            return nextPage;
        }
    }
}