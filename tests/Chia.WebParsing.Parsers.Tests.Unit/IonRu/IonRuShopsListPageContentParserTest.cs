using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.IonRu;
using Chia.WebParsing.Parsers.IonRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.IonRu
{
    [TestClass]
    public class IonRuShopsListPageContentParserTest : IonRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\IonRu\Pages\shops_list.mht", "IonRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"IonRu\shops_list.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, IonRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new IonRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Shops.ToArray());
            Assert.AreEqual(new WebShop("ТЦ Город", "г. Москва, Шоссе Энтузиастов, 12 корп.2"), result.Shops.First());
            Assert.AreEqual(new WebShop("ТЦ Карнавал", "г. Чехов, Московская, владение 96, ТЦ «Карнавал», 1 этаж "), result.Shops.Last());
        }
    }
}