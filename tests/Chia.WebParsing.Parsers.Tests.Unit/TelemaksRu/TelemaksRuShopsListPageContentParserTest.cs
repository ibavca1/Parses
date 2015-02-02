using System.Linq;
using Chia.WebParsing.Companies.TelemaksRu;
using Chia.WebParsing.Parsers.TelemaksRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TelemaksRu
{
    [TestClass]
    public class TelemaksRuShopsListPageContentParserTest : TelemaksRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\shops_list.mht", "TelemaksRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\shops_list.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops);
            Assert.AreEqual("Ленинский пр., дом 127", shops.First().Name);
            Assert.AreEqual("Ленинский пр., дом 127", shops.First().Address);
            Assert.AreEqual("Энгельса пр., дом 139/21", shops.Last().Name);
            Assert.AreEqual("Энгельса пр., дом 139/21", shops.Last().Address);
        }
    }
}