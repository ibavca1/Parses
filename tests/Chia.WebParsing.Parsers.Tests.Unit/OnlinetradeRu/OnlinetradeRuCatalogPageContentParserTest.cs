using System.Linq;
using Chia.WebParsing.Companies.OnlinetradeRu;
using Chia.WebParsing.Parsers.OnlinetradeRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OnlinetradeRu
{
    [TestClass]
    public class OnlinetradeRuCatalogPageContentParserTest : OnlinetradeRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\catalog.mht", "OnlinetradeRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\catalog.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.Moscow, OnlinetradeRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OnlinetradeRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("87804", product.Article);
            Assert.AreEqual("Цифровой фотоаппарат Canon PowerShot SX50 HS", product.Name);
            Assert.AreEqual(13400, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalogue/tsifrovie_fotoapparati-c5/canon/tsifrovoy_fotoapparat_canon_powershot_sx50_hs-87804.html#place_breadcrumbs"), product.Uri);
        }


        //[TestMethod]
        //[DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\catalog_empty.mht", "OnlinetradeRu")]
        //public void Test_ParseHtml_Empty()
        //{
        //    // arrange
        //    MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\catalog_empty.mht");
        //    WebPage page = CreatePage(content, OnlinetradeRuCity.Ekaterinburg, OnlinetradeRuWebPageType.Catalog);
        //    var context = new WebPageContentParsingContext();
        //    var parser = new OnlinetradeRuCatalogPageContentParser();

        //    // act
        //    WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

        //    // assert
        //    Assert.IsTrue(result.IsEmpty);
        //}

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\catalog.mht", "OnlinetradeRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\catalog.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.Moscow, OnlinetradeRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new OnlinetradeRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(OnlinetradeRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OnlinetradeRuWebPageType.Catalog, "/catalogue/tsifrovie_fotoapparati-c5/?page=1", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\catalog_last_page.mht", "OnlinetradeRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.Ekaterinburg, OnlinetradeRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OnlinetradeRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(OnlinetradeRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}