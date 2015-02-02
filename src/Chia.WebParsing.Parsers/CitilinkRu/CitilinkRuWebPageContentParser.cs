using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.CitilinkRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CitilinkRu
{
    public class CitilinkRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<CitilinkRuWebPageType, ICitilinkRuWebPageContentParser> Parsers;

        static CitilinkRuWebPageContentParser()
        {
            Parsers = new ICitilinkRuWebPageContentParser[]
            {
                new CitilinkRuMainPageContentParser(),
                new CitilinkRuCatalogPageContentParser(),
                new CitilinkRuRazdelPageContentParser(),
                new CitilinkRuProductPageContentParser(),
            }
                .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return CitilinkRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (CitilinkRuWebPageType)page.Type;
            ICitilinkRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}