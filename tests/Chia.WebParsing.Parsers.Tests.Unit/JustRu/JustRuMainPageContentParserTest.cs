using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.JustRu;
using Chia.WebParsing.Parsers.JustRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.JustRu
{
    [TestClass]
    public class JustRuMainPageContentParserTest : JustRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\JustRu\Pages\main.mht", "JustRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"JustRu\main.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, JustRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new JustRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(JustRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, JustRuWebPageType.Catalog, "/terminali_sbora_dannih/", "Компьютеры", "Терминалы сбора данных"), pages.First());
            Assert.AreEqual(CreatePage(page, JustRuWebPageType.Catalog, "/discount/hardware/", "Железо", "JUST.Уценка:Железо"), pages.Last());
        }
    }
}