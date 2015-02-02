using System.Linq;
using Chia.WebParsing.Companies.TdPoiskRu;
using Chia.WebParsing.Parsers.TdPoiskRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TdPoiskRu
{
    [TestClass]
    public class TdPoiskRuShopsListPageContentParserTest : TdPoiskRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\shops_list.mht", "TdPoiskRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\shops_list.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.Krasnodar, TdPoiskRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TdPoiskRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops.ToArray());
            Assert.AreEqual("г. Краснодар, ул. Дзержинского, д.42", shops.First().Name);
            Assert.AreEqual("г. Краснодар, ул. Стасова, д.1/Cелезнева, д.178", shops.Last().Name);
        }
    }
}