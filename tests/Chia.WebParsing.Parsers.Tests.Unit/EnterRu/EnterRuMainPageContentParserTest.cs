using System.Linq;
using Chia.WebParsing.Companies.EnterRu;
using Chia.WebParsing.Parsers.EnterRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EnterRu
{
    [TestClass]
    public class EnterRuMainPageContentParserTest : EnterRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\main.mht", "EnterRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\main.mht");
            WebPage page = CreatePage(content, EnterRuCity.Petrozavodsk, EnterRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new EnterRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => EnterRuWebPageType.Razdel == (EnterRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Razdel, "/catalog/furniture", "Мебель"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, EnterRuWebPageType.Razdel, "/catalog/tchibo", "Tchibo"), result.Pages.Last());
        }
    }
}