using Chia.WebParsing.Companies.NewmansRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NewmansRu
{
    public abstract class NewmansRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return NewmansRuCompany.Instance; }
        }
    }
}