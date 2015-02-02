using System;
using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using Chia.WebParsing.Parsers.MvideoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MvideoRu
{
    [TestClass]
    public class MvideoRuProductPageContentParserTest : MvideoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\product.mht", "MvideoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\product.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("30020511", product.Article);
            Assert.AreEqual("Ноутбук Lenovo IdeaPad G5030 (80G0001FRK)", product.Name);
            Assert.AreEqual(page.Uri, product.Uri);
            Assert.AreEqual(11990, product.OnlinePrice);
            Assert.AreEqual(11990, product.RetailPrice);
            Assert.AreEqual(false, product.IsAction);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\product_action.mht", "MvideoRu")]
        public void Test_Parse_IsAction()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\product_action.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Product);
            var parser = new MvideoRuProductPageContentParser();
            var context = new WebPageContentParsingContext();

            // act
            
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(true, product.IsAction);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\product_out_of_stock.mht", "MvideoRu")]
        public void Test_Parse_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\product_unavailable_for_buy.mht", "MvideoRu")]
        public void Test_Parse_UnavailableForBuy()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\product_unavailable_for_buy.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\product_unavailable_for_buy_on_site.mht", "MvideoRu")]
        public void Test_Parse_UnavailableForBuyOnSite()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\product_unavailable_for_buy_on_site.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\product_retail_only.mht", "MvideoRu")]
        public void Test_Parse_RetailOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\product_retail_only.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(23990, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\product.mht", "MvideoRu")]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\availability_in_shops.html", "MvideoRu")]
        public void Test_Parse_ShopsAvailability()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\product.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Cheboksary, MvideoRuWebPageType.Product);
            var parser = new MvideoRuProductPageContentParser();
            var context = new WebPageContentParsingContext{Options = {AvailabiltyInShops = true}};

            Uri availabilityUri = CreateUri(page, "?ssb_block=availabilityContentBlockContainer&ajax=true");
            Mock.Get((MockedWebSite)page.Site)
                .Setup(s => s.LoadPageContent(It.IsAny<WebPageRequest>(), context))
                .Returns(
                    (WebPageRequest r, WebPageContentParsingContext c) =>
                    {
                        Assert.AreSame(availabilityUri, r.Uri);
                        Assert.AreEqual(MvideoRuWebPageType.AvailabilityInShops, (MvideoRuWebPageType)r.Type);
                        return ReadHtmlContent(@"MvideoRu\availability_in_shops.html");
                    });


            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("ул. Ленинского комсомола, 21а, ТЦ «Мадагаскар»", availabilityInShops.First().ShopAddress);
            Assert.AreEqual(false, availabilityInShops.Last().IsAvailable);
            Assert.AreEqual("ул. К.Маркса, 52, к.1, ТЦ «Карусель»", availabilityInShops.Last().ShopAddress);
        }
    }
}