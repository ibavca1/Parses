using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using Chia.WebParsing.Parsers.MvideoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MvideoRu
{
    [TestClass]
    public class MvideoRuShopsListPageContentParserTest : MvideoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\shops_list.mht", "MvideoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\shops_list.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuShopsListPageContentParser();

            // act
           WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops);
            Assert.AreEqual("Пр-т Мира, 91 к.1", shops.First().Address);
            Assert.AreEqual("МО, г.Ногинск, ул. Трудовая, 11, ТЦ «Ногинский», 3 этаж", shops.Last().Address);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\shops_list.mht", "MvideoRu")]
        public void Test_Parse_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\shops_list.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(MvideoRuWebPageType.ShopsList);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.ShopsList, "/shops/store-list?page=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\shops_list_last_page.mht", "MvideoRu")]
        public void Test_Parse_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\shops_list_last_page.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(MvideoRuWebPageType.ShopsList);
            Assert.IsNull(nextPage);
        }
    }
}