using Chia.WebParsing.Companies.RegardRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RegardRu
{
    internal interface IRegardRuWebPageContentParser : IWebPageContentParser
    {
       RegardRuWebPageType PageType { get; }
    }
}