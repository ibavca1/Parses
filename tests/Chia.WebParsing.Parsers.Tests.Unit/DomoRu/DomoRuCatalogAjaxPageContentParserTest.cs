using System;
using System.Linq;
using Chia.WebParsing.Companies.DomoRu;
using Chia.WebParsing.Parsers.DomoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.Tests.Unit.DomoRu
{
    [TestClass]
    public class DomoRuCatalogAjaxPageContentParserTest : DomoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\catalog_ajax.html", "DomoRu")]
        public void Test_Parse()
        {
            // arrange
            HtmlPageContent content = ReadHtmlContent(@"DomoRu\catalog_ajax.html");
            WebPage page = CreatePage((Uri)null, DomoRuCity.Kazan, DomoRuWebPageType.CatalogAjax);
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuCatalogAjaxPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition[] positions = result.Positions.ToArray();
            CollectionAssert.IsNotEmpty(positions);
            Assert.AreEqual("Haier CFL 633 CC Холодильник", positions.First().Name);
            Assert.AreEqual(null, positions.First().Article);
            Assert.AreEqual(27530, positions.First().OnlinePrice);
            Assert.AreEqual(0, positions.First().RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalog/kuhonnaya-tehnika-3/holodilniki-200/haier-cfl-633-cc-holodilnik-0135768"), positions.First().Uri);
            Assert.AreEqual("Samsung RL-58GHEIH Холодильник", positions.Last().Name);
            Assert.AreEqual(null, positions.Last().Article);
            Assert.AreEqual(35990, positions.Last().OnlinePrice);
            Assert.AreEqual(0, positions.Last().RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalog/kuhonnaya-tehnika-3/holodilniki-200/samsung-rl-58gheih-holodilnik-0126769"), positions.Last().Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\catalog_ajax.html", "DomoRu")]
        public void Test_Parse_NextPage()
        {
            // arrange
            HtmlPageContent content = ReadHtmlContent(@"DomoRu\catalog_ajax.html");
            WebPage page = CreatePage("?categoryId=10103", DomoRuCity.Kazan, DomoRuWebPageType.CatalogAjax, "path");
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuCatalogAjaxPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(DomoRuWebPageType.CatalogAjax);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(page.Path, nextPage.Path);
            Assert.AreEqual("3", nextPage.Uri.GetQueryParam("page"));
            Assert.AreEqual("10103", nextPage.Uri.GetQueryParam("categoryId"));
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\catalog_ajax_last_page.html", "DomoRu")]
        public void Test_Parse_LastPage()
        {
            // arrange
            HtmlPageContent content = ReadHtmlContent(@"DomoRu\catalog_ajax_last_page.html");
            WebPage page = CreatePage("?categoryId=10103", DomoRuCity.Kazan, DomoRuWebPageType.CatalogAjax);
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuCatalogAjaxPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(DomoRuWebPageType.CatalogAjax);
            Assert.IsNull(nextPage);
        }
    }
}