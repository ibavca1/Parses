using Chia.WebParsing.Companies.SvyaznoyRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.SvyaznoyRu
{
    internal interface ISvyaznoyRuWebPageContentParser : IWebPageContentParser
    {
        SvyaznoyRuWebPageType PageType { get; }
    }
}