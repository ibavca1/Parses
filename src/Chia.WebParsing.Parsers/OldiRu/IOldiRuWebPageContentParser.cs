using Chia.WebParsing.Companies.OldiRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OldiRu
{
    internal interface IOldiRuWebPageContentParser : IWebPageContentParser
    {
       OldiRuWebPageType PageType { get; }
    }
}