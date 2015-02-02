using System.Linq;
using Chia.WebParsing.Companies.Nord24Ru;
using Chia.WebParsing.Parsers.Nord24Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Nord24Ru
{
    [TestClass]
    public class Nord24RuProductPageContentParserTest : Nord24RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Nord24Ru\Pages\product.mht", "Nord24Ru")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Nord24Ru\product.mht");
            WebPage page = CreatePage(content, Nord24RuCity.Chelyabinsk, Nord24RuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new Nord24RuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Гриль Rolsen RG 1410", product.Name);
            Assert.AreEqual(2850, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }
    }
}