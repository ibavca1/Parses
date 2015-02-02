using System.Linq;
using Chia.WebParsing.Companies.RegardRu;
using Chia.WebParsing.Parsers.RegardRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.RegardRu
{
    [TestClass]
    public class RegardRuCatalogPageContentParserTest : RegardRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RegardRu\Pages\catalog.mht", "RegardRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RegardRu\catalog.mht");
            WebPage page = CreatePage(content, RegardRuCity.Moscow, RegardRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new RegardRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstOrDefaultWithType(RegardRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RegardRuWebPageType.Product, "/catalog/tovar139801.htm", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RegardRu\Pages\catalog.mht", "RegardRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RegardRu\catalog.mht");
            WebPage page = CreatePage(content, RegardRuCity.Moscow, RegardRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new RegardRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(RegardRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RegardRuWebPageType.Catalog, "/catalog/group24000/page2.htm", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RegardRu\Pages\catalog_last_page.mht", "RegardRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RegardRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, RegardRuCity.Moscow, RegardRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new RegardRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(RegardRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}