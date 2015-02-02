using Chia.WebParsing.Companies.MvideoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    internal interface IMvideoRuWebPageContentParser : IWebPageContentParser
    {
        MvideoRuWebPageType Type { get; }
    }
}