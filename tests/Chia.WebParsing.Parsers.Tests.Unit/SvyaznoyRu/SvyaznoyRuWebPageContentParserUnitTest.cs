using Chia.WebParsing.Companies.SvyaznoyRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.SvyaznoyRu
{
    public abstract class SvyaznoyRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return SvyaznoyRuCompany.Instance; }
        }
    }
}