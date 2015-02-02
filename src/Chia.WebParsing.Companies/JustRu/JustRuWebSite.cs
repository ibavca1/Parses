using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.JustRu
{
    public class JustRuWebSite : WebSite
    {
        private readonly JustRuCity _city;

        public JustRuWebSite(JustRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return JustRuCompany.Instance; }
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
                    ProductActicle = JustRuConstants.SupportArticles,
                    PriceTypes = JustRuConstants.SupportedPriceTypes,
                    ProxyTimeout = JustRuConstants.ProxyTimeout,
                    AvailabilityInShops = JustRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = JustRuConstants.ExcludeKeywords,
                        ExcludePaths = JustRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((JustRuWebPageType)type == JustRuWebPageType.Catalog)
            {
                uri = uri.AddQueryParam("expensive", "0");
            }
            else if ((JustRuWebPageType)type == JustRuWebPageType.SetOptions)
            {
                uri = MakeUri("/catalog/setoptions.php?action=setCountPerPage&per_page=128");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("geo", _city.CookieValue, "/", "www.just.ru");
            request.Cookies.Add(cookie);

            if (JustRuWebPageType.Product == (JustRuWebPageType)request.Type)
            {
                CookieCollection optionsCookies = GetOptionsCookies(context);
                request.Cookies.Add(optionsCookies);
            }

            return base.LoadPageContent(request, context);
        }

        private CookieCollection GetOptionsCookies(WebPageContentParsingContext context)
        {
            WebPage setOptionsPage = GetPage(EmptyUri.Value, JustRuWebPageType.SetOptions);
            WebPageRequest setOptionsRequest = WebPageRequest.Create(setOptionsPage);
            WebPageContent content = LoadPageContent(setOptionsRequest, context);
            return content.Cookies;
        }
    }
}