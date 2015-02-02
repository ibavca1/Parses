using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.HolodilnikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.HolodilnikRu
{
    internal class HolodilnikRuCatalogPageContentParser : HolodilnikRuHtmlPageContentParser, IHolodilnikRuWebPageContentParser
    {
        public virtual HolodilnikRuWebPageType PageType
        {
            get { return HolodilnikRuWebPageType.Catalog; }
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
                content.GetSingleNode(@"//form[@name='comp_form']");
            HtmlNodeCollection productsNodes =
                //productsList.GetNodes(@".//div[contains(@class,'prodbox')]");
                content.SelectNodes(@".//div[contains(@class,'prodbox')]");
            if (productsNodes == null)
                return WebPageContentParsingResult.Empty;

            var products = new List<WebMonitoringPosition>();
            foreach (HtmlNode productNode in productsNodes)
            {
                string article = productNode
                    .GetSingleNode(@".//span[contains(text(), 'Код товара:')]")
                    .GetDigitsText();
                string name = productNode
                    .GetSingleNode(@".//div[@class='prodname_line']//a[@class='h3']")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//div[@class='prodname_line']//a[@class='h3']")
                    .GetUri(page);
                decimal price = 0;

                bool isAvailable = productNode.HasNode(@".//div[@class='add2cart']");   
                if (isAvailable)
                {
                    price = productNode
                        //.GetSingleNode(@".//div[contains(@class,'price') and not(@class='price_old')]/text()")
                        .GetSingleNode(@".//div[contains(@class,'price')]/text()")
                        .GetPrice();
                }
                
                var position = new WebMonitoringPosition(article, name, price, uri);
                products.Add(position);
            }

            return new WebPageContentParsingResult {Positions = products};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode pagingNode =
                content.SelectSingleNode(@"//b[contains(text(), 'Страницы:')]");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
                pagingNode.SelectSingleNode(@"span/following-sibling::a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, HolodilnikRuWebPageType.Catalog, page.Path);
        }
    }
}