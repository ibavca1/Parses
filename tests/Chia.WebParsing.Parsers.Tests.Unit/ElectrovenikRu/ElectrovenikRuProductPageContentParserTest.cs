using System.Linq;
using Chia.WebParsing.Companies.ElectrovenikRu;
using Chia.WebParsing.Parsers.ElectrovenikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.ElectrovenikRu
{
    [TestClass]
    public class ElectrovenikRuProductPageContentParserTest : ElectrovenikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\ElectrovenikRu\Pages\product.mht", "ElectrovenikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"ElectrovenikRu\product.mht");
            WebPage page = CreatePage(content, ElectrovenikRuCity.Moscow, ElectrovenikRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new ElectrovenikRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("14310 (с4)", product.Article);
            Assert.AreEqual("Однокамерный холодильник KORTING KS 50 HW", product.Name);
            Assert.AreEqual(7290, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(page.Uri, product.Uri);
        }
    }
}