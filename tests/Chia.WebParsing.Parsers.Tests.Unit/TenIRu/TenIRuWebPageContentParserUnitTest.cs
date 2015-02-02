using System.Text;
using Chia.WebParsing.Companies.TenIRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TenIRu
{
    public abstract class TenIRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return TenIRuCompany.Instance; }
        }
    }
}