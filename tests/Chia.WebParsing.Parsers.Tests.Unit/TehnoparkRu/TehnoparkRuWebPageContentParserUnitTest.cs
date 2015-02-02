using Chia.WebParsing.Companies.TehnoparkRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoparkRu
{
    public abstract class TehnoparkRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return TehnoparkRuCompany.Instance; }
        }
    }
}