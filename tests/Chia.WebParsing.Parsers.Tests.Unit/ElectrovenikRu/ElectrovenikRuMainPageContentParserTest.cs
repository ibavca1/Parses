using System.Linq;
using Chia.WebParsing.Companies.ElectrovenikRu;
using Chia.WebParsing.Parsers.ElectrovenikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.ElectrovenikRu
{
    [TestClass]
    public class ElectrovenikRuMainPageContentParserTest : ElectrovenikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\ElectrovenikRu\Pages\main.mht", "ElectrovenikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"ElectrovenikRu\main.mht");
            WebPage page = CreatePage(content, ElectrovenikRuCity.Chelyabinsk, ElectrovenikRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new ElectrovenikRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(ElectrovenikRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, ElectrovenikRuWebPageType.Razdel, "/catalog/krupnaya_bytovaya_tekhnika", "Крупная бытовая техника"), pages.First());
            Assert.AreEqual(CreatePage(page, ElectrovenikRuWebPageType.Razdel, "/catalog/uborochnaya_tekhnika_i_oborudovanie", "Уборочная техника и оборудование"), pages.Last());
        }
    }
}