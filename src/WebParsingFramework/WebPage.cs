using System;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Net;
using System.Web;
using WebParsingFramework.Utils;

namespace WebParsingFramework
{
    /// <summary>
    /// Веб-страница.
    /// </summary>
    public class WebPage
    {
        private readonly WebSite _site;
        //private readonly Uri _uri;
        private Uri _uri;
        private readonly WebPageType _type;
        private readonly WebSiteMapPath _path;
        private CookieCollection _cookies;

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebPage"/>.
        /// </summary>
        /// <param name="site">Веб-сайт.</param>
        /// <param name="uri">Адрес страницы.</param>
        /// <param name="type">Тип страницы.</param>
        /// <param name="path">Путь в карте веб-сайта.</param>
        public WebPage(WebSite site, Uri uri, WebPageType type, WebSiteMapPath path = null)
            : this(uri, type, path)
        {
            Contract.Requires<ArgumentNullException>(site != null, "site");

            _site = site;
        }

        internal WebPage(Uri uri, WebPageType type, WebSiteMapPath path = null)
        {
            Contract.Requires<ArgumentNullException>(uri != null, "uri");

            _uri = uri;
            _type = type;
            _path = path ?? WebSiteMapPath.Empty;
        }

        /// <summary>
        /// Веб-сайт.
        /// </summary>
        [Pure]
        public virtual WebSite Site
        {
            get
            {
                Contract.Ensures(Contract.Result<WebSite>() != null);
                return _site;
            }
        }

        /// <summary>
        /// Адрес.
        /// </summary>
        [Pure]
        public virtual Uri Uri
        {
            get
            {
                Contract.Ensures(Contract.Result<Uri>() != null);
                return _uri;
            }
            set
            {
                _uri = value;
            }
        }

        /// <summary>
        /// Тип страницы.
        /// </summary>
        [Pure]
        public virtual WebPageType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Путь в карте веб-сайта.
        /// </summary>
        [Pure]
        public virtual WebSiteMapPath Path
        {
            get
            {
                Contract.Ensures(Contract.Result<WebSiteMapPath>() != null);
                return _path;
            }
        }

        [Pure]
        public bool IsPartOfSiteMap { get; set; }

        [Pure]
        public bool IsPartOfShopsInformation { get; set; }

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
            
        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="Object"/> текущему объекту <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если заданный объект <see cref="Object"/> равен текущему объекту <see cref="Object"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="other">Элемент <see cref="Object"/>, который требуется сравнить с текущим элементом <see cref="Object"/>. </param>
        [Pure]
        public bool Equals(WebPage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Uri, Uri) && Equals(other.Type, Type);
        }

        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="Object"/> текущему объекту <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если заданный объект <see cref="Object"/> равен текущему объекту <see cref="Object"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="obj">Элемент <see cref="Object"/>, который требуется сравнить с текущим элементом <see cref="Object"/>. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as WebPage);
        }

        /// <summary>
        /// Играет роль хэш-функции для определенного типа. 
        /// </summary>
        /// <returns>
        /// Хэш-код для текущего объекта <see cref="Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Uri.GetHashCode()*397) ^ Type.GetHashCode();
            }
        }

        /// <summary>
        /// Возвращает объект <see cref="String"/>, который представляет текущий объект <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// Объект <see cref="String"/>, представляющий текущий объект <see cref="Object"/>.
        /// </returns>
        public override string ToString()
        {
            return Uri.ToString();
        }

        [Pure]
        public Uri GetUri(string href)
        {
            NameValueCollection siteQuery = HttpUtility.ParseQueryString(Site.Uri.Query);

            if (href.IsAbsoluteUrl())
            {
                return new Uri(href).AddOrReplaceQueryParams(siteQuery);
            }

            if (href.StartsWith("?"))
            {
                NameValueCollection hrefQuery = HttpUtility.ParseQueryString(href);
                hrefQuery.AddOrReplace(siteQuery);
                return Uri.AddOrReplaceQueryParams(hrefQuery);
            }

            return new Uri(Site.Uri, href).AddOrReplaceQueryParams(siteQuery);
        }
    }
}

