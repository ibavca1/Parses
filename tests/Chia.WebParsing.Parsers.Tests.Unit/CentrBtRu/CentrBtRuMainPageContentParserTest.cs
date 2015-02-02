using System.Linq;
using Chia.WebParsing.Companies.CentrBtRu;
using Chia.WebParsing.Parsers.CentrBtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CentrBtRu
{
    [TestClass]
    public class CentrBtRuMainPageContentParserTest : CentrBtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\main.mht", "CentrBtRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\main.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Moscow, CentrBtRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new CentrBtRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(CentrBtRuWebPageType.Razdel).ToArray();
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CentrBtRuWebPageType.Razdel, "/index.php?cat=0xSSDRPPP", "Специальные предложения"), pages.First());
            Assert.AreEqual(CreatePage(page, CentrBtRuWebPageType.Razdel, "/index.php?cat=0xQHFFPPP", "Товары для детей"), pages.Last());
        }
    }
}