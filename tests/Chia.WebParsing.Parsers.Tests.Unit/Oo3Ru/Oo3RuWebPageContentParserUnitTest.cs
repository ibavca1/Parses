using System.Text;
using Chia.WebParsing.Companies.Oo3Ru;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Oo3Ru
{
    public abstract class Oo3RuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return Oo3RuCompany.Instance; }
        }
    }
}