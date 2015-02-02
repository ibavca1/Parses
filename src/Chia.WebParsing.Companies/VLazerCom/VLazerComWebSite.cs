using System;
using System.Net;
using System.Text;
using System.Web;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.VLazerCom
{
    public class VLazerComWebSite : WebSite
    {
        private readonly VLazerComCity _city;
        private const string UriMask = VLazerComConstants.SiteUriMask;

        public VLazerComWebSite(VLazerComCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return VLazerComCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                if (string.IsNullOrEmpty(_city.UriPrefix))
                    return Company.SiteUri;

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
                    ProductActicle = VLazerComConstants.SupportArticles,
                    PriceTypes = VLazerComConstants.SupportedPriceTypes,
                    ProxyTimeout = VLazerComConstants.ProxyTimeout,
                    AvailabilityInShops = VLazerComConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = VLazerComConstants.ExcludeKeywords,
                        ExcludePaths = VLazerComConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            if ((VLazerComWebPageType)request.Type == VLazerComWebPageType.PriceOffset)
            {
                string offset = request.Content.ReadAsString();
                string encodedOffset = HttpUtility.UrlEncode(offset);
                string content = string.Format("offset={0}", encodedOffset);

                request.Uri = MakeUri("/catalog/~/offset/");
                request.Method = WebRequestMethods.Http.Post;
                request.Content = new StringWebPageContent(content);
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Accept = "application/json; charset=UTF-8";
            }

            return base.LoadPageContent(request, context);
        }
    }
}