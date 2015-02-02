using System.Linq;
using Chia.WebParsing.Companies.TehnosilaRu;
using Chia.WebParsing.Parsers.TehnosilaRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnosilaRu
{
    [TestClass]
    public class TehnosilaRuCatalogPageContentParserTest : TehnosilaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\catalog.mht", "TehnosilaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\catalog.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Вытяжка SHINDO REMY sensor 60 Black 3ET", product.Name);
            Assert.AreEqual(10499, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(CreateUri(page, "/catalog/vstraivaemaja_tehnika/vytyajki/-/76696"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\catalog.mht", "TehnosilaRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\catalog.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {ProductArticle = true}};
            var parser = new TehnosilaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TehnosilaRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.Product, "/catalog/vstraivaemaja_tehnika/vytyajki/-/76696", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\catalog.mht", "TehnosilaRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\catalog.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new TehnosilaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TehnosilaRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.Product, "/catalog/vstraivaemaja_tehnika/vytyajki/-/76696", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\catalog_long_name.mht", "TehnosilaRu")]
        public void Test_ParseHtml_GoToProductPageIfLongProductName()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\catalog_long_name.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TehnosilaRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.Product, "/catalog/foto_i_videokamery/optika/obektivy/-/35845", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\catalog_out_of_stock.mht", "TehnosilaRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\catalog_next_page.mht", "TehnosilaRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(TehnosilaRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.Catalog, "/catalog/kompjutery_i_orgtehnika/computer_peripherals/oborudovanie_wifi_bluetooth_i_adsl?p=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\catalog_last_page.mht", "TehnosilaRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Catalog);
            
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(TehnosilaRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}