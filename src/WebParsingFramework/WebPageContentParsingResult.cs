using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace WebParsingFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class WebPageContentParsingResult
    {
        private IList<WebPage> _pages;
        private IList<WebMonitoringPosition> _positions;
        private IList<WebShop> _shops; 

        public static readonly WebPageContentParsingResult Empty = new WebPageContentParsingResult();

        public WebPageContentParsingResult()
        {
            _pages = new List<WebPage>();
            _positions = new List<WebMonitoringPosition>();
            _shops = new List<WebShop>();
        }

        public IList<WebPage> Pages
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<WebPage>>() != null);
                return _pages;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Pages");
                _pages = value.ToList();
            }
        }

        public IList<WebMonitoringPosition> Positions
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<WebMonitoringPosition>>() != null);
                return _positions;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Positions");
                _positions = value.ToList();
            }
        }

        public IList<WebShop> Shops
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<WebShop>>() != null);
                return _shops;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Shops");
                _shops = value.ToList();
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _shops.Count == 0 &&
                       _positions.Count == 0 &&
                       _pages.Count == 0;
            }
        }

        public void Add(WebPageContentParsingResult result)
        {
            Contract.Requires<ArgumentNullException>(result != null, "result");

            foreach (WebMonitoringPosition position in result.Positions)
                Positions.Add(position);

            foreach (WebPage page in result.Pages)
                Pages.Add(page);

            foreach (WebShop shop in Shops)
                Shops.Add(shop);
        }

        public static WebPageContentParsingResult FromPage(WebPage page)
        {
            Contract.Requires<ArgumentNullException>(page != null, "page");

            return new WebPageContentParsingResult
                       {
                           Pages = new[] {page}
                       };
        }

        public static WebPageContentParsingResult FromPosition(WebMonitoringPosition position)
        {
            Contract.Requires<ArgumentNullException>(position != null, "position");

            return new WebPageContentParsingResult
                       {
                           Positions = new []{position}
                       };
        }

        public static WebPageContentParsingResult FromShop(WebShop shop)
        {
            Contract.Requires<ArgumentNullException>(shop != null, "shop");

            return new WebPageContentParsingResult
            {
                Shops = new[] {shop}
            };
        }
    }
}