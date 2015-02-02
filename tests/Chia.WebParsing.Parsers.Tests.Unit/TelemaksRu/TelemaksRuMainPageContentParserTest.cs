using System.Linq;
using Chia.WebParsing.Companies.TelemaksRu;
using Chia.WebParsing.Parsers.TelemaksRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TelemaksRu
{
    [TestClass]
    public class TelemaksRuMainPageContentParserTest : TelemaksRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\main.mht", "TelemaksRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\main.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TelemaksRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TelemaksRuWebPageType.Razdel, "/catalog/dep1223/", "Телевизоры"), pages.First());
            Assert.AreEqual(CreatePage(page, TelemaksRuWebPageType.Razdel, "/catalog/dep1229/", "Игровые приставки и ПО"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\main.mht", "TelemaksRu")]
        public void Test_ParseHtml_ShopsPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\main.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Main);
            
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};
            var parser = new TelemaksRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(TelemaksRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, TelemaksRuWebPageType.ShopsList, "/info/shops/city1/", page.Path), shopsPage);
        }
    }
}