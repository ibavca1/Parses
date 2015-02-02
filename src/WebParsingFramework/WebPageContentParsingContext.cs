using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebParsingFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class WebPageContentParsingContext
    {
        private WebSiteParsingOptions _options;
        private readonly WebProxy _proxy;
        private IWebEngine _webEngine;
        private TimeSpan _requestTimeout;
        private bool _isReadOnly;

        public WebPageContentParsingContext()
        {
            _webEngine = new NetWebEngine();
            _requestTimeout = TimeSpan.FromSeconds(120);
        }

        public WebPageContentParsingContext(WebProxy proxy)
            : this()
        {
            _proxy = proxy;
        }

        public WebSiteParsingOptions Options
        {
            get
            {
                Contract.Ensures(Contract.Result<WebSiteParsingOptions>() != null);
                return _options ?? (_options = new WebSiteParsingOptions());
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Options");
                _options = value;
            }
        }

        public WebProxy Proxy
        {
            get { return _proxy; }
        }

        public IWebEngine WebEngine
        {
            get
            {
                Contract.Ensures(Contract.Result<IWebEngine>() != null);
                return _webEngine;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "WebEngine");
                _webEngine = value;
            }
        }

        public TimeSpan RequestTimeout
        {
            get { return _requestTimeout; }
        }

        internal WebPageContent LoadPageContent(WebPageRequest request, Encoding encoding)
        {
            return _webEngine.LoadPageContent(request, encoding, Proxy, RequestTimeout);
        }

        internal Task<WebPageContent> LoadPageContentAsync(WebPageRequest request, Encoding encoding, CancellationToken cancellationToken)
        {
            return _webEngine.LoadPageContentAsync(request, encoding, Proxy, RequestTimeout, cancellationToken);
        }
    }
}