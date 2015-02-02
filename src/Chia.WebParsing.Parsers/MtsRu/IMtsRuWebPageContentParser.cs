using Chia.WebParsing.Companies.MtsRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MtsRu
{
    internal interface IMtsRuWebPageContentParser : IWebPageContentParser
    {
       MtsRuWebPageType PageType { get; }
    }
}