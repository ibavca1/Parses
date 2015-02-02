using Chia.WebParsing.Companies.NotikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NotikRu
{
    public abstract class NotikRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return NotikRuCompany.Instance; }
        }
    }
}