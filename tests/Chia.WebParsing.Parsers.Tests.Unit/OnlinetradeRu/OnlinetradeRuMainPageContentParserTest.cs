using Chia.WebParsing.Companies.OnlinetradeRu;
using Chia.WebParsing.Parsers.OnlinetradeRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OnlinetradeRu
{
    [TestClass]
    public class OnlinetradeRuMainPageContentParserTest : OnlinetradeRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\main.mht", "OnlinetradeRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\main.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.Moscow, OnlinetradeRuWebPageType.Main, "path");
            var context = new WebPageContentParsingContext();
            var parser = new OnlinetradeRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage razdelsPage = result.Pages.SingleWithType(OnlinetradeRuWebPageType.RazdelsList);
            Assert.AreEqual(true, razdelsPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OnlinetradeRuWebPageType.RazdelsList, "/catalogue.html", page.Path), razdelsPage);
        }
    }
}