using Chia.WebParsing.Companies.MtsRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MtsRu
{
    public abstract class MtsRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return MtsRuCompany.Instance; }
        }
    }
}