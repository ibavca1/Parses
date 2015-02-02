using System.Linq;
using Chia.WebParsing.Companies.DomoRu;
using Chia.WebParsing.Parsers.DomoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DomoRu
{
    [TestClass]
    public class DomoRuProductPageContentParserTest : DomoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\product.mht", "DomoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\product.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("P_0135241", product.Article);
            Assert.AreEqual("LG GA-B489YVQZ Холодильник", product.Name);
            Assert.AreEqual(page.Uri, product.Uri);
            Assert.AreEqual(25040, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\product_out_of_stock.mht", "DomoRu")]
        public void Test_Parse_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new DomoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}