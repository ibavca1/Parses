using System.Linq;
using Chia.WebParsing.Companies.UtinetRu;
using Chia.WebParsing.Parsers.UtinetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UtinetRu
{
    [TestClass]
    public class UtinetRuProductPageContentParserTest : UtinetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\product.mht", "UtinetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\product.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Ноутбук Dell Alienware A18 Silver $$ A18-9264", product.Name);
            Assert.AreEqual("A18-9264", product.Characteristics);
            Assert.AreEqual(150360, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\product_manufacturer_code_inside_name.mht", "UtinetRu")]
        public void Test_ParseHtml_ManufacturerCodeInsideName()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\product_manufacturer_code_inside_name.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("Ноутбук Lenovo ThinkPad T440 20B60047RT", product.Name);
            Assert.AreEqual("20B60047RT", product.Characteristics);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\product_out_of_stock.mht", "UtinetRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\catalog.mht", "UtinetRu")]
        public void Test_ParseHtml_NotProductPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\catalog.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }
    }
}