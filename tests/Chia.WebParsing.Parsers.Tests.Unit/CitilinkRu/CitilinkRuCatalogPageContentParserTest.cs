using Chia.WebParsing.Companies.CitilinkRu;
using Chia.WebParsing.Parsers.CitilinkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CitilinkRu
{
    [TestClass]
    public class CitilinkRuCatalogPageContentParserTest : CitilinkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\catalog.mht", "CitilinkRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\catalog.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.Moscow, CitilinkRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new CitilinkRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(CitilinkRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CitilinkRuWebPageType.Product, "/catalog/mobile/notebooks/913666/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\catalog.mht", "CitilinkRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\catalog.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.Moscow, CitilinkRuWebPageType.Catalog, "path"); 
            var context = new WebPageContentParsingContext();
            var parser = new CitilinkRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(CitilinkRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CitilinkRuWebPageType.Catalog, "/catalog/mobile/notebooks/?p=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\catalog_last_page.mht", "CitilinkRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.Moscow, CitilinkRuWebPageType.Catalog); 
            var context = new WebPageContentParsingContext();
            var parser = new CitilinkRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(CitilinkRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}