using Chia.WebParsing.Companies.CorpCentreRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    internal interface ICorpCentreRuWebPageContentParser : IWebPageContentParser
    {
        CorpCentreRuWebPageType PageType { get; }
    }
}