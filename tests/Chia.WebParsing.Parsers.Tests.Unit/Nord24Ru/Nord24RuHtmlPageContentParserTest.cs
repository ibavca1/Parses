using Chia.WebParsing.Companies.Nord24Ru;
using Chia.WebParsing.Parsers.Nord24Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.Nord24Ru
{
    [TestClass]
    public class Nord24RuHtmlPageContentParserTest : Nord24RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Nord24Ru\Pages\main.mht", "Nord24Ru")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Nord24Ru\main.mht");
            WebPage page = CreatePage(content, Nord24RuCity.Ekaterinburg, Nord24RuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<Nord24RuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(Nord24RuCity.Ekaterinburg.Name, ex.Expected);
                Assert.AreEqual(Nord24RuCity.Chelyabinsk.Name, ex.Actual);
                throw;
            }
        }
    }
}