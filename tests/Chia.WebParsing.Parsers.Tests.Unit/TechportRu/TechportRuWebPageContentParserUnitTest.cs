using Chia.WebParsing.Companies.TechportRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechportRu
{
    public abstract class TechportRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return TechportRuCompany.Instance; }
        }
    }
}