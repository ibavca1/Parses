using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.CentrBtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CentrBtRu
{
    public class CentrBtRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<CentrBtRuWebPageType, ICentrBtRuWebPageContentParser> Parsers;

        static CentrBtRuWebPageContentParser()
        {
            Parsers = new ICentrBtRuWebPageContentParser[]
            {
                new CentrBtRuMainPageContentParser(),
                new CentrBtRuCatalogPageContentParser(),
                new CentrBtRuRazdelPageContentParser(),
                new CentrBtRuProductPageContentParser(),
            }
                .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return CentrBtRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (CentrBtRuWebPageType)page.Type;
            ICentrBtRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}