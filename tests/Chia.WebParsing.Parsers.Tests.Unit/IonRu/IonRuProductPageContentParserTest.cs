using System;
using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.IonRu;
using Chia.WebParsing.Parsers.IonRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.Tests.Unit.IonRu
{
    [TestClass]
    public class IonRuProductPageContentParserTest : IonRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\product.mht", "IonRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\product.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("46060", product.Article);
            Assert.AreEqual("Asus FonePad 8 FE380CG 3G 16Gb Black 90NK0162-M01320", product.Name);
            Assert.AreEqual(8990, product.OnlinePrice);
            Assert.AreEqual(8990, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\product_out_of_stock.mht", "IonRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\product_retail_only.mht", "IonRu")]
        public void Test_ParseHtml_RetailOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\product_retail_only.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreNotEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\product_online_only.mht", "IonRu")]
        public void Test_ParseHtml_OnlineOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\product_online_only.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreNotEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\product.mht", "IonRu")]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\availability_in_shops.mht", "IonRu")]
        public void Test_ParseHtml_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\product.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new IonRuProductPageContentParser();
            var availabilityUri = page.Uri.Append("remains");

            Mock.Get((MockedWebSite)page.Site)
                .Setup(s => s.LoadPageContent(It.IsAny<WebPageRequest>(), context))
                .Returns(
                    (WebPageRequest r, WebPageContentParsingContext c) =>
                        {
                            Assert.AreEqual(availabilityUri, r.Uri);
                            Assert.AreEqual(IonRuWebPageType.AvailabilityInShops, (IonRuWebPageType) r.Type);
                            return ReadMhtmlContent(@"IonRu\availability_in_shops.mht");
                        });

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            Assert.AreEqual("ТЦ Город", availabilityInShops.First().ShopName);
            Assert.AreEqual("г. Москва, Шоссе Энтузиастов, 12 корп.2", availabilityInShops.First().ShopAddress);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("ТЦ Акварель", availabilityInShops.Last().ShopName);
            Assert.AreEqual("г. Щербинка, Железнодорожная, 44", availabilityInShops.Last().ShopAddress);
            Assert.AreEqual(true, availabilityInShops.Last().IsAvailable);
        }
    }
}