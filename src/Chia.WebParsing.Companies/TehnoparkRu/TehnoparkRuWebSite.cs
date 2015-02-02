using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.TehnoparkRu
{
    public class TehnoparkRuWebSite : WebSite
    {
        private readonly TehnoparkRuCity _city;
        private const string UriMask = TehnoparkRuConstants.UriMask;

        public TehnoparkRuWebSite(TehnoparkRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return TehnoparkRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                string address = string.Format(UriMask, _city.UriPrefix);
                var uri = new Uri(address);
                return uri.AddQueryParam("c", _city.IdInCookie.ToString());
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = TehnoparkRuConstants.SupportArticles,
                    PriceTypes = TehnoparkRuConstants.SupportedPriceTypes,
                    ProxyTimeout = TehnoparkRuConstants.ProxyTimeout,
                    AvailabilityInShops = TehnoparkRuConstants.SupportAvailabilityInShops,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = TehnoparkRuConstants.ExcludeKeywords,
                        ExcludePaths = TehnoparkRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((TehnoparkRuWebPageType)type == TehnoparkRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("count", "50")
                    .AddQueryParam("view-style", "view-list");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("TP_Location_Name", _city.IdInCookie.ToString(), "/", "www.technopark.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}