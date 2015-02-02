using System.Linq;
using Chia.WebParsing.Companies.CorpCentreRu;
using Chia.WebParsing.Parsers.CorpCentreRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CorpCentreRu
{
    [TestClass]
    public class CorpCentreRuProductPageContentParserTest : CorpCentreRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\product.mht", "CorpCentreRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\product.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("0031203",product.Article);
            Assert.AreEqual("Встраиваемая электрическая панель Zanussi ZEV 56240 FA", product.Name);
            Assert.AreEqual(9490, product.OnlinePrice);
            Assert.AreEqual(9490, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\product_out_of_stock.mht", "CorpCentreRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\product_online_only.mht", "CorpCentreRu")]
        public void Test_ParseHtml_OnlineOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\product_online_only.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(74590, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\product.mht", "CorpCentreRu")]
        public void Test_ParseHtml_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\product.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new CorpCentreRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            Assert.AreEqual(7, availabilityInShops.Length);
            Assert.AreEqual("Центр-1 на ул. Уральской, 61 А, ТЦ «Гуд’зон»", availabilityInShops.First().ShopName);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("Центр-8 на ул. Щербакова, 4А, ТРК «Глобус»", availabilityInShops.Last().ShopName);
            Assert.AreEqual(false, availabilityInShops.Last().IsAvailable);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\product_availability_in_shops_order.mht", "CorpCentreRu")]
        public void Test_ParseHtml_AvailabilityInShops_Order()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\product_availability_in_shops_order.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Syktyvkar, CorpCentreRuWebPageType.Product);
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new CorpCentreRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            Assert.AreEqual(3, availabilityInShops.Length);
            Assert.AreEqual("Центр-1 на ул. Димитрова, 20 А", availabilityInShops.First().ShopName);
            Assert.AreEqual(false, availabilityInShops.First().IsAvailable);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\product_no_article.mht", "CorpCentreRu")]
        public void Test_ParseHtml_NoArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\product_no_article.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\product_daily_hit.mht", "CorpCentreRu")]
        public void Test_ParseHtml_DailyHit()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\product_daily_hit.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreNotEqual(0, product.RetailPrice);
        }
    }
}