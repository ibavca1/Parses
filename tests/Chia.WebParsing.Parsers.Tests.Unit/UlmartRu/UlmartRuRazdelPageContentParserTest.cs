using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using Chia.WebParsing.Parsers.UlmartRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UlmartRu
{
    [TestClass]
    public class UlmartRuRazdelPageContentParserTest : UlmartRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\razdel.mht", "UlmartRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\razdel.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(UlmartRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Catalog, "/catalog/93402", "Корм для животных"), pages.First());
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Catalog, "/catalog/93251", "Аксессуары для животных"), pages.Last());
        }
    }
}