using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace WebParsingFramework
{
    /// <summary>
    /// ���������� ��������-��������.
    /// </summary>
    public class WebCompanyMetadata
    {
        private HashSet<WebCity> _�ities;

        /// <summary>
        /// ������� ����� ��������� ������ <see cref="WebCompanyMetadata"/>.
        /// </summary>
        public WebCompanyMetadata()
        {
        }

        /// <summary>
        /// ������ �������������� �������.
        /// </summary>
        public IEnumerable<WebCity> Cities
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<WebCity>>() != null);
                return _�ities ?? Enumerable.Empty<WebCity>();
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "Cities");
                _�ities = new HashSet<WebCity>(value);
            }
        }
    }
}