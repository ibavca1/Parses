using System.Linq;
using Chia.WebParsing.Companies.KeyRu;
using Chia.WebParsing.Parsers.KeyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.KeyRu
{
    [TestClass]
    public class KeyRuRazdelPageContentParserTest : KeyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\razdel.mht", "KeyRu")]
        public void Test_ParseCatalog()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\razdel.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuRazdelPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(KeyRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, KeyRuWebPageType.Catalog, "/shop/monobloki_i_kompyutery/kompyutery/", "Компьютеры"), pages.First());
            Assert.AreEqual(CreatePage(page, KeyRuWebPageType.Catalog, "/shop/monobloki_i_kompyutery/planshety_graficheskie/", "Графические планшеты"), pages.Last());
        }
    }
}