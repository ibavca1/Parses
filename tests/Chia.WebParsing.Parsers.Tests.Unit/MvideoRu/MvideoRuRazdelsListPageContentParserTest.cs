using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using Chia.WebParsing.Parsers.MvideoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MvideoRu
{
    [TestClass]
    public class MvideoRuRazdelsListPageContentParserTest : MvideoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\razdels_list.mht", "MvideoRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\razdels_list.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.RazdelsList);
            var parser = new MvideoRuRazdelsListPageContentParser();
            var context = new WebPageContentParsingContext();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(MvideoRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Razdel, "/tehnika-iz-nashey-reklamy", "Акции", "Федеральные акции", "Товары из нашей рекламы"), pages.First());
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Razdel, "/igrovye-aksessuary/igrovye-ruli-dzhoistiki-i-geimpady-285", "Игры и развлечения", "Игровые аксессуары", "Игровые рули, джойстики и геймпады"), pages.Last());
        }
    }
}