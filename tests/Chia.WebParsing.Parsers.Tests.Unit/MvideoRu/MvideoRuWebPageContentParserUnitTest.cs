using Chia.WebParsing.Companies.MvideoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MvideoRu
{
    public abstract class MvideoRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return MvideoRuCompany.Instance; }
        }
    }
}