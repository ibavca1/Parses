using Chia.WebParsing.Companies.LogoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.LogoRu
{
    internal interface ILogoRuWebPageContentParser : IWebPageContentParser
    {
       LogoRuWebPageType PageType { get; }
    }
}