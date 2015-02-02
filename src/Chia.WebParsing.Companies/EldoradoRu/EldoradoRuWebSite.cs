using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.EldoradoRu
{
    public class EldoradoRuWebSite : WebSite
    {
        private readonly EldoradoRuCity _city;

        public EldoradoRuWebSite(EldoradoRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return EldoradoRuCompany.Instance; }
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
                    ProductActicle = EldoradoRuConstants.ProductArticle,
                    PriceTypes = EldoradoRuConstants.PriceTypes,
                    ProxyTimeout = EldoradoRuConstants.ProxyTimeout,
                    AvailabilityInShops = EldoradoRuConstants.AvailabilityInShops,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = EldoradoRuConstants.ExcludeKeywords,
                        ExcludePaths = EldoradoRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("iRegionSectionId", _city.CookieValue, "/", "eldorado.ru");
            request.Cookies.Add(cookie);

            if ((EldoradoRuWebPageType)request.Type == EldoradoRuWebPageType.AvailabilityInShops)
            {
                Contract.Assert(request.Data != null);

                var article = (string) request.Data;
                request.Uri =
                    MakeUri("/cat/SLSubPage.php")
                        .AddQueryParam("city_id", _city.CookieValue)
                        .AddQueryParam("mode", "ajax")
                        .AddQueryParam("content", "listShopsContPickup")
                        .AddQueryParam("XID", article);
                request.Accept = "text/html, */*; q=0.01";
            }
            else if ((EldoradoRuWebPageType)request.Type == EldoradoRuWebPageType.Prices)
            {
                Contract.Assert(request.Data != null);

                var bids = (string[]) request.Data;
                var query = new HttpValueCollection();
                foreach (var bid in bids)
                    query.Add("ids[]", bid);

                Uri uri =
                       MakeUri("/_ajax/update_info_catalog.php")
                           .AddQueryParam("action", "list")
                           .AddQueryParams(query);
                request = WebPageRequest.Create(request.Type, uri);
            }

            return base.LoadPageContent(request, context);
        }

        protected override WebPage GetPage(Uri uri, WebPageType t, WebSiteMapPath path)
        {
            if (t == WebPageType.Main)
            {
                uri = uri.AddQueryParam("list_num", "50");
            }
            else
            {
                uri = uri.AddQueryParam("sort", "price").
                    AddQueryParam("type", "asc").
                    AddQueryParam("list_num", "50");
            }
            //uri = uri.AddQueryParam("sort=price&type=desc&list_num", "50");
            return base.GetPage(uri, t, path);
        }
    }
}