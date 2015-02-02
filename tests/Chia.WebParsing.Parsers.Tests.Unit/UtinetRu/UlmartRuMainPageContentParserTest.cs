using System.Linq;
using Chia.WebParsing.Companies.UtinetRu;
using Chia.WebParsing.Parsers.UtinetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UtinetRu
{
    [TestClass]
    public class UtinetRuMainPageContentParserTest : UtinetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\main.mht", "UtinetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\main.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(UtinetRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, UtinetRuWebPageType.Razdel, "http://note.utinet.ru/", "Компьютеры"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, UtinetRuWebPageType.Razdel, "http://utinet.ru/poleznoe", "Круглосуточная распродажа"), result.Pages.Last());
        }
    }
}