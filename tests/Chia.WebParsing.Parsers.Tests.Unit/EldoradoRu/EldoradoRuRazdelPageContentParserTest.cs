using System.Linq;
using Chia.WebParsing.Companies.EldoradoRu;
using Chia.WebParsing.Parsers.EldoradoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EldoradoRu
{
    [TestClass]
    public class EldoradoRuRazdelPageContentParserTest : EldoradoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\razdel.mht", "EldoradoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\razdel.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(EldoradoRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EldoradoRuWebPageType.Razdel, "/cat/1482093/", "LED телевизоры"), pages.First());
            Assert.AreEqual(CreatePage(page, EldoradoRuWebPageType.Razdel, "/cat/1032/", "Аксессуары для телевизоров и видео"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog.mht", "EldoradoRu")]
        public void Test_Parse_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(EldoradoRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EldoradoRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}