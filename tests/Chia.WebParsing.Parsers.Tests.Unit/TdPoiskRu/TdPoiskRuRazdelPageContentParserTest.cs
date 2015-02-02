using System.Linq;
using Chia.WebParsing.Companies.TdPoiskRu;
using Chia.WebParsing.Parsers.TdPoiskRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TdPoiskRu
{
    [TestClass]
    public class TdPoiskRuRazdelPageContentParserTest : TdPoiskRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\razdel.mht", "TdPoiskRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\razdel.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TdPoiskRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Razdel, "/catalog/dvd_i_blu-ray_pleery/", "DVD и Blu-ray плееры"), pages.First());
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Razdel, "/catalog/televizory/", "Телевизоры"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\catalog.mht", "TdPoiskRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\catalog.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(TdPoiskRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TdPoiskRuWebPageType.Catalog, page.Uri, page.Path), catalogPage); 
        }
    }
}