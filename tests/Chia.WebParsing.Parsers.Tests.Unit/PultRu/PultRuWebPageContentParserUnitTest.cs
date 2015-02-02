using Chia.WebParsing.Companies.PultRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.PultRu
{
    public abstract class PultRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return PultRuCompany.Instance; }
        }
    }
}