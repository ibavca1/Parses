using System.Linq;
using Chia.WebParsing.Companies.SvyaznoyRu;
using Chia.WebParsing.Parsers.SvyaznoyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.SvyaznoyRu
{
    [TestClass]
    public class SvyaznoyRuMainPageContentParserTest : SvyaznoyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\main.mht", "SvyaznoyRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\main.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.Chelyabinsk, SvyaznoyRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(SvyaznoyRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Razdel, "/catalog/8660", "Телефоны, связь"), pages.First());
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Razdel, "/catalog/8897", "Аксессуары и услуги"), pages.Last());
        }
    }
}