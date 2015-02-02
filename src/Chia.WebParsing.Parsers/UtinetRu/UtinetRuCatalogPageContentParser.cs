using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Chia.WebParsing.Companies.UtinetRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.UtinetRu
{
    internal class UtinetRuCatalogPageContentParser : UtinetRuHtmlPageContentParser, IUtinetRuWebPageContentParser
    {
        public UtinetRuWebPageType PageType
        {
            get { return UtinetRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts2(page, content);
            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
        {
            // в каталоге корявые названия, приходится идти в карточк

            HtmlNodeCollection productsNodes =
                content.GetNodes(@"//div[@data-id and @id]");

            var pages = new List<WebPage>();
            foreach (HtmlNode productNode in productsNodes)
            {
                Uri uri = productNode
                    .GetSingleNode(@".//a[@class='image-link']")
                    .GetUri(page);

                WebPage productPage = page.Site.GetPage(uri, UtinetRuWebPageType.Product, page.Path);
                pages.Add(productPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

         private static WebPageContentParsingResult ParseProducts2(WebPage page, HtmlPageContent content)
         {
             HtmlNodeCollection productsNodes =
                content.GetNodes(@"//div[@data-id and @id]");

             var result = new WebPageContentParsingResult();
             foreach (HtmlNode productNode in productsNodes)
             {
                 Uri uri = productNode
                   .GetSingleNode(@".//a[@class='image-link']")
                   .GetUri(page);
                 string name = productNode
                     .GetSingleNode(@".//a[@class='image-link']")
                     .GetInnerText();
                 string manufacturerCode = GetManufacturerCode(productNode);
                 name = MakeName(name, manufacturerCode);
                 decimal price = 0;

                 bool isOutOfStock =
                     productNode.HasNode(@".//b[text()='Нет в наличии']");
                 if (!isOutOfStock)
                 {
                     price = productNode
                         .GetSingleNode(@".//div[@class='mc-offer']/b")
                         .GetPrice();
                 }

                 bool doesNameHasPathWords = HasDoubleWords(name);
                 if (doesNameHasPathWords)
                 {
                     WebPage productPage = page.Site.GetPage(uri, UtinetRuWebPageType.Product, page.Path);
                     result.Pages.Add(productPage);
                 }
                 else
                 {
                     var product = new WebMonitoringPosition(null, name, price, uri)
                                       {Characteristics = manufacturerCode};
                     result.Positions.Add(product);
                 }
             }

             return result;
         }

        private static bool HasDoubleWords(string name)
        {
            var words = new List<string>();

            MatchCollection matches = Regex.Matches(name, @"[\w\d_]+", RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                if (match.Success)
                    words.Add(match.Value.ToUpper());
            }

            for (int i = 0; i < words.Count; i++)
                for (int j = i+1; j < words.Count; j++)
                {
                    int distance = LevenshteinDistance(words[i], words[j]);
                    if (distance <= 1)
                        return true;
                }

            return false;
        }

        private static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            var d = new int[n + 1, m + 1];

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }

        private static string GetManufacturerCode(HtmlNode productNode)
        {
            HtmlNode codeNode = productNode
                .SelectSingleNode(@".//span[contains(text(),'Код') and contains(text(), 'производителя:')]");
            if (codeNode == null)
                return null;

            string code = codeNode.GetInnerText()
                .Replace("Код", "").Replace("производителя:", "").Trim();
            return code;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode nextPageNode =
               content.SelectSingleNode(@"//a[@class='page_nav go_next c-a1']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            string offset = nextPageNode
                .GetAttributeText("data-offset");
            string limit = nextPageNode
                .GetAttributeText("data-limit");

            Uri nextPageUri =
                page.Uri
                    .AddOrReplaceQueryParam("offset", offset)
                    .AddOrReplaceQueryParam("limit", limit);
            return page.Site.GetPage(nextPageUri, UtinetRuWebPageType.Catalog, page.Path);
        }
    }
}