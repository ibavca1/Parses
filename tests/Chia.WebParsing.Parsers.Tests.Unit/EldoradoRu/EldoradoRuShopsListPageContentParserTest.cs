using System.Linq;
using Chia.WebParsing.Companies.EldoradoRu;
using Chia.WebParsing.Parsers.EldoradoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EldoradoRu
{
    [TestClass]
    public class EldoradoRuShopsListPageContentParserTest : EldoradoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\shops_list.mht", "EldoradoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\shops_list.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops);
            Assert.AreEqual("ТЦ «Первомайский»", shops.First().Name);
            Assert.AreEqual("ул. 9-ая Парковая, д. 62/64", shops.First().Address);
            Assert.AreEqual("ТРЦ «Райкин Плаза»", shops.Last().Name);
            Assert.AreEqual("ул. Шереметьевская, д. 6, стр.1", shops.Last().Address);
        }
    }
}