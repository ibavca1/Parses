using System.Linq;
using Chia.WebParsing.Companies.VLazerCom;
using Chia.WebParsing.Parsers.VLazerCom;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.VLazerCom
{
    [TestClass]
    public class VLazerComProductPageContentParserTest : VLazerComWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\VLazerCom\Pages\product.mht", "VLazerCom")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"VLazerCom\product.mht");
            WebPage page = CreatePage(content, VLazerComCity.Vladivostok, VLazerComWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new VLazerComProductPageContentParser();

            Mock.Get((MockedWebSite) page.Site)
                .Setup(s => s.LoadPageContent(It.IsAny<WebPageRequest>(), context))
                .Returns(
                    (WebPageRequest r, WebPageContentParsingContext c) =>
                        {
                            Assert.AreEqual(VLazerComWebPageType.PriceOffset, (VLazerComWebPageType)r.Type);
                            Assert.IsNotNull(r.Content);
                            Assert.AreEqual("y+eCqUGIKYNFUsR4CrsjaP3PNaXFHN6zuP8aiLJMVw==", r.Content.ReadAsString());
                            return new StringWebPageContent("{\"success\":true,\"offset\":\"1886\"}");
                        });

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("4150310", product.Article);
            Assert.AreEqual("Телевизор Supra STV LC19663WL", product.Name);
            Assert.AreEqual(9994 - 1886, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Uri, product.Uri);
        }
    }
}