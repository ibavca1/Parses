using Chia.WebParsing.Companies.TehnosilaRu;
using Chia.WebParsing.Parsers.TehnosilaRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnosilaRu
{
    [TestClass]
    public class TehnosilaRuHtmlPageContentParserTest : TehnosilaRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnosilaRu\Pages\main.mht", "TehnosilaRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnosilaRu\main.mht");
            WebPage page = CreatePage(content, TehnosilaRuCity.StPetersburg, TehnosilaRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<TehnosilaRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(TehnosilaRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(TehnosilaRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}