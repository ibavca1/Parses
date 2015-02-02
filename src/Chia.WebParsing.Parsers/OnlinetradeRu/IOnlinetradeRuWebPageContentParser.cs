using Chia.WebParsing.Companies.OnlinetradeRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OnlinetradeRu
{
    internal interface IOnlinetradeRuWebPageContentParser : IWebPageContentParser
    {
        OnlinetradeRuWebPageType PageType { get; }
    }
}