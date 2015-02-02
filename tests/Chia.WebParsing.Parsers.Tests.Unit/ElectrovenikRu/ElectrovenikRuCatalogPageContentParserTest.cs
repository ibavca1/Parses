using System.Linq;
using Chia.WebParsing.Companies.ElectrovenikRu;
using Chia.WebParsing.Parsers.ElectrovenikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.ElectrovenikRu
{
    [TestClass]
    public class ElectrovenikRuCatalogPageContentParserTest : ElectrovenikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\ElectrovenikRu\Pages\catalog.mht", "ElectrovenikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"ElectrovenikRu\catalog.mht");
            WebPage page = CreatePage(content, ElectrovenikRuCity.Moscow,ElectrovenikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new ElectrovenikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("14310 (с4)", product.Article);
            Assert.AreEqual("Однокамерный холодильник KORTING KS 50 HW", product.Name);
            Assert.AreEqual(7290, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(CreateUri(page, "/catalog/krupnaya_bytovaya_tekhnika/kholodilniki/odnokamernye/odnokamernyy-kholodilnik-korting-ks-50-hw.html"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\ElectrovenikRu\Pages\catalog.mht", "ElectrovenikRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"ElectrovenikRu\catalog.mht");
            WebPage page = CreatePage(content, ElectrovenikRuCity.Moscow, ElectrovenikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new ElectrovenikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(ElectrovenikRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, ElectrovenikRuWebPageType.Catalog, "/catalog/krupnaya_bytovaya_tekhnika/kholodilniki/odnokamernye?start=30", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\ElectrovenikRu\Pages\catalog_last_page.mht", "ElectrovenikRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"ElectrovenikRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, ElectrovenikRuCity.Moscow, ElectrovenikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new ElectrovenikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(ElectrovenikRuWebPageType.Catalog); 
            Assert.IsNull(nextPage);
        }
    }
}