using System;
using Chia.WebParsing.Companies.DomoRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.DomoRu
{
    internal class DomoRuCatalogAjaxPageContentParser : DomoRuCatalogPageContentParser
    {
        public override DomoRuWebPageType Type
        {
            get { return DomoRuWebPageType.CatalogAjax; }
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

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode paginationNode =
                content.SelectSingleNode(@"//ul[@id='pages_list_id']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(".//li[@class='active']/following-sibling::li/a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            string nextPageNumber = nextPageNode.GetInnerText();
            Uri nextPageUri = page.Uri.AddOrReplaceQueryParam("page", nextPageNumber);
            return page.Site.GetPage(nextPageUri, DomoRuWebPageType.CatalogAjax, page.Path);
        }

        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
        }
    }
}