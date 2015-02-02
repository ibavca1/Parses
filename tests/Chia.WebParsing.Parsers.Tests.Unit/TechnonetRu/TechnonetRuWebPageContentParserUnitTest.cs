using Chia.WebParsing.Companies.TechnonetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechnonetRu
{
    public abstract class TechnonetRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return TechnonetRuCompany.Instance; }
        }
    }
}