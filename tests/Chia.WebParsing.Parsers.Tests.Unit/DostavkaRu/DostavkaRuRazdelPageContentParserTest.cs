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
    public class DostavkaRuRazdelPageContentParserTest : DostavkaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DostavkaRu\Pages\razdel.mht", "DostavkaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DostavkaRu\razdel.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, DostavkaRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new DostavkaRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(DostavkaRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DostavkaRuWebPageType.Razdel, "/category_id/17644", "Холодильники и морозильники"), pages.First());
            Assert.AreEqual(CreatePage(page, DostavkaRuWebPageType.Razdel, "/category_id/17654", "Аксессуары для бытовой техники"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DostavkaRu\Pages\catalog.mht", "DostavkaRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DostavkaRu\catalog.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, DostavkaRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new DostavkaRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DostavkaRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}