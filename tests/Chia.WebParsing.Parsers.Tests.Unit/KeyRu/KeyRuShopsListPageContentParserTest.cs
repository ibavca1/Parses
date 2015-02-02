using System.Linq;
using Chia.WebParsing.Companies.KeyRu;
using Chia.WebParsing.Parsers.KeyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.KeyRu
{
    [TestClass]
    public class KeyRuShopsListPageContentParserTest : KeyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\shops_list.mht", "KeyRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\shops_list.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();

            var parser = new KeyRuShopsListPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops);
            Assert.AreEqual("Стачек пл., д.9.", shops.First().Name);
            Assert.AreEqual("Стачек пл., д.9.", shops.First().Address);
            Assert.AreEqual("Фучика ул., д.2", shops.Last().Name);
            Assert.AreEqual("Фучика ул., д.2", shops.Last().Address);
        }
    }
}