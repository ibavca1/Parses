using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    internal class MvideoRuRazdelsListPageContentParser : MvideoRuHtmlPageContentParser, IMvideoRuWebPageContentParser
    {
        public MvideoRuWebPageType Type
        {
            get { return MvideoRuWebPageType.RazdelsList; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode razdelsList =
                content.GetSingleNode(@"//div[contains(@class,'sitemap-product-list')]");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@".//div[@data-init]");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string razdelName = razdelNode
                    .GetSingleNode(@"h2")
                    .GetInnerText();
                WebSiteMapPath razdelPath = page.Path.Add(razdelName);

                HtmlNodeCollection groupsNodes =
                    razdelNode.GetNodes(".//div[@class='accordion-group']");

                WebPage[] groupsPages = ParseGroups(page, groupsNodes, razdelPath).ToArray();
                pages.AddRange(groupsPages);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }

        private static IEnumerable<WebPage> ParseGroups(WebPage page, IEnumerable<HtmlNode> groupsNodes, WebSiteMapPath razdelPath)
        {
            var pages = new List<WebPage>();
            foreach (HtmlNode groupNode in groupsNodes)
            {
                string groupName = groupNode
                    .GetSingleNode(@".//div[@class='accordion-heading']//a")
                    .GetInnerText();
                WebSiteMapPath groupPath =
                    string.IsNullOrWhiteSpace(groupName)
                        ? razdelPath
                        : razdelPath.Add(groupName);

                HtmlNodeCollection categoriesNodes =
                    groupNode.SelectNodes(@".//li[@class='list-element']");
                if (categoriesNodes != null)
                {
                    WebPage[] categoriesPages = ParseCategories(page, categoriesNodes, groupPath).ToArray();
                    pages.AddRange(categoriesPages);
                    continue;
                }

                Uri groupUri = groupNode
                    .GetSingleNode(@".//div[@class='accordion-heading']//a")
                    .GetUri(page);
                WebPage groupPage = page.Site.GetPage(groupUri, MvideoRuWebPageType.Razdel, groupPath);
                groupPage.IsPartOfSiteMap = true;
                pages.Add(groupPage);
            }

            return pages;
        }

        private static IEnumerable<WebPage> ParseCategories(WebPage page, IEnumerable<HtmlNode> categoriesNodes, WebSiteMapPath groupPath)
        {
            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                Uri categoryUri = categoryNode
                    .GetSingleNode(@"a")
                    .GetUri(page);
                string categoryName = categoryNode
                    .GetSingleNode(@"a")
                    .GetInnerText();
                WebSiteMapPath categoryPath = groupPath.Add(categoryName);
                WebPage categoryPage = page.Site.GetPage(categoryUri, MvideoRuWebPageType.Razdel, categoryPath);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return pages;
        }
    }
}