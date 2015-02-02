using System.Text;
using Chia.WebParsing.Companies.RbtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.RbtRu
{
    public abstract class RbtRuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return RbtRuCompany.Instance; }
        }
    }
}