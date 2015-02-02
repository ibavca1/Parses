using System.Linq;
using Chia.WebParsing.Companies.Nord24Ru;
using Chia.WebParsing.Parsers.Nord24Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Nord24Ru
{
    [TestClass]
    public class Nord24RuCatalogPageContentParserTest : Nord24RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Nord24Ru\Pages\catalog.mht", "Nord24Ru")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Nord24Ru\catalog.mht");
            WebPage page = CreatePage(content, Nord24RuCity.Chelyabinsk, Nord24RuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new Nord24RuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Гриль Rolsen RG 1410", product.Name);
            Assert.AreEqual(2850, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/shop/tehnika-dlya-kuhni/yelektrogrili/yelektrogrili-Rolsen/4459-rolsen-rg-1410.html"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Nord24Ru\Pages\catalog_next_page.mht", "Nord24Ru")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Nord24Ru\catalog_next_page.mht");
            WebPage page = CreatePage(content, Nord24RuCity.Chelyabinsk, Nord24RuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new Nord24RuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(Nord24RuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, Nord24RuWebPageType.Catalog, "/shop/kompyuternaya-technika/noutbuki/?page=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Nord24Ru\Pages\catalog_last_page.mht", "Nord24Ru")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Nord24Ru\catalog_last_page.mht");
            WebPage page = CreatePage(content, Nord24RuCity.Chelyabinsk, Nord24RuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new Nord24RuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(Nord24RuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}