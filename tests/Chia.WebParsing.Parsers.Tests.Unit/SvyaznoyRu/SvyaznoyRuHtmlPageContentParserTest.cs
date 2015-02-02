using System.Net;
using Chia.WebParsing.Companies.SvyaznoyRu;
using Chia.WebParsing.Parsers.SvyaznoyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.SvyaznoyRu
{
    [TestClass]
    public class SvyaznoyRuHtmlPageContentParserTest : SvyaznoyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\SvyaznoyRu\Pages\main.mht", "SvyaznoyRu")]
        [ExpectedException(typeof (InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"SvyaznoyRu\main.mht");
            WebPage page = CreatePage(content, SvyaznoyRuCity.StPetersburg, SvyaznoyRuWebPageType.Main);
            content.Cookies.Add(new Cookie("SHOWCITY", SvyaznoyRuCity.Moscow.Name, "/", ".svyaznoy.ru"));
            var context = new WebPageContentParsingContext();
            var parser = new Mock<SvyaznoyRuHtmlPageContentParser> {CallBase = true}.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(SvyaznoyRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(SvyaznoyRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}