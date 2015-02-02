using System.Linq;
using Chia.WebParsing.Companies.Nord24Ru;
using Chia.WebParsing.Parsers.Nord24Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Nord24Ru
{
    [TestClass]
    public class Nord24RuRazdelPageContentParserTest : Nord24RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Nord24Ru\Pages\razdel.mht", "Nord24Ru")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Nord24Ru\razdel.mht");
            WebPage page = CreatePage(content, Nord24RuCity.Chelyabinsk, Nord24RuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new Nord24RuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(Nord24RuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, Nord24RuWebPageType.Razdel, "/shop/tehnika-dlya-kuhni/aerogrili/", "Аэрогрили"), pages.First());
            Assert.AreEqual(CreatePage(page, Nord24RuWebPageType.Razdel, "/shop/tehnika-dlya-kuhni/yelektrogrili/", "Электрогрили"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Nord24Ru\Pages\catalog.mht", "Nord24Ru")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Nord24Ru\catalog.mht");
            WebPage page = CreatePage(content, Nord24RuCity.Chelyabinsk, Nord24RuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new Nord24RuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(Nord24RuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, Nord24RuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}