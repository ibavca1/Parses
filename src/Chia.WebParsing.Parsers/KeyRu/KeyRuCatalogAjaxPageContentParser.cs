using System;
using System.Collections.Specialized;
using System.Globalization;
using Chia.WebParsing.Companies.KeyRu;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.KeyRu
{
    internal class KeyRuCatalogAjaxPageContentParser : HtmlPageContentParser, IKeyRuWebPageContentParser
    {
        public KeyRuWebPageType Type
        {
            get { return KeyRuWebPageType.CatalogAjax; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            bool isContentEmpty = string.IsNullOrWhiteSpace(content.ReadAsString());
            if (isContentEmpty)
                return WebPageContentParsingResult.Empty;

            WebPageContentParsingResult result = ParseProducts(page, content, context.Options);

            if (!result.IsEmpty)
            {
                WebPage nextPage = ParseNextPage(page);
                if (nextPage != null)
                    result.Pages.Add(nextPage);
            }

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content,
            WebSiteParsingOptions options)
        {
            return KeyRuCatalogPageContentParser.ParseProductsNodes(page, content.Document.DocumentNode, options);
        }

        private static WebPage ParseNextPage(WebPage page)
        {
            NameValueCollection query = page.Uri.GetQueryParams();

            if (query["p"] == null)
                return null;

            int nextPageNumber = int.Parse(query["p"])+1;
            Uri nextPageUri = page.Uri.AddOrReplaceQueryParam("p", nextPageNumber.ToString(CultureInfo.InvariantCulture));
            return page.Site.GetPage(nextPageUri, KeyRuWebPageType.CatalogAjax, page.Path);
        }
    }
}