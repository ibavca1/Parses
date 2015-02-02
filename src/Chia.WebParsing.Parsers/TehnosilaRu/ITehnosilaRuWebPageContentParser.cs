using Chia.WebParsing.Companies.TehnosilaRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnosilaRu
{
    internal interface ITehnosilaRuWebPageContentParser : IWebPageContentParser
    {
        TehnosilaRuWebPageType PageType { get; }
    }
}