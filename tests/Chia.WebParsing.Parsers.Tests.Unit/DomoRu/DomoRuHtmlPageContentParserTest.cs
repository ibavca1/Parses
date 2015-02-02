using Chia.WebParsing.Companies.DomoRu;
using Chia.WebParsing.Parsers.DomoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.DomoRu
{
    [TestClass]
    public class DomoRuHtmlPageContentParserTest : DomoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DomoRu\Pages\main.mht", "DomoRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DomoRu\main.mht");
            WebPage page = CreatePage(content, DomoRuCity.Cheboksary, DomoRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<DomoRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(DomoRuCity.Cheboksary.Name, ex.Expected);
                Assert.AreEqual(DomoRuCity.Kazan.Name, ex.Actual);
                throw;
            }
        }
    }
}