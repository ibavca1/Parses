using Chia.WebParsing.Companies.JustRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.JustRu
{
    public abstract class JustRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return JustRuCompany.Instance; }
        }
    }
}