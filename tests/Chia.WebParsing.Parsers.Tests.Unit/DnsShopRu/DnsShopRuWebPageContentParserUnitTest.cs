using Chia.WebParsing.Companies.DnsShopRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DnsShopRu
{
    public abstract class DnsShopRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return DnsShopRuCompany.Instance; }
        }
    }
}