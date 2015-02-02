using Chia.WebParsing.Companies.Nord24Ru;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Nord24Ru
{
    public abstract class Nord24RuWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return Nord24RuCompany.Instance; }
        }
    }
}