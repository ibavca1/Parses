using Chia.WebParsing.Companies.IonRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.IonRu
{
    public abstract class IonRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return IonRuCompany.Instance; }
        }
    }
}