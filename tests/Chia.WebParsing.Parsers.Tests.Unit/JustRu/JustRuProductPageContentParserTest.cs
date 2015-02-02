using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.JustRu;
using Chia.WebParsing.Parsers.JustRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.JustRu
{
    [TestClass]
    public class JustRuProductPageContentParserTest : JustRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\JustRu\Pages\product.mht", "JustRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"JustRu\product.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, JustRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new JustRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("335622", product.Article);
            Assert.AreEqual("Ноутбук Lenovo IdeaPad Flex 10 10.1\" 1366x768 N2807 1.58GHz 2Gb 320Gb Intel HD Bluetooth Win8.1 + 2013 Office H&S коричневый 59422994", product.Name);
            Assert.AreEqual(17700, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }
    }
}