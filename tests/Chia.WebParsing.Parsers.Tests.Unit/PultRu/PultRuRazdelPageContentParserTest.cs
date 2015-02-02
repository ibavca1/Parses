using System.Linq;
using Chia.WebParsing.Companies.PultRu;
using Chia.WebParsing.Parsers.PultRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.PultRu
{
    [TestClass]
    public class PultRuRazdelPageContentParserTest : PultRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\razdel.mht", "PultRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\razdel.mht");
            WebPage page = CreatePage(content, PultRuCity.StPetersburg, PultRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(PultRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, PultRuWebPageType.Catalog, "/product/akusticheskie-sistemy-akustika/komplekty-akustiki/", "Комплекты акустики"), pages.First());
            Assert.AreEqual(CreatePage(page, PultRuWebPageType.Catalog, "/product/akusticheskie-sistemy-akustika/aksessuary-dlya-as/", "Аксессуары для акустики"), pages.Last());
            
            // Динамики ведут на ту же самую страницу, поэтому их пропускаем
            //Assert.AreEqual(page.Path.Add("Динамики"), result.Pages.Last().Path);
            //Assert.AreSame(site.MakeUri("/product/akusticheskie-sistemy-akustika/"), result.Pages.Last().Uri);
        }
    }
}