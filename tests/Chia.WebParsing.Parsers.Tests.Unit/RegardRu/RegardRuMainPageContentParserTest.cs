using System.Linq;
using Chia.WebParsing.Companies.RegardRu;
using Chia.WebParsing.Parsers.RegardRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.RegardRu
{
    [TestClass]
    public class RegardRuMainPageContentParserTest : RegardRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RegardRu\Pages\main.mht", "RegardRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RegardRu\main.mht");
            WebPage page = CreatePage(content, RegardRuCity.Moscow, RegardRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new RegardRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(RegardRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RegardRuWebPageType.Catalog, "/catalog/group34000.htm", "Автомобильная электроника и GPS", "GPS Навигаторы"), pages.First());
            Assert.AreEqual(CreatePage(page, RegardRuWebPageType.Catalog, "/catalog/group29009.htm", "Фотоаппараты", "Штативы"), pages.Last());
        }
    }
}