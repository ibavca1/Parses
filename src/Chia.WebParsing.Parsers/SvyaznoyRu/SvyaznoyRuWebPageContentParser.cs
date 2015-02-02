using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.SvyaznoyRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.SvyaznoyRu
{
    public class SvyaznoyRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<SvyaznoyRuWebPageType, ISvyaznoyRuWebPageContentParser> Parsers;
       
        static SvyaznoyRuWebPageContentParser()
        {
            Parsers =
                new ISvyaznoyRuWebPageContentParser[]
                    {
                        new SvyaznoyRuMainPageContentParser(),
                        new SvyaznoyRuRazdelPageContentParser(),
                        new SvyaznoyRuCatalogPageContentParser(),
                        new SvyaznoyRuProductPageContentParser(),
                        //new SvyaznoyRuShopsListPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return SvyaznoyRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (SvyaznoyRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}