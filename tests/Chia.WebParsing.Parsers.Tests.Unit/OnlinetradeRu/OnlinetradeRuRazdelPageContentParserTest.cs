using System.Linq;
using Chia.WebParsing.Companies.OnlinetradeRu;
using Chia.WebParsing.Parsers.OnlinetradeRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OnlinetradeRu
{
    [TestClass]
    public class OnlinetradeRuRazdelPageContentParserTest : OnlinetradeRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\razdel.mht", "OnlinetradeRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\razdel.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.Moscow, OnlinetradeRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new OnlinetradeRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());
            CollectionAssert.All(result.Pages, p => p.IsPartOfSiteMap);
            CollectionAssert.All(result.Pages, p => OnlinetradeRuWebPageType.Razdel == (OnlinetradeRuWebPageType)p.Type);
            Assert.AreEqual(CreatePage(page, OnlinetradeRuWebPageType.Razdel, "/catalogue/bluetooth_garnituri-c114/#place_breadcrumbs", "Bluetooth-гарнитуры"), result.Pages.First());
            Assert.AreEqual(CreatePage(page, OnlinetradeRuWebPageType.Razdel, "/catalogue/razlichnie_aksessuari-c123/#place_breadcrumbs", "Различные аксессуары"), result.Pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\catalog.mht", "OnlinetradeRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\catalog.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.Moscow, OnlinetradeRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new OnlinetradeRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OnlinetradeRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}