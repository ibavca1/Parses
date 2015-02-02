using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.NewmansRu
{
    public class NewmansRuWebSite : WebSite
    {
        private readonly NewmansRuCity _city;
        private const string UriMask = NewmansRuConstants.UriMask;

        public NewmansRuWebSite(NewmansRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return NewmansRuCompany.Instance; }
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
            get { return Encoding.GetEncoding(1251); }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = NewmansRuConstants.Articles,
                    PriceTypes = NewmansRuConstants.PriceTypes,
                    ProxyTimeout = NewmansRuConstants.ProxyTimeout,
                    AvailabilityInShops = NewmansRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = NewmansRuConstants.ExcludeKeywords,
                        ExcludePaths = NewmansRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var pageSizeCookie = new Cookie("COUNT_IN_PAGE", "100", "/", ".newmans.ru");
            request.Cookies.Add(pageSizeCookie);
            return base.LoadPageContent(request, context);
        }
    }
}