using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace WebParsingFramework
{
    /// <summary>
    /// Метаданные интернет-компании.
    /// </summary>
    public class WebCompanyMetadata
    {
        private HashSet<WebCity> _сities;

        /// <summary>
        /// Создает новый экземпляр класса <see cref="WebCompanyMetadata"/>.
        /// </summary>
        public WebCompanyMetadata()
        {
        }

        /// <summary>
        /// Список поддерживаемых городов.
        /// </summary>
        public IEnumerable<WebCity> Cities
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<WebCity>>() != null);
                return _сities ?? Enumerable.Empty<WebCity>();
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Cities");
                _сities = new HashSet<WebCity>(value);
            }
        }
    }
}