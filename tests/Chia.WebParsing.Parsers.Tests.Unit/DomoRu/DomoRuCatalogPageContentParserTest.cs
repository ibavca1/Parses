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
    public class DomoRuCatalogPageContentParserTest : DomoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\catalog.mht", "DomoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\catalog.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("POZIS-—¬»я√ј-513-3 C холодильник", product.Name);
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual(9290, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalog/kuhonnaya-tehnika-3/holodilniki-200/pozis-sviyaga-513-3-c-holodilnik-0003587"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\catalog.mht", "DomoRu")]
        public void Test_Parse_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\catalog.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {ProductArticle = true}};
            var parser = new DomoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(DomoRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DomoRuWebPageType.Product, "/catalog/kuhonnaya-tehnika-3/holodilniki-200/pozis-sviyaga-513-3-c-holodilnik-0003587", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\catalog.mht", "DomoRu")]
        public void Test_Parse_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\catalog.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(DomoRuWebPageType.CatalogAjax);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(page.Path, nextPage.Path);
            Assert.AreEqual("10103", nextPage.Uri.GetQueryParam("categoryId"));
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\catalog_single_page.mht", "DomoRu")]
        public void Test_Parse_SinglePage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\catalog_single_page.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(DomoRuWebPageType.CatalogAjax);
            Assert.IsNull(nextPage);
        }
    }
}