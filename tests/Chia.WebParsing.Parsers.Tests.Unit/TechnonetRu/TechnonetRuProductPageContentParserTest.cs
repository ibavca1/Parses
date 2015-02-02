using System.Linq;
using Chia.WebParsing.Companies.TechnonetRu;
using Chia.WebParsing.Parsers.TechnonetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechnonetRu
{
    [TestClass]
    public class TechnonetRuProductPageContentParserTest : TechnonetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\product.mht", "TechnonetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\product.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Посудомоечная машина 60 см", product.Characteristics);
            Assert.AreEqual("Bosch SMS-40 D02 RU", product.Name);
            Assert.AreEqual(16410, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\product.mht", "TechnonetRu")]
        public void Test_ParseHtml_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\product.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TechnonetRuProductPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("ул. Чернышевского, 88", availabilityInShops.First().ShopName);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("ул. Дагестанская, 2", availabilityInShops.Last().ShopName);
            Assert.AreEqual(true, availabilityInShops.Last().IsAvailable);
        }
    }
}