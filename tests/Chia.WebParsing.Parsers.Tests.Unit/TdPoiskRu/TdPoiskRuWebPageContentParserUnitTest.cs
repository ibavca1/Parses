using Chia.WebParsing.Companies.TdPoiskRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TdPoiskRu
{
    public abstract class TdPoiskRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return TdPoiskRuCompany.Instance; }
        }
    }
}