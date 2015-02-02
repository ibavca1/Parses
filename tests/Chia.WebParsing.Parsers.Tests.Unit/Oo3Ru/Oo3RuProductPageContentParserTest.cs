using System.Linq;
using Chia.WebParsing.Companies.Oo3Ru;
using Chia.WebParsing.Parsers.Oo3Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Oo3Ru
{
    [TestClass]
    public class Oo3RuProductPageContentParserTest : Oo3RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\product.mht", "Oo3Ru")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\product.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("1216128", product.Article);
            Assert.AreEqual("Холодильник Liebherr Premium CTNesf 3663-20 001", product.Name);
            Assert.AreEqual(39990, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\product_out_of_stock.mht", "Oo3Ru")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\product_out_of_stock.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\product_no_article.mht", "Oo3Ru")]
        public void Test_ParseHtml_NoArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\product_no_article.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\product_no_price.mht", "Oo3Ru")]
        public void Test_ParseHtml_NoPrice()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\product_no_price.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}