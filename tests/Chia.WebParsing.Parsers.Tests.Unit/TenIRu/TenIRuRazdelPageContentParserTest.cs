using System.Linq;
using Chia.WebParsing.Companies.TenIRu;
using Chia.WebParsing.Parsers.TenIRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TenIRu
{
    [TestClass]
    public class TenIRuRazdelPageContentParserTest : TenIRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TenIRu\Pages\razdel.mht", "TenIRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TenIRu\razdel.mht");
            WebPage page = CreatePage(content, TenIRuCity.Moscow, TenIRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TenIRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TenIRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TenIRuWebPageType.Razdel, "/cat/parovarki/", "Пароварки"), pages.First());
            Assert.AreEqual(CreatePage(page, TenIRuWebPageType.Razdel, "/cat/elektroskovorodki/", "Электросковородки"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TenIRu\Pages\catalog.mht", "TenIRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TenIRu\catalog.mht");
            WebPage page = CreatePage(content, TenIRuCity.Moscow, TenIRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TenIRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TenIRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}