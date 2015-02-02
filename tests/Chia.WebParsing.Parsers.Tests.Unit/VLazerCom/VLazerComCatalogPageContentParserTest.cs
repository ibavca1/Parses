using System.Linq;
using Chia.WebParsing.Companies.VLazerCom;
using Chia.WebParsing.Parsers.VLazerCom;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.VLazerCom
{
    [TestClass]
    public class VLazerComCatalogPageContentParserTest : VLazerComWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\VLazerCom\Pages\catalog_categories.mht", "VLazerCom")]
        public void Test_ParseHtml_Categories()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"VLazerCom\catalog_categories.mht");
            WebPage page = CreatePage(content, VLazerComCity.Vladivostok, VLazerComWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new VLazerComCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(VLazerComWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, VLazerComWebPageType.Catalog, "/catalog/tv-audio-video/lcd", "ЖК телевизоры"), pages.First());
            Assert.AreEqual(CreatePage(page, VLazerComWebPageType.Catalog, "/catalog/tv-audio-video/accessories", "Аксессуары"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\VLazerCom\Pages\catalog.mht", "VLazerCom")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"VLazerCom\catalog.mht");
            WebPage page = CreatePage(content, VLazerComCity.Vladivostok, VLazerComWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<VLazerComCatalogPageContentParser> {CallBase = true}.Object;

            Mock.Get((MockedWebSite)page.Site)
                .Setup(s => s.LoadPageContent(It.IsAny<WebPageRequest>(), context))
                .Returns(
                    (WebPageRequest r, WebPageContentParsingContext c) =>
                    {
                        Assert.AreEqual(VLazerComWebPageType.PriceOffset, (VLazerComWebPageType)r.Type);
                        Assert.IsNotNull(r.Content);
                        Assert.AreEqual("ATxE9cooBzEgbtCKT9adIMQBmvtRcExT/IekZUD0gA==", r.Content.ReadAsString());
                        return new StringWebPageContent("{\"success\":true,\"offset\":\"1886\"}");
                    });

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("4150310", product.Article);
            Assert.AreEqual("Телевизор Supra STV LC19663WL", product.Name);
            Assert.AreEqual(7176 - 1886, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Site.MakeUri("/product/4150310.html"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\VLazerCom\Pages\catalog_empty.mht", "VLazerCom")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"VLazerCom\catalog_empty.mht");
            WebPage page = CreatePage(content, VLazerComCity.Vladivostok, VLazerComWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new VLazerComCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\VLazerCom\Pages\catalog.mht", "VLazerCom")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"VLazerCom\catalog.mht");
            WebPage page = CreatePage(content, VLazerComCity.Vladivostok, VLazerComWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new VLazerComCatalogPageContentParser();

            Mock.Get((MockedWebSite)page.Site)
                .Setup(s => s.LoadPageContent(It.IsAny<WebPageRequest>(), context))
                .Returns(() => new StringWebPageContent("{\"success\":true,\"offset\":\"1886\"}"));

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(VLazerComWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, VLazerComWebPageType.Catalog, "/catalog/tv-audio-video/lcd/~/page-2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\VLazerCom\Pages\catalog_last_page.mht", "VLazerCom")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"VLazerCom\catalog_last_page.mht");
            WebPage page = CreatePage(content, VLazerComCity.Vladivostok, VLazerComWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new VLazerComCatalogPageContentParser();

            Mock.Get((MockedWebSite)page.Site)
                .Setup(s => s.LoadPageContent(It.IsAny<WebPageRequest>(), context))
                .Returns(() => new StringWebPageContent("{\"success\":true,\"offset\":\"1886\"}"));

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(VLazerComWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}