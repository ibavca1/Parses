using System.Linq;
using Chia.WebParsing.Companies.EnterRu;
using Chia.WebParsing.Parsers.EnterRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EnterRu
{
    [TestClass]
    public class EnterRuProductPageContentParserTest : EnterRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\product.mht", "EnterRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\product.mht");
            WebPage page = CreatePage(content, EnterRuCity.StPetersburg, EnterRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("473-0145", product.Article);
            Assert.AreEqual("Ноутбук HP 15-r098sr (J8D70EA) черный", product.Name);
            Assert.AreEqual(10890, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\product_empty_model.mht", "EnterRu")]
        public void Test_ParseHtml_EmptyModel()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\product_empty_model.mht");
            WebPage page = CreatePage(content, EnterRuCity.StPetersburg, EnterRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("Удлинитель 1/4DR 50 мм Ombra 221402", product.Name);
        }
    }
}