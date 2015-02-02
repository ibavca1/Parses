using System.Linq;
using Chia.WebParsing.Companies.EurosetRu;
using Chia.WebParsing.Parsers.EurosetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EurosetRu
{
    [TestClass]
    public class EurosetRuRazdelPageContentParserTest : EurosetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\razdel.mht", "EurosetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\razdel.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(EurosetRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EurosetRuWebPageType.Razdel, "/catalog/phones/mobile/", "Мобильные телефоны"), pages.First());
            Assert.AreEqual(CreatePage(page, EurosetRuWebPageType.Razdel, "/catalog/phones/accessories/", "Аксессуары"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\catalog.mht", "EurosetRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\catalog.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Chelyabinsk, EurosetRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(EurosetRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EurosetRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        
        }
    }
}