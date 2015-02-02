using System.Linq;
using Chia.WebParsing.Companies.TehnoparkRu;
using Chia.WebParsing.Parsers.TehnoparkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoparkRu
{
    [TestClass]
    public class TehnoparkRuProductPageContentParserTest : TehnoparkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\product.mht", "TehnoparkRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\product.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("50982", product.Article);
            Assert.AreEqual("Музыкальный центр Philips DCM2260", product.Name);
            Assert.AreEqual(5390, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\product_out_of_stock.mht",
            "TehnoparkRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}