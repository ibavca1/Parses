using System.Linq;
using Chia.WebParsing.Companies.NewmansRu;
using Chia.WebParsing.Parsers.NewmansRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NewmansRu
{
    [TestClass]
    public class NewmansRuRazdelPageContentParserTest : NewmansRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\razdel.mht", "NewmansRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\razdel.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Krasnodar, NewmansRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => NewmansRuWebPageType.Razdel == (NewmansRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, NewmansRuWebPageType.Razdel, "/computers/display/", "Мониторы"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, NewmansRuWebPageType.Razdel, "/computers/1276/", "Системы видеонаблюдения"), result.Pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\catalog.mht", "NewmansRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\catalog.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Moscow, NewmansRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, NewmansRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}