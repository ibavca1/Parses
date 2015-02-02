using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.DomoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DomoRu
{
    public class DomoRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<DomoRuWebPageType, IDomoRuWebPageContentParser> Parsers;

        static DomoRuWebPageContentParser()
        {
            Parsers = new IDomoRuWebPageContentParser[]
            {
                new DomoRuMainPageContentParser(),
                new DomoRuRazdelPageContentParser(),
                new DomoRuCatalogPageContentParser(),
                new DomoRuCatalogAjaxPageContentParser(),
                new DomoRuProductPageContentParser(),
            }
                .ToDictionary(x => x.Type, x => x);
        }

        public override WebCompany Company
        {
            get { return DomoRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (DomoRuWebPageType)page.Type;
            IDomoRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}