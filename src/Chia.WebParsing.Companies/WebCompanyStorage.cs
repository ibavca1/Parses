using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies
{
    /// <summary>
    /// Хранилище интернет-компаний.
    /// </summary>
    public class WebCompanyStorage : IWebCompanyStorage
    {
        private readonly Dictionary<int, WebCompany> _companies;

        ///<summary>
        /// Создает новый экземпляр <see cref="WebCompanyStorage"/>.
        ///</summary>
        public WebCompanyStorage()
            : this(WebCompanies.All)
        {

        }

        ///<summary>
        /// Создает новый экземпляр <see cref="WebCompanyStorage"/> 
        /// с использованием списка интернет-компаний.
        ///</summary>
        /// <param name="companies">Список интернет-компаний.</param>
        public WebCompanyStorage(IEnumerable<WebCompany> companies)
        {
            Contract.Requires<ArgumentNullException>(companies != null, "companies");
            Contract.Requires<ArgumentException>(companies.IsUnique(), "Коллекция компаний не уникальна.");

            _companies = companies.ToDictionary(c => c.Id, c => c);
        }

        /// <summary>
        /// Получить экземпляр интернет-компании по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор интернет-компании.</param>
        /// <returns>Экземпляр интернет-компании.</returns>
        /// <exception cref="ObjectNotFoundException">Интернет-компания с запрашиваемым идентификатором отсутствует.</exception>
        public WebCompany Get(int id)
        {
            WebCompany company;
            if (!_companies.TryGetValue(id, out company))
            {
                string message = string.Format("WebCompany {0} isn't found", id);
                throw new ObjectNotFoundException(message);
            }

            return company;
        }

        /// <summary>
        /// Получить полный список интернет-компаний.
        /// </summary>
        /// <returns>Коллекцтя интернет-компаний.</returns>
        public IEnumerable<WebCompany> GetAll()
        {
            return _companies.Values;
        }

        
    }
}