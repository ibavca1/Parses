using System.Linq;
using Chia.WebParsing.Companies.TelemaksRu;
using Chia.WebParsing.Parsers.TelemaksRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TelemaksRu
{
    [TestClass]
    public class TelemaksRuProductPageContentParserTest : TelemaksRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\product.mht", "TelemaksRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\product.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("23059603", product.Article);
            Assert.AreEqual("3D SMART UltraHD LED телевизор LG 55UB850V", product.Name);
            Assert.AreEqual(69990, product.OnlinePrice);
            Assert.AreEqual(69999, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\product_retail_only.mht", "TelemaksRu")]
        public void Test_ParseHtml_RetailOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\product_retail_only.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Product);

            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreNotEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\product.mht", "TelemaksRu")]
        public void Test_ParseHtml_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\product.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TelemaksRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("Ленинский пр., дом 127", availabilityInShops.First().ShopName);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("Энгельса пр., дом 139/21", availabilityInShops.Last().ShopName);
            Assert.AreEqual(true, availabilityInShops.Last().IsAvailable);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\product_no_shops.mht", "TelemaksRu")]
        public void Test_ParseHtml_NoAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\product_no_shops.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TelemaksRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsEmpty(availabilityInShops);
        }
    }
}