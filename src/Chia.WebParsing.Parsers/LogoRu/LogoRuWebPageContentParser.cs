using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.LogoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.LogoRu
{
    public class LogoRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<LogoRuWebPageType, ILogoRuWebPageContentParser> Parsers;

        static LogoRuWebPageContentParser()
        {
            Parsers =
                new ILogoRuWebPageContentParser[]
                    {
                        new LogoRuMainPageContentParser(),
                        new LogoRuRazdelPageContentParser(),
                        new LogoRuCatalogPageContentParser(),
                        new LogoRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return LogoRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (LogoRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}