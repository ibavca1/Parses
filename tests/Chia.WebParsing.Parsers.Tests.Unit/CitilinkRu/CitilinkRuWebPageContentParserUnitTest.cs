using Chia.WebParsing.Companies.CitilinkRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CitilinkRu
{
    public abstract class CitilinkRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return CitilinkRuCompany.Instance; }
        }
    }
}