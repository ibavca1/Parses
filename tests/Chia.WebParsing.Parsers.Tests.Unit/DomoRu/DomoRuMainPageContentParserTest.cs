using System.Linq;
using Chia.WebParsing.Companies.DomoRu;
using Chia.WebParsing.Parsers.DomoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DomoRu
{
    [TestClass]
    public class DomoRuMainPageContentParserTest : DomoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\main.mht", "DomoRu")]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\main_ajax.html", "DomoRu")]
        public void Test_Parse_MainMenu()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\main.mht");
            WebPage page = CreatePage(content, DomoRuCity.Kazan, DomoRuWebPageType.Main);
            var parser = new DomoRuMainPageContentParser();
            var context = new WebPageContentParsingContext();

            Mock.Get((MockedWebSite)page.Site)
               .Setup(x => x.LoadPageContent(It.IsAny<WebPageRequest>(), context))
               .Returns(
                   (WebPageRequest r, WebPageContentParsingContext c) =>
                   {
                       Assert.AreEqual(DomoRuWebPageType.MainMenuAjax, (DomoRuWebPageType)r.Type);
                       return ReadHtmlContent(@"DomoRu\main_ajax.html");
                   });

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(DomoRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DomoRuWebPageType.Razdel, "/catalog/kino-i-muzika-6", "Кино и музыка"), pages.First());
            Assert.AreEqual(CreatePage(page, DomoRuWebPageType.Razdel, "/catalog/igrovie-pristavki-i-muzikalnie-instrumenti", "Игровые приставки и музыкальные инструменты"), pages.Last());
        }
    }
}