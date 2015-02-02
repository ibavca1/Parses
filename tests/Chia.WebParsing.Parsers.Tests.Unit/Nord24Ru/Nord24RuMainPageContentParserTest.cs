using System.Linq;
using Chia.WebParsing.Companies.Nord24Ru;
using Chia.WebParsing.Parsers.Nord24Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Nord24Ru
{
    [TestClass]
    public class Nord24RuMainPageContentParserTest : Nord24RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Nord24Ru\Pages\main.mht", "Nord24Ru")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Nord24Ru\main.mht");
            WebPage page = CreatePage(content, Nord24RuCity.Chelyabinsk, Nord24RuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Nord24RuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(Nord24RuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, Nord24RuWebPageType.Razdel, "/shop/tehnika-dlya-kuhni/", "Техника для кухни"), pages.First());
            Assert.AreEqual(CreatePage(page, Nord24RuWebPageType.Razdel, "/shop/tehnika-dlya-doma/", "Техника для дома"), pages.Last());
        }
    }
}