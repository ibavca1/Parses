using System.Linq;
using Chia.WebParsing.Companies.TechportRu;
using Chia.WebParsing.Parsers.TechportRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechportRu
{
    [TestClass]
    public class TechportRuMainPageContentParserTest : TechportRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\main.mht", "TechportRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\main.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TechportRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TechportRuWebPageType.Catalog, "/katalog/products/otdelnostojaschaja-bytovaja-tehnika", "Крупная бытовая техника"), pages.First());
            Assert.AreEqual(CreatePage(page, TechportRuWebPageType.Catalog, "/katalog/products/novyj-god", "Всё для нового года"), pages.Last());
        }
    }
}