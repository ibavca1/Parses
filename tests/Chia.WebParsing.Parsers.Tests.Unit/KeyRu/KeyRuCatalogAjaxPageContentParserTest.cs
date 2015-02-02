using System;
using Chia.WebParsing.Companies.KeyRu;
using Chia.WebParsing.Parsers.KeyRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.Tests.Unit.KeyRu
{
    [TestClass]
    public class KeyRuCatalogAjaxPageContentParserTest : KeyRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\catalog_ajax.html", "KeyRu")]
        public void Test_Parse()
        {
            // arrange
            HtmlPageContent content = ReadHtmlContent(@"KeyRu\catalog_ajax.html");
            WebPage page = CreatePage(EmptyUri.Value, KeyRuCity.StPetersburg, KeyRuWebPageType.CatalogAjax);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuCatalogAjaxPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(KeyRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, KeyRuWebPageType.Product, "/shop/monobloki_i_kompyutery/kompyutery/komp_yuter_pk_kej_r914_optima_cdc_847/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\catalog_ajax_last_page.html", "KeyRu")]
        public void Test_Parse_Empty()
        {
            // arrange
            HtmlPageContent content = ReadHtmlContent(@"KeyRu\catalog_ajax_last_page.html");
            WebPage page = CreatePage(EmptyUri.Value, KeyRuCity.StPetersburg, KeyRuWebPageType.CatalogAjax);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuCatalogAjaxPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\catalog_ajax.html", "KeyRu")]
        public void Test_Parse_NextPage()
        {
            // arrange
            HtmlPageContent content = ReadHtmlContent(@"KeyRu\catalog_ajax.html");
            Uri uri = EmptyUri.Value
               .AddQueryParam("category_id", "26145")
               .AddQueryParam("p", "1");
            WebPage page = CreatePage(uri, KeyRuCity.StPetersburg, KeyRuWebPageType.CatalogAjax, "path");
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuCatalogAjaxPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstWithType(KeyRuWebPageType.CatalogAjax);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual("26145", nextPage.Uri.GetQueryParams()["category_id"]);
            Assert.AreEqual("2", nextPage.Uri.GetQueryParams()["p"]);
            Assert.AreEqual(page.Path, nextPage.Path);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\KeyRu\Pages\catalog_ajax_last_page.html", "KeyRu")]
        public void Test_Parse_NoNextPageIfEmptyContent()
        {
            // arrange
            HtmlPageContent content = ReadHtmlContent(@"KeyRu\catalog_ajax_last_page.html");
            WebPage page = CreatePage(EmptyUri.Value, KeyRuCity.StPetersburg, KeyRuWebPageType.CatalogAjax);
            var context = new WebPageContentParsingContext();
            var parser = new KeyRuCatalogAjaxPageContentParser();

            // act
            var result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }
    }
}