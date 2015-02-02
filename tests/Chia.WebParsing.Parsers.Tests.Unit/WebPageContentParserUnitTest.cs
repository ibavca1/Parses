using System;
using System.IO;
using System.Linq;
using System.Text;
using EnterpriseLibrary.UnitTesting;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit
{
    public abstract class WebPageContentParserUnitTest : UnitTest
    {
        protected abstract WebCompany Company { get; }

        protected virtual Encoding Encoding
        {
            get
            {
                WebCity city = Company.Metadata.Cities.First();
                WebSite site = Company.GetSite(city);
                return site.Encoding;
            }
        }

        protected MhtmlPageContent ReadMhtmlContent(string path)
        {
            return ReadMhtmlContent(path, Encoding);
        }

        protected MhtmlPageContent ReadMhtmlContent(string path, Encoding encoding)
        {
            string mhtml = File.ReadAllText(path, encoding);
            return new MhtmlPageContent(mhtml, encoding);
        }

        protected HtmlPageContent ReadHtmlContent(string path)
        {
            return ReadHtmlContent(path, Encoding);
        }

        protected HtmlPageContent ReadHtmlContent(string path, Encoding encoding)
        {
            string html = File.ReadAllText(path, encoding);
            return new HtmlPageContent(html);
        }

        protected WebPage CreatePage<TPageType>(MhtmlPageContent content, WebCity city, TPageType pageType, params string[] pagePath)
        {
            WebSite site = CreateSite(city);
            var path = new WebSiteMapPath(pagePath);
            return site.GetPage(content.Location, pageType, path);
        }

        protected WebPage CreatePage<TPageType>(MhtmlPageContent content, WebSite site, TPageType pageType, params string[] pagePath)
        {
            var path = new WebSiteMapPath(pagePath);
            return site.GetPage(content.Location, pageType, path);
        }

        protected WebPage CreatePage<TPageType>(Uri uri, WebCity city, TPageType pageType, params string[] pagePath)
        {
            WebSite site = CreateSite(city);
            var path = new WebSiteMapPath(pagePath);
            uri = uri ?? site.Uri;
            return site.GetPage(uri, pageType, path);
        }

        protected WebPage CreatePage<TPageType>(string uri, WebCity city, TPageType pageType, params string[] pagePath)
        {
            WebSite site = CreateSite(city);
            var path = new WebSiteMapPath(pagePath);
            return site.GetPage(uri, pageType, path);
        }

        protected WebPage CreatePage<TPageType>(WebPage page, TPageType pageType, Uri uri, WebSiteMapPath path)
            where TPageType : struct
        {
            return page.Site.GetPage(uri, pageType, path);
        }

        protected WebPage CreatePage<TPageType>(WebPage page, TPageType pageType, Uri uri, params string[] pagePath)
            where TPageType : struct 
        {
            var path = page.Path.Add(pagePath);
            WebPage result = page.Site.GetPage(uri, pageType, path);
            result.IsPartOfSiteMap = pagePath.Length != 0;
            return result;
        }

        protected WebPage CreatePage<TPageType>(WebPage page, TPageType pageType, string address, WebSiteMapPath path)
        {
            Uri uri = page.GetUri(address);
            return page.Site.GetPage(uri, pageType, path);
        }

        protected WebPage CreatePage<TPageType>(WebPage page, TPageType pageType, string address, params string[] pagePath)
        {
            Uri uri = page.GetUri(address);
            WebSiteMapPath path =
                pagePath.Length != 0
                    ? page.Path.Add(pagePath)
                    : WebSiteMapPath.Empty;
            WebPage result = page.Site.GetPage(uri, pageType, path);
            result.IsPartOfSiteMap = pagePath.Length != 0;
            return result;
        }

        protected Uri CreateUri(WebPage page, string uri)
        {
            return page.GetUri(uri);
        }

        private WebSite CreateSite(WebCity city)
        {
            var site = Company.GetSite(city);
            return new Mock<MockedWebSite>(site) {CallBase = true}.Object;
        }
    }
}