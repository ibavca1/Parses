using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;

namespace WebParsingFramework
{
    /// <summary>
    /// ��������� ��������-��������.
    /// </summary>
    [ContractClass(typeof(WebCompanyStorageContract))]
    public interface IWebCompanyStorage
    {
        /// <summary>
        /// �������� ��������� ��������-�������� �� ���������� ��������������.
        /// </summary>
        /// <param name="id">���������� ������������� ��������-��������.</param>
        /// <returns>��������� ��������-��������.</returns>
        /// <exception cref="ObjectNotFoundException">��������-�������� � ������������� ��������������� �����������.</exception>
        WebCompany Get(int id);

        /// <summary>
        /// �������� ������ ������ ��������-��������.
        /// </summary>
        /// <returns>��������� ��������-��������.</returns>
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