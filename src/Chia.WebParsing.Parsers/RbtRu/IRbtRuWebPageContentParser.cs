using Chia.WebParsing.Companies.RbtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RbtRu
{
    internal interface IRbtRuWebPageContentParser : IWebPageContentParser
    {
       RbtRuWebPageType PageType { get; }
    }
}