using System.Linq;
using Chia.WebParsing.Companies.TehnoshokRu;
using Chia.WebParsing.Parsers.TehnoshokRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoshokRu
{
    [TestClass]
    public class TehnoshokRuProductPageContentParserTest : TehnoshokRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\product.mht", "TehnoshokRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\product.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("1162926", product.Article);
            Assert.AreEqual("Увлажнитель воздуха Vitek VT-1764", product.Name);
            Assert.AreEqual(2890, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\product_out_of_stock.mht", "TehnoshokRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\product_out_of_stock.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\product_availability_in_shops.mht", "TehnoshokRu")]
        public void Test_ParseHtml_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\product_availability_in_shops.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TehnoshokRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("Большой пр. В.О., 18",availabilityInShops.First().ShopAddress);
            Assert.AreEqual("Большой пр. В.О., 18", availabilityInShops.First().ShopName);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual(true, availabilityInShops[11].IsAvailable);
            Assert.AreEqual(false, availabilityInShops[12].IsAvailable);
            Assert.AreEqual("Просвещения пр. 81", availabilityInShops.Last().ShopAddress);
            Assert.AreEqual("Просвещения пр. 81", availabilityInShops.Last().ShopName);
            Assert.AreEqual(false, availabilityInShops.Last().IsAvailable);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\product_availability_in_shops_2.mht", "TehnoshokRu")]
        public void Test_ParseHtml_AvailabilityInShops_AvailableNow()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\product_availability_in_shops_2.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Product);
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new TehnoshokRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("Караваевская ул. 24", availabilityInShops.First().ShopAddress);
            Assert.AreEqual("Караваевская ул. 24", availabilityInShops.First().ShopName);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("Техносила, Красноармейская 15", availabilityInShops.Last().ShopAddress);
            Assert.AreEqual("Техносила, Красноармейская 15", availabilityInShops.Last().ShopName);
            Assert.AreEqual(true, availabilityInShops.Last().IsAvailable);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\product_not_found.mht", "TehnoshokRu")]
        public void Test_ParseHtml_NotFound()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\product_not_found.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }
    }
}