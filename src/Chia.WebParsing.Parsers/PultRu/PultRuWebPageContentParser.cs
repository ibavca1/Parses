using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.PultRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.PultRu
{
    public class PultRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<PultRuWebPageType, IPultRuWebPageContentParser> Parsers;

        static PultRuWebPageContentParser()
        {
            Parsers = new IPultRuWebPageContentParser[]
            {
                new PultRuMainPageContentParser(),
                new PultRuRazdelPageContentParser(),
                new PultRuCatalogPageContentParser(),
                new PultRuProductPageContentParser(),
            }
                .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return PultRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (PultRuWebPageType)page.Type;
            IPultRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}