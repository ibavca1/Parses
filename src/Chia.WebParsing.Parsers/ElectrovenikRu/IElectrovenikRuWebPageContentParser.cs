using Chia.WebParsing.Companies.ElectrovenikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.ElectrovenikRu
{
    internal interface IElectrovenikRuWebPageContentParser : IWebPageContentParser
    {
        ElectrovenikRuWebPageType PageType { get; }
    }
}