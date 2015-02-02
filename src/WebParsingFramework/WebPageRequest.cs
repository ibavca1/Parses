using System;
using System.Diagnostics.Contracts;
using System.Net;

namespace WebParsingFramework
{
    public class WebPageRequest
    {       
        private Uri _uri;
        private readonly WebPageType _type;
        private CookieCollection _cookies;
        private string _method;

        private WebPageRequest()
        {
            _method = WebRequestMethods.Http.Get;
            Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        }

        private WebPageRequest(WebPageType pageType, Uri uri)
            : this()
        {
            Contract.Requires<ArgumentNullException>(uri != null, "uri");

            _uri = uri;
            _type = pageType;
        }

        public WebPageType Type
        {
            get { return _type; }
        }

        public Uri Uri
        {
            get
            {
                Contract.Ensures(Contract.Result<Uri>() != null);
                return _uri;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Uri");
                _uri = value;
            }
        }

        public CookieCollection Cookies
        {
            get
            {
                Contract.Ensures(Contract.Result<CookieCollection>() != null);
                return _cookies ?? (_cookies = new CookieCollection());
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Cookies");
                _cookies = value;
            }
        }

        public string Method
        {
            get { return _method; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Method");
                Contract.Requires<ArgumentException>(value.Length != 0, "Method");
                _method = value;
            }
        }

        public WebPageContent Content { get; set; }

        public string ContentType { get; set; }

        public string Accept { get; set; }

        public string Referer { get; set; }

        public object Data { get; set; }

        public static WebPageRequest Create<TPageType>(TPageType pageType, Uri uri)
        {
            var type = (WebPageType)Convert.ToInt32(pageType);
            return new WebPageRequest(type, uri);
        }

        public static WebPageRequest Create<TPageType>(TPageType pageType)
        {
            return Create(pageType, EmptyUri.Value);
        }

        public static WebPageRequest Create(WebPage page)
        {
            return Create(page.Type, page.Uri);
        }        
    }
}