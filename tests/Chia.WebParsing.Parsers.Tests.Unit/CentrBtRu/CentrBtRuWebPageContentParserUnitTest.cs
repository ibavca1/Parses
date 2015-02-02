using System.Text;
using Chia.WebParsing.Companies.CentrBtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CentrBtRu
{
    public abstract class CentrBtRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return CentrBtRuCompany.Instance; }
        }
    }
}