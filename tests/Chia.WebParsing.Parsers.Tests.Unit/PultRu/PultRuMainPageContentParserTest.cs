using System.Linq;
using Chia.WebParsing.Companies.PultRu;
using Chia.WebParsing.Parsers.PultRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.PultRu
{
    [TestClass]
    public class PultRuMainPageContentParserTest : PultRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\main.mht", "PultRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\main.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(PultRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, PultRuWebPageType.Razdel, "/product/akusticheskie-sistemy-akustika/", "Акустические системы (Акустика)"), pages.First());
            Assert.AreEqual(CreatePage(page, PultRuWebPageType.Razdel, "/product/sistema-umnyy-dom/", "Система умный дом"), pages.Last());
        }
    }
}