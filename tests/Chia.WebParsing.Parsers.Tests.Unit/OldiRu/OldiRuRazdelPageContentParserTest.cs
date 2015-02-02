using System.Linq;
using Chia.WebParsing.Companies.OldiRu;
using Chia.WebParsing.Parsers.OldiRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OldiRu
{
    [TestClass]
    public class OldiRuRazdelPageContentParserTest : OldiRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\razdel.mht", "OldiRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\razdel.mht");
            WebPage page = CreatePage(content, OldiRuCity.Moscow, OldiRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => OldiRuWebPageType.Razdel == (OldiRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, OldiRuWebPageType.Razdel, "/catalog/6535/", "Ноутбуки"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, OldiRuWebPageType.Razdel, "/catalog/7621/", "Оперативная память для ноутбуков"), result.Pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\catalog.mht", "OldiRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\catalog.mht");
            WebPage page = CreatePage(content, OldiRuCity.Moscow, OldiRuWebPageType.Razdel,"path");
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OldiRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}