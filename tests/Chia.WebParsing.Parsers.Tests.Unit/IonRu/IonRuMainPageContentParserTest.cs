using System.IO;
using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.IonRu;
using Chia.WebParsing.Parsers.IonRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.IonRu
{
    [TestClass]
    public class IonRuMainPageContentParserTest : IonRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\main.mht", "IonRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\main.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => IonRuWebPageType.Catalog == (IonRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, IonRuWebPageType.Catalog, "/articles", "Обзоры"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, IonRuWebPageType.Catalog, "/catalog", "Все товары"), result.Pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\main.mht", "IonRu")]
        public void Test_ParseHtml_ShopsPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\main.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Main);
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};
            var parser = new IonRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(IonRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, IonRuWebPageType.ShopsList, "/shops"), shopsPage);
        }
    }
}