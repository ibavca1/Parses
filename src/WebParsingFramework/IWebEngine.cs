using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebParsingFramework
{
    [ContractClass(typeof(WebEngineContract))]
    public interface IWebEngine
    {
        WebPageContent LoadPageContent(WebPageRequest request, Encoding encoding, WebProxy proxy, TimeSpan timeout);

        Task<WebPageContent> LoadPageContentAsync(WebPageRequest request, Encoding encoding, WebProxy proxy, TimeSpan timeout, CancellationToken cancellationToken);

        bool SupportsJavaScript { get; }
    }

    [ContractClassFor(typeof(IWebEngine))]
    abstract class WebEngineContract : IWebEngine
    {
        WebPageContent IWebEngine.LoadPageContent(WebPageRequest request, Encoding encoding, WebProxy proxy, TimeSpan timeout)
        {
            Contract.Requires<ArgumentNullException>(request != null, "request");
            Contract.Ensures(Contract.Result<WebPageContent>() != null);

            return default(WebPageContent);
        }

        Task<WebPageContent> IWebEngine.LoadPageContentAsync(WebPageRequest request, Encoding encoding, WebProxy proxy, TimeSpan timeout, CancellationToken cancellationToken)
        {
            Contract.Requires<ArgumentNullException>(request != null, "request");
            Contract.Ensures(Contract.Result<WebPageContent>() != null);

            return default(Task<WebPageContent>);
        }

        bool IWebEngine.SupportsJavaScript
        {
            get { return default(bool); }
        }
    }
}