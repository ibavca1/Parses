using System.Linq;
using Chia.WebParsing.Companies.RbtRu;
using Chia.WebParsing.Parsers.RbtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.RbtRu
{
    [TestClass]
    public class RbtRuCatalogPageContentParserTest : RbtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\catalog.mht", "RbtRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\catalog.mht");
            WebPage page = CreatePage(content, RbtRuCity.Chelyabinsk, RbtRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new RbtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Холодильник LERAN HC-698 WEN", product.Name);
            Assert.AreEqual(49999, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/cat/kuhonnaya_tehnika/holodilniki/leran_hc-698_wen/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\catalog.mht", "RbtRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\catalog.mht");
            WebPage page = CreatePage(content, RbtRuCity.Chelyabinsk, RbtRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new RbtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(RbtRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RbtRuWebPageType.Product, "/cat/kuhonnaya_tehnika/holodilniki/leran_hc-698_wen/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\catalog_long_name.mht", "RbtRu")]
        public void Test_ParseHtml_GoToProductPageIfLongProductName()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\catalog_long_name.mht");
            WebPage page = CreatePage(content, RbtRuCity.Chelyabinsk, RbtRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new RbtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(RbtRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RbtRuWebPageType.Product, "/cat/cifrovye_ustroistva/obektivy/sigma_af_18-200mm_f3.5-6.3_dc_macro_os_hsm_dlya_canon/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\catalog_next_page.mht", "RbtRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, RbtRuCity.Chelyabinsk, RbtRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new RbtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(RbtRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RbtRuWebPageType.Catalog, "/cat/kuhonnaya_tehnika/holodilniki/~/page/2/", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\catalog_last_page.mht", "RbtRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, RbtRuCity.Chelyabinsk, RbtRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new RbtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(RbtRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}