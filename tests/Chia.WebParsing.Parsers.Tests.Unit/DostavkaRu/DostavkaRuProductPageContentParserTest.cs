using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.DostavkaRu;
using Chia.WebParsing.Parsers.DostavkaRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DostavkaRu
{
    [TestClass]
    public class DostavkaRuProductPageContentParserTest : DostavkaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DostavkaRu\Pages\product.mht", "DostavkaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DostavkaRu\product.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, DostavkaRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new DostavkaRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("6949441", product.Article);
            Assert.AreEqual("Холодильник Shivaki SHRF-240CH", product.Name);
            Assert.AreEqual(11490, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DostavkaRu\Pages\product_out_of_stock.mht", "DostavkaRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DostavkaRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, DostavkaRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new DostavkaRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}