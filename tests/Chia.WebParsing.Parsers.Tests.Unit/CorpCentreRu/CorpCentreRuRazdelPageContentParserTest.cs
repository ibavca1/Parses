using System.Linq;
using Chia.WebParsing.Companies.CorpCentreRu;
using Chia.WebParsing.Parsers.CorpCentreRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CorpCentreRu
{
    [TestClass]
    public class CorpCentreRuRazdelPageContentParserTest : CorpCentreRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\razdel.mht", "CorpCentreRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\razdel.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(CorpCentreRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CorpCentreRuWebPageType.Razdel, "/catalog/vstraivaemye_gazovye_paneli/", "Встраиваемые газовые панели"), pages.First());
            Assert.AreEqual(CreatePage(page, CorpCentreRuWebPageType.Razdel, "/catalog/aksessuary_dlya_vstraivaemoy_tekhniki/", "Аксессуары для встраиваемой техники"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\catalog.mht", "CorpCentreRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\catalog.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CorpCentreRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}