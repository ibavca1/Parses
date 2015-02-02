using Chia.WebParsing.Companies.CentrBtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CentrBtRu
{
    internal interface ICentrBtRuWebPageContentParser : IWebPageContentParser
    {
        CentrBtRuWebPageType PageType { get; }
    }
}