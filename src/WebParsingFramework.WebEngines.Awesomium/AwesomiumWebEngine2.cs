using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Awesomium.Core;

namespace WebParsingFramework.WebEngines.Awesomium
{
    public class AwesomiumWebEngine2 : NetWebEngine, IDisposable
    {
        private static readonly object Lock = new object();
        private static bool _webCoreStarted;
        private static Thread _thread;
        private static SynchronizationContext _context;

        public void Dispose()
        {
            WebCore.Shutdown();
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, Encoding encoding, WebProxy proxy, TimeSpan timeout)
        {
            if (request.Method != "GET")
                return base.LoadPageContent(request, encoding, proxy, timeout);

            StartAwesomiumThreadIfNeed();

            using (var session = AwesomiumWebSession2.Create(request, proxy))
            {
                var evnt = new WebEvent();
                WebCoreEx.Execute(() => Do(request, session, proxy, timeout, evnt));

                if (!evnt.WaitOne(timeout))
                    throw new WebException("The request was aborted: The operation has timed out.",
                                           WebExceptionStatus.Timeout);

                return evnt.Content;
            }
        }

        public override bool SupportsJavaScript
        {
            get { return true; }
        }

        private void Do(WebPageRequest request, AwesomiumWebSession2 session, WebProxy proxy, TimeSpan timeout, WebEvent evnt)
        {
            session.View.LoadingFrameComplete +=
                (s, e) =>
                    {
                        if (!e.IsMainFrame)
                            return;

                        var v = (WebView) s;
                        string html = v.ExecuteJavascriptWithResult("document.documentElement.outerHTML");
                        string cookies = v.ExecuteJavascriptWithResult("document.cookie;");
                        string encoding = v.ExecuteJavascriptWithResult("document.characterSet;");
                        var content = new StringWebPageContent(html)
                                          {
                                              Encoding = Encoding.GetEncoding(encoding),
                                              Cookies =
                                                  ParseCookies(cookies, request.Uri.Host)
                                          };
                        evnt.Complete(content);
                    };
            session.View.LoadingFrameFailed += (s, e) =>
                                                   {

                                                   };

            session.View.Source = request.Uri;
        }

        private static void StartAwesomiumThreadIfNeed()
        {
            if (_thread == null)
            {
                lock(Lock)
                {
                    if (_thread == null)
                    {
                        if (!WebCore.IsInitialized)
                            WebCore.Initialize(WebConfig.Default);
                        WebCore.Started += (s, e) =>
                                               {
                                                   _webCoreStarted = true;
                                                   _context = SynchronizationContext.Current;
                                               };
 
                        _thread = new Thread(AwesomiumThread);
                        _thread.Start();
                    }
                }
            }

            while (!_webCoreStarted)
                Thread.Sleep(50);
        }

        private static void AwesomiumThread()
        {
            WebCore.Run();
        }

        private static CookieCollection ParseCookies(string input, string host)
        {
            var cookies = new CookieCollection();
            string[] tokens = input.Split(new[]{";"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string token in tokens)
            {
                string[] parts = token.Trim().Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);
                string name = parts[0].Trim();
                string value = parts.Length > 1 ? HttpUtility.UrlDecode(parts[1]).Trim() : string.Empty;

                if (cookies[name] == null)
                {
                    var cookie = new Cookie(name, value, "/", host);
                    cookies.Add(cookie);
                }
            }

            return cookies;
        }

        private class WebEvent : EventWaitHandle
        {
            public WebEvent()
                : base(false, EventResetMode.ManualReset)
            {
            }

            public WebPageContent Content { get; private set; }

            public void Complete(WebPageContent content)
            {
                Content = content;
                Set();
            }
        }
    }
}
