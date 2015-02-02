using System;
using System.Diagnostics.Contracts;

namespace WebParsingFramework
{
    /// <summary>
    /// ������� ���-��������, ���������� �� ������.
    /// </summary>
    public class StringWebPageContent : WebPageContent
    {
        private readonly string _content;

        /// <summary>
        /// ������� ����� ��������� ������ <see cref="StringWebPageContent"/>.
        /// </summary>
        protected StringWebPageContent()
        {
        }

        /// <summary>
        /// ������� ����� ��������� ������ <see cref="StringWebPageContent"/>.
        /// </summary>
        /// <param name="content">������� � ���� ������.</param>
        public StringWebPageContent(string content)
        {
            Contract.Requires<ArgumentNullException>(content != null, "content");

            _content = content;
        }

        /// <summary>
        /// ���������� ���������� � ���� ������.
        /// </summary>
        protected virtual string Content
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return _content;
            }
        }

        /// <summary>
        /// ���������� ���������� � ���� ������� ������.
        /// </summary>
        /// <returns>���������� � ���� ������� ������.</returns>
        public override byte[] ReadAsByteArray()
        {
            return Encoding.GetBytes(Content);
        }

        /// <summary>
        /// ���������� ���������� � ���� ������.
        /// </summary>
        /// <returns>���������� � ���� ������.</returns>
        public override string ReadAsString()
        {
            return Content;
        }
    }
}