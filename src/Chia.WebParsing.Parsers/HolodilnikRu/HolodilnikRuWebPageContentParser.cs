using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.HolodilnikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.HolodilnikRu
{
    public class HolodilnikRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<HolodilnikRuWebPageType, IHolodilnikRuWebPageContentParser> Parsers;

        static HolodilnikRuWebPageContentParser()
        {
            Parsers = new IHolodilnikRuWebPageContentParser[]
            {
                new HolodilnikRuMainPageContentParser(),
                new HolodilnikRuCatalogPageContentParser(),
                new HolodilnikRuRazdelPageContentParser(),
                new HolodilnikRuProductPageContentParser(),
            }
                .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return HolodilnikRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (HolodilnikRuWebPageType)page.Type;
            IHolodilnikRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}