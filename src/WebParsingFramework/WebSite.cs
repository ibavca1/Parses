using System;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebParsingFramework.Utils;

namespace WebParsingFramework
{
    /// <summary>
    /// Веб-сайт.
    /// </summary>
    public abstract class WebSite
    {
        /// <summary>
        /// Интернет-компания - владелец сайта.
        /// </summary>
        public abstract WebCompany Company { get; }

        /// <summary>
        /// Город, который представляет этот сайт.
        /// </summary>
        public abstract WebCity City { get; }

        /// <summary>
        /// Адрес сайта.
        /// </summary>
        public abstract Uri Uri { get; }

        /// <summary>
        /// Кодировка сайта.
        /// </summary>
        public abstract Encoding Encoding { get; }

        /// <summary>
        /// Метаданные сайта.
        /// </summary>
        public abstract WebSiteMetadata Metadata { get; }

        public virtual WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            Contract.Requires<ArgumentNullException>(request != null, "request");
            Contract.Requires<ArgumentNullException>(context != null, "context");
            Contract.Ensures(Contract.Result<WebPageContent>() != null);

            return context.LoadPageContent(request, Encoding);
        }

        public virtual Task<WebPageContent> LoadPageContentAsync(WebPageRequest request, WebPageContentParsingContext context, CancellationToken cancellationToken)
        {
            Contract.Requires<ArgumentNullException>(request != null, "request");
            Contract.Requires<ArgumentNullException>(context != null, "context");
            Contract.Ensures(Contract.Result<WebPageContent>() != null);

            return Task.Factory.StartNew(() => LoadPageContent(request, context), cancellationToken);
        }

        /// <summary>
        /// Создает новый экземпляр <see cref="Uri"/> в рамках текущего сайта.
        /// </summary>
        /// <param name="relative">Адрес внутри сайта.</param>
        /// <returns>Новый экземпляр <see cref="Uri"/>.</returns>
        public virtual Uri MakeUri(string relative)
        {
            Contract.Requires<ArgumentNullException>(relative != null, "relative");
            Contract.Requires<ArgumentException>(!relative.Equals("#"), "relative");

            Uri siteUri = Uri;
            NameValueCollection siteUriQuery = HttpUtility.ParseQueryString(siteUri.Query);

            var uri = relative.IsAbsoluteUrl() ? new Uri(relative) : new Uri(siteUri, relative);
            uri = uri.AddOrReplaceQueryParams(siteUriQuery);

            return uri;
        }

        /// <summary>
        /// Получить экземпляр страницы сайта.
        /// </summary>
        /// <param name="relative">Адрес страницы.</param>
        /// <param name="type">Тип страницы.</param>
        /// <param name="path">Путь страницы.</param>
        /// <returns>Экземпляр страницы.</returns>
        public WebPage GetPage<TPageType>(string relative, TPageType type, WebSiteMapPath path = null)
        {
            Uri uri = MakeUri(relative);
            return GetPage(uri, type, path);
        }

        /// <summary>
        /// Получить экземпляр страницы сайта.
        /// </summary>
        /// <param name="uri">Адрес страницы.</param>
        /// <param name="type">Тип страницы.</param>
        /// <param name="path">Путь страницы.</param>
        /// <returns>Экземпляр страницы.</returns>
        public WebPage GetPage<TPageType>(Uri uri, TPageType type, WebSiteMapPath path = null)
        {
            var pageType = (WebPageType) Convert.ToInt32(type);
            return GetPage(uri, pageType, path);
        }

        protected virtual WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if (type == WebPageType.Main)
            {
                uri = Uri;
            }

            return new WebPage(this, uri, type, path);
        }
    }
}