using System.Linq;
using Chia.WebParsing.Companies.OnlinetradeRu;
using Chia.WebParsing.Parsers.OnlinetradeRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OnlinetradeRu
{
    [TestClass]
    public class OnlinetradeRuProductPageContentParserTest : OnlinetradeRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\product.mht", "OnlinetradeRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\product.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.Moscow, OnlinetradeRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new OnlinetradeRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("87804", product.Article);
            Assert.AreEqual("Цифровой фотоаппарат Canon PowerShot SX50 HS", product.Name);
            Assert.AreEqual(13400, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }
    }
}