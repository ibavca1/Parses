using System.Linq;
using Chia.WebParsing.Companies.KeyRu;
using Chia.WebParsing.Parsers.KeyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.KeyRu
{
    [TestClass]
    public class KeyRuMainPageContentParserTest : KeyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\main.mht", "KeyRu")]
        public void Test_ParseCatalog()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\main.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuMainPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(KeyRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, KeyRuWebPageType.Razdel, "/otdel-noutbuki/", "Ноутбуки"), pages.First());
            Assert.AreEqual(CreatePage(page, KeyRuWebPageType.Razdel, "/otdel-suveniry/", "Сувениры"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\main.mht", "KeyRu")]
        public void Test_ParseShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\main.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Main);
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};
            var parser = new KeyRuMainPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(KeyRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, KeyRuWebPageType.ShopsList, "/magaziny/"), shopsPage);
        }
    }
}