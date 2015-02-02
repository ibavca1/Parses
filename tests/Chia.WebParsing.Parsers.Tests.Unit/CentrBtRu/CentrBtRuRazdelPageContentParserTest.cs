using System.Linq;
using Chia.WebParsing.Companies.CentrBtRu;
using Chia.WebParsing.Parsers.CentrBtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CentrBtRu
{
    [TestClass]
    public class CentrBtRuRazdelPageContentParserTest : CentrBtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\razdel.mht", "CentrBtRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\razdel.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Moscow, CentrBtRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new CentrBtRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(CentrBtRuWebPageType.Razdel).ToArray();
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CentrBtRuWebPageType.Razdel, "/index.php?cat=0xRRQPPP", "Стиральные машины"), pages.First());
            Assert.AreEqual(CreatePage(page, CentrBtRuWebPageType.Razdel, "/index.php?cat=0xRRDPPP", "Посудомоечные машины"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\catalog.mht", "CentrBtRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\catalog.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Moscow, CentrBtRuWebPageType.Razdel); 
            var context = new WebPageContentParsingContext();
            var parser = new CentrBtRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CentrBtRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}