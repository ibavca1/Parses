using System.Linq;
using Chia.WebParsing.Companies.EldoradoRu;
using Chia.WebParsing.Parsers.EldoradoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EldoradoRu
{
    [TestClass]
    public class EldoradoRuMainPageContentParserTest : EldoradoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\main.mht", "EldoradoRu")]
        public void Test_Parse_MainMenu()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\main.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(EldoradoRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EldoradoRuWebPageType.Razdel,"/cat/159976213/", "Товары для дома, сада и ремонта"), pages.First());
            Assert.AreEqual(CreatePage(page, EldoradoRuWebPageType.Razdel, "/cat/1000/", "Аксессуары"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\main.mht", "EldoradoRu")]
        public void Test_Parse_ShopsPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\main.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Main);
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};
            var parser = new EldoradoRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(EldoradoRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, EldoradoRuWebPageType.ShopsList, "/info/shops/"), shopsPage);
        }
    }
}