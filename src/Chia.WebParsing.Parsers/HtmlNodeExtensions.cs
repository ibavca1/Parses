using System.Diagnostics;
using HtmlAgilityPack;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers
{
    [DebuggerNonUserCode, DebuggerStepThrough]
    internal static class HtmlNodeExtensions
    {
        public static HtmlNode WithValidate(this HtmlNode node)
        {
            if (node == null)
                throw new InvalidWebPageMarkupException();
            return RemoveComments(node);
        }

        public static HtmlNodeCollection WithValidate(this HtmlNodeCollection collection)
        {
            if (collection == null)
                throw new InvalidWebPageMarkupException();
            return collection;
        }

        public static HtmlNode GetSingleNode(this HtmlNode node, string xpath)
        {
            HtmlNode result = node.SelectSingleNode(xpath);
            if (result == null)
                throw new InvalidWebPageMarkupException();
            return RemoveComments(result);
        }

        public static HtmlNodeCollection GetNodes(this HtmlNode node, string xpath)
        {
            HtmlNodeCollection result = node.SelectNodes(xpath);
            if (result == null)
                throw new InvalidWebPageMarkupException();
            return result;
        }

        public static bool HasNode(this HtmlNode node, string xpath)
        {
            return node.SelectSingleNode(xpath) != null;
        }

        public static bool HasNode(this HtmlDocument document, string xpath)
        {
            return document.DocumentNode.HasNode(xpath);
        }

        public static bool DoesNotHaveNode(this HtmlNode node, string xpath)
        {
            return node.SelectSingleNode(xpath) == null;
        }

        public static bool DoesNotHaveNode(this HtmlDocument document, string xpath)
        {
            return document.DocumentNode.DoesNotHaveNode(xpath);
        }

        private static HtmlNode RemoveComments(HtmlNode node)
        {
            if (node == null)
                return null;

            HtmlNodeCollection comments = node.SelectNodes("//comment()");
            if (comments == null)
                return node;

            foreach (HtmlNode comment in comments)
                comment.ParentNode.RemoveChild(comment);

            return node;
        }
    }
}