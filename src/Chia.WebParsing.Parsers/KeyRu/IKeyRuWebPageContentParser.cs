using Chia.WebParsing.Companies.KeyRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.KeyRu
{
    internal interface IKeyRuWebPageContentParser : IWebPageContentParser
    {
        KeyRuWebPageType Type { get; }
    }
}