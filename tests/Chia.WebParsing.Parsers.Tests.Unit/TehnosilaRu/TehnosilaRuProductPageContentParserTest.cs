using System.Linq;
using Chia.WebParsing.Companies.TehnosilaRu;
using Chia.WebParsing.Parsers.TehnosilaRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnosilaRu
{
    [TestClass]
    public class TehnosilaRuProductPageContentParserTest : TehnosilaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\product.mht", "TehnosilaRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\product.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("1150520", product.Article);
            Assert.AreEqual("Wi-Fi маршрутизатор Zyxel Keenetic Lite II 802.11n", product.Name);
            Assert.AreEqual(1729, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\product_out_of_stock.mht", "TehnosilaRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\product_out_of_stock_2.mht", "TehnosilaRu")]
        public void Test_ParseHtml_OutOfStock2()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\product_out_of_stock_2.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\product.mht", "TehnosilaRu")]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\product_availability_in_shops.html", "TehnosilaRu")]
        public void Test_ParseHtml_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\product.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TehnosilaRuProductPageContentParser();

            Mock.Get((MockedWebSite)page.Site)
                .Setup(x => x.LoadPageContent(It.IsAny<WebPageRequest>(), context))
                .Returns(
                    (WebPageRequest r, WebPageContentParsingContext c) =>
                    {
                        Assert.AreEqual("26882", (string)r.Data);
                        return ReadHtmlContent(@"TehnosilaRu\product_availability_in_shops.html");
                    });

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("Москва, Андропова проспект, д.8, ТЦ \"Мегаполис\"", availabilityInShops.First().ShopAddress);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("Балашиха, Носовихинское шоссе, мкр. Салтыковка, вл.4, ТЦ \"Салтыковский\"", availabilityInShops.Last().ShopAddress);
            Assert.AreEqual(true, availabilityInShops.Last().IsAvailable);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\product_pre_order.mht", "TehnosilaRu")]
        public void Test_ParseHtml_PreOrder()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\product_pre_order.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnosilaRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(22990,product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            CollectionAssert.IsEmpty(product.AvailabilityInShops.ToArray());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\product_not_available_in_shops.mht", "TehnosilaRu")]
        public void Test_ParseHtml_NotAvailableInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\product_not_available_in_shops.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.Moscow, TehnosilaRuWebPageType.Product);
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new TehnosilaRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsEmpty(availabilityInShops);
        }
    }
}