using System.Linq;
using Chia.WebParsing.Companies.TehnoshokRu;
using Chia.WebParsing.Parsers.TehnoshokRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoshokRu
{
    [TestClass]
    public class TehnoshokRuMainPageContentParserTest : TehnoshokRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\main.mht", "TehnoshokRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\main.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TehnoshokRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoshokRuWebPageType.Razdel, "/tv_audio_video", "Телевизоры, аудио, видео"), pages.First());
            Assert.AreEqual(CreatePage(page, TehnoshokRuWebPageType.Razdel, "/group.html/624", "Товары для дома, дачи и туризма"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\main.mht", "TehnoshokRu")]
        public void Test_ParseHtml_ShopsListPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\main.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Main);
            var context = new WebPageContentParsingContext { Options = { InformationAboutShops = true } };
            var parser = new TehnoshokRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(TehnoshokRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, TehnoshokRuWebPageType.ShopsList, "http://tshok.ru/shops/", page.Path), shopsPage);
        }
    }
}