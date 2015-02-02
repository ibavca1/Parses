using Chia.WebParsing.Companies.UlmartRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    internal interface IUlmartRuWebPageContentParser : IWebPageContentParser
    {
        UlmartRuWebPageType PageType { get; }
    }
}