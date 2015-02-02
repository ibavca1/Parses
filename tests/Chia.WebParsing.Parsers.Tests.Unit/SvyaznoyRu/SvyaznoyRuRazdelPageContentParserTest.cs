using System.Linq;
using Chia.WebParsing.Companies.SvyaznoyRu;
using Chia.WebParsing.Parsers.SvyaznoyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.SvyaznoyRu
{
    [TestClass]
    public class SvyaznoyRuRazdelPageContentParserTest : SvyaznoyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\razdel_1.mht", "SvyaznoyRu")]
        public void Test_ParseHtml_Type1()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\razdel_1.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.StPetersburg, SvyaznoyRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(SvyaznoyRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Razdel, "/catalog/phone/224", "Все телефоны"), pages.First());
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Razdel, "/service/#t1993127", "Услуги по настройке телефона"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\razdel_2.mht", "SvyaznoyRu")]
        public void Test_ParseHtml_Type2()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\razdel_2.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.StPetersburg, SvyaznoyRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(SvyaznoyRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Razdel, "/access-service/?OPEN=1749064", "Подбор аксессуаров"), pages.First());
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Razdel, "/catalog/accessories/1838", "Автомобильные держатели"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\catalog.mht", "SvyaznoyRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\catalog.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.StPetersburg, SvyaznoyRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new SvyaznoyRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(SvyaznoyRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, SvyaznoyRuWebPageType.Catalog, page.Uri, page.Path), catalogPage); 
        }
    }
}