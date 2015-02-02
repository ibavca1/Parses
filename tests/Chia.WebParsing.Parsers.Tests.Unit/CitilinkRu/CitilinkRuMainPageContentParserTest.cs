using System.Linq;
using Chia.WebParsing.Companies.CitilinkRu;
using Chia.WebParsing.Parsers.CitilinkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CitilinkRu
{
    [TestClass]
    public class CitilinkRuMainPageContentParserTest : CitilinkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\main.mht", "CitilinkRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\main.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.Moscow, CitilinkRuWebPageType.Main, "path");
            var context = new WebPageContentParsingContext();
            var parser = new CitilinkRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(true, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CitilinkRuWebPageType.Razdel, "/catalog/", page.Path), catalogPage);
        }
    }
}