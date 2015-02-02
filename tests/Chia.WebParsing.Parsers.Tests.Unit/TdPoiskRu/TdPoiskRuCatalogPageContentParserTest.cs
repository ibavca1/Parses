using System.Linq;
using Chia.WebParsing.Companies.TdPoiskRu;
using Chia.WebParsing.Parsers.TdPoiskRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TdPoiskRu
{
    [TestClass]
    public class TdPoiskRuCatalogPageContentParserTest : TdPoiskRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\catalog.mht", "TdPoiskRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\catalog.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("SUPRA DVS-755HKU Bl", product.Name);
            Assert.AreEqual(1290, product.OnlinePrice);
            Assert.AreEqual(1290, product.RetailPrice);
            Assert.AreEqual(CreateUri(page,"/catalog/dvd_i_blu-ray_pleery/hidvd_supra_dvs-755hku_bl/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\catalog_retail_only.mht", "TdPoiskRu")]
        public void Test_ParseHtml_RetailOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\catalog_retail_only.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreNotEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\catalog_next_page.mht", "TdPoiskRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(TdPoiskRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Catalog, "/catalog/dvd_i_blu-ray_pleery/?PAGEN_1=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\catalog_last_page.mht", "TdPoiskRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(TdPoiskRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\catalog.mht", "TdPoiskRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\catalog.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {ProductArticle = true}};
            var parser = new TdPoiskRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TdPoiskRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Product, "/catalog/dvd_i_blu-ray_pleery/hidvd_supra_dvs-755hku_bl/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\catalog.mht", "TdPoiskRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\catalog.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TdPoiskRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TdPoiskRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Product, "/catalog/dvd_i_blu-ray_pleery/hidvd_supra_dvs-755hku_bl/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\catalog_long_name.mht", "TdPoiskRu")]
        public void Test_ParseHtml_GoToProductPageIfLongName()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\catalog_long_name.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TdPoiskRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Product, "/catalog/obektivy/foto-obektiv_sigma_af_70-300mm_f_4-56_dg_macro_motor_dlya_nikon/", page.Path), productPage);
        }
    }
}