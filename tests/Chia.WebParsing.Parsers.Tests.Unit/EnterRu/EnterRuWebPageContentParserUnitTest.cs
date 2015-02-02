using Chia.WebParsing.Companies.EnterRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EnterRu
{
    public abstract class EnterRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return EnterRuCompany.Instance; }
        }
    }
}