using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.KeyRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.KeyRu
{
    public class KeyRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<KeyRuWebPageType, IKeyRuWebPageContentParser> Parsers;

        static KeyRuWebPageContentParser()
        {
            Parsers = new IKeyRuWebPageContentParser[]
            {
                new KeyRuMainPageContentParser(),
                new KeyRuRazdelPageContentParser(),
                new KeyRuCatalogPageContentParser(),
                new KeyRuCatalogAjaxPageContentParser(), 
                new KeyRuProductPageContentParser(),
                new KeyRuShopsListPageContentParser()
            }
                .ToDictionary(x => x.Type, x => x);
        }

        public override WebCompany Company
        {
            get { return KeyRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (KeyRuWebPageType) page.Type;
            IKeyRuWebPageContentParser parser = Parsers[type];
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            WebPage[] invalidPages =
                result.Pages
                    .Where(p => p.Type != WebPageType.Main && Equals(page.Site.Uri, p.Uri))
                    .ToArray();
            Array.ForEach(invalidPages, p => result.Pages.Remove(p));
            return result;
        }
    }
}