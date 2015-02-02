using System.Linq;
using Chia.WebParsing.Companies.TenIRu;
using Chia.WebParsing.Parsers.TenIRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TenIRu
{
    [TestClass]
    public class TenIRuProductPageContentParserTest : TenIRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TenIRu\Pages\product.mht", "TenIRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TenIRu\product.mht");
            WebPage page = CreatePage(content, TenIRuCity.Moscow, TenIRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TenIRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("207189", product.Article);
            Assert.AreEqual("Пароварка Philips HD 9120/00 White", product.Name);
            Assert.AreEqual(1880, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }
    }
}