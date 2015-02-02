using System.Linq;
using Chia.WebParsing.Companies.TehnosilaRu;
using Chia.WebParsing.Parsers.TehnosilaRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnosilaRu
{
    [TestClass]
    public class TehnosilaRuShopsListPageContentParserTest : TehnosilaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\shops_list.mht", "TehnosilaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\shops_list.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops);
            Assert.AreEqual("Москва, Менжинского ул., д.38", shops.First().Address);
            Assert.AreEqual("Бабушкинская", shops.First().Name);
            Assert.AreEqual("Подольск, ул.Свердлова, д.26", shops.Last().Address);
            Assert.AreEqual("ТЦ \"Галерея\"", shops.Last().Name);
        }
    }
}