using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.KeyRu
{
    public class KeyRuWebSite : WebSite
    {
        private readonly KeyRuCity _city;

        public KeyRuWebSite(KeyRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return KeyRuCompany.Instance; }
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
            get { return Encoding.UTF8; }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                           {
                               ProductActicle = KeyRuConstants.ProductsActicle,
                               PriceTypes = KeyRuConstants.PriceTypes,
                               ProxyTimeout = KeyRuConstants.ProxyTimeout,
                               AvailabilityInShops = KeyRuConstants.AvailabilityInShops,
                               PagesFilter = new WebPagesFilter
                                                 {
                                                     ExcludeKeywords = KeyRuConstants.ExcludeKeywords,
                                                     ExcludePaths = KeyRuConstants.ExcludePaths
                                                 }
                           };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if (KeyRuWebPageType.CatalogAjax == (KeyRuWebPageType) type)
            {
                NameValueCollection query = uri.GetQueryParams();
                uri =
                    MakeUri("catalog/ajaxLoadingGoods_v2/")
                        .AddQueryParam("is_ajax", "1")
                        .AddQueryParam("params", "")
                        .AddOrReplaceQueryParams(query);
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookies = new CookieCollection
            {
                new Cookie("city_id", _city.CityId, "/", ".key.ru"),
                new Cookie("mode_view", "table", "/", ".key.ru")
            };
            request.Cookies.Add(cookies);

            if (KeyRuWebPageType.CatalogAjax == (KeyRuWebPageType)request.Type)
            {
                request.Method = "POST";
            } 

            return base.LoadPageContent(request, context);
        }
    }
}