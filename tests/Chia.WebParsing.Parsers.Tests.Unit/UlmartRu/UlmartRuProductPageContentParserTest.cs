using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using Chia.WebParsing.Parsers.UlmartRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UlmartRu
{
    [TestClass]
    public class UlmartRuProductPageContentParserTest : UlmartRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\product.mht", "UlmartRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\product.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("894149", product.Article);
            Assert.AreEqual("ноутбук ASUS X551MAV, 90NB0481-M08800", product.Name);
            Assert.AreEqual(
                "ноутбук ASUS X551MAV, 90NB0481-M08800, 15.6\" (1366x768), 2048, 320, Intel Celeron N2830, Intel HD Graphics, LAN, WiFi, Bluetooth, веб камера, Win8, веб камера",
                product.Characteristics);
            Assert.AreEqual(10990, product.OnlinePrice);
            Assert.AreEqual(10990, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\product_out_of_stock.mht", "UlmartRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, UlmartRuCity.RostovOnDon, UlmartRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\product.mht", "UlmartRu")]
        public void Test_ParseHtml_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\product.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new UlmartRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.IsNotNull(product);
            CollectionAssert.IsNotEmpty(product.AvailabilityInShops.ToArray());
            Assert.AreEqual("Варшавское ш., д. 143а", product.AvailabilityInShops.First().ShopAddress);
            Assert.AreEqual("Варшавское ш., д. 143а", product.AvailabilityInShops.First().ShopName);
            Assert.AreEqual(true, product.AvailabilityInShops.First().IsAvailable);
            Assert.AreEqual("ул. 9-ая Парковая, д. 62", product.AvailabilityInShops.Last().ShopAddress);
            Assert.AreEqual("ул. 9-ая Парковая, д. 62", product.AvailabilityInShops.Last().ShopName);
            Assert.AreEqual(true, product.AvailabilityInShops.First().IsAvailable);
        }
    }
}