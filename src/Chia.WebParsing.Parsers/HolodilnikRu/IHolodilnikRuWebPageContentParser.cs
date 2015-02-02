using Chia.WebParsing.Companies.HolodilnikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.HolodilnikRu
{
    internal interface IHolodilnikRuWebPageContentParser : IWebPageContentParser
    {
        HolodilnikRuWebPageType PageType { get; }
    }
}