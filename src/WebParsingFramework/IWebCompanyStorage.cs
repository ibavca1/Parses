using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;

namespace WebParsingFramework
{
    /// <summary>
    /// Хранилище интернет-компаний.
    /// </summary>
    [ContractClass(typeof(WebCompanyStorageContract))]
    public interface IWebCompanyStorage
    {
        /// <summary>
        /// Получить экземпляр интернет-компании по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор интернет-компании.</param>
        /// <returns>Экземпляр интернет-компании.</returns>
        /// <exception cref="ObjectNotFoundException">Интернет-компания с запрашиваемым идентификатором отсутствует.</exception>
        WebCompany Get(int id);

        /// <summary>
        /// Получить полный список интернет-компаний.
        /// </summary>
        /// <returns>Коллекцтя интернет-компаний.</returns>
        IEnumerable<WebCompany> GetAll();
    }

    [ContractClassFor(typeof(IWebCompanyStorage))]
    abstract class WebCompanyStorageContract : IWebCompanyStorage
    {
        public WebCompany Get(int id)
        {
            Contract.Ensures(Contract.Result<WebCompany>() != null);
            return null;
        }

        public IEnumerable<WebCompany> GetAll()
        {
            Contract.Ensures(Contract.Result<IEnumerable<WebCompany>>() != null);
            return null;
        }
    }
}