using System;
using System.Text;
using Moq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit
{
    public class MockedWebSite : WebSite
    {
        private readonly WebSite _site;

        public MockedWebSite(WebSite site)
        {
            _site = site;
        }

        public override WebCompany Company
        {
            get { return _site.Company; }
        }

        public override WebCity City
        {
            get { return _site.City; }
        }

        public override Uri Uri
        {
            get { return _site.Uri; }
        }

        public override Encoding Encoding
        {
            get { return _site.Encoding; }
        }

        public override WebSiteMetadata Metadata
        {
            get { return _site.Metadata; }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            WebPage page = _site.GetPage(uri, type, path);
            return base.GetPage(page.Uri, page.Type, page.Path);
        }
    }

    public static class MockedWebSiteEx
    {
        public static MockedWebSite AsMocked(this WebSite site)
        {
            return new Mock<MockedWebSite>(site) { CallBase = true }.Object;
        }
    }
}