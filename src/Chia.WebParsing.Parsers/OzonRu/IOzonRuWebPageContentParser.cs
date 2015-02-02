using Chia.WebParsing.Companies.OzonRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OzonRu
{
    internal interface IOzonRuWebPageContentParser : IWebPageContentParser
    {
        OzonRuWebPageType PageType { get; }
    }
}