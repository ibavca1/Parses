using System.Linq;
using Chia.WebParsing.Companies.DnsShopRu;
using Chia.WebParsing.Parsers.DnsShopRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DnsShopRu
{
    [TestClass]
    public class DnsShopRuProductPageContentParserTest : DnsShopRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\product.mht", "DnsShopRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\product.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("801318", product.Article);
            Assert.AreEqual("Смартфон Apple iPhone 4 RF 3.5\" 8Gb Black $$ 1x1Ghz, 512Mb, 960x640, S-IPS, 3G, GPS, Cam5.1, iOS7", product.Name);
            Assert.AreEqual(9990, product.OnlinePrice);
            Assert.AreEqual(9990, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\product_discounted_price.mht", "DnsShopRu")]
        public void Test_Parse_DiscountedPrice()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\product_discounted_price.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(9490, product.OnlinePrice);
            Assert.AreEqual(9490, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\product_out_of_stock.mht", "DnsShopRu")]
        public void Test_Parse_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\product.mht", "DnsShopRu")]
        public void Test_Parse_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\product.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new DnsShopRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("«ТЦ «Кольцо»", availabilityInShops.First().ShopName);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("«ГМ «Теорема»", availabilityInShops.Last().ShopName);
            Assert.AreEqual(true, availabilityInShops.Last().IsAvailable);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\product_no_shops.mht", "DnsShopRu")]
        public void Test_Parse_AvailabilityInShops_NoOnes()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\product_no_shops.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Product);
            
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new DnsShopRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsEmpty(availabilityInShops);
        }
    }
}