using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.NotikRu;
using Chia.WebParsing.Parsers.NotikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NotikRu
{
    [TestClass]
    public class NotikRuRazdelPageContentParserTest : NotikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\razdel.mht", "NotikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\razdel.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new NotikRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => NotikRuWebPageType.Catalog == (NotikRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, NotikRuWebPageType.Catalog, "/search_catalog/filter/new.htm", "Новинки"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, NotikRuWebPageType.Catalog, "/search_catalog/filter/notebages.htm", "Сумки для ноутбуков"), result.Pages.Last());
        }
    }
}