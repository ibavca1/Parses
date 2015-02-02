using Chia.WebParsing.Companies.LogoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.LogoRu
{
    public abstract class LogoRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return LogoRuCompany.Instance; }
        }
    }
}