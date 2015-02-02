using System.Linq;
using Chia.WebParsing.Companies.CorpCentreRu;
using Chia.WebParsing.Parsers.CorpCentreRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CorpCentreRu
{
    [TestClass]
    public class CorpCentreRuRazdelsListPageContentParserTest : CorpCentreRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\razdels_list.mht", "CorpCentreRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\razdels_list.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.RazdelsList);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuRazdelsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(CorpCentreRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CorpCentreRuWebPageType.Razdel, "/catalog/televizory_video/", "Телевизоры, видео"), pages.First());
            Assert.AreEqual(CreatePage(page, CorpCentreRuWebPageType.Razdel, "/catalog/sport_turizm_dacha/", "Спорт и отдых"), pages.Last());
        }
    }
}