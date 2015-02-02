using Chia.WebParsing.Companies.JustRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.JustRu
{
    internal interface IJustRuWebPageContentParser : IWebPageContentParser
    {
       JustRuWebPageType PageType { get; }
    }
}