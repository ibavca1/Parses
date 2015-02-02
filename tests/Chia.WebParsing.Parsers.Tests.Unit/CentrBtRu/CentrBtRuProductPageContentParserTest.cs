using System.Linq;
using Chia.WebParsing.Companies.CentrBtRu;
using Chia.WebParsing.Parsers.CentrBtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CentrBtRu
{
    [TestClass]
    public class CentrBtRuProductPageContentParserTest : CentrBtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\product.mht", "CentrBtRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\product.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Moscow, CentrBtRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new CentrBtRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("138756", product.Article);
            Assert.AreEqual("Стиральная машина AEG L 88489 FL 2", product.Name);
            Assert.AreEqual(35210, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(page.Uri, product.Uri);
        }
    }
}