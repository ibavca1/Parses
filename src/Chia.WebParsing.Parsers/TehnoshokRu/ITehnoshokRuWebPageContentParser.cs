using Chia.WebParsing.Companies.TehnoshokRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoshokRu
{
    internal interface ITehnoshokRuWebPageContentParser : IWebPageContentParser
    {
        TehnoshokRuWebPageType PageType { get; }
    }
}