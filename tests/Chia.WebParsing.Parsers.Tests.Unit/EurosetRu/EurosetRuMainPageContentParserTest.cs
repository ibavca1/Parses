using System.Linq;
using Chia.WebParsing.Companies.EurosetRu;
using Chia.WebParsing.Parsers.EurosetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EurosetRu
{
    [TestClass]
    public class EurosetRuMainPageContentParserTest : EurosetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\main.mht", "EurosetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\main.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(EurosetRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EurosetRuWebPageType.Razdel, "/catalog/phones/", "Телефоны"), pages.First());
            Assert.AreEqual(CreatePage(page, EurosetRuWebPageType.Razdel, "/catalog/books-and-gifts/", "КНИГИ И ПОДАРКИ"), pages.Last());
        }
    }
}