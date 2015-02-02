using System.Linq;
using Chia.WebParsing.Companies.OnlinetradeRu;
using Chia.WebParsing.Parsers.OnlinetradeRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OnlinetradeRu
{
    [TestClass]
    public class OnlinetradeRuRazdelsListPageContentParserTest : OnlinetradeRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\razdels_list.mht", "OnlinetradeRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\razdels_list.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.Moscow, OnlinetradeRuWebPageType.RazdelsList);
            var context = new WebPageContentParsingContext();
            var parser = new OnlinetradeRuRazdelsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => OnlinetradeRuWebPageType.Razdel == (OnlinetradeRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, OnlinetradeRuWebPageType.Razdel, "/catalogue/tsifrovie_fotoapparati-c5/", "Фото-видео", "Фото и видеокамеры", "Цифровые фотоаппараты"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, OnlinetradeRuWebPageType.Razdel, "/catalogue/chasi_karen_millen-c1481/", "Часы", "Часы KAREN MILLEN"), result.Pages.Last());
        }
    }
}