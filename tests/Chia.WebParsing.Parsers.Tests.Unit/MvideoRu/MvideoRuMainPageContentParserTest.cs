using Chia.WebParsing.Companies.MvideoRu;
using Chia.WebParsing.Parsers.MvideoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MvideoRu
{
    [TestClass]
    public class MvideoRuMainPageContentParserTest : MvideoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\main.mht", "MvideoRu")]
        public void Test_ParseHtml_RazdelsListPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\main.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage razdelsPage = result.Pages.SingleWithType(MvideoRuWebPageType.RazdelsList);
            Assert.AreEqual(true, razdelsPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.RazdelsList, "/catalog", page.Path), razdelsPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\main.mht", "MvideoRu")]
        public void Test_ParseShopsPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\main.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Main);
            var parser = new MvideoRuMainPageContentParser();
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(MvideoRuWebPageType.ShopsList);
            Assert.IsTrue(shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.ShopsList, "/shops/store-list", page.Path), shopsPage);
        }
    }
}