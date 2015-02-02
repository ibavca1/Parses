using Chia.WebParsing.Companies.Oo3Ru;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Oo3Ru
{
    internal interface IOo3RuWebPageContentParser : IWebPageContentParser
    {
       Oo3RuWebPageType PageType { get; }
    }
}