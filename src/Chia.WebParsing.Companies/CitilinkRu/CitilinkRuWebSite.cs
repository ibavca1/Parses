using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.CitilinkRu
{
    public class CitilinkRuWebSite : WebSite
    {
        private readonly CitilinkRuCity _city;

        public CitilinkRuWebSite(CitilinkRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return CitilinkRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get { return Company.SiteUri; }
        }

        public override Encoding Encoding
        {
            get { return Encoding.GetEncoding(1251); }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = CitilinkRuConstants.SupportArticles,
                    PriceTypes = CitilinkRuConstants.SupportedPriceTypes,
                    ProxyTimeout = CitilinkRuConstants.ProxyTimeout,
                    AvailabilityInShops = CitilinkRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = CitilinkRuConstants.ExcludeKeywords,
                        ExcludePaths = CitilinkRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((CitilinkRuWebPageType)type == CitilinkRuWebPageType.Catalog)
            {
                uri = uri.AddQueryParam("showOrder", "6");
            }
            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("citilink_space", _city.CookieValue, "/", "citilink.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}