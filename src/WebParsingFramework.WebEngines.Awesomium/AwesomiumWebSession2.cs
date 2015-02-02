using System;
using System.Net;
using Awesomium.Core;

namespace WebParsingFramework.WebEngines.Awesomium
{
    internal class AwesomiumWebSession2 : IDisposable
    {
        private AwesomiumWebSession2()
        {
        }

        public WebSession Session { get; private set; }

        public WebView View { get; private set; }

        public static AwesomiumWebSession2 Create(WebPageRequest request, WebProxy proxy)
        {
            WebSession session = null;
            WebView view = null;
            try
            {
                WebCoreEx.Execute(
                    () =>
                        {
                            session = CreateSession(request, proxy);
                            view = WebCore.CreateWebView(1024, 768, session);
                        });
                return new AwesomiumWebSession2 { Session = session, View = view };
            }
            catch
            {
                if (view != null)
                    WebCoreEx.Execute(() => view.Dispose());
                if (session != null)
                    WebCoreEx.Execute(() => session.Dispose());
                throw;
            }
        }

        public void Dispose()
        {
            if (View != null)
            {
                WebCoreEx.Execute(() => View.Dispose());
                View = null;
            }
            if (Session != null)
            {
                WebCoreEx.Execute(() => Session.Dispose());
                Session = null;
            }
        }

        private static WebSession CreateSession(WebPageRequest request, WebProxy proxy)
        {
            var preferences =
                new WebPreferences
                    {
                        LoadImagesAutomatically = false,
                        CustomCSS = "::-webkit-scrollbar { visibility: hidden; }",
                        ProxyConfig = string.Format("{0}:{1}", proxy.Address.Host, proxy.Address.Port),
                    };

            WebSession session =  WebCore.CreateWebSession(preferences);

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(request.Cookies);
            string cookieHeader = cookieContainer.GetCookieHeader(request.Uri);
            session.SetCookie(request.Uri, cookieHeader, false, true);
            return session;
        }
    }
}