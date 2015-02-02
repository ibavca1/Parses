using Chia.WebParsing.Companies.HolodilnikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.HolodilnikRu
{
    public abstract class HolodilnikRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return HolodilnikRuCompany.Instance; }
        }
    }
}