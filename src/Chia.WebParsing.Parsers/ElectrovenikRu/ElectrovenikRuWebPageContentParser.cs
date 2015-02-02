using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.ElectrovenikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.ElectrovenikRu
{
    public class ElectrovenikRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<ElectrovenikRuWebPageType, IElectrovenikRuWebPageContentParser> Parsers;

        static ElectrovenikRuWebPageContentParser()
        {
            Parsers = new IElectrovenikRuWebPageContentParser[]
            {
                new ElectrovenikRuMainPageContentParser(),
                new ElectrovenikRuCatalogPageContentParser(),
                new ElectrovenikRuRazdelPageContentParser(),
                new ElectrovenikRuProductPageContentParser(),
            }
                .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return ElectrovenikRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (ElectrovenikRuWebPageType)page.Type;
            IElectrovenikRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}