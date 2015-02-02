using System.Linq;
using Chia.WebParsing.Companies.TehnosilaRu;
using Chia.WebParsing.Parsers.TehnosilaRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnosilaRu
{
    [TestClass]
    public class TehnosilaRuMainPageContentParserTest : TehnosilaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\main.mht", "TehnosilaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\main.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TehnosilaRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.Razdel, "catalog/tv_i_video", "Телевизоры, аудио, видео"), pages.First());
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.Razdel, "catalog/elitnaya_tehnika", "Элитная техника"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\main.mht", "TehnosilaRu")]
        public void Test_ParseHtml_ShopsListPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\main.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Main);
            var context = new WebPageContentParsingContext { Options = { InformationAboutShops = true } };
            var parser = new TehnosilaRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(TehnosilaRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.ShopsList, "info/stores", page.Path), shopsPage);
        }
    }
}