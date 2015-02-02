using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chia.WebParsing.Companies.EldoradoRu;
using Chia.WebParsing.Parsers.EldoradoRu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseLibrary.UnitTesting;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EldoradoRu
{
    [TestClass]
    public class EldoradoRuProductPageContentParserTest : EldoradoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product.mht", "EldoradoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\product.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuProductPageContentParser();
  
            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("LED телевизор THOMSON T32E02U", product.Name);
            Assert.AreEqual("71096212", product.Article);
            Assert.AreEqual(page.Uri, product.Uri);
            Assert.AreEqual(9199, product.OnlinePrice);
            Assert.AreEqual(9199, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product_out_of_stock.mht", "EldoradoRu")]
        public void Test_Parse_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product_only_in_shops.mht", "EldoradoRu")]
        public void Test_Parse_OnlyInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\product_only_in_shops.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product_comming_soon.mht", "EldoradoRu")]
        public void Test_Parse_CommingSoon()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\product_comming_soon.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product.mht", "EldoradoRu")]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product_availability_in_shops.mht", "EldoradoRu")]
        public void Test_Parse_AvailabilityInRetailShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\product.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Product);
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new EldoradoRuProductPageContentParser();

            Mock.Get((MockedWebSite)page.Site)
                .Setup(x => x.LoadPageContent(It.IsAny<WebPageRequest>(), context))
                .Returns((WebPageRequest r, WebPageContentParsingContext c) =>
                {
                    Assert.AreEqual("71096212", r.Data);
                    return ReadMhtmlContent(@"EldoradoRu\product_availability_in_shops.mht");
                });

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("ул. Менделеева, д.137", availabilityInShops.First().ShopName);
            Assert.AreEqual("ул. Менделеева, д.137", availabilityInShops.First().ShopAddress);
            Assert.AreEqual("ул. Чернышевского, д.100", availabilityInShops.Last().ShopName);
            Assert.AreEqual("ул. Чернышевского, д.100", availabilityInShops.Last().ShopAddress);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product.mht", "EldoradoRu")]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product_availability_in_shops_no_one.mht", "EldoradoRu")]
        public void Test_ParseRetailShopsHtml_NoShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\product.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Product);
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new EldoradoRuProductPageContentParser();

            Mock.Get((MockedWebSite)page.Site)
               .Setup(x => x.LoadPageContent(It.IsAny<WebPageRequest>(), context))
               .Returns(ReadMhtmlContent(@"EldoradoRu\product_availability_in_shops_no_one.mht"));

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsEmpty(availabilityInShops);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\product_availability_in_shops_failed.html", "EldoradoRu")]
        public void Test_ParseRetailShopsHtml_Failed()
        {
            // arrange
            string html = File.ReadAllText(@"EldoradoRu\product_availability_in_shops_failed.html", Encoding.GetEncoding(1251));
            var content = new HtmlPageContent(html);
            var parser = new EldoradoRuProductPageContentParser();

            // act
            IList<WebProductAvailabilityInShop> shops;
            bool result = parser.TryParseAvailabilityInShopsHtml(content, out shops);

            //// assert
            Assert.IsFalse(result);
        }
    }
}