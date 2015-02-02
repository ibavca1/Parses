using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.MtsRu;
using Chia.WebParsing.Parsers.MtsRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MtsRu
{
    [TestClass]
    public class MtsRuMainPageContentParserTest : MtsRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\main.mht", "MtsRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\main.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, MtsRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new MtsRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(MtsRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MtsRuWebPageType.Razdel, "/tarify/", "Тарифы"), pages.First());
            Assert.AreEqual(CreatePage(page, MtsRuWebPageType.Razdel, "/mobilnyye-prilozheniya/", "Мобильные приложения"), pages.Last());
        }
    }
}