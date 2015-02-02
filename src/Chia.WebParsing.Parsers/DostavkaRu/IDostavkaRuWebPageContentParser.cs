using Chia.WebParsing.Companies.DostavkaRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DostavkaRu
{
    internal interface IDostavkaRuWebPageContentParser : IWebPageContentParser
    {
        DostavkaRuWebPageType PageType { get; }
    }
}