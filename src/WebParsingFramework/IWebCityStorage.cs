using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;

namespace WebParsingFramework
{
    /// <summary>
    /// Храшнилище городов.
    /// </summary>
     [ContractClass(typeof(WebCityStorageContract))]
    public interface IWebCityStorage
    {
        /// <summary>
        /// Получить город по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор города.</param>
        /// <returns>Экземпляр города.</returns>
        /// <exception cref="ObjectNotFoundException">Город с запрашиваемым идентификатором отсутствует.</exception>
        WebCity Get(int id);

        /// <summary>
        /// Получить полный список городов.
        /// </summary>
        /// <returns>Коллекцтя городов.</returns>
        IEnumerable<WebCity> GetAll();
    }

     [ContractClassFor(typeof(IWebCityStorage))]
     abstract class WebCityStorageContract : IWebCityStorage
     {
         public WebCity Get(int id)
         {
             Contract.Ensures(Contract.Result<WebCity>() != null);
             return null;
         }

         public IEnumerable<WebCity> GetAll()
         {
             Contract.Ensures(Contract.Result<IEnumerable<WebCity>>() != null);
             return null;
         }
     }
}