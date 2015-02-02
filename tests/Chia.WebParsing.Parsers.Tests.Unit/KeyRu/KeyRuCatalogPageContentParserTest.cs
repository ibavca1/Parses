using Chia.WebParsing.Companies.KeyRu;
using Chia.WebParsing.Parsers.KeyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.Tests.Unit.KeyRu
{
    [TestClass]
    public class KeyRuCatalogPageContentParserTest : KeyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\catalog.mht", "KeyRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\catalog.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuCatalogPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(KeyRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, KeyRuWebPageType.Product, "/shop/monobloki_i_kompyutery/kompyutery/komp_yuter_key_gm_geek_g-745-16g2000_128w/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\catalog.mht", "KeyRu")]
        public void Test_Parse_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\catalog.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new KeyRuCatalogPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(KeyRuWebPageType.CatalogAjax);
            Assert.AreEqual("26145", nextPage.Uri.GetQueryParams()["category_id"]);
            Assert.AreEqual("1", nextPage.Uri.GetQueryParams()["p"]);
            Assert.AreEqual(page.Path, nextPage.Path);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\catalog_empty.mht", "KeyRu")]
        public void Test_Parse_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"KeyRu\catalog_empty.mht");
            WebPage page = CreatePage(content, KeyRuCity.StPetersburg, KeyRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuCatalogPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }
    }
}