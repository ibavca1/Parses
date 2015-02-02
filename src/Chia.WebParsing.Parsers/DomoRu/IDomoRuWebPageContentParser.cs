using Chia.WebParsing.Companies.DomoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DomoRu
{
    internal interface IDomoRuWebPageContentParser : IWebPageContentParser
    {
        DomoRuWebPageType Type { get; }
    }
}