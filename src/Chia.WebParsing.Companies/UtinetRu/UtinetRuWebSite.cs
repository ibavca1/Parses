using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.UtinetRu
{
    public class UtinetRuWebSite : WebSite
    {
        private readonly UtinetRuCity _city;

        public UtinetRuWebSite(UtinetRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return UtinetRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                if (UtinetRuCity.Moscow.Equals(_city))
                    return Company.SiteUri;
                
                string address = string.Format(UtinetRuConstants.UriMask, _city.UriPrefix);
                return new Uri(address);
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
                    ProductActicle = UtinetRuConstants.SupportArticles,
                    PriceTypes = UtinetRuConstants.SupportedPriceTypes,
                    ProxyTimeout = UtinetRuConstants.ProxyTimeout,
                    AvailabilityInShops = UtinetRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = UtinetRuConstants.ExcludeKeywords,
                        ExcludePaths = UtinetRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if (UtinetRuWebPageType.Catalog == (UtinetRuWebPageType)type)
            {
                List<string> segments = uri.Segments.ToList();
                if (segments[segments.Count - 2] != "search/")
                {
                    segments.Insert(segments.Count - 1, "search/");
                }

                string uriPath = string.Join("", segments);

                NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
                query["view_case"] = "vertical";
                query["availability"] = "true";

                uri = new UriBuilder(uri) { Path = uriPath, Query = query.ToString() }.Uri;
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            if (UtinetRuCity.Moscow.Equals(_city))
            {
                Uri locationUri = MakeUri("/U2Site/Geo/setRegionManual.json?location_id=116489");
                WebPageRequest locationRequest = WebPageRequest.Create(UtinetRuWebPageType.Location, locationUri);
                WebPageContent locationResponse = base.LoadPageContent(locationRequest, context);
                request.Cookies.Add(locationResponse.Cookies);
            }

            return base.LoadPageContent(request, context);
        }
    }
}