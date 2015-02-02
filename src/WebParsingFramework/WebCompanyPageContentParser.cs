using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace WebParsingFramework
{
    /// <summary>
    /// Парсер содержимого веб-страницы интернет-компании.
    /// </summary>
    [ContractClass(typeof (WebCompanyPageContentParserContract))]
    public abstract class WebCompanyPageContentParser : IWebPageContentParser
    {
        ///<summary>
        /// Интернет-компания, для страниц которой предназначен данный парсер.
        ///</summary>
        public abstract WebCompany Company { get; }

        /// <summary>
        /// Производит разбор контента веб-страницы.
        /// </summary>
        /// <param name="page">Экземпляр страницы, разбор которой производится.</param>
        /// <param name="content">Контент страницы, разбор которого производится.</param>
        /// <param name="context">Контекст, в рамках которого производится разбор.</param>
        /// <returns>Результат разбора.</returns>
        public abstract WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context);

        /// <summary>
        /// Производит асинхронный разбор контента веб-страницы.
        /// </summary>
        /// <param name="page">Экземпляр страницы, разбор которой производится.</param>
        /// <param name="content">Контент страницы, разбор которого производится.</param>
        /// <param name="context">Контекст, в рамках которого производится разбор.</param>
        /// <param name="cancellationToken">Токен <see cref="CancellationToken"/>, который будет назначен задаче.</param>
        /// <returns>Задача, содержащая результат разбора.</returns>
        public virtual Task<WebPageContentParsingResult> ParseAsync(WebPage page, WebPageContent content, WebPageContentParsingContext context,
            CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => Parse(page, content, context), cancellationToken);
        }
    }

    [ContractClassFor(typeof (WebCompanyPageContentParser))]
    abstract class WebCompanyPageContentParserContract : WebCompanyPageContentParser
    {
        public override WebCompany Company
        {
            get
            {
                Contract.Ensures(Contract.Result<WebCompany>() != null);
                return null;
            }
        }
    }
}