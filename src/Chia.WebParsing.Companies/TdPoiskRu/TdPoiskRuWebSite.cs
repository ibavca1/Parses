using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.TdPoiskRu
{
    public class TdPoiskRuWebSite : WebSite
    {
        private readonly TdPoiskRuCity _city;
        private const string UriMask = TdPoiskRuConstants.UriMask;

        public TdPoiskRuWebSite(TdPoiskRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return TdPoiskRuCompany.Instance; }
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
                    ProductActicle = TdPoiskRuConstants.Articles,
                    PriceTypes = TdPoiskRuConstants.PriceTypes,
                    ProxyTimeout = TdPoiskRuConstants.ProxyTimeout,
                    AvailabilityInShops = TdPoiskRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = TdPoiskRuConstants.ExcludeKeywords,
                        ExcludePaths = TdPoiskRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((TdPoiskRuWebPageType)type == TdPoiskRuWebPageType.Catalog)
            {
                uri = uri.AddQueryParam("NEWS_COUNT_SELECT", "60");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            if ((TdPoiskRuWebPageType)request.Type == TdPoiskRuWebPageType.AvailabilityInShops)
            {
                Contract.Assert(request.Data != null);

                var pid = (string)request.Data;
                request.Method = WebRequestMethods.Http.Post;
                request.Uri = MakeUri("ajax/product_warehouses_tab.php").AddQueryParam("pid", pid);
            }

            return base.LoadPageContent(request, context);
        }
    }
}