using System.Linq;
using Chia.WebParsing.Companies.EurosetRu;
using Chia.WebParsing.Parsers.EurosetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EurosetRu
{
    [TestClass]
    public class EurosetRuProductPageContentParserTest : EurosetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\product.mht", "EurosetRu")]
        public void Test_ParseHtm()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\product.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Packard Bell ENTE69KB-12502G50Mnsk", product.Name);
            Assert.AreEqual(11490, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\product_out_of_stock.mht", "EurosetRu")]
        public void Test_ParseHtm_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}