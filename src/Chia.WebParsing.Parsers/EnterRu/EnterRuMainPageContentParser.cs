using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.EnterRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EnterRu
{
    internal class EnterRuMainPageContentParser : EnterRuHtmlPageContentParser, IEnterRuWebPageContentParser
    {
        public EnterRuWebPageType PageType
        {
            get { return EnterRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//ul[@class='navsite']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(@".//li[contains(@class,'navsite_i navsite_i-child')]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                Uri uri = menuItemNode.GetUri(page);
                string name = menuItemNode
                    .GetSingleNode(".//span[@class='navsite_tx']")
                    .GetInnerText();

                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, EnterRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}