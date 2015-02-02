using Chia.WebParsing.Companies.TehnosilaRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnosilaRu
{
    public abstract class TehnosilaRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return TehnosilaRuCompany.Instance; }
        }
    }
}