using System.Linq;
using Chia.WebParsing.Companies.NotikRu;
using Chia.WebParsing.Parsers.NotikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NotikRu
{
    [TestClass]
    public class NotikRuMainPageContentParserTest : NotikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\main.mht", "NotikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\main.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new NotikRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(NotikRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, NotikRuWebPageType.Razdel, "/index/notebooks.htm", "Ноутбуки"), pages.First());
            Assert.AreEqual(CreatePage(page, NotikRuWebPageType.Razdel, "/index/tv.htm", "ТВ-Аудио-Видео"), pages.Last());
        }
    }
}