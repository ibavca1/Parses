namespace WebParsingFramework
{
    /// <summary>
    /// ������� ��������.
    /// </summary>
    public interface IWebPageContentParserFactory
    {
        /// <summary>
        /// ������� ������ ��� ��������.
        /// </summary>
        /// <param name="page">�������� ��������.</param>
        /// <param name="context">��������� ���������.</param>
        /// <returns>��������� �������.</returns>
        IWebPageContentParser Create(WebPage page, WebPageContentParsingContext context);
    }
}