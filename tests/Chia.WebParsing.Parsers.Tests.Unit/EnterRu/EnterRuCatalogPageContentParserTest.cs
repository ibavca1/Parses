using System.Linq;
using Chia.WebParsing.Companies.EnterRu;
using Chia.WebParsing.Parsers.EnterRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EnterRu
{
    [TestClass]
    public class EnterRuCatalogPageContentParserTest : EnterRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\catalog.mht", "EnterRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\catalog.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Наушники Explay BoomX for Lady’s", product.Name);
            Assert.AreEqual(80, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/product/electronics/naushniki-explay-boomx-for-ladys-2060508012293"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\catalog.mht", "EnterRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\catalog.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext {Options = {ProductArticle = true}};
            var parser = new EnterRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(EnterRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Product, "/product/electronics/naushniki-explay-boomx-for-ladys-2060508012293", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\catalog_next_page.mht", "EnterRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(EnterRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Catalog, "/catalog/electronics/noutbuki-i-monobloki-noutbuki-4280?page=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\catalog_last_page.mht", "EnterRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(EnterRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\catalog_empty.mht", "EnterRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\catalog_empty.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\catalog_categories.mht", "EnterRu")]
        public void Test_ParseHtml_Categories()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\catalog_categories.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => EnterRuWebPageType.Catalog == (EnterRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Catalog, "/catalog/electronics/aksessuari-dlya-elektroniki-aksessuari-dlya-kompyuterov-i-planshetov-1031", "Аксессуары для компьютеров и ноутбуков"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Catalog, "/catalog/elektronika/aksessuari-dlya-elektroniki-rashodnie-materiali-dlya-3d-printerov-9dab", "Расходные материалы для 3D-принтеров"), result.Pages.Last());
        }
    }
}