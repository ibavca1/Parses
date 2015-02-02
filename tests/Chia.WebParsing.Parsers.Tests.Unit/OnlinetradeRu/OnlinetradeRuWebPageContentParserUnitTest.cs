using System.Text;
using Chia.WebParsing.Companies.OnlinetradeRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OnlinetradeRu
{
    public abstract class OnlinetradeRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return OnlinetradeRuCompany.Instance; }
        }
    }
}