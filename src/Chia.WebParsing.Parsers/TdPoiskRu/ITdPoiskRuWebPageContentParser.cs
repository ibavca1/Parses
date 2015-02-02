using Chia.WebParsing.Companies.TdPoiskRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TdPoiskRu
{
    internal interface ITdPoiskRuWebPageContentParser : IWebPageContentParser
    {
        TdPoiskRuWebPageType PageType { get; }
    }
}