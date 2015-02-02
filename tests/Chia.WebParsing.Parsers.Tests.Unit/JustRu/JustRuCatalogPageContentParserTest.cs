using System.Linq;
using Chia.WebParsing.Companies.JustRu;
using Chia.WebParsing.Parsers.JustRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.JustRu
{
    [TestClass]
    public class JustRuCatalogPageContentParserTest : JustRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\JustRu\Pages\catalog.mht", "JustRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"JustRu\catalog.mht");
            WebPage page = CreatePage(content, JustRuCity.Chelyabinsk, JustRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new JustRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("292113", product.Article);
            Assert.AreEqual("Ноутбук Lenovo IdeaPad B590 15.6\" 1366x768 2020M 2.4GHz 2Gb 320Gb DVD-RW Wi-Fi Bluetooth Win8 черный 59397711", product.Name);
            Assert.AreEqual(14840, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Site.MakeUri("/notebooks/292113_noytbyk_15_6_lenovo_ideapad_b590_chernii/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\JustRu\Pages\catalog.mht", "JustRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"JustRu\catalog.mht");
            WebPage page = CreatePage(content, JustRuCity.Chelyabinsk, JustRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new JustRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(JustRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, JustRuWebPageType.Catalog, "?&p=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\JustRu\Pages\catalog_last_page.mht", "JustRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"JustRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, JustRuCity.Chelyabinsk, JustRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new JustRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(JustRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}