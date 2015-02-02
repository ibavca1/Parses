using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace WebParsingFramework
{
    /// <summary>
    /// Фильтра веб-страниц.
    /// </summary>
    public class WebPagesFilter
    {
        private HashSet<WebSiteMapPath> _includePaths;
        private HashSet<WebSiteMapPath> _excludePaths;
        private HashSet<string> _excludeKeywords;

        /// <summary>
        /// Пути, которые следует включать, если пусто, то все.
        /// </summary>
        public ICollection<WebSiteMapPath> IncludePaths
        {
            get { return _includePaths ?? (_includePaths = new HashSet<WebSiteMapPath>()); }
            set { _includePaths = new HashSet<WebSiteMapPath>(value); }
        }

        /// <summary>
        /// Пути, которые следует исключать, если пусто, то .
        /// </summary>
        public ICollection<WebSiteMapPath> ExcludePaths
        {
            get { return _excludePaths ?? (_excludePaths = new HashSet<WebSiteMapPath>()); }
            set { _excludePaths = new HashSet<WebSiteMapPath>(value); }
        }

        public ICollection<string> ExcludeKeywords
        {
            get { return _excludeKeywords ?? (_excludeKeywords = new HashSet<string>()); }
            set { _excludeKeywords = new HashSet<string>(value); }
        }

        public bool IncludeAll
        {
            get { return IncludePaths.Count == 0; }
        }

        public bool Satisfy(WebSiteMapPath path)
        {
            Contract.Requires<ArgumentNullException>(path != null, "path");

            if (path.IsEmpty)
                return true;

            WebSiteMapPath temp = path;
            bool isSatisfyToAnyFromInclude =
                IncludeAll || IncludePaths.Any(temp.IsSatisfy);
            bool isSatisfyToAnyFromExclude =
                ExcludePaths.Any(temp.IsSatisfy) || ExcludeKeywords.Any(temp.ContainsElement);

            return isSatisfyToAnyFromInclude && !isSatisfyToAnyFromExclude;
        }

        public IEnumerable<WebSiteMapPath> Filter(IEnumerable<WebSiteMapPath> paths)
        {
            return paths.Where(Satisfy);
        }

        public void Add(WebPagesFilter other)
        {
            Contract.Requires<ArgumentNullException>(other != null, "other");

            foreach (string keyword in other.ExcludeKeywords)
                ExcludeKeywords.Add(keyword);

            foreach (WebSiteMapPath path in other.ExcludePaths)
                ExcludePaths.Add(path);

            foreach (WebSiteMapPath path in other.IncludePaths)
                IncludePaths.Add(path);
        }

        public static WebPagesFilter Empty
        {
            get { return new WebPagesFilter(); }
        }
    }
}