using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.OzonRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OzonRu
{
    internal class OzonRuRazdelPageContentParser : OzonRuHtmlPageContentParser, IOzonRuWebPageContentParser
    {
        public OzonRuWebPageType PageType
        {
            get { return OzonRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            var pages = new List<WebPage>();

            HtmlNodeCollection catalogsNodes =
                content.SelectNodes(@"//div[@class='bLeftMainMenu_eElementsBlock']");

            if (catalogsNodes == null)
            {
                pages = ParseCategories(page, content.Document.DocumentNode, page.Path).ToList();
            }
            else
            {
                foreach (HtmlNode catalogsNode in catalogsNodes)
                {
                    // этим элементом может быть как <span> так и <a>, поэтому *
                    HtmlNode uriNode =
                        catalogsNode.GetSingleNode(@".//*[@class='bLeftMainMenu_eTitle']");
                    string name = uriNode.GetInnerText();
                    WebSiteMapPath path = page.Path.Add(name);

                    if (uriNode.Name == "a")
                    {
                        Uri catalogUri = uriNode.GetUri(page);
                        WebPage catalogPage = page.Site.GetPage(catalogUri, OzonRuWebPageType.Catalog, path);
                        catalogPage.IsPartOfSiteMap = true;
                        pages.Add(catalogPage);
                    }
                    else
                    {
                        WebPage[] categoriesPages = ParseCategories(page, catalogsNode, path).ToArray();
                        pages.AddRange(categoriesPages);
                    }
                }
            }
            
            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseCategories(WebPage page, HtmlNode catalogNode, WebSiteMapPath catalogPath)
        {
            HtmlNodeCollection categoriesNodes =
                catalogNode.GetNodes(@".//a[@class='bLeftMainMenu_eLink']");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string categoryName = categoryNode.GetInnerText();
                WebSiteMapPath categoryPath = catalogPath.Add(categoryName);
                Uri categoryUri = categoryNode.GetUri(page);
                WebPage categoryPage = page.Site.GetPage(categoryUri, OzonRuWebPageType.Catalog, categoryPath);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return pages;
        }
    }
}