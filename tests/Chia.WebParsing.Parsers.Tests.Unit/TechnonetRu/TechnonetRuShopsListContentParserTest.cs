using System.Linq;
using Chia.WebParsing.Companies.TechnonetRu;
using Chia.WebParsing.Parsers.TechnonetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechnonetRu
{
    [TestClass]
    public class TechnonetRuShopsListContentParserTest : TechnonetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\shops_list.mht", "TechnonetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\shops_list.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops);
            Assert.AreEqual("ул. Ферина, 19",shops.First().Name);
            Assert.AreEqual("ул. Чернышевского, 88", shops.Last().Name);
        }
    }
}