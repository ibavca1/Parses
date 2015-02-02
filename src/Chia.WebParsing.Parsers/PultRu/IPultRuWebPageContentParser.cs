using Chia.WebParsing.Companies.PultRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.PultRu
{
    internal interface IPultRuWebPageContentParser : IWebPageContentParser
    {
        PultRuWebPageType PageType { get; }
    }
}