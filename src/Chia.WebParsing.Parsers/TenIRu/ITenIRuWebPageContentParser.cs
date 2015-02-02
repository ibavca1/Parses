using Chia.WebParsing.Companies.TenIRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TenIRu
{
    internal interface ITenIRuWebPageContentParser : IWebPageContentParser
    {
       TenIRuWebPageType PageType { get; }
    }
}