using System.Linq;
using Chia.WebParsing.Companies.TdPoiskRu;
using Chia.WebParsing.Parsers.TdPoiskRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TdPoiskRu
{
    [TestClass]
    public class TdPoiskRuMainPageContentParserTest : TdPoiskRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\main.mht", "TdPoiskRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\main.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TdPoiskRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Razdel, "/catalog/tv_video/", "ТВ/Видео"), pages.First());
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Razdel, "/catalog/avtomobilnaya_elektronika/", "Автомобильная электроника"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\main.mht", "TdPoiskRu")]
        public void Test_ParseHtml_ShopsPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\main.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Main, "path");
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};
            var parser = new TdPoiskRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(TdPoiskRuWebPageType.ShopsList);
            Assert.AreEqual(true,  shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.ShopsList, "/shops/", page.Path), shopsPage);
        }
    }
}