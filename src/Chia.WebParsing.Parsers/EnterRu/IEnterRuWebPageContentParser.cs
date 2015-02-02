using Chia.WebParsing.Companies.EnterRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EnterRu
{
    internal interface IEnterRuWebPageContentParser : IWebPageContentParser
    {
        EnterRuWebPageType PageType { get; }
    }
}