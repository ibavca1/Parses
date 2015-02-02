using Chia.WebParsing.Companies.DnsShopRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    internal interface IDnsShopRuWebPageContentParser : IWebPageContentParser
    {
        DnsShopRuWebPageType Type { get; }
    }
}