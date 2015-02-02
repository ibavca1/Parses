using System.Linq;
using Chia.WebParsing.Companies.Oo3Ru;
using Chia.WebParsing.Parsers.Oo3Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Oo3Ru
{
    [TestClass]
    public class Oo3RuMainPageContentParserTest : Oo3RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\main.mht", "Oo3Ru")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\main.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(Oo3RuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, Oo3RuWebPageType.Catalog, "/catalog-423903.html", "Крупная и встраиваемая бытовая техника"), pages.First());
            Assert.AreEqual(CreatePage(page, Oo3RuWebPageType.Catalog, "/catalog-3003911.html", "Кофе и Чай"), pages.Last());
        }
    }
}