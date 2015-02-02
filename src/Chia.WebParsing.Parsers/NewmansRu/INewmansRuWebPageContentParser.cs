using Chia.WebParsing.Companies.NewmansRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NewmansRu
{
    internal interface INewmansRuWebPageContentParser : IWebPageContentParser
    {
       NewmansRuWebPageType PageType { get; }
    }
}