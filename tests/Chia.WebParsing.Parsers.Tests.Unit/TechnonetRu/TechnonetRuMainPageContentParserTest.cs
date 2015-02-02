using System.Linq;
using Chia.WebParsing.Companies.TechnonetRu;
using Chia.WebParsing.Parsers.TechnonetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechnonetRu
{
    [TestClass]
    public class TechnonetRuMainPageContentParserTest : TechnonetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\main.mht", "TechnonetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\main.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TechnonetRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TechnonetRuWebPageType.Razdel, "/catalog/G02310/", "Бытовая техника"), pages.First());
            Assert.AreEqual(CreatePage(page, TechnonetRuWebPageType.Razdel, "/catalog/G09418/", "Фото и видеокамеры"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\main.mht", "TechnonetRu")]
        public void Test_ParseHtml_ShopsPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\main.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Main, "path");
            var context = new WebPageContentParsingContext { Options = { InformationAboutShops = true } };
            var parser = new TechnonetRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(TechnonetRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, TechnonetRuWebPageType.ShopsList, "/shops/", page.Path), shopsPage);
        }
    }
}