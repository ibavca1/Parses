using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.OzonRu;
using Chia.WebParsing.Parsers.OzonRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.Tests.Unit.OzonRu
{
    [TestClass]
    public class OzonRuProductPageContentParserTest : OzonRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\product.mht", "OzonRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\product.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("iPhone 6 64GB, Space Gray", product.Name);
            Assert.AreEqual(36990, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\product.mht", "OzonRu")]
        public void Test_ParseHtml_AdditionalPages()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\product.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(OzonRuWebPageType.Product).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => Equals(p.Path, page.Path));
            CollectionAssert.Any(pages, p => CreateUri(page, "/context/detail/id/29743815/?color=true").IsSame(p.Uri));
            CollectionAssert.Any(pages, p => CreateUri(page, "/context/detail/id/29743816/?color=true").IsSame(p.Uri));
            CollectionAssert.Any(pages, p => CreateUri(page, "/context/detail/id/29743814/?size=true").IsSame(p.Uri));
            CollectionAssert.Any(pages, p => CreateUri(page, "/context/detail/id/29743822/?size=true").IsSame(p.Uri));
        }
    }
}