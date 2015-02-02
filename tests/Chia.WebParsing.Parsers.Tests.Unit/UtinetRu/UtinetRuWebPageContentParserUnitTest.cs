using System.Text;
using Chia.WebParsing.Companies.UtinetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UtinetRu
{
    public abstract class UtinetRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return UtinetRuCompany.Instance; }
        }
    }
}