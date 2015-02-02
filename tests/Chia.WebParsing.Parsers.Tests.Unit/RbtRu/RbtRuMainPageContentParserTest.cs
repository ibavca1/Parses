using System.Linq;
using Chia.WebParsing.Companies.RbtRu;
using Chia.WebParsing.Parsers.RbtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.RbtRu
{
    [TestClass]
    public class RbtRuMainPageContentParserTest : RbtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\main.mht", "RbtRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\main.mht");
            WebPage page = CreatePage(content, RbtRuCity.Chelyabinsk, RbtRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new RbtRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(RbtRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RbtRuWebPageType.Razdel, "/cat/kuhonnaya_tehnika/", "Кухонная техника"), pages.First());
            Assert.AreEqual(CreatePage(page, RbtRuWebPageType.Razdel, "/cat/tovary_dlya_detey/", "Товары для детей"), pages.Last());
        }
    }
}