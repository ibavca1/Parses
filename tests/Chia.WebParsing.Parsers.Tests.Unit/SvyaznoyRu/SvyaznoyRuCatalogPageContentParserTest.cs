using Chia.WebParsing.Companies.SvyaznoyRu;
using Chia.WebParsing.Parsers.SvyaznoyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.SvyaznoyRu
{
    [TestClass]
    public class SvyaznoyRuCatalogPageContentParserTest : SvyaznoyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\catalog.mht", "SvyaznoyRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\catalog.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.Chelyabinsk, SvyaznoyRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(SvyaznoyRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Product, "/catalog/phone/224/2190158", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\catalog_next_page.mht", "SvyaznoyRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.Chelyabinsk, SvyaznoyRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(SvyaznoyRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Catalog, "/catalog/phone/224?PAGEN_1=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\catalog_last_page.mht", "SvyaznoyRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.Chelyabinsk, SvyaznoyRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(SvyaznoyRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}