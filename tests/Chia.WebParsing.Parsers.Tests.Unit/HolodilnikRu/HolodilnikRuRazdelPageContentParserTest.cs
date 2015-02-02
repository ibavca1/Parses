using System.Linq;
using Chia.WebParsing.Companies.HolodilnikRu;
using Chia.WebParsing.Parsers.HolodilnikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.HolodilnikRu
{
    [TestClass]
    public class HolodilnikRuRazdelPageContentParserTest : HolodilnikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\razdel.mht", "HolodilnikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\razdel.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(HolodilnikRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, HolodilnikRuWebPageType.Razdel, "/refrigerator/one_chamber_refrigerators/", "Однокамерные холодильники"), pages.First());
            Assert.AreEqual(CreatePage(page, HolodilnikRuWebPageType.Razdel, "/refrigerator/refrigerator_acessories_and_goods/", "Аксессуары и сопутствующие товары для холодильников"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\catalog.mht", "HolodilnikRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\catalog.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(HolodilnikRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, HolodilnikRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}