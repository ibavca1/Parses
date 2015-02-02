using Chia.WebParsing.Companies.EurosetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EurosetRu
{
    public abstract class EurosetRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return EurosetRuCompany.Instance; }
        }
    }
}