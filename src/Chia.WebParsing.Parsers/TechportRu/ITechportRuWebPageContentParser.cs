using Chia.WebParsing.Companies.TechportRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechportRu
{
    internal interface ITechportRuWebPageContentParser : IWebPageContentParser
    {
       TechportRuWebPageType PageType { get; }
    }
}