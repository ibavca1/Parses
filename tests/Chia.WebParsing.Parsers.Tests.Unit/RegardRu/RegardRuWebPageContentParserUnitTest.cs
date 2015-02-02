using System.Text;
using Chia.WebParsing.Companies.RegardRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.RegardRu
{
    public abstract class RegardRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return RegardRuCompany.Instance; }
        }
    }
}