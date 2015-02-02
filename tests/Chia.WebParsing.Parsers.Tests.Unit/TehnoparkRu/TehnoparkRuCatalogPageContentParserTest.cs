using System.Linq;
using Chia.WebParsing.Companies.TehnoparkRu;
using Chia.WebParsing.Parsers.TehnoparkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoparkRu
{
    [TestClass]
    public class TehnoparkRuCatalogPageContentParserTest : TehnoparkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\catalog.mht", "TehnoparkRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\catalog.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("45843", product.Article);
            Assert.AreEqual("Музыкальный центр Magnat MC2 S", product.Name);
            Assert.AreEqual(30990, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(CreateUri(page,"/muzykalnyy-tsentr-magnat-mc2-s/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\catalog_out_of_stock.mht", "TehnoparkRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\catalog_next_page.mht", "TehnoparkRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(TehnoparkRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoparkRuWebPageType.Catalog, "/muzykalnye-tsentry/?p=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\catalog_last_page.mht", "TehnoparkRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Catalog);
            
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(TehnoparkRuWebPageType.Catalog); 
            Assert.IsNull(nextPage);
        }
    }
}