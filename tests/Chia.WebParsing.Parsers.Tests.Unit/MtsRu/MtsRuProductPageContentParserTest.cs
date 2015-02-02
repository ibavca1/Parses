using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.MtsRu;
using Chia.WebParsing.Parsers.MtsRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MtsRu
{
    [TestClass]
    public class MtsRuProductPageContentParserTest : MtsRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\product.mht", "MtsRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\product.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, MtsRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Nokia Lumia 735 Dark Gray", product.Name);
            Assert.AreEqual(12990, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\product_out_of_stock.mht", "MtsRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, MtsRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}