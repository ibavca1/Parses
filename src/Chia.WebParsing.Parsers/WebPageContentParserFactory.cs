using System;
using System.Collections.Generic;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers
{
    public class WebPageContentParserFactory : IWebPageContentParserFactory
    {
        private readonly Dictionary<WebCompany, WebCompanyPageContentParser> _parsers;

        public WebPageContentParserFactory()
            : this(Parsers)
        {
        }

        public WebPageContentParserFactory(IEnumerable<WebCompanyPageContentParser> parsers)
        {
            _parsers = parsers.ToDictionary(p => p.Company, p => p);
        }

        public IWebPageContentParser Create(WebPage page, WebPageContentParsingContext context)
        {
            WebCompany company = page.Site.Company;
            WebCompanyPageContentParser parser;
            if (!_parsers.TryGetValue(company, out parser))
                throw new Exception();

            return parser;
        }

        public IEnumerable<IWebPageContentParser> All
        {
            get { return _parsers.Values; }
        }

        private static IEnumerable<WebCompanyPageContentParser> Parsers
        {
            get
            {
                Type baseType = typeof (WebCompanyPageContentParser);
                Type[] parserTypes =
                    typeof(WebPageContentParserFactory).Assembly
                        .GetTypes()
                        .Where(t => t.IsPublic && t.IsClass && !t.IsAbstract)
                        .Where(baseType.IsAssignableFrom)
                        .ToArray();
                WebCompanyPageContentParser[] parsers =
                    parserTypes
                     .Select(Activator.CreateInstance)
                     .Cast<WebCompanyPageContentParser>()
                     .ToArray();
                return parsers;
            }
        }
    }
}