using System.Linq;
using Chia.WebParsing.Companies.TechnonetRu;
using Chia.WebParsing.Parsers.TechnonetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechnonetRu
{
    [TestClass]
    public class TechnonetRuRazdelPageContentParserTest : TechnonetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\razdel.mht", "TechnonetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\razdel.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TechnonetRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TechnonetRuWebPageType.Razdel, "/catalog/G10104/", "Холодильники и морозильные камеры"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, TechnonetRuWebPageType.Razdel, "/catalog/G56240/", "Инструмент"), result.Pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\catalog.mht", "TechnonetRu")]
        public void Test_ParseHtml_ProductsList()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\catalog.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(TechnonetRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TechnonetRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);      
        }
    }
}