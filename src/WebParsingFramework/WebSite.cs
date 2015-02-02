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
    /// ���-����.
    /// </summary>
    public abstract class WebSite
    {
        /// <summary>
        /// ��������-�������� - �������� �����.
        /// </summary>
        public abstract WebCompany Company { get; }

        /// <summary>
        /// �����, ������� ������������ ���� ����.
        /// </summary>
        public abstract WebCity City { get; }

        /// <summary>
        /// ����� �����.
        /// </summary>
        public abstract Uri Uri { get; }

        /// <summary>
        /// ��������� �����.
        /// </summary>
        public abstract Encoding Encoding { get; }

        /// <summary>
        /// ���������� �����.
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
        /// ������� ����� ��������� <see cref="Uri"/> � ������ �������� �����.
        /// </summary>
        /// <param name="relative">����� ������ �����.</param>
        /// <returns>����� ��������� <see cref="Uri"/>.</returns>
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
        /// �������� ��������� �������� �����.
        /// </summary>
        /// <param name="relative">����� ��������.</param>
        /// <param name="type">��� ��������.</param>
        /// <param name="path">���� ��������.</param>
        /// <returns>��������� ��������.</returns>
        public WebPage GetPage<TPageType>(string relative, TPageType type, WebSiteMapPath path = null)
        {
            Uri uri = MakeUri(relative);
            return GetPage(uri, type, path);
        }

        /// <summary>
        /// �������� ��������� �������� �����.
        /// </summary>
        /// <param name="uri">����� ��������.</param>
        /// <param name="type">��� ��������.</param>
        /// <param name="path">���� ��������.</param>
        /// <returns>��������� ��������.</returns>
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