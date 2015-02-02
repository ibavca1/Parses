using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.Oo3Ru;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Oo3Ru
{
    public class Oo3RuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<Oo3RuWebPageType, IOo3RuWebPageContentParser> Parsers;

        static Oo3RuWebPageContentParser()
        {
            Parsers =
                new IOo3RuWebPageContentParser[]
                    {
                        new Oo3RuMainPageContentParser(),
                        new Oo3RuCatalogPageContentParser(),
                        new Oo3RuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return Oo3RuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (Oo3RuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}