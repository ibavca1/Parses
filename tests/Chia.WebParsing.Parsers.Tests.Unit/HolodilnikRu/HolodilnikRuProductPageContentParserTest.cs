using System.Linq;
using Chia.WebParsing.Companies.HolodilnikRu;
using Chia.WebParsing.Parsers.HolodilnikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.HolodilnikRu
{
    [TestClass]
    public class HolodilnikRuProductPageContentParserTest : HolodilnikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\product.mht", "HolodilnikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\product.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("176941", product.Article);
            Assert.AreEqual("Однокамерный холодильник Daewoo Electronics FR 081 AR", product.Name);
            Assert.AreEqual(6375, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(CreateUri(page, "/refrigerator/one_chamber_refrigerators/daewoo/fr081ar/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\product_out_of_stock.mht", "HolodilnikRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}