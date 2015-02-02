using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.OzonRu;
using Chia.WebParsing.Parsers.OzonRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OzonRu
{
    [TestClass]
    public class OzonRuMainPageContentParserTest : OzonRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\main.mht", "OzonRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\main.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(OzonRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Razdel, "/context/div_kid/", "Детям и мамам"), pages.First());
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Razdel, "/context/div_travel/", "Авиабилеты и Ж/Д билеты"), pages.Last());
        }
    }
}