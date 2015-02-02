using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TechportRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechportRu
{
    internal class TechportRuCatalogPageContentParser : TechportRuHtmlPageContentParser, ITechportRuWebPageContentParser
    {
        public TechportRuWebPageType PageType
        {
            get { return TechportRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPage[] categoriesPages = ParseCategories(page, content).ToArray();
            if (categoriesPages.Any())
                return new WebPageContentParsingResult { Pages = categoriesPages };

            WebPageContentParsingResult result = ParseProducts(page, content);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static IEnumerable<WebPage> ParseCategories(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection categoriesNodes =
                content.SelectNodes(@"//a[@class='catalog_main_level']");
            bool noCategories = categoriesNodes == null;
            if (noCategories)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode.GetAttributeText("title");
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);

                WebPage categoryPage = page.Site.GetPage(uri, TechportRuWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return pages;
        }

         private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
         {
             TechportRuCity city = TechportRuCity.Get(page.Site.City);
             HtmlNode productsList =
                content.GetSingleNode(@"//div[@id='katalog_list_content']");

             bool isEmpty = productsList.HasNode(@".//p[contains(text(),'Товаров не найдено!')]");
             if (isEmpty)
                 return WebPageContentParsingResult.Empty;

             HtmlNodeCollection positionsNodes =
                 productsList.GetNodes(@".//table[@class='product_row']");
   
             var result = new WebPageContentParsingResult();
             foreach (HtmlNode productNode in positionsNodes)
             {
                 string article = productNode
                     .GetSingleNode(@".//div[@class='b' and contains(text(),'Код:')]")
                     .GetInnerText()
                     .Replace("Код:", "").Trim();
                 string name = productNode
                     .GetSingleNode(@".//a[@class='bk_name']")
                     .GetInnerText();
                 Uri uri = productNode
                     .GetSingleNode(@".//a[@class='bk_name']")
                     .GetUri(page);
                 decimal price = GetPrice(city, productNode);

                 var position = new WebMonitoringPosition(article, name, price, uri);
                 result.Positions.Add(position);
             }

             return result;
         }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagingNode =
                document.SelectSingleNode(@"//div[@id='tlist-pager']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
              pagingNode.SelectSingleNode(@".//a[@class='tpagerp next']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, TechportRuWebPageType.Catalog, page.Path);
        }
    }
}