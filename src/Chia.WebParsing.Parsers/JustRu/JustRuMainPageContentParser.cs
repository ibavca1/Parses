using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.JustRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.JustRu
{
    internal class JustRuMainPageContentParser : HtmlPageContentParser, IJustRuWebPageContentParser
    {
        public JustRuWebPageType PageType
        {
            get { return JustRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode.ElementsFlags.Add("li", HtmlElementFlag.Empty | HtmlElementFlag.Closed);

            HtmlNodeCollection menuItemsNodes =
                content.GetNodes(@"//ul[@id='nav']/li");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                HtmlNodeCollection subItemsNodes =
                    menuItemNode.GetNodes(@".//div[@class='c']//ul/li/a");

                string menuItemName = menuItemNode
                    .GetSingleNode(@"a/em/span")
                    .GetInnerText();

                bool isAppleNode = 
                    menuItemNode.HasNode(@".//strong[@class='apple-symbol']");
                if (isAppleNode)
                {
                    menuItemName = "Apple";
                }

                WebSiteMapPath menuItemPath = page.Path.Add(menuItemName);

                foreach (HtmlNode subItemsNode in subItemsNodes)
                {
                    string subItemName = subItemsNode.GetInnerText();
                    Uri subItemUri = subItemsNode.GetUri(page);
                    WebSiteMapPath subItemPath = menuItemPath.Add(subItemName);
                    WebPage subItemPage = page.Site.GetPage(subItemUri, JustRuWebPageType.Catalog, subItemPath);
                    subItemPage.IsPartOfSiteMap = true;
                    pages.Add(subItemPage);
                }
            }

            HtmlNode.ElementsFlags.Remove("li");

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}