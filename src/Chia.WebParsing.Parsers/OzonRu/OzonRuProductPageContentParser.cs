using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Chia.WebParsing.Companies.OzonRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.OzonRu
{
    internal class OzonRuProductPageContentParser : OzonRuHtmlPageContentParser, IOzonRuWebPageContentParser
    {
        public OzonRuWebPageType PageType
        {
            get { return OzonRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string name = content
                .GetSingleNode(@"//div[@class='bContentColumn']//h1[@itemprop='name']")
                .GetInnerText();

            HtmlNode priceNode = content
                .GetSingleNode(@"//div[@class='bSaleColumn']//div[@itemtype='http://schema.org/Offer']");
            decimal price = ParsePrice(priceNode);

            var product = new WebMonitoringPosition(null, name, price, page.Uri);
            WebPage[] additionalPages = GetAdditionalPages(page, content).ToArray();

            return new WebPageContentParsingResult {Pages = additionalPages, Positions = new[] {product}};
        }


        private static IEnumerable<WebPage> GetAdditionalPages(WebPage page, HtmlPageContent content)
        {
            NameValueCollection query = page.Uri.GetQueryParams();

            var pages = new List<WebPage>();

            if (query["color"] != "true")
            {
                WebPage[] colorPages = GetColorPages(page, content).ToArray();
                pages.AddRange(colorPages);
            }

            if (query["size"] != "true")
            {
                WebPage[] sizePages = GetSizePages(page, content).ToArray();
                pages.AddRange(sizePages);
            }

            return pages;
        }

        private static IEnumerable<WebPage> GetColorPages(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection colorNodes =
                content.SelectNodes(@"//div[@id='js_color']//li[not(contains(@class,'selected'))]");
            if (colorNodes == null)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode colorNode in colorNodes)
            {
                HtmlNode uriNode = colorNode.SelectSingleNode(@".//a");
                if (uriNode == null)
                    continue;

                Uri uri = uriNode.GetUri(page).AddQueryParam("color", "true");
                WebPage colorPage = page.Site.GetPage(uri, OzonRuWebPageType.Product, page.Path);
                pages.Add(colorPage);
            }

            return pages;
        }

        private static IEnumerable<WebPage> GetSizePages(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection sizeNodes =
                content.SelectNodes(@"//div[@id='js_type']//li[not(@class='act')]");
            if (sizeNodes == null)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode colorNode in sizeNodes)
            {
                HtmlNode uriNode = colorNode.SelectSingleNode(@".//a");
                if (uriNode == null)
                    continue;

                Uri uri = uriNode.GetUri(page).AddQueryParam("size", "true");
                WebPage sizePage = page.Site.GetPage(uri, OzonRuWebPageType.Product, page.Path);
                pages.Add(sizePage);
            }

            return pages;
        }
    }
}