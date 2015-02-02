using Chia.WebParsing.Companies.VLazerCom;
using Chia.WebParsing.Parsers.VLazerCom;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.VLazerCom
{
    [TestClass]
    public class VLazerComHtmlPageContentParserTest : VLazerComWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\VLazerCom\Pages\main.mht", "VLazerCom")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"VLazerCom\main.mht");
            WebPage page = CreatePage(content, VLazerComCity.Khabarovsk, VLazerComWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<VLazerComHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(VLazerComCity.Khabarovsk.Name, ex.Expected);
                Assert.AreEqual(VLazerComCity.Vladivostok.Name, ex.Actual);
                throw;
            }
        }
    }
}