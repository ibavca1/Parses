using System.Linq;
using Chia.WebParsing.Companies.TdPoiskRu;
using Chia.WebParsing.Parsers.TdPoiskRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TdPoiskRu
{
    [TestClass]
    public class TdPoiskRuProductPageContentParserTest : TdPoiskRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\product.mht", "TdPoiskRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\product.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("95173", product.Article);
            Assert.AreEqual("SUPRA DVS-755HKU Bl", product.Name);
            Assert.AreEqual(1290, product.OnlinePrice);
            Assert.AreEqual(1290, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\product_retail_only.mht", "TdPoiskRu")]
        public void Test_ParseHtml_RetailOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\product_retail_only.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreNotEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\product_no_price.mht", "TdPoiskRu")]
        public void Test_ParseHtml_NoPrice()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\product_no_price.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\product.mht", "TdPoiskRu")]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\product_availability_in_shops.html", "TdPoiskRu")]
        public void Test_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\product.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TdPoiskRuProductPageContentParser();

            Mock.Get((MockedWebSite)page.Site)
               .Setup(x => x.LoadPageContent(It.IsAny<WebPageRequest>(), context))
               .Returns((WebPageRequest r, WebPageContentParsingContext c) =>
                   {
                       Assert.AreEqual("8844923", (string)r.Data);
                       return ReadHtmlContent(@"TdPoiskRu\product_availability_in_shops.html");
                   });

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("г. Краснодар, ул. Стасова, д.1/Cелезнева, д.178", availabilityInShops.First().ShopName);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
        }
    }
}