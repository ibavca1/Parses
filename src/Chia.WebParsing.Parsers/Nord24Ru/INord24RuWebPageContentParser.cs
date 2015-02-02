using Chia.WebParsing.Companies.Nord24Ru;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Nord24Ru
{
    internal interface INord24RuWebPageContentParser : IWebPageContentParser
    {
        Nord24RuWebPageType PageType { get; }
    }
}