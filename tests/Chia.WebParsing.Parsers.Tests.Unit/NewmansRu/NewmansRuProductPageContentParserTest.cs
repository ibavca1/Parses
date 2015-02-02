using System.Linq;
using Chia.WebParsing.Companies.NewmansRu;
using Chia.WebParsing.Parsers.NewmansRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NewmansRu
{
    [TestClass]
    public class NewmansRuProductPageContentParserTest : NewmansRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\product.mht", "NewmansRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\product.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Moscow, NewmansRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("2412-0902 [564556]", product.Article);
            Assert.AreEqual("Монитор 24\" Dell U2412M", product.Name);
            Assert.AreEqual(14250, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\product_out_of_stock.mht", "NewmansRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Moscow, NewmansRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\product_empty_name.mht", "NewmansRu")]
        public void Test_ParseHtml_EmptyName()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\product_empty_name.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Moscow, NewmansRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }
    }
}