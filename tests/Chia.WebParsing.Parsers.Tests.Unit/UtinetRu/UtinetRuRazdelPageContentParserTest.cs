using System.Linq;
using Chia.WebParsing.Companies.UtinetRu;
using Chia.WebParsing.Parsers.UtinetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UtinetRu
{
    [TestClass]
    public class UtinetRuRazdelPageContentParserTest : UtinetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\razdel.mht", "UtinetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\razdel.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => UtinetRuWebPageType.Razdel == (UtinetRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, UtinetRuWebPageType.Razdel, "http://note.utinet.ru/notebook/", "Ноутбуки"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, UtinetRuWebPageType.Razdel, "http://note.utinet.ru/accessoires/", "Аксессуары"), result.Pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\catalog.mht", "UtinetRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\catalog.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, UtinetRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}