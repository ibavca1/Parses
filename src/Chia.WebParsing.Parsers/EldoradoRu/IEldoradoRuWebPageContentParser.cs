using Chia.WebParsing.Companies.EldoradoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EldoradoRu
{
    internal interface IEldoradoRuWebPageContentParser : IWebPageContentParser
    {
        EldoradoRuWebPageType Type { get; }
    }
}