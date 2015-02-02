using System.Text;
using Chia.WebParsing.Companies.VLazerCom;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.VLazerCom
{
    public abstract class VLazerComWebPageContentParserUnitTest : WebPageContentParserUnitTest
    {
        protected override WebCompany Company
        {
            get { return VLazerComCompany.Instance; }
        }
    }
}