using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.OzonRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OzonRu
{
    public class OzonRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<OzonRuWebPageType, IOzonRuWebPageContentParser> Parsers;

        static OzonRuWebPageContentParser()
        {
            Parsers = new IOzonRuWebPageContentParser[]
            {
                new OzonRuMainPageContentParser(),
                new OzonRuRazdelPageContentParser(),
                new OzonRuCatalogPageContentParser(),
                new OzonRuProductPageContentParser(),
            }
                .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return OzonRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (OzonRuWebPageType)page.Type;
            IOzonRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}