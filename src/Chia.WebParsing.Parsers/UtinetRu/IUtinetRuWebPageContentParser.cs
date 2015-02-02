using Chia.WebParsing.Companies.UtinetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UtinetRu
{
    internal interface IUtinetRuWebPageContentParser : IWebPageContentParser
    {
       UtinetRuWebPageType PageType { get; }
    }
}