using Chia.WebParsing.Companies.DostavkaRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DostavkaRu
{
    public abstract class DostavkaRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return DostavkaRuCompany.Instance; }
        }
    }
}