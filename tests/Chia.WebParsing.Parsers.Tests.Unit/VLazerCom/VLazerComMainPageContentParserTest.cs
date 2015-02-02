using System.Linq;
using Chia.WebParsing.Companies.VLazerCom;
using Chia.WebParsing.Parsers.VLazerCom;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.VLazerCom
{
    [TestClass]
    public class VLazerComMainPageContentParserTest : VLazerComWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\VLazerCom\Pages\main.mht", "VLazerCom")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"VLazerCom\main.mht");
            WebPage page = CreatePage(content, VLazerComCity.Vladivostok, VLazerComWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new VLazerComMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => VLazerComWebPageType.Catalog == (VLazerComWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, VLazerComWebPageType.Catalog, "/catalog/tv-audio-video/", "Телевизоры, аудио, видео"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, VLazerComWebPageType.Catalog, "/catalog/beauty-health/", "Красота и здоровье"), result.Pages.Last());
        }
    }
}