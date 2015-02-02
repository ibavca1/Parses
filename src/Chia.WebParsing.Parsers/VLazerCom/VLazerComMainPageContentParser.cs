using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.VLazerCom;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.VLazerCom
{
    internal class VLazerComMainPageContentParser : VLazerComHtmlPageContentParser, IVLazerComWebPageContentParser
    {
        public VLazerComWebPageType PageType
        {
            get { return VLazerComWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode razdelsList =
                content.GetSingleNode(@"//div[contains(@class,'catmenu_container')]");
            HtmlNodeCollection razldesNodes =
                razdelsList.SelectNodes(@".//td[@class='bg_catmenu']//span[@class='name']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in razldesNodes)
            {
                string name = categoryNode.GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, VLazerComWebPageType.Catalog, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}