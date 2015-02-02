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
    public class DostavkaRuMainPageContentParserTest : DostavkaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DostavkaRu\Pages\main.mht", "DostavkaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DostavkaRu\main.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, DostavkaRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new DostavkaRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(DostavkaRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DostavkaRuWebPageType.Razdel, "/category_id/17643", "Бытовая техника"), pages.First());
            Assert.AreEqual(CreatePage(page, DostavkaRuWebPageType.Razdel, "/category_id/16932", "Уцененные товары"), pages.Last());
        }
    }
}