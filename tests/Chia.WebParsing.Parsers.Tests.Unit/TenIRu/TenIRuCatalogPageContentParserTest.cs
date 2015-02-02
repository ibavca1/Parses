using System.Linq;
using Chia.WebParsing.Companies.TenIRu;
using Chia.WebParsing.Parsers.TenIRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TenIRu
{
    [TestClass]
    public class TenIRuCatalogPageContentParserTest : TenIRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TenIRu\Pages\catalog.mht", "TenIRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TenIRu\catalog.mht");
            WebPage page = CreatePage(content, TenIRuCity.Moscow, TenIRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TenIRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("207189", product.Article);
            Assert.AreEqual("Пароварка Philips HD 9120/00 White", product.Name);
            Assert.AreEqual(1880, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/product/Parovarka_Philips_HD_9120_00_White_207189.html"), product.Uri);
        }

        
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TenIRu\Pages\catalog.mht", "TenIRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TenIRu\catalog.mht");
            WebPage page = CreatePage(content, TenIRuCity.Moscow, TenIRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TenIRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(TenIRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TenIRuWebPageType.Catalog, "/cat/parovarki/page-2.html", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TenIRu\Pages\catalog_last_page.mht", "TenIRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TenIRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, TenIRuCity.Moscow, TenIRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TenIRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(TenIRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}