using System.Linq;
using Chia.WebParsing.Companies.EnterRu;
using Chia.WebParsing.Parsers.EnterRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EnterRu
{
    [TestClass]
    public class EnterRuRazdelPageContentParserTest : EnterRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\razdel.mht", "EnterRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\razdel.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(EnterRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Razdel, "/catalog/appliances/stiralnie-i-sushilnie-mashini-3734", "Стиральные и сушильные машины"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Razdel, "/catalog/appliances/aksessuari-dlya-bitovoy-tehniki-1479", "Аксессуары для бытовой техники"), result.Pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\catalog.mht", "EnterRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\catalog.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\catalog_empty.mht", "EnterRu")]
        public void Test_ParseHtml_EmptyCatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\catalog_empty.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}