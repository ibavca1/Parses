using Chia.WebParsing.Companies.EldoradoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EldoradoRu
{
    public abstract class EldoradoRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return EldoradoRuCompany.Instance; }
        }
    }
}