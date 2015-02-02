using System.Linq;
using Chia.WebParsing.Companies.TechportRu;
using Chia.WebParsing.Parsers.TechportRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechportRu
{
    [TestClass]
    public class TechportRuProductPageContentParserTest : TechportRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\product.mht", "TechportRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\product.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("id3363", product.Article);
            Assert.AreEqual("Холодильник Indesit TT 85", product.Name);
            Assert.AreEqual(8690, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Site.MakeUri("/katalog/products/holodilniki/odnokamernye/holodilnik-indesit-tt-85"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\product_out_of_stock.mht", "TechportRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}