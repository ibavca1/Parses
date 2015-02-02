using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers
{
    public class HtmlPageContent : StringWebPageContent
    {
        private readonly HtmlDocument _document;

        static HtmlPageContent()
        {
            // по-умолчанию HtmlAgilityPack не читает текст из тега option, исправляем это дело
            HtmlNode.ElementsFlags.Remove("option");
        }

        protected HtmlPageContent()
        {
        }

        public HtmlPageContent(string html)
        {
            Contract.Requires<ArgumentNullException>(html != null, "html");

            _document = new HtmlDocument();
            _document.LoadHtml(html);
            Encoding = Encoding.UTF8;
        }

        internal HtmlPageContent(HtmlDocument document)
        {
            Contract.Requires<ArgumentNullException>(document != null, "document");

            _document = document;
            Encoding = document.Encoding;
        }

        protected override string Content
        {
            get
            {
                return Document.DocumentNode.InnerHtml;
            }
        }

        internal virtual HtmlDocument Document
        {
            get
            {
                Contract.Ensures(Contract.Result<HtmlDocument>() != null);

                return _document;
            }
        }

        internal HtmlNode SelectSingleNode(string xpath)
        {
            return Document.DocumentNode.SelectSingleNode(xpath);
        }

        internal HtmlNode GetSingleNode(string xpath)
        {
            return Document.DocumentNode.GetSingleNode(xpath);
        }

        internal HtmlNodeCollection SelectNodes(string xpath)
        {
            return Document.DocumentNode.SelectNodes(xpath);
        }

        internal HtmlNodeCollection GetNodes(string xpath)
        {
            return Document.DocumentNode.GetNodes(xpath);
        }

        internal bool HasNode(string xpath)
        {
            return Document.DocumentNode.HasNode(xpath);
        }

        internal bool DoesNotHaveNode(string xpath)
        {
            return Document.DocumentNode.DoesNotHaveNode(xpath);
        }

        public static HtmlPageContent Create(WebPageContent content)
        {
            Contract.Requires<ArgumentNullException>(content != null, "content");
            Contract.Ensures(Contract.Result<HtmlPageContent>() != null);

            var htmlContent = content as HtmlPageContent;
            if (htmlContent != null)
                return htmlContent;

            Stream stream = content.ReadAsStream();
            var document = new HtmlDocument();
            document.Load(stream, content.Encoding);
            return new HtmlPageContent(document)
            {
                Cookies = content.Cookies,
            };
        }
    }
}