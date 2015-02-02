using Chia.WebParsing.Companies.UlmartRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UlmartRu
{
    public abstract class UlmartRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return UlmartRuCompany.Instance; }
        }
    }
}