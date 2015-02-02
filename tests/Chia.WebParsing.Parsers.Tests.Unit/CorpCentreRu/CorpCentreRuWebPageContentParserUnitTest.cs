using Chia.WebParsing.Companies.CorpCentreRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CorpCentreRu
{
    public abstract class CorpCentreRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return CorpCentreRuCompany.Instance; }
        }
    }
}