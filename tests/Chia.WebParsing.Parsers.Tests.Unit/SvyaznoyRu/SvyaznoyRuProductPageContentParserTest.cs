using System.Linq;
using Chia.WebParsing.Companies.SvyaznoyRu;
using Chia.WebParsing.Parsers.SvyaznoyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.SvyaznoyRu
{
    [TestClass]
    public class SvyaznoyRuProductPageContentParserTest : SvyaznoyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\product.mht", "SvyaznoyRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\product.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.Chelyabinsk, SvyaznoyRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("1716079", product.Article);
            Assert.AreEqual("Мобильный телефон Apple iPhone 5s 16Gb (золотистый)", product.Name);
            Assert.AreEqual(24990, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\product_coming_soon.mht", "SvyaznoyRu")]
        public void Test_ParseHtml_ComingSoon()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\product_coming_soon.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.Chelyabinsk, SvyaznoyRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}