using Chia.WebParsing.Companies.VLazerCom;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.VLazerCom
{
    internal interface IVLazerComWebPageContentParser : IWebPageContentParser
    {
       VLazerComWebPageType PageType { get; }
    }
}