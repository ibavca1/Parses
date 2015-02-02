using Chia.WebParsing.Companies.CitilinkRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CitilinkRu
{
    internal interface ICitilinkRuWebPageContentParser : IWebPageContentParser
    {
        CitilinkRuWebPageType PageType { get; }
    }
}