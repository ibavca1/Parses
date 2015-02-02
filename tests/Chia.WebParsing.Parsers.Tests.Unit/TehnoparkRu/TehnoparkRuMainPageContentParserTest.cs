using System.Linq;
using Chia.WebParsing.Companies.TehnoparkRu;
using Chia.WebParsing.Parsers.TehnoparkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoparkRu
{
    [TestClass]
    public class TehnoparkRuMainPageContentParserTest : TehnoparkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\main.mht", "TehnoparkRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\main.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TehnoparkRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoparkRuWebPageType.Razdel, "/dlya-kuhni/", "Для кухни"), pages.First());
            Assert.AreEqual(CreatePage(page, TehnoparkRuWebPageType.Razdel, "/kompyutery/", "Цифровая техника"), pages.Last());
        }
    }
}