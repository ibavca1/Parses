using Chia.WebParsing.Companies.LogoRu;
using Chia.WebParsing.Parsers.LogoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.LogoRu
{
    [TestClass]
    public class LogoRuHtmlPageContentParserTest : LogoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\main.mht", "LogoRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\main.mht");
            WebPage page = CreatePage(content, LogoRuCity.Perm, LogoRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<LogoRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(LogoRuCity.Perm.Name, ex.Expected);
                Assert.AreEqual(LogoRuCity.Ekaterinburg.Name, ex.Actual);
                throw;
            }
        }
    }
}