using Chia.WebParsing.Companies.NotikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NotikRu
{
    internal interface INotikRuWebPageContentParser : IWebPageContentParser
    {
       NotikRuWebPageType PageType { get; }
    }
}