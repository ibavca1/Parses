using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.DostavkaRu;
using Chia.WebParsing.Parsers.DostavkaRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DostavkaRu
{
    [TestClass]
    public class DostavkaRuCatalogPageContentParserTest : DostavkaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DostavkaRu\Pages\catalog.mht", "DostavkaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DostavkaRu\catalog.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, DostavkaRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new DostavkaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("6949441", product.Article);
            Assert.AreEqual("Холодильник Shivaki SHRF-240CH", product.Name);
            Assert.AreEqual(11490, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(CreateUri(page,"/Shivaki-SHRF-240CH-id_6949441"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DostavkaRu\Pages\catalog.mht", "DostavkaRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DostavkaRu\catalog.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, DostavkaRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new DostavkaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(DostavkaRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DostavkaRuWebPageType.Catalog, "/category_id/17659?page_number=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DostavkaRu\Pages\catalog_last_page.mht", "DostavkaRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DostavkaRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, DostavkaRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new DostavkaRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(DostavkaRuWebPageType.Catalog); 
            Assert.IsNull(nextPage);
        }
    }
}