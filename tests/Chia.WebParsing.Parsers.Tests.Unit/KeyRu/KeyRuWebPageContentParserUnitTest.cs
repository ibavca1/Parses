using Chia.WebParsing.Companies.KeyRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.KeyRu
{
    public abstract class KeyRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return KeyRuCompany.Instance; }
        }
    }
}