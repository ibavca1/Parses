using System.Linq;
using Chia.WebParsing.Companies.OldiRu;
using Chia.WebParsing.Parsers.OldiRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OldiRu
{
    [TestClass]
    public class OldiRuMainPageContentParserTest : OldiRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\main.mht", "OldiRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\main.mht");
            WebPage page = CreatePage(content, OldiRuCity.Moscow, OldiRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(OldiRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OldiRuWebPageType.Razdel, "/catalog/9625/", "Новогодние подарки"), pages.First());
            Assert.AreEqual(CreatePage(page, OldiRuWebPageType.Razdel, "/catalog/all/", "Весь каталог"), pages.Last());
        }
    }
}