using System.Linq;
using Chia.WebParsing.Companies.LogoRu;
using Chia.WebParsing.Parsers.LogoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.LogoRu
{
    [TestClass]
    public class LogoRuProductPageContentParserTest : LogoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\product.mht", "LogoRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\product.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Пылесос Bosch BSB 2982", product.Name);
            Assert.AreEqual(4790, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\product_out_of_stock.mht", "LogoRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}