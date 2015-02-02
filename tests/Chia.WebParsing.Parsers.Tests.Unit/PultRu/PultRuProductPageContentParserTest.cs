using System.Linq;
using Chia.WebParsing.Companies.PultRu;
using Chia.WebParsing.Parsers.PultRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.PultRu
{
    [TestClass]
    public class PultRuProductPageContentParserTest : PultRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\product.mht", "PultRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\product.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("47227", product.Article);
            Assert.AreEqual("Комплект акустики Yamaha NS-PC210 piano black", product.Name);
            Assert.AreEqual(10499, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\product_no_price.mht", "PultRu")]
        public void Test_ParseHtml_NoPrice()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\product_no_price.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\product_no_article.mht", "PultRu")]
        public void Test_ParseHtml_NoArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\product_no_article.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\product_under_order.mht", "PultRu")]
        public void Test_ParseHtml_UnderOrder()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\product_under_order.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\product_error_404.mht", "PultRu")]
        public void Test_ParseHtml_404()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\product_error_404.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsEmpty(result.Positions.ToArray());
        }
    }
}