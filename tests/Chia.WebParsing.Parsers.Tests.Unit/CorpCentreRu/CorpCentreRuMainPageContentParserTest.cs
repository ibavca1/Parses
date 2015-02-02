using Chia.WebParsing.Companies.CorpCentreRu;
using Chia.WebParsing.Parsers.CorpCentreRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CorpCentreRu
{
    [TestClass]
    public class CorpCentreRuMainPageContentParserTest : CorpCentreRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\main.mht", "CorpCentreRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\main.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage razdelsPage = result.Pages.SingleWithType(CorpCentreRuWebPageType.RazdelsList);
            Assert.AreEqual(true, razdelsPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CorpCentreRuWebPageType.RazdelsList, "/catalog/", page.Path), razdelsPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\main.mht", "CorpCentreRu")]
        public void Test_ParseHtml_ShopsList()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\main.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Main);
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};
            var parser = new CorpCentreRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(CorpCentreRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, CorpCentreRuWebPageType.ShopsList, "/shops/ekaterinburg/", page.Path), shopsPage);
        }
    }
}