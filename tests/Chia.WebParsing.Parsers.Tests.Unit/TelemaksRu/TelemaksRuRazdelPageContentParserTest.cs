using System.Linq;
using Chia.WebParsing.Companies.TelemaksRu;
using Chia.WebParsing.Parsers.TelemaksRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TelemaksRu
{
    [TestClass]
    public class TelemaksRuRazdelPageContentParserTest : TelemaksRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\razdel.mht", "TelemaksRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\razdel.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TelemaksRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TelemaksRuWebPageType.Catalog, "/catalog/dep63/", "Техника для приготовления сладостей"), pages.First());
            Assert.AreEqual(CreatePage(page, TelemaksRuWebPageType.Catalog, "/catalog/dep45/", "Электрочайники, термопоты"), pages.Last());
        }
    }
}