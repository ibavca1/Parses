using Chia.WebParsing.Companies.TehnoparkRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoparkRu
{
    internal interface ITehnoparkRuWebPageContentParser : IWebPageContentParser
    {
        TehnoparkRuWebPageType PageType { get; }
    }
}