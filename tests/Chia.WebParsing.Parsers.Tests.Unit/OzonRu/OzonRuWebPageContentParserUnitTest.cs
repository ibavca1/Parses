using Chia.WebParsing.Companies.OzonRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OzonRu
{
    public abstract class OzonRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return OzonRuCompany.Instance; }
        }
    }
}