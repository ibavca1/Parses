using Chia.WebParsing.Companies.TehnoshokRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoshokRu
{
    public abstract class TehnoshokRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return TehnoshokRuCompany.Instance; }
        }
    }
}