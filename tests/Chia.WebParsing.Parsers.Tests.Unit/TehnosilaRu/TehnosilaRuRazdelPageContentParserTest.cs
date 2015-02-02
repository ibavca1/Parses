using System.Linq;
using Chia.WebParsing.Companies.TehnosilaRu;
using Chia.WebParsing.Parsers.TehnosilaRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnosilaRu
{
    [TestClass]
    public class TehnosilaRuRazdelPageContentParserTest : TehnosilaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\razdel.mht", "TehnosilaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\razdel.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TehnosilaRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.Catalog, "/catalog/vstraivaemaja_tehnika/varochnye_paneli/gazovye_paneli", "Варочные панели", "Встраиваемые газовые панели"), pages.First());
            Assert.AreEqual(CreatePage(page, TehnosilaRuWebPageType.Catalog, "/catalog/vstraivaemaja_tehnika/aksessuary", "Аксессуары для встраиваемой техники"), pages.Last());
        }
    }
}