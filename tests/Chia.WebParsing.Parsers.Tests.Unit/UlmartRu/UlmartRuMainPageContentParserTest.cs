using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using Chia.WebParsing.Parsers.UlmartRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UlmartRu
{
    [TestClass]
    public class UlmartRuMainPageContentParserTest : UlmartRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\main.mht", "UlmartRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\main.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(UlmartRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Razdel, "/catalog/92993", "Товары для животных"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Razdel, "/catalog/utuning", "Услуги"), result.Pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\main.mht", "UlmartRu")]
        public void Test_ParseHtml_ShopsListPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\main.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Main);
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};
            var parser = new UlmartRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(UlmartRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.ShopsList, "help/current/contacts", page.Path), result.Pages.Last());
        }
    }
}