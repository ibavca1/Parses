using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.OzonRu;
using Chia.WebParsing.Parsers.OzonRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OzonRu
{
    [TestClass]
    public class OzonRuRazdelPageContentParserTest : OzonRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\razdel.mht", "OzonRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\razdel.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(OzonRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Catalog, "/catalog/1133763/", "ТВ, Аудио, Видео", "Телевизоры"), pages.First());
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Catalog, "/context/certificat/", "Подарочные сертификаты"), pages.Last());
        }
    }
}