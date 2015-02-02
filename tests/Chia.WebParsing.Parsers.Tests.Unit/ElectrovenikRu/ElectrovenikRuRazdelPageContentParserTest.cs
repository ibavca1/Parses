using System.Linq;
using Chia.WebParsing.Companies.ElectrovenikRu;
using Chia.WebParsing.Parsers.ElectrovenikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.ElectrovenikRu
{
    [TestClass]
    public class ElectrovenikRuRazdelPageContentParserTest : ElectrovenikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\ElectrovenikRu\Pages\razdel.mht", "ElectrovenikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"ElectrovenikRu\razdel.mht");
            WebPage page = CreatePage(content, ElectrovenikRuCity.Moscow, ElectrovenikRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new ElectrovenikRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(ElectrovenikRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, ElectrovenikRuWebPageType.Razdel, "/catalog/krupnaya_bytovaya_tekhnika/kholodilniki", "Холодильники"), pages.First());
            Assert.AreEqual(CreatePage(page, ElectrovenikRuWebPageType.Razdel, "/catalog/krupnaya_bytovaya_tekhnika/aksessuary_i_sredstva_ukhoda", "Аксессуары и средства ухода"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\ElectrovenikRu\Pages\catalog.mht", "ElectrovenikRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"ElectrovenikRu\catalog.mht");
            WebPage page = CreatePage(content, ElectrovenikRuCity.Moscow, ElectrovenikRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new ElectrovenikRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(ElectrovenikRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, ElectrovenikRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}