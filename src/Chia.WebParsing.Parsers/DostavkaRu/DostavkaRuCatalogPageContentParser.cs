using System;
using Chia.WebParsing.Companies.DostavkaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DostavkaRu
{
    internal class DostavkaRuCatalogPageContentParser : HtmlPageContentParser, IDostavkaRuWebPageContentParser
    {
        public DostavkaRuWebPageType PageType
        {
            get { return DostavkaRuWebPageType.Catalog; }
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
            HtmlNode productsList =
               content.GetSingleNode(@"//div[@id='ProductListBlock']");
            HtmlNodeCollection productsNodes =
               productsList.GetNodes(@"//div[@class='ProductListItemBlock']");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                HtmlNode nameNode =
                    productNode.GetSingleNode(@".//div[@class='productTitleBox']//a");
                Uri uri = nameNode.GetUri(page);
                string name = nameNode.GetInnerText();
                string article = productNode.GetAttributeText("product_id");
                decimal price = 0;
                bool isAvailable = 
                    productNode.HasNode(@".//div[contains(@class,'is_available')]");
                if (isAvailable)
                {
                    price = productNode
                        .GetSingleNode(@".//div[contains(@class,'productPriceContainer')]")
                        .GetPrice();
                }

                var product = new WebMonitoringPosition(article, name, price, uri);
                result.Positions.Add(product);
            }


            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode paginationNode =
                content.SelectSingleNode(@"//div[contains(@class,'pagination')]");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(".//a/img[contains(@class,'nextPagination')]/..");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, DostavkaRuWebPageType.Catalog, page.Path);
        }
    }
}