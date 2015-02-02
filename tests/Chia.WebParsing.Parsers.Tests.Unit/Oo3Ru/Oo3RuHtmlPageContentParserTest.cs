using Chia.WebParsing.Companies.Oo3Ru;
using Chia.WebParsing.Parsers.Oo3Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.Oo3Ru
{
    [TestClass]
    public class Oo3RuHtmlPageContentParserTest : Oo3RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\main.mht", "Oo3Ru")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\main.mht");
            WebPage page = CreatePage(content, Oo3RuCity.StPetersburg, Oo3RuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<Oo3RuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(Oo3RuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(Oo3RuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}