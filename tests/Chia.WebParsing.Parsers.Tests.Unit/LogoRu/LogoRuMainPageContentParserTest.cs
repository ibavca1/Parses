using System.Linq;
using Chia.WebParsing.Companies.LogoRu;
using Chia.WebParsing.Parsers.LogoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.LogoRu
{
    [TestClass]
    public class LogoRuMainPageContentParserTest : LogoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\main.mht", "LogoRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\main.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(LogoRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, LogoRuWebPageType.Razdel, "/shop/telefony/", "Телефоны"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, LogoRuWebPageType.Razdel, "/shop/instrumenty/", "Инструменты"), result.Pages.Last());
        }
    }
}