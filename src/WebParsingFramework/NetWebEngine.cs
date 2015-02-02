using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebParsingFramework
{
    public class NetWebEngine : IWebEngine
    {
        public virtual WebPageContent LoadPageContent(WebPageRequest request, Encoding encoding, WebProxy proxy, TimeSpan timeout)
        {
            WebRequest webRequest = CreateRequest(request, proxy, timeout);
            WebResponse webResponse = webRequest.GetResponse();
            Encoding contentEncoding = encoding ?? ExtractEncoding(webResponse);
            return CreateContent(webResponse, contentEncoding);
        }

        public virtual Task<WebPageContent> LoadPageContentAsync(WebPageRequest request, Encoding encoding, WebProxy proxy, TimeSpan timeout, CancellationToken cancellationToken)
        {
            WebRequest webRequest = CreateRequest(request, proxy, timeout);

            return Task.Factory
                .FromAsync(webRequest.BeginGetResponse, ar => webRequest.EndGetResponse(ar), null)
                .ContinueWith(
                    task =>
                        {
                            WebResponse webResponse = task.Result;
                            Encoding contentEncoding = encoding ?? ExtractEncoding(webResponse);
                            return CreateContent(webResponse, contentEncoding);
                        }, cancellationToken);
        }

        public virtual bool SupportsJavaScript
        {
            get { return false; }
        }

        private static WebRequest CreateRequest(WebPageRequest request, WebProxy proxy, TimeSpan timeout)
        {
            var webRequest = (HttpWebRequest) WebRequest.CreateDefault(request.Uri);
            webRequest.Proxy = proxy;
            webRequest.Timeout = (int) timeout.TotalMilliseconds;
            webRequest.Method = request.Method;
            webRequest.CookieContainer = new CookieContainer();
            webRequest.CookieContainer.Add(request.Cookies);
            webRequest.Accept = request.Accept;
            webRequest.ContentType = request.ContentType;
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";

            if (webRequest.Method == WebRequestMethods.Http.Post)
            {
                webRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            }

            if (request.Content != null)
            {
                string content = request.Content.ReadAsString();
                byte[] buffer = Encoding.UTF8.GetBytes(content);
                webRequest.ContentLength = buffer.Length;
                webRequest.GetRequestStream().Write(buffer, 0, buffer.Length);
            }

            return webRequest;
        }

        protected static Encoding ExtractEncoding(WebResponse response)
        {
            Contract.Requires<ArgumentNullException>(response != null, "response");

            var httpResponse = response as HttpWebResponse;

            if (httpResponse != null && httpResponse.CharacterSet != null)
                return Encoding.GetEncoding(httpResponse.CharacterSet);

            return Encoding.UTF8;
        }

        private static WebPageContent CreateContent(WebResponse response, Encoding encoding)
        {
            Contract.Requires<ArgumentNullException>(response != null, "response");
            Contract.Requires<ArgumentNullException>(encoding != null, "encoding");
            Contract.Ensures(Contract.Result<WebPageContent>() != null);

            string text = GetResponseText(response, encoding);
            var content = new StringWebPageContent(text){Encoding = encoding};
            var httpResponse = response as HttpWebResponse;
            if (httpResponse != null)
            {
                content.Cookies = httpResponse.Cookies;
            }

            return content;
        }

        private static string GetResponseText(WebResponse response, Encoding encoding)
        {
            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream == null)
                    return null;

                using (var reader = new StreamReader(responseStream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}