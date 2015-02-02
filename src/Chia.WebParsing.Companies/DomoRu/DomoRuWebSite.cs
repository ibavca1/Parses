using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.DomoRu
{
    public class DomoRuWebSite : WebSite
    {
        private readonly DomoRuCity _city;
        private const string UriMask = DomoRuConstants.SiteUriMask;

        public DomoRuWebSite(DomoRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return DomoRuCompany.Instance; }
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
                return new Uri(address);
            }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                           {
                               ProductActicle = DomoRuConstants.ProductArticle,
                               PriceTypes = DomoRuConstants.PriceTypes,
                               ProxyTimeout = DomoRuConstants.ProxyTimeout,
                               AvailabilityInShops = DomoRuConstants.AvailabilityInShops,
                               PagesFilter = new WebPagesFilter
                                                 {
                                                     ExcludeKeywords = DomoRuConstants.ExcludeKeywords,
                                                     ExcludePaths = DomoRuConstants.ExcludePaths
                                                 }
                           };
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if (DomoRuWebPageType.MainMenuAjax == (DomoRuWebPageType)type)
            {
                uri =
                    MakeUri("/webapp/wcs/stores/servlet/DepartmentDropdownView")
                        .AddOrReplaceQueryParam("storeId", _city.StoreId.ToString())
                        .AddOrReplaceQueryParam("catalogId", "10001")
                        .AddOrReplaceQueryParam("langId", "-20")
                        .AddOrReplaceQueryParam("isFirstRefresh", "true");
            }
            if (DomoRuWebPageType.CatalogAjax == (DomoRuWebPageType)type)
            {
                NameValueCollection query = uri.GetQueryParams();
                uri =
                    MakeUri("/catalog/CategoryNavigationResultsView")
                        .AddOrReplaceQueryParam("advancedSearch","")
                        .AddOrReplaceQueryParam("catalogId", "10001")
                        .AddOrReplaceQueryParam("filterFacet", "")
                        .AddOrReplaceQueryParam("filterTerm", "")
                        .AddOrReplaceQueryParam("filterType", "")
                        .AddOrReplaceQueryParam("landId", "-20")
                        .AddOrReplaceQueryParam("manufacturer", "")
                        .AddOrReplaceQueryParam("metaData", "")
                        .AddOrReplaceQueryParam("pageSize", "12")
                        .AddOrReplaceQueryParam("resultCatEntryType", "")
                        .AddOrReplaceQueryParam("sType", "SimpleSearch")
                        .AddOrReplaceQueryParam("searchTermScope", "")
                        .AddOrReplaceQueryParam("searchType", "")
                        .AddOrReplaceQueryParam("storeId", _city.StoreId.ToString())
                        .AddOrReplaceQueryParams(query);
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            if (DomoRuWebPageType.CatalogAjax == (DomoRuWebPageType) request.Type)
            {
                int page = int.Parse(request.Uri.GetQueryParam("page"));
                int pageSize = int.Parse(request.Uri.GetQueryParam("pageSize"));
                int beginIndex = (page - 1)*pageSize;

                string data = string.Format(
                    "contentBeginIndex=0&productBeginIndex={0}&beginIndex={0}&orderBy=&isHistory=false&" +
                    "pageView=list&resultType=products&orderByContent=&searchTerm=&facet=&facetLimit=&" +
                    "minPrice=&maxPrice=&storeId={1}&catalogId=10001&langId=-20&objectId=&requesttype=ajax",
                    beginIndex, _city.StoreId);

                request.Content = new StringWebPageContent(data);
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Method = WebRequestMethods.Http.Post;
                request.Uri = request.Uri.RemoveQueryParam("page");
                
            }

            return base.LoadPageContent(request, context);
        }
    }
}