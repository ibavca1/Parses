namespace WebParsingFramework
{
    /// <summary>
    /// ‘абрика парсеров.
    /// </summary>
    public interface IWebPageContentParserFactory
    {
        /// <summary>
        /// —оздает парсер дл€ страницы.
        /// </summary>
        /// <param name="page">Ёкзепл€р страницы.</param>
        /// <param name="context">Ёкземпл€р контекста.</param>
        /// <returns>Ёкземпл€р парсера.</returns>
        IWebPageContentParser Create(WebPage page, WebPageContentParsingContext context);
    }
}