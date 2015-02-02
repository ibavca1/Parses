using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.IonRu;
using Chia.WebParsing.Parsers.IonRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.IonRu
{
    [TestClass]
    public class IonRuCatalogPageContentParserTest : IonRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\catalog.mht", "IonRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\catalog.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.FirstWithType(IonRuWebPageType.Product);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, IonRuWebPageType.Product, "/catalog/alkotester-parkcity-sober-7"), catalogPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\catalog_variants.mht", "IonRu")]
        public void Test_ParseHtml_ProductVariants()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\catalog_variants.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(IonRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, IonRuWebPageType.Catalog, "/catalog/variants/onext-care-phone-4"), catalogPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\catalog_empty.mht", "IonRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\catalog_empty.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\catalog.mht", "IonRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\catalog.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new IonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(IonRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, IonRuWebPageType.Catalog, "/catalog/-/dlja-avto", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\catalog_last_page.mht", "IonRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new IonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(IonRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}