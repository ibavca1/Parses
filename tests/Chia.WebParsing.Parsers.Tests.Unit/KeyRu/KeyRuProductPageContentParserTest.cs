using System.Linq;
using Chia.WebParsing.Companies.KeyRu;
using Chia.WebParsing.Parsers.KeyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.KeyRu
{
    [TestClass]
    public class KeyRuProductPageContentParserTest : KeyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\product.mht", "KeyRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\product.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuProductPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("807580", product.Article);
            Assert.AreEqual("Компьютер HP Envy 700-300nr, J2G72EA", product.Name);
            Assert.AreEqual(47550, product.OnlinePrice);
            Assert.AreEqual(47550, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\product_availability_in_shops.mht", "KeyRu")]
        public void Test_Parse_AvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\product_availability_in_shops.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Product);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new KeyRuProductPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            WebProductAvailabilityInShop[] availabilityInShops = product.AvailabilityInShops.ToArray();
            CollectionAssert.IsNotEmpty(availabilityInShops);
            Assert.AreEqual("Новосмоленская наб., д.1", availabilityInShops.First().ShopAddress);
            Assert.AreEqual(true, availabilityInShops.First().IsAvailable);
            Assert.AreEqual("Ветеранов пр.,  д.101", availabilityInShops.Last().ShopAddress);
            Assert.AreEqual(true, availabilityInShops.Last().IsAvailable);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\product_legals_only.mht", "KeyRu")]
        public void Test_Parse_LegalsOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\product_legals_only.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Product);
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new KeyRuProductPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\product_characteristics.mht", "KeyRu")]
        public void Test_Parse_Characteristics()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\product_characteristics.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuProductPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("зеленый,1025009", product.Characteristics);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\product_characteristics_manufacturer_code.mht", "KeyRu")]
        public void Test_Parse_Characteristics_ManufacturerCode()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\product_characteristics_manufacturer_code.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Product);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuProductPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("J2G72EA", product.Characteristics);
        }
    }
}