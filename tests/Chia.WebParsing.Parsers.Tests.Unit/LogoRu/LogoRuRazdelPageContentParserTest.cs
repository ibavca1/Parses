using System.Linq;
using Chia.WebParsing.Companies.LogoRu;
using Chia.WebParsing.Parsers.LogoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.LogoRu
{
    [TestClass]
    public class LogoRuRazdelPageContentParserTest : LogoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\razdel.mht", "LogoRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\razdel.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(LogoRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, LogoRuWebPageType.Razdel, "/shop/kompyuternaya-technika/noutbuki/", "Ноутбуки"), pages.First());
            Assert.AreEqual(CreatePage(page, LogoRuWebPageType.Razdel, "/shop/kompyuternaya-technika/kartridzhi-i-bumaga/", "Картриджи для принтеров"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\catalog.mht", "LogoRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\catalog.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Razdel,"path");
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, LogoRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}