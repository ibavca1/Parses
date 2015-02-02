using System.Linq;
using Chia.WebParsing.Companies.CitilinkRu;
using Chia.WebParsing.Parsers.CitilinkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CitilinkRu
{
    [TestClass]
    public class CitilinkRuProductPageContentParserTest : CitilinkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\product.mht", "CitilinkRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\product.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.Moscow, CitilinkRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new CitilinkRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("913666", product.Article);
            Assert.AreEqual("Нетбук IRU K1002S, 10.1\", Via WM8880, 1.5ГГц, 1Гб, 8Гб SSD,  Mali 400, Android 4.2, серебристый [w1001n]", product.Name);
            Assert.AreEqual(6490, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\product_out_of_stock.mht", "CitilinkRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.Moscow, CitilinkRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new CitilinkRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}