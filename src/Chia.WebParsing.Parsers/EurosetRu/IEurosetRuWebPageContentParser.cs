using Chia.WebParsing.Companies.EurosetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EurosetRu
{
    internal interface IEurosetRuWebPageContentParser : IWebPageContentParser
    {
        EurosetRuWebPageType PageType { get; }
    }
}