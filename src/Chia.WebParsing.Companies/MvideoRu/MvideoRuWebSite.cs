using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.MvideoRu
{
    public class MvideoRuWebSite : WebSite
    {
        private readonly MvideoRuCity _city;

        public MvideoRuWebSite(MvideoRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return MvideoRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                return Company.SiteUri
                    .AddQueryParam("cityId", _city.CookieValue);
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
                               ProductActicle = MvideoRuConstants.ProductArticle,
                               PriceTypes = MvideoRuConstants.PriceTypes,
                               ProxyTimeout = MvideoRuConstants.ProxyTimeout,
                               AvailabilityInShops = MvideoRuConstants.AvailabilityInShops,
                               PagesFilter = new WebPagesFilter
                                                 {
                                                     ExcludeKeywords = MvideoRuConstants.ExcludeKeywords,
                                                     ExcludePaths = MvideoRuConstants.ExcludePaths
                                                 }
                           };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((MvideoRuWebPageType)type == MvideoRuWebPageType.AvailabilityInShops)
            {
                uri = uri
                    .AddQueryParam("ssb_block", "availabilityContentBlockContainer")
                    .AddQueryParam("ajax", "true");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            if ((MvideoRuWebPageType)request.Type == MvideoRuWebPageType.AvailabilityInShops)
            {
                var cookie = new Cookie("MVID_CITY_ID", _city.CookieValue, "/", ".mvideo.ru");
                request.Cookies.Add(cookie);
            }

            return base.LoadPageContent(request, context);
        }
    }
}