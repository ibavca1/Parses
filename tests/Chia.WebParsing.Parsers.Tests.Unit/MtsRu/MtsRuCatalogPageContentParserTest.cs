using System.Linq;
using Chia.WebParsing.Companies.MtsRu;
using Chia.WebParsing.Parsers.MtsRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MtsRu
{
    [TestClass]
    public class MtsRuCatalogPageContentParserTest : MtsRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\catalog.mht", "MtsRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\catalog.mht");
            WebPage page = CreatePage(content, MtsRuCity.Chelyabinsk, MtsRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("МТС 982T White для работы в сети МТС", product.Name);
            Assert.AreEqual(2490, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Site.MakeUri("/smartfony/mts/smartfon-mts-982t-white-dlya-raboty-v-seti-mts.html"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\catalog_out_of_stock.mht", "MtsRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, MtsRuCity.Chelyabinsk, MtsRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\catalog.mht", "MtsRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\catalog.mht");
            WebPage page = CreatePage(content, MtsRuCity.Chelyabinsk, MtsRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(MtsRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MtsRuWebPageType.Catalog, "/smartfony/?PAGEN_1=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\catalog_last_page.mht", "MtsRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, MtsRuCity.Chelyabinsk, MtsRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(MtsRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}