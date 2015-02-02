using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.TehnosilaRu
{
    public class TehnosilaRuWebSite : WebSite
    {
        private readonly TehnosilaRuCity _city;
        private const string UriMask = TehnosilaRuConstants.UriMask;

        public TehnosilaRuWebSite(TehnosilaRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return TehnosilaRuCompany.Instance; }
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
                    ProductActicle = TehnosilaRuConstants.SupportProductsArticle,
                    PriceTypes = TehnosilaRuConstants.SupportedPriceTypes,
                    ProxyTimeout = TehnosilaRuConstants.ProxyTimeout,
                    AvailabilityInShops = TehnosilaRuConstants.SupportAvailabilityInShops,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = TehnosilaRuConstants.ExcludeKeywords,
                        ExcludePaths = TehnosilaRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((TehnosilaRuWebPageType)type == TehnosilaRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("c", "45")
                    .AddQueryParam("o", "price_asc");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            if ((TehnosilaRuWebPageType)request.Type == TehnosilaRuWebPageType.ProductAvailabilityInShops)
            {
                Contract.Assert(request.Data != null);

                var itemId = (string) request.Data;
                request.Uri =
                    MakeUri("/item/getShopListContent")
                        .AddQueryParam("type", "courier")
                        .AddQueryParam("itemId", itemId);
                request.Method = WebRequestMethods.Http.Post;
            }

            return base.LoadPageContent(request, context);
        }
    }
}