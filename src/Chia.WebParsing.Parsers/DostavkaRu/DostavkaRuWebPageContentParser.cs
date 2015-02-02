using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.DostavkaRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DostavkaRu
{
    public class DostavkaRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<DostavkaRuWebPageType, IDostavkaRuWebPageContentParser> Parsers;

        static DostavkaRuWebPageContentParser()
        {
            Parsers = new IDostavkaRuWebPageContentParser[]
            {
                new DostavkaRuMainPageContentParser(),
                new DostavkaRuCatalogPageContentParser(),
                new DostavkaRuRazdelPageContentParser(),
                new DostavkaRuProductPageContentParser(),
            }
                .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return DostavkaRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (DostavkaRuWebPageType)page.Type;
            IDostavkaRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}