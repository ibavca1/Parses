using System.Linq;
using Chia.WebParsing.Companies.NewmansRu;
using Chia.WebParsing.Parsers.NewmansRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NewmansRu
{
    [TestClass]
    public class NewmansRuMainPageContentParserTest : NewmansRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\main.mht", "NewmansRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\main.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Krasnodar, NewmansRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(NewmansRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, NewmansRuWebPageType.Razdel, "/discounted/", "Дисконт"), pages.First());
            Assert.AreEqual(CreatePage(page, NewmansRuWebPageType.Razdel, "/adv/", "Акции!"), pages.Last());
        }
    }
}