using System.Linq;
using Chia.WebParsing.Companies.RegardRu;
using Chia.WebParsing.Parsers.RegardRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.RegardRu
{
    [TestClass]
    public class RegardRuProductPageContentParserTest : RegardRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RegardRu\Pages\product.mht", "RegardRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RegardRu\product.mht");
            WebPage page = CreatePage(content, RegardRuCity.Moscow, RegardRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new RegardRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("165266", product.Article);
            Assert.AreEqual("Твердотельный накопитель 480Gb SSD OCZ Vertex 460A (VTX460A-25SAT3-480G)", product.Name);
            Assert.AreEqual("внутренний SSD, 2.5\", 480 Гб, SATA-III, чтение: 545 Мб/сек, запись: 525 Мб/сек, MLC", product.Characteristics);
            Assert.AreEqual(14130, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RegardRu\Pages\product_out_of_stock.mht", "RegardRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RegardRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, RegardRuCity.Moscow, RegardRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new RegardRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}