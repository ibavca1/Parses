using Chia.WebParsing.Companies.TelemaksRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TelemaksRu
{
    internal interface ITelemaksRuWebPageContentParser : IWebPageContentParser
    {
        TelemaksRuWebPageType PageType { get; }
    }
}