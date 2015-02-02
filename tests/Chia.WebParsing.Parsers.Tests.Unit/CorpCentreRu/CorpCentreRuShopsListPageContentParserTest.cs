using System.Linq;
using Chia.WebParsing.Companies.CorpCentreRu;
using Chia.WebParsing.Parsers.CorpCentreRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CorpCentreRu
{
    [TestClass]
    public class CorpCentreRuShopsListPageContentParserTest : CorpCentreRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\shops_list.mht", "CorpCentreRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\shops_list.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Ekaterinburg, CorpCentreRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new CorpCentreRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Shops.ToArray());
            Assert.AreEqual("Центр-1 на ул. Уральской, 61 А, ТЦ «Гуд’зон»", result.Shops.First().Name);
            Assert.AreEqual("Свердловская область, г. Екатеринбург, ул. Уральская, 61 А, ТЦ  «Гуд’зон», 3 этаж", result.Shops.First().Address);
            Assert.AreEqual("Центр-8 на ул. Щербакова, 4А, ТРК «Глобус»", result.Shops.Last().Name);
            Assert.AreEqual("Свердловская область, г. Екатеринбург, ул. Щербакова, 4А, ТРК «Глобус»", result.Shops.Last().Address);
        }
    }
}