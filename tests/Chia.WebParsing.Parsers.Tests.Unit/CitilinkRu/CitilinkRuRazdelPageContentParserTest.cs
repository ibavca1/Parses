using System.Linq;
using Chia.WebParsing.Companies.CitilinkRu;
using Chia.WebParsing.Parsers.CitilinkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CitilinkRu
{
    [TestClass]
    public class CitilinkRuRazdelPageContentParserTest : CitilinkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\razdel.mht", "CitilinkRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\razdel.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.Moscow, CitilinkRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new CitilinkRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);
            
            // assert
             WebPage[] pages = result.Pages.WhereType(CitilinkRuWebPageType.Razdel).ToArray();
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CitilinkRuWebPageType.Razdel, "/catalog/mobile/", "Ноутбуки, планшеты, смартфоны"), pages.First());
            Assert.AreEqual(CreatePage(page, CitilinkRuWebPageType.Razdel, "/catalog/computers_and_notebooks/gift_certificates/", "Подарочные сертификаты"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\catalog.mht", "CitilinkRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\catalog.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.Moscow, CitilinkRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new CitilinkRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CitilinkRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}