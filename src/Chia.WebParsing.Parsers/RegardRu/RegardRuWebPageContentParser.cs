using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.RegardRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RegardRu
{
    public class RegardRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<RegardRuWebPageType, IRegardRuWebPageContentParser> Parsers;

        static RegardRuWebPageContentParser()
        {
            Parsers = new IRegardRuWebPageContentParser[]
            {
                new RegardRuMainPageContentParser(),
                new RegardRuCatalogPageContentParser(),
                new RegardRuProductPageContentParser(),
            }
                .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return RegardRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (RegardRuWebPageType)page.Type;
            IRegardRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}