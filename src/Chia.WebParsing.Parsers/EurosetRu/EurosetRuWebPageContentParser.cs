using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.EurosetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EurosetRu
{
    public class EurosetRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<EurosetRuWebPageType, IEurosetRuWebPageContentParser> Parsers;
       
        static EurosetRuWebPageContentParser()
        {
            Parsers =
                new IEurosetRuWebPageContentParser[]
                    {
                        new EurosetRuMainPageContentParser(),
                        new EurosetRuRazdelPageContentParser(),
                        new EurosetRuCatalogPageContentParser(),
                        new EurosetRuProductPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return EurosetRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (EurosetRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}