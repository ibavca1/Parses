using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies
{
    /// <summary>
    /// Храшнилище городов.
    /// </summary>
    public class WebCityStorage : IWebCityStorage
    {
        private readonly Dictionary<int, WebCity> _cities;

        ///<summary>
        /// Создает новый экземпляр <see cref="WebCityStorage"/>.
        ///</summary>
        public WebCityStorage()
            : this(WebCities.All)
        {
        }

        ///<summary>
        /// Создает новый экземпляр <see cref="WebCityStorage"/> с использованием списка городов.
        ///</summary>
        /// <param name="cities">Список городов.</param>
        public WebCityStorage(ICollection<WebCity> cities)
        {
            Contract.Requires<ArgumentNullException>(cities != null, "cities");
            Contract.Requires<ArgumentException>(cities.IsUnique(), "Коллекция городов не уникальна.");

            _cities = cities.ToDictionary(c => c.Id, c => c);
        }

        /// <summary>
        /// Получить город по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор города.</param>
        /// <returns>Экземпляр города.</returns>
        /// <exception cref="ObjectNotFoundException">Город с запрашиваемым идентификатором отсутствует.</exception>
        public WebCity Get(int id)
        {
            WebCity city;
            if (!_cities.TryGetValue(id, out city))
            {
                // TODO
                // string message = string.Format(SR.WebCityNotFound, id);
                //throw new ObjectNotFoundException(message);
            }

            return city;
        }

        /// <summary>
        /// Получить полный список городов.
        /// </summary>
        /// <returns>Коллекцтя городов.</returns>
        public IEnumerable<WebCity> GetAll()
        {
            return _cities.Values;
        }
    }
}