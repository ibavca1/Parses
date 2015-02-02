using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.EldoradoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EldoradoRu
{
    public class EldoradoRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<EldoradoRuWebPageType, IEldoradoRuWebPageContentParser> Parsers;

        static EldoradoRuWebPageContentParser()
        {
            Parsers = new IEldoradoRuWebPageContentParser[]
            {
                new EldoradoRuMainPageContentParser(),
                new EldoradoRuRazdelPageContentParser(),
                new EldoradoRuCatalogPageContentParser(),
                new EldoradoRuProductPageContentParser(),
                new EldoradoRuShopsListPageContentParser()
            }
                .ToDictionary(x => x.Type, x => x);
        }

        public override WebCompany Company
        {
            get { return EldoradoRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (EldoradoRuWebPageType)page.Type;
            IEldoradoRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}