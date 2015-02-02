using System.Linq;
using Chia.WebParsing.Companies.HolodilnikRu;
using Chia.WebParsing.Parsers.HolodilnikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.HolodilnikRu
{
    [TestClass]
    public class HolodilnikRuMainPageContentParserTest : HolodilnikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\main.mht", "HolodilnikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\main.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(HolodilnikRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, HolodilnikRuWebPageType.Razdel, "/refrigerator/", "Холодильники и морозильники"), pages.First());
            Assert.AreEqual(CreatePage(page, HolodilnikRuWebPageType.Razdel, "/digital_tech/", "Цифровая техника, ноутбуки"), pages.Last());
        }
    }
}