using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.NotikRu;
using Chia.WebParsing.Parsers.NotikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NotikRu
{
    [TestClass]
    public class NotikRuProductPageContentParserTest : NotikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\product.mht", "NotikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\product.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new NotikRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("36140", product.Article);
            Assert.AreEqual("Acer Extensa 2509 N2930 2Gb 500Gb Intel HD Graphics 15,6 HD DVD(DL) BT Cam 4700мАч Linux OS Черный 2509-C1NP NX.EEZER.002", product.Name);
            Assert.AreEqual(11290, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\product_no_article.mht", "NotikRu")]
        public void Test_ParseHtml_NoArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\product_no_article.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new NotikRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
        }
    }
}