using System.Linq;
using Chia.WebParsing.Companies.TenIRu;
using Chia.WebParsing.Parsers.TenIRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TenIRu
{
    [TestClass]
    public class TenIRuMainPageContentParserTest : TenIRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TenIRu\Pages\main.mht", "TenIRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TenIRu\main.mht");
            WebPage page = CreatePage(content, TenIRuCity.Moscow, TenIRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new TenIRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TenIRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TenIRuWebPageType.Razdel, "/cat/bytovaya-tehnika/", "Бытовая техника"), pages.First());
            Assert.AreEqual(CreatePage(page, TenIRuWebPageType.Razdel, "/cat/dlya-doma-i-dachi/", "Для дома и дачи"), pages.Last());
        }
    }
}