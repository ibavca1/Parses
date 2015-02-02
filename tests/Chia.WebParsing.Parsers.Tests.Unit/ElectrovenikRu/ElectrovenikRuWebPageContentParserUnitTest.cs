using Chia.WebParsing.Companies.ElectrovenikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.ElectrovenikRu
{
    public abstract class ElectrovenikRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return ElectrovenikRuCompany.Instance; }
        }
    }
}