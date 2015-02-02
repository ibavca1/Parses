using Chia.WebParsing.Companies.TechnonetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechnonetRu
{
    internal interface ITechnonetRuWebPageContentParser : IWebPageContentParser
    {
        TechnonetRuWebPageType PageType { get; }
    }
}