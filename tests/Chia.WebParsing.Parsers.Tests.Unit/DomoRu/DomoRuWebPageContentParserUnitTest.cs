using Chia.WebParsing.Companies.DomoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DomoRu
{
    public abstract class DomoRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return DomoRuCompany.Instance; }
        }
    }
}