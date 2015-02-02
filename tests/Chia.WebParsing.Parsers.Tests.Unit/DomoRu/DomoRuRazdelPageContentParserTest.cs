using System.Linq;
using Chia.WebParsing.Companies.DomoRu;
using Chia.WebParsing.Parsers.DomoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DomoRu
{
    [TestClass]
    public class DomoRuRazdelPageContentParserTest : DomoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\razdel.mht", "DomoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\razdel.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(DomoRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DomoRuWebPageType.Razdel, "/catalog/holodilniki-200", "Холодильники"), pages.First());
            Assert.AreEqual(CreatePage(page, DomoRuWebPageType.Razdel, "/catalog/mikseri-31", "Миксеры"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\catalog.mht", "DomoRu")]
        public void Test_Parse_ProductList()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\catalog.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();

            var parser = new DomoRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DomoRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}