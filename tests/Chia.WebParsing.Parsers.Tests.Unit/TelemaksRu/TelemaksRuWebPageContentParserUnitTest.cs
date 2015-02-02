using Chia.WebParsing.Companies.TelemaksRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TelemaksRu
{
    public abstract class TelemaksRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return TelemaksRuCompany.Instance; }
        }
    }
}