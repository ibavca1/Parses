using System.Linq;
using Chia.WebParsing.Companies.TehnoshokRu;
using Chia.WebParsing.Parsers.TehnoshokRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoshokRu
{
    [TestClass]
    public class TehnoshokRuShopsListPageContentParserTest : TehnoshokRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\shops_list.mht", "TehnoshokRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\shops_list.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops);
            Assert.AreEqual("г. Санкт-Петербург, Большой пр. В.О. д. 18, \"Андреевский\"", shops.First().Name);
            Assert.AreEqual("г. Санкт-Петербург, Большой пр. В.О. д. 18, \"Андреевский\"", shops.First().Address);
            Assert.AreEqual("г. Санкт-Петербург, Революции шоссе, д.41/39, ТЦ \"Норд\", Техносила (Партнёр)", shops.Last().Name);
            Assert.AreEqual("г. Санкт-Петербург, Революции шоссе, д.41/39, ТЦ \"Норд\", Техносила (Партнёр)", shops.Last().Address);
        }
    }
}