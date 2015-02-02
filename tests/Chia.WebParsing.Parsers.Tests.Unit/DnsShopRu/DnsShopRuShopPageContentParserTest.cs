using System.Linq;
using Chia.WebParsing.Companies.DnsShopRu;
using Chia.WebParsing.Parsers.DnsShopRu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseLibrary.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DnsShopRu
{
    [TestClass]
    public class DnsShopRuShopPageContentParserTest : DnsShopRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\shop.mht", "DnsShopRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\shop.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Shop);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuShopPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop shop = result.Shops.Single();
            Assert.AreEqual("«ТЦ «Кольцо»", shop.Name);
            Assert.AreEqual("ул. Дарвина, 18", shop.Address);
        }
    }
}