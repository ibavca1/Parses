using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using Chia.WebParsing.Parsers.UlmartRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UlmartRu
{
    [TestClass]
    public class UlmartRuShopPageContentParserTest : UlmartRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\shop.mht", "UlmartRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\shop.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuShopPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop shop = result.Shops.Single();
            Assert.AreEqual("Варшавское шоссе д. 143а", shop.Name);
            Assert.AreEqual("Варшавское шоссе д. 143а", shop.Address);
        }
    }
}