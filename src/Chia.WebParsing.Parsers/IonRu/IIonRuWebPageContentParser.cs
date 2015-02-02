using Chia.WebParsing.Companies.IonRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.IonRu
{
    internal interface IIonRuWebPageContentParser : IWebPageContentParser
    {
       IonRuWebPageType PageType { get; }
    }
}