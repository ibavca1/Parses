using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.MtsRu;
using Chia.WebParsing.Parsers.MtsRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MtsRu
{
    [TestClass]
    public class MtsRuRazdelPageContentParserTest : MtsRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\razdel.mht", "MtsRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\razdel.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, MtsRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(MtsRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MtsRuWebPageType.Catalog, "/avto-elektronika/avtomobilnyye-gps-navigatory/", "Навигаторы"), pages.First());
            Assert.AreEqual(CreatePage(page, MtsRuWebPageType.Catalog, "/avto-elektronika/avtomobilnyye-antiradary-radar-detektory/", "Радары"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\catalog.mht", "MtsRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\catalog.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, MtsRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MtsRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}