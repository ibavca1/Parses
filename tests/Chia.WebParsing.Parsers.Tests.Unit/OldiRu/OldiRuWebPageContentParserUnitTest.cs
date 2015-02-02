using System.Text;
using Chia.WebParsing.Companies.OldiRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OldiRu
{
    public abstract class OldiRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return OldiRuCompany.Instance; }
        }
    }
}