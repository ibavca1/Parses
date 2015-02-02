using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.OldiRu;
using Chia.WebParsing.Parsers.OldiRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OldiRu
{
    [TestClass]
    public class OldiRuProductPageContentParserTest : OldiRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\product.mht", "OldiRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\product.mht");
            WebPage page = CreatePage(content, OldiRuCity.Moscow, OldiRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("0259273", product.Article);
            Assert.AreEqual("Ноутбук Lenovo IdeaPad G5030", product.Name);
            Assert.AreEqual("Celeron N2830 (2.16)/4G/320G/15.6\"HD GL/Int:Intel HD/No ODD/BT/Win8.1 (80G00096RK) (Black)", product.Characteristics);
            Assert.AreEqual(15796, product.OnlinePrice);
            Assert.AreEqual(16020, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\product_online_only.mht", "OldiRu")]
        public void Test_ParseHtml_OnlinePriceOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\product_online_only.mht");
            WebPage page = CreatePage(content, OldiRuCity.Moscow, OldiRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreNotEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}